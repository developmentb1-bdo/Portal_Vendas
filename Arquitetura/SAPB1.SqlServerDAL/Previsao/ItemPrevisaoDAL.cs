using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Previsao;
using SAPB1.IDAL.Previsao;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Previsao
{
    public class ItemPrevisaoDAL : IItemPrevisao
    {
        public IList<ItemPrevisaoDTO> ObeterTodosItensPrevisoes()
        {
            IList<ItemPrevisaoDTO> listaItensProducao = new List<ItemPrevisaoDTO>();

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                string query = $@"SELECT ""ItemCode"", ""Date"", ""Quantity"" FROM FCT1";
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow rdr in dt.Rows)
                        {
                            ItemPrevisaoDTO ordemProducao = new ItemPrevisaoDTO();
                            ordemProducao.ItemCode = rdr["ItemCode"].ToString();
                            ordemProducao.Quantity = (rdr["Quantity"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Quantity"]));
                            ordemProducao.Date = (rdr["Date"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["Date"]));

                            listaItensProducao.Add(ordemProducao);
                        }
                    }

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
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT ItemCode, Date, Quantity FROM FCT1 (NOLOCK)");

                SqlCommand cmd = new SqlCommand();

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();


                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ItemPrevisaoDTO ordemProducao = new ItemPrevisaoDTO();
                            ordemProducao.ItemCode = rdr["ItemCode"].ToString();
                            ordemProducao.Quantity = (rdr["Quantity"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Quantity"]));
                            ordemProducao.Date = (rdr["Date"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["Date"]));

                            listaItensProducao.Add(ordemProducao);
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                    cmd.Dispose();

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
            return listaItensProducao;

        }
    }
}
