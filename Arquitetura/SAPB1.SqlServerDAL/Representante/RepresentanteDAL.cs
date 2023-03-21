using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Representante;
using SAPB1.DTO.Representante;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Representante
{
    public class RepresentanteDAL : IRepresentante
    {
        public IList<RepresentanteDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OAGP WHERE ""Locked"" IS NULL ORDER BY ""AgentName""";
                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query, conexaoHana);
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
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OAGP WHERE Locked IS NULL ");
                stb.Append("ORDER BY AgentName");

                SqlServerConexao conexao = new SqlServerConexao();

                SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);

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
                    conexao.Desconectar();
                }
            }

        }

        private IList<RepresentanteDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<RepresentanteDTO> listRepresentante = new List<RepresentanteDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    RepresentanteDTO representanteDTO = new RepresentanteDTO();
                    representanteDTO.AgentCode = Convert.ToInt32(rdr["AgentCode"]);
                    representanteDTO.AgentName = rdr["AgentName"].ToString();

                    listRepresentante.Add(representanteDTO);
                }
            }

            rdr.Close();

            return listRepresentante;
        }

        private IList<RepresentanteDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<RepresentanteDTO> listRepresentante = new List<RepresentanteDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    RepresentanteDTO representanteDTO = new RepresentanteDTO();
                    representanteDTO.AgentCode = Convert.ToInt32(dr["AgentCode"]);
                    representanteDTO.AgentName = dr["AgentName"].ToString();

                    listRepresentante.Add(representanteDTO);
                }
            }
            return listRepresentante;
        }
    }
}
