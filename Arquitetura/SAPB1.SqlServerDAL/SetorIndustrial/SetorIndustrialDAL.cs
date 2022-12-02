using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.SetorIndustrial;
using SAPB1.IDAL.SetorIndustrial;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.SetorIndustrial
{
    public class SetorIndustrialDAL : ISetorIndustrial
    {
        public IList<SetorIndustrialDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string query = "SELECT * FROM OOND";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

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
                stb.Append("SELECT * FROM OOND");

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

        private IList<SetorIndustrialDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<SetorIndustrialDTO> listSetorIndustrial = new List<SetorIndustrialDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    SetorIndustrialDTO setorIndustrialDTO = new SetorIndustrialDTO();
                    setorIndustrialDTO.IndCode = Convert.ToInt32(rdr["IndCode"]);
                    setorIndustrialDTO.IndName = rdr["IndName"].ToString();

                    listSetorIndustrial.Add(setorIndustrialDTO);
                }
            }

            rdr.Close();

            return listSetorIndustrial;
        }

        private IList<SetorIndustrialDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<SetorIndustrialDTO> listSetorIndustrial = new List<SetorIndustrialDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SetorIndustrialDTO setorIndustrialDTO = new SetorIndustrialDTO();
                    setorIndustrialDTO.IndCode = Convert.ToInt32(dr["IndCode"]);
                    setorIndustrialDTO.IndName = dr["IndName"].ToString();

                    listSetorIndustrial.Add(setorIndustrialDTO);
                }
            }

            return listSetorIndustrial;
        }
    }
}
