using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Estado;
using SAPB1.DTO.Estado;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SAPB1.SqlServerDAL.Estado
{
    public class EstadoDAL : IEstado
    {

        public IList<EstadoDTO> Listar(EstadoDTO estadoDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT * FROM OCST WHERE ""Country"" = '{estadoDTO.Pais.Name}' ";
                HanaConexao conexaoHana = new HanaConexao();

                if (!string.IsNullOrEmpty(estadoDTO.Code))
                {
                    query += $@"AND ""Code"" = '{estadoDTO.Code}' ";
                }

                query += $@"ORDER BY ""Name""";

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query);
                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de dados: " + err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                SqlServerConexao conexao = new SqlServerConexao();
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OCST ");
                stb.Append("WHERE Country = @Country ");

                cmd.Parameters.AddWithValue("@Country", estadoDTO.Pais.Name);

                if (!string.IsNullOrEmpty(estadoDTO.Code))
                {
                    stb.Append("AND Code = @Code ");
                    cmd.Parameters.AddWithValue("@Code", estadoDTO.Code);
                }

                stb.Append("ORDER BY Name");

                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                try
                {
                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conexao.Desconectar();
                }
            }

        }

        private IList<EstadoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<EstadoDTO> listEstado = new List<EstadoDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    EstadoDTO estado = new EstadoDTO();
                    estado.Code = rdr["Code"].ToString();
                    estado.Name = rdr["Name"].ToString();

                    listEstado.Add(estado);
                }
            }

            rdr.Close();
            rdr.Dispose();
            cmd.Dispose();

            return listEstado;
        }

        private IList<EstadoDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();

            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<EstadoDTO> listEstado = new List<EstadoDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EstadoDTO estado = new EstadoDTO();
                    estado.Code = dr["Code"].ToString();
                    estado.Name = dr["Name"].ToString();

                    listEstado.Add(estado);
                }
            }

            return listEstado;
        }
    }
}
