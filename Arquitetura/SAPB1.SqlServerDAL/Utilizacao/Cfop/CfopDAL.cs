using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.IDAL.Utilizacao.Cfop;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Utilizacao.Cfop
{
    public class CfopDAL : ICfop
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<CfopDTO> Listar(CfopDTO cfopDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""Code"", CONCAT(""Code"", '-', ""Descrip"") AS ""Descrip"" FROM OCFP WHERE ""Locked"" = 'N' AND ";
                if (cfopDTO.TipoCfop == CfopType.Venda)
                {
                    query += $@"CAST(""Code"" AS INT) >= 5000 ";
                }
                else
                {
                    query += $@"CAST(""Code"" AS INT) < 5000 ";
                }

                query += $@"ORDER BY ""Code""";

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
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT Code, CONCAT(Code, '-', Descrip) AS 'Descrip' FROM OCFP ");
                stb.Append("WHERE Locked = 'N' AND ");

                if (cfopDTO.TipoCfop == CfopType.Venda)
                {
                    stb.Append("CAST(Code AS INT) >= 5000 ");
                }
                else
                {
                    stb.Append("CAST(Code AS INT) < 5000 ");
                }

                stb.Append("ORDER BY Code");

                cmd.Connection = conexao.Conexao;
                cmd.CommandText = stb.ToString();

                try
                {
                    conexao.Conectar();

                    return PopularDados(ref cmd);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }

        }

        private IList<CfopDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<CfopDTO> listCfop = new List<CfopDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    CfopDTO cfopDTO = new CfopDTO();
                    cfopDTO.Code = rdr["Code"].ToString();
                    cfopDTO.Descrip = rdr["Descrip"].ToString();

                    listCfop.Add(cfopDTO);
                }
            }

            rdr.Close();

            return listCfop;
        }

        private IList<CfopDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<CfopDTO> listCfop = new List<CfopDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CfopDTO cfopDTO = new CfopDTO();
                    cfopDTO.Code = dr["Code"].ToString();
                    cfopDTO.Descrip = dr["Descrip"].ToString();

                    listCfop.Add(cfopDTO);
                }
            }

            return listCfop;
        }
    }
}
