using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.TiposEnvio;
using SAPB1.DTO.TiposEnvio;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.TiposEnvio
{
    public class TipoEnvioDAL : ITipoEnvio
    {
        string queryPadrao = "SELECT TrnspCode, TrnspName, UserSign, WebSite FROM OSHP ";

        SqlServerConexao conexao = new SqlServerConexao();

        public IList<TipoEnvioDTO> Listar(TipoEnvioDTO tipoEnvioDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""TrnspCode"", ""TrnspName"", ""UserSign"", ""WebSite"" FROM OSHP ";
                if (tipoEnvioDTO != null)
                {
                    if (tipoEnvioDTO.UserSign != 0)
                    {
                        query += $@"WHERE ""UserSign"" = '{tipoEnvioDTO.UserSign}' ";
                    }
                }

                query += $@"ORDER BY ""TrnspName""";

                try
                {
                    conexaoHana.Connection();

                    return PopularDadosHana(query, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
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
                stb.Append(queryPadrao);

                if (tipoEnvioDTO != null)
                {
                    if (tipoEnvioDTO.UserSign != 0)
                    {
                        stb.Append("WHERE ");
                        stb.Append("UserSign = @UserSign ");

                        cmd.Parameters.AddWithValue("@UserSign", tipoEnvioDTO.UserSign);
                    }
                }

                stb.Append("ORDER BY TrnspName");

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

        private IList<TipoEnvioDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<TipoEnvioDTO> listTipoEnvio = new List<TipoEnvioDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    TipoEnvioDTO tipoEnvioDTO = new TipoEnvioDTO();
                    tipoEnvioDTO.TrnspCode = Convert.ToInt32(rdr["TrnspCode"].ToString());
                    tipoEnvioDTO.TrnspName = rdr["TrnspName"].ToString();
                    tipoEnvioDTO.UserSign = Convert.ToInt32(rdr["UserSign"].ToString());
                    tipoEnvioDTO.WebSite = rdr["WebSite"].ToString();

                    listTipoEnvio.Add(tipoEnvioDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listTipoEnvio;
        }


        private IList<TipoEnvioDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);
            IList<TipoEnvioDTO> listTipoEnvio = new List<TipoEnvioDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TipoEnvioDTO tipoEnvioDTO = new TipoEnvioDTO();
                    tipoEnvioDTO.TrnspCode = Convert.ToInt32(dr["TrnspCode"].ToString());
                    tipoEnvioDTO.TrnspName = dr["TrnspName"].ToString();
                    tipoEnvioDTO.UserSign = Convert.ToInt32(dr["UserSign"].ToString());
                    tipoEnvioDTO.WebSite = dr["WebSite"].ToString();

                    listTipoEnvio.Add(tipoEnvioDTO);
                }
            }

            return listTipoEnvio;
        }
    }
}
