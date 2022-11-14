using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Estado;
using SAPB1.DTO.Estado;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Estado
{
    public class EstadoDAL:IEstado
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<EstadoDTO> Listar(EstadoDTO estadoDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM OCST ");
            stb.Append("WHERE Country = @Country ");

            cmd.Parameters.AddWithValue("@Country", estadoDTO.Pais.Name);
            
            if(!string.IsNullOrEmpty(estadoDTO.Code))
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
    }
}
