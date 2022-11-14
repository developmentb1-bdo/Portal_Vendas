using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Relatorio;
using SAPB1.IDAL.Relatorio;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Relatorio
{
    public class FaturamentoDAL : IFaturamento
    {
        SqlServerConexao _conexao;

        public FaturamentoDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public List<FaturamentoDTO> ObterFaturamentoMes(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("SUM([Valor Total Item]) Valor, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data ");
            stb.Append("FROM [HOM_NEW_PROD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("WHERE [Data NF] BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT SUM([Valor Total Item]) Valor, SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data ");
            stb.Append("FROM [SBO_FOTON_PRD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK)  ");
            stb.Append("WHERE [Data NF] BETWEEN @DataInicial2 AND @DataFinal2  ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ");
            stb.Append("ORDER BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ASC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FaturamentoDTO> listFatumento = new List<FaturamentoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFatumento.Add(new FaturamentoDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            Valor = (rdr["Valor"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Valor"].ToString()))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFatumento;
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

        public List<FaturamentoDTO> BuscaFaturamentoMes(DateTime dataInicial, DateTime dataFinal, string cliente, string grupoProduto)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("SUM([Valor Total Item]) Valor, ");
            stb.Append("SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data ");
            stb.Append("FROM [HOM_NEW_PROD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("WHERE ([Data NF] BETWEEN @DataInicial AND @DataFinal) ");

            if (!cliente.Equals("-1") || (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione")))
            {
                stb.Append("AND ");

                if (!cliente.Equals("-1"))
                {
                    stb.Append("[Código Cliente] = @CodigoCliente ");
                    cmd.Parameters.AddWithValue("@CodigoCliente", cliente);

                    if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                        stb.Append("AND ");
                }

                if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                {
                    stb.Append("[Grupo de Produto] = @GrupoProduto ");
                    cmd.Parameters.AddWithValue("@GrupoProduto", grupoProduto);
                }
            }

            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT SUM([Valor Total Item]) Valor, SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data ");
            stb.Append("FROM [SBO_FOTON_PRD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK)  ");
            stb.Append("WHERE ([Data NF] BETWEEN @DataInicial2 AND @DataFinal2)  ");

            if (!cliente.Equals("-1") || (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione")))
            {
                stb.Append("AND ");

                if (!cliente.Equals("-1"))
                {
                    stb.Append("[Código Cliente] = @CodigoCliente2 ");
                    cmd.Parameters.AddWithValue("@CodigoCliente2", cliente);

                    if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                        stb.Append("AND ");
                }

                if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                {
                    stb.Append("[Grupo de Produto] = @GrupoProduto2 ");
                    cmd.Parameters.AddWithValue("@GrupoProduto2", grupoProduto);
                }
            }

            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ");
            stb.Append("ORDER BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ASC");

            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = _conexao.Conexao;

                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FaturamentoDTO> listFatumento = new List<FaturamentoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFatumento.Add(new FaturamentoDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            Valor = (rdr["Valor"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Valor"].ToString()))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFatumento;
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

        public List<FaturamentoDTO> ObterFaturamentoPorCliente(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT TOP 10 SUM(Valor) Valor, [Código Cliente], [Nome Cliente] ");
            stb.Append("FROM ( ");
            stb.Append("SELECT SUM([Valor Total Item]) Valor, [Código Cliente], [Nome Cliente] ");
            stb.Append("FROM [HOM_NEW_PROD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("WHERE [Data NF] BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY [Código Cliente], [Nome Cliente] ");
            stb.Append("UNION ALL ");
            stb.Append("SELECT SUM([Valor Total Item]) Valor, [Código Cliente], [Nome Cliente] ");
            stb.Append("FROM [SBO_FOTON_PRD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("WHERE [Data NF] BETWEEN @DataInicial2 AND @DataFinal2 ");
            stb.Append("GROUP BY [Código Cliente], [Nome Cliente] ");
            stb.Append(") FATURAMENTO ");
            stb.Append("GROUP BY [Código Cliente], [Nome Cliente] ");
            stb.Append("ORDER BY 1 DESC");

            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FaturamentoDTO> listFatumento = new List<FaturamentoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFatumento.Add(new FaturamentoDTO()
                        {
                            NomeCliente = rdr["Nome Cliente"].ToString(),
                            Valor = (rdr["Valor"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Valor"].ToString()))
                        });
                    }
                }

                return listFatumento;
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

        public List<FaturamentoDTO> ObterFaturamentoMesPorGrupoProduto(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM([Valor Total Item]) Valor, SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data, i.ItmsGrpCod, i.ItmsGrpNam ");
            stb.Append("FROM [HOM_NEW_PROD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("INNER JOIN [HOM_NEW_PROD].[dbo].OITM P ON P.ItemCode = [Cod.Item] ");
            stb.Append("INNER JOIN [HOM_NEW_PROD].[dbo].OITB i on i.ItmsGrpCod = P.ItmsGrpCod ");
            stb.Append("WHERE [Data NF] ");
            stb.Append("BETWEEN @DataInicial AND @DataFinal ");
            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7), i.ItmsGrpCod, i.ItmsGrpNam ");
            stb.Append("UNION ALL  ");
            stb.Append("SELECT SUM([Valor Total Item]) Valor, SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data, G.ItmsGrpCod, G.ItmsGrpNam ");
            stb.Append("FROM [SBO_FOTON_PRD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("INNER JOIN [SBO_FOTON_PRD].[dbo].OITM PR ON PR.ItemCode = [Cod.Item] ");
            stb.Append("INNER JOIN [SBO_FOTON_PRD].[dbo].OITB G on G.ItmsGrpCod = PR.ItmsGrpCod ");
            stb.Append("WHERE [Data NF] ");
            stb.Append("BETWEEN @DataInicial2 AND @DataFinal2 GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7), G.ItmsGrpCod, G.ItmsGrpNam ");
            stb.Append("ORDER BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ASC ");


            SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FaturamentoDTO> listFatumento = new List<FaturamentoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFatumento.Add(new FaturamentoDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            Valor = (rdr["Valor"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Valor"].ToString())),
                            CodigoGrupo = rdr["ItmsGrpCod"].ToString(),
                            GrupoProdutoNome = rdr["ItmsGrpNam"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFatumento;
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

        public List<FaturamentoDTO> BuscarFaturamentoMesPorGrupoProduto(DateTime dataInicial, DateTime dataFinal, string cliente, string grupoProduto)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM([Valor Total Item]) Valor, SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data, i.ItmsGrpCod, i.ItmsGrpNam ");
            stb.Append("FROM [HOM_NEW_PROD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("INNER JOIN [HOM_NEW_PROD].[dbo].OITM P ON P.ItemCode = [Cod.Item] ");
            stb.Append("INNER JOIN [HOM_NEW_PROD].[dbo].OITB i on i.ItmsGrpCod = P.ItmsGrpCod ");
            stb.Append("WHERE ([Data NF] ");
            stb.Append("BETWEEN @DataInicial AND @DataFinal) ");

            if (!cliente.Equals("-1") || (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione")))
            {
                stb.Append("AND ");

                if (!cliente.Equals("-1"))
                {
                    stb.Append("[Código Cliente] = @CodigoCliente ");
                    cmd.Parameters.AddWithValue("@CodigoCliente", cliente);

                    if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                        stb.Append("AND ");
                }

                if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                {
                    stb.Append("[Grupo de Produto] = @GrupoProduto ");
                    cmd.Parameters.AddWithValue("@GrupoProduto", grupoProduto);
                }
            }

            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7), i.ItmsGrpCod, i.ItmsGrpNam ");
            stb.Append("UNION ALL  ");
            stb.Append("SELECT SUM([Valor Total Item]) Valor, SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) Data, G.ItmsGrpCod, G.ItmsGrpNam ");
            stb.Append("FROM [SBO_FOTON_PRD].[dbo].[RSD_NF_Saida_Itens] (NOLOCK) ");
            stb.Append("INNER JOIN [SBO_FOTON_PRD].[dbo].OITM PR ON PR.ItemCode = [Cod.Item] ");
            stb.Append("INNER JOIN [SBO_FOTON_PRD].[dbo].OITB G on G.ItmsGrpCod = PR.ItmsGrpCod ");
            stb.Append("WHERE ([Data NF] ");
            stb.Append("BETWEEN @DataInicial2 AND @DataFinal2) ");

            if (!cliente.Equals("-1") || (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione")))
            {
                stb.Append("AND ");

                if (!cliente.Equals("-1"))
                {
                    stb.Append("[Código Cliente] = @CodigoCliente2 ");
                    cmd.Parameters.AddWithValue("@CodigoCliente2", cliente);

                    if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                        stb.Append("AND ");
                }

                if (!grupoProduto.Equals("-1") && !grupoProduto.Equals("Selecione"))
                {
                    stb.Append("[Grupo de Produto] = @GrupoProduto2 ");
                    cmd.Parameters.AddWithValue("@GrupoProduto2", grupoProduto);
                }
            }

            stb.Append("GROUP BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7), G.ItmsGrpCod, G.ItmsGrpNam ");
            stb.Append("ORDER BY SUBSTRING(CONVERT(VARCHAR(10),[Data NF],112), 0,7) ASC ");

            cmd.CommandText = stb.ToString();
            cmd.Connection = _conexao.Conexao;

            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@DataInicial2", dataInicial.ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@DataFinal2", dataFinal.ToString("yyyy-MM-dd") + " 23:59:59");

            try
            {
                _conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<FaturamentoDTO> listFatumento = new List<FaturamentoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listFatumento.Add(new FaturamentoDTO()
                        {
                            Data = rdr["Data"].ToString(),
                            Valor = (rdr["Valor"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Valor"].ToString())),
                            CodigoGrupo = rdr["ItmsGrpCod"].ToString(),
                            GrupoProdutoNome = rdr["ItmsGrpNam"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listFatumento;
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
