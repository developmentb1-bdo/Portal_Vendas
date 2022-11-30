using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;
using System.Data.SqlClient;
using SAPB1.IDAL.Administracao.Configuracao;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Administracao.Configuracao
{
    public class IdiomaDAL : IIdioma
    {
        public IList<IdiomaDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            StringBuilder stb = new StringBuilder();
            stb.Append($@"SELECT * FROM OLNG ORDER BY ""Name""");

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(stb.ToString());
                }
                catch (Exception err)
                {
                    throw new Exception(err.Message);
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

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                    cmd.Dispose();
                }
            }
        }

        private IList<IdiomaDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<IdiomaDTO> listIdioma = new List<IdiomaDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    IdiomaDTO idiomaDTO = new IdiomaDTO();
                    idiomaDTO.Name = rdr["Name"].ToString();
                    idiomaDTO.Code = Convert.ToInt32(rdr["Code"].ToString());

                    listIdioma.Add(idiomaDTO);
                }
            }

            rdr.Close();

            return listIdioma;
        }

        private IList<IdiomaDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<IdiomaDTO> listIdioma = new List<IdiomaDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    IdiomaDTO idiomaDTO = new IdiomaDTO();
                    idiomaDTO.Name = dr["Name"].ToString();
                    idiomaDTO.Code = Convert.ToInt32(dr["Code"].ToString());

                    listIdioma.Add(idiomaDTO);
                }
            }

            return listIdioma;
        }
    }
}
