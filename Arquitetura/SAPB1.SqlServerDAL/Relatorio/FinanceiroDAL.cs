using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Relatorio;
using SAPB1.IDAL.Relatorio;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Relatorio
{
    public class FinanceiroDAL : IFinanceiro
    {
        SqlServerConexao _conexao;

        public FinanceiroDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public decimal RetonarValorEmAberto(DateTime data)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(Total) Total FROM ");
            stb.Append("( ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Aberto ");
            stb.Append("WHERE [Vencimento] <= @Data ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Aberto ");
            stb.Append("WHERE [Vencimento] <= @Data2 ");
            stb.Append(") Total ");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Data2", data.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                _conexao.Conectar();

                object valor = cmd.ExecuteScalar();

                return (valor != null ? Convert.ToDecimal(valor) : 0);
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public decimal RetonarValorVencimento(DateTime data)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(Total) Total FROM ");
            stb.Append("( ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Aberto ");
            stb.Append("WHERE [Vencimento] > @Data ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Aberto ");
            stb.Append("WHERE [Vencimento] > @Data2 ");
            stb.Append(") Total ");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Data2", data.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                _conexao.Conectar();

                object valor = cmd.ExecuteScalar();

                return (valor != null ? Convert.ToDecimal(valor) : 0);
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public List<FinanceiroDTO> ObterRecimentosEmAbertoPorParceiroNegocio()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(Total) Total, Data, [Codigo_PN], [Razao_Social] ");
            stb.Append("FROM ");
            stb.Append("( ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) Data, ");
            stb.Append("[Codigo_PN], ");
            stb.Append("[Razao_Social] ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Aberto (NOLOCK) ");
            stb.Append("GROUP BY Data, [Codigo_PN],[Razao_Social] ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total, ");
            stb.Append("[Codigo_PN], ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) Data, ");
            stb.Append("[Razao_Social] ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Aberto (NOLOCK) ");
            stb.Append("GROUP BY [Codigo_PN],[Razao_Social] ");
            stb.Append(") Lista ");
            stb.Append("GROUP BY Data, [Codigo_PN],[Razao_Social] ");
            stb.Append("ORDER BY Data ASC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FinanceiroDTO> listFinanceiro = new List<FinanceiroDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFinanceiro.Add(new FinanceiroDTO()
                        {
                            Nome = rdr["Razao_Social"].ToString(),
                            ValorTotal = (rdr["Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Total"].ToString()))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFinanceiro;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public decimal RetonarValorEmAbertoPagamento(DateTime data)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(Total) Total FROM ");
            stb.Append("( ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_a_Pagar]) Total ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CP_Aberto ");
            stb.Append("WHERE [Vencimento] <= @Data ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_a_Pagar]) Total ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CP_Aberto ");
            stb.Append("WHERE [Vencimento] <= @Data2 ");
            stb.Append(") Total ");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Data2", data.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                _conexao.Conectar();

                object valor = cmd.ExecuteScalar();

                return (valor != null ? Convert.ToDecimal(valor) : 0);
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public decimal RetonarValorVencimentoPagamento(DateTime data)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(Total) Total FROM ");
            stb.Append("( ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_a_Pagar]) Total ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CP_Aberto ");
            stb.Append("WHERE [Vencimento] > @Data ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_a_Pagar]) Total ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CP_Aberto ");
            stb.Append("WHERE [Vencimento] > @Data2 ");
            stb.Append(") Total ");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@Data", data.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Data2", data.ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                _conexao.Conectar();

                object valor = cmd.ExecuteScalar();

                return (valor != null ? Convert.ToDecimal(valor) : 0);
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public List<FinanceiroDTO> ObterPagamentoEmAbertoPorParceiroNegocio()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT Sum(Total) AS Total, Data, [Codigo_PN], [Razao_Social] ");
            stb.Append("FROM ");
            stb.Append("( ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_a_Pagar]) Total, ");
            stb.Append("[Codigo_PN], ");
            stb.Append("[Razao_Social] ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CP_Aberto (NOLOCK) ");
            stb.Append("WHERE [Valor_a_Pagar] > 0 ");
            stb.Append("GROUP BY [Codigo_PN],[Razao_Social] ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_a_Pagar]) Total, ");
            stb.Append("[Codigo_PN], ");
            stb.Append("[Razao_Social] ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CP_Aberto (NOLOCK) ");
            stb.Append("WHERE [Valor_a_Pagar] > 0 ");
            stb.Append("GROUP BY [Codigo_PN],[Razao_Social] ");
            stb.Append(") Lista ");
            stb.Append("GROUP BY [Codigo_PN],[Razao_Social] ");
            stb.Append("ORDER BY Total DESC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FinanceiroDTO> listFinanceiro = new List<FinanceiroDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFinanceiro.Add(new FinanceiroDTO()
                        {
                            Nome = rdr["Razao_Social"].ToString(),
                            ValorTotal = (rdr["Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Total"].ToString()))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFinanceiro;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public List<FinanceiroDTO> ObterRecebimentosPorMesEmAberto(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT Sum(Total) AS 'Total', Data FROM (");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) Data ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Aberto (NOLOCK) ");
            stb.Append("WHERE [Emissao] BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) Data ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Aberto (NOLOCK) ");
            stb.Append("WHERE [Emissao] BETWEEN @DataInicial2 AND @DataFinal2 ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) ");
            stb.Append(") Lista ");
            stb.Append("GROUP BY Data ");
            stb.Append("ORDER BY Data ASC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FinanceiroDTO> listFinanceiro = new List<FinanceiroDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFinanceiro.Add(new FinanceiroDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            ValorTotal = (rdr["Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Total"].ToString()))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFinanceiro;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public List<FinanceiroDTO> ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT Sum(Total) AS 'Total', Data, Codigo_PN, Razao_Social FROM (");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) Data, Codigo_PN, Razao_Social ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Aberto (NOLOCK) ");
            stb.Append("WHERE [Emissao] BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7), Codigo_PN, Razao_Social ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Aberto]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7) Data, Codigo_PN, Razao_Social ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Aberto (NOLOCK) ");
            stb.Append("WHERE [Emissao] BETWEEN @DataInicial2 AND @DataFinal2 ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Emissao],112), 0,7), Codigo_PN, Razao_Social ");
            stb.Append(") Lista ");
            stb.Append("GROUP BY Data, Codigo_PN, Razao_Social ");
            stb.Append("ORDER BY Data ASC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FinanceiroDTO> listFinanceiro = new List<FinanceiroDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFinanceiro.Add(new FinanceiroDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            ValorTotal = (rdr["Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Total"].ToString())),
                            Nome = rdr["Razao_Social"].ToString(),
                            CodigoPn = rdr["Codigo_PN"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFinanceiro;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public List<FinanceiroDTO> ObterRecebimentoPagoPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT Sum(Total) AS 'Total', Data FROM (");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Recebido]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7) Data ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Pago (NOLOCK) ");
            stb.Append("WHERE [Data_Recebimento] BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7) ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Recebido]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7) Data ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Pago (NOLOCK) ");
            stb.Append("WHERE [Data_Recebimento] BETWEEN @DataInicial2 AND @DataFinal2 ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7) ");
            stb.Append(") Lista ");
            stb.Append("GROUP BY Data ");
            stb.Append("ORDER BY Data ASC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FinanceiroDTO> listFinanceiro = new List<FinanceiroDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFinanceiro.Add(new FinanceiroDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            ValorTotal = (rdr["Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Total"].ToString()))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFinanceiro;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public List<FinanceiroDTO> ObterRecebimentoPagoPorParceiroNegocioPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT Sum(Total) AS 'Total', Data, Codigo_PN, Razao_Social FROM (");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Recebido]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7) Data, Codigo_PN, Razao_Social ");
            stb.Append("FROM HOM_NEW_PROD.dbo.RSD_CR_Pago (NOLOCK) ");
            stb.Append("WHERE [Data_Recebimento] BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7),  Codigo_PN, Razao_Social ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT ");
            stb.Append("SUM([Valor_Recebido]) Total, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7) Data, Codigo_PN, Razao_Social ");
            stb.Append("FROM SBO_FOTON_PRD.dbo.RSD_CR_Pago (NOLOCK) ");
            stb.Append("WHERE [Data_Recebimento] BETWEEN @DataInicial2 AND @DataFinal2 ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data_Recebimento],112), 0,7),  Codigo_PN, Razao_Social ");
            stb.Append(") Lista ");
            stb.Append("GROUP BY Data, Codigo_PN, Razao_Social ");
            stb.Append("ORDER BY Data ASC ");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FinanceiroDTO> listFinanceiro = new List<FinanceiroDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFinanceiro.Add(new FinanceiroDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            ValorTotal = (rdr["Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Total"].ToString())),
                            Nome = rdr["Razao_Social"].ToString(),
                            CodigoPn = rdr["Codigo_PN"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFinanceiro;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }
    }
}
