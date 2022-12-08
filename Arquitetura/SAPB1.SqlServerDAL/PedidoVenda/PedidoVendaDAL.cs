using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoVenda;
using SAPB1.DTO.PedidoVenda;
using SAPB1.DTO.ParceiroNegocio;
using System.Data.SqlClient;
using SAPB1.DTO.Empresa.Filial;
using SAPB1.DTO.Funcionario.Vendedor;
using SAPB1.DTO.TiposEnvio;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.PedidoVenda
{
    public class PedidoVendaDAL : IPedidoVenda
    {
        /// <summary>
        /// Lista os pedidos de venda
        /// </summary>
        /// <param name="pedidoVendaDTO">Classe PedidoVendaDTO</param>
        /// <returns></returns>
        public IList<PedidoVendaDTO> Listar(PedidoVendaDTO pedidoVendaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT /*TOP 100*/ p.""DocEntry"", p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", p.""DocDate"", p.""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""DocTotal"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"", p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", c.""U_CNPJ"", p.""PeyMethod"", p.""GroupNum"", p.""VatSum"", p.""Comments"", " +
                             $@"p.""U_S7_CobrarFrete"", p.""U_S7_TaxaFrete"", p.""U_S7_ValorFrete""  FROM ORDR p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" ";

                if (pedidoVendaDTO != null && string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                {
                    if (pedidoVendaDTO.DocEntry != 0 || pedidoVendaDTO.Vendedor != null)
                        query += "WHERE ";

                    if (pedidoVendaDTO.DocEntry != 0)
                    {
                        query += $@"p.""DocEntry"" = '{pedidoVendaDTO.DocEntry}' ";


                        if (pedidoVendaDTO.Vendedor != null)
                            query += "AND ";
                    }

                    if (pedidoVendaDTO.Vendedor != null)
                    {
                        query += $@"p.""SlpCode"" = '{pedidoVendaDTO.Vendedor.SlpCode}' ";
                    }
                }
                else
                {
                    if (pedidoVendaDTO != null && !string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                    {
                        query += $@"WHERE p.""OwnerCode"" = '{pedidoVendaDTO.OwnerCode}' ";
                    }
                }
                query += $@"ORDER BY p.""DocNum"" DESC";

                HanaConexao conexaoHana = new HanaConexao();

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
                string queryPadrao = "SELECT /*TOP 100*/ p.DocEntry, p.DocNum, p.CardCode, p.CardName, DocCur, p.DocStatus, p.DocDate, p.DocDueDate, p.TaxDate, p.DocTotalSy, p.DocTotal, p.CANCELED, p.NumAtCard, p.BPLId, p.VATRegNum, p.SlpCode, p.JrnlMemo, p.Address, p.Address2, p.TrnspCode, p.Confirmed, p.PartSupply, p.PoPrss, p.LangCode, p.Pick, p.PickRmrk, p.AgentCode, p.OwnerCode, c.CardName, c.U_CNPJ, p.PeyMethod, p.GroupNum, p.VatSum, p.Comments, " +
                             "p.U_S7_CobrarFrete, p.U_S7_TaxaFrete, p.U_S7_ValorFrete  FROM ORDR p LEFT JOIN OCRD c ON c.CardCode = p.CardCode ";
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);

                if (pedidoVendaDTO != null && string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                {
                    if (pedidoVendaDTO.DocNum != 0 || pedidoVendaDTO.Vendedor != null)
                        stb.Append("WHERE ");

                    if (pedidoVendaDTO.DocNum != 0)
                    {
                        stb.Append("p.DocNum = @DocNum ");

                        cmd.Parameters.AddWithValue("@DocNum", pedidoVendaDTO.DocNum);

                        if (pedidoVendaDTO.Vendedor != null)
                            stb.Append("AND ");
                    }

                    if (pedidoVendaDTO.Vendedor != null)
                    {
                        stb.Append("p.SlpCode = @SlpCode ");

                        cmd.Parameters.AddWithValue("@SlpCode", pedidoVendaDTO.Vendedor.SlpCode);
                    }
                }
                else
                {
                    if (pedidoVendaDTO != null && !string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                    {
                        stb.Append("WHERE p.OwnerCode = @OwnerCode ");
                        cmd.Parameters.AddWithValue("@OwnerCode", pedidoVendaDTO.OwnerCode);
                    }
                }

                stb.Append("ORDER BY p.DocNum DESC");


                SqlServerConexao conexao = new SqlServerConexao();

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

        /// <summary>
        /// Popula os dados da consulta em uma lista
        /// </summary>
        /// <param name="cmd">Classe SqlCommand</param>
        /// <returns>Lista de PedidoVendaDTO</returns>
        private IList<PedidoVendaDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                IList<PedidoVendaDTO> listPedidos = new List<PedidoVendaDTO>();

                while (rdr.Read())
                {
                    PedidoVendaDTO pedidoVendaDTO = new PedidoVendaDTO();
                    pedidoVendaDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    pedidoVendaDTO.DocNum = Convert.ToInt32(rdr["DocNum"].ToString());
                    pedidoVendaDTO.DocStatus = rdr["DocStatus"].ToString();
                    pedidoVendaDTO.DocDate = Convert.ToDateTime(rdr["DocDate"].ToString());
                    pedidoVendaDTO.DocDueDate = Convert.ToDateTime(rdr["DocDueDate"].ToString());
                    pedidoVendaDTO.DocTotalSy = Convert.ToDouble(rdr["DocTotalSy"].ToString());
                    pedidoVendaDTO.TaxDate = Convert.ToDateTime(rdr["TaxDate"]);
                    pedidoVendaDTO.Canceled = rdr["CANCELED"].ToString();
                    pedidoVendaDTO.JrnlMemo = rdr["JrnlMemo"].ToString();
                    pedidoVendaDTO.Address = rdr["Address"].ToString();
                    pedidoVendaDTO.Address2 = rdr["Address2"].ToString();
                    pedidoVendaDTO.Confirmed = rdr["Confirmed"].ToString();
                    pedidoVendaDTO.PartSupply = rdr["PartSupply"].ToString();
                    pedidoVendaDTO.PoPrss = rdr["PoPrss"].ToString();
                    pedidoVendaDTO.LangCode = rdr["LangCode"].ToString();
                    pedidoVendaDTO.Pick = rdr["Pick"].ToString();
                    pedidoVendaDTO.PickRmrk = rdr["PickRmrk"].ToString();
                    pedidoVendaDTO.AgentCode = rdr["AgentCode"].ToString();
                    pedidoVendaDTO.CardCode = rdr["CardCode"].ToString();
                    pedidoVendaDTO.CardName = rdr["CardName"].ToString();
                    pedidoVendaDTO.DocCur = rdr["DocCur"].ToString();
                    pedidoVendaDTO.OwnerCode = rdr["OwnerCode"].ToString();
                    pedidoVendaDTO.PeyMethod = rdr["PeyMethod"].ToString();
                    pedidoVendaDTO.GroupNum = rdr["GroupNum"].ToString();
                    pedidoVendaDTO.DocTotal = Convert.ToDouble(rdr["DocTotal"]);
                    pedidoVendaDTO.VatSum = Convert.ToDouble(rdr["VatSum"]);
                    pedidoVendaDTO.Comments = rdr["Comments"].ToString();
                    pedidoVendaDTO.TemFrete = rdr["U_S7_CobrarFrete"].ToString();
                    pedidoVendaDTO.PercentualFrete = rdr["U_S7_TaxaFrete"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_S7_TaxaFrete"]);
                    pedidoVendaDTO.ValorFreteCab = rdr["U_S7_ValorFrete"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_S7_ValorFrete"]);

                    FilialDTO filialDTO = new FilialDTO();
                    filialDTO.BPLId = Convert.ToInt32(rdr["BPLId"].ToString().Equals("") ? "0" : rdr["BPLId"].ToString());
                    filialDTO.TaxIdNum = rdr["VATRegNum"].ToString();
                    pedidoVendaDTO.Filial = filialDTO;

                    VendedorDTO vendedorDTO = new VendedorDTO();
                    vendedorDTO.SlpCode = Convert.ToInt32(rdr["SlpCode"].ToString());
                    pedidoVendaDTO.Vendedor = vendedorDTO;

                    TipoEnvioDTO tipoEnvioDTO = new TipoEnvioDTO();
                    tipoEnvioDTO.TrnspCode = Convert.ToInt32(rdr["TrnspCode"].ToString());
                    pedidoVendaDTO.TipoEnvio = tipoEnvioDTO;

                    pedidoVendaDTO.CardName = rdr["CardName"].ToString();
                    pedidoVendaDTO.U_CNPJ = rdr["U_CNPJ"].ToString();
                    listPedidos.Add(pedidoVendaDTO);
                }

                rdr.Close();
                rdr.Dispose();

                return listPedidos;
            }
            else
            {
                return new List<PedidoVendaDTO>();
            }
        }


        private IList<PedidoVendaDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            if (dt.Rows.Count > 0)
            {
                IList<PedidoVendaDTO> listPedidos = new List<PedidoVendaDTO>();

                foreach (DataRow rdr in dt.Rows)
                {
                    PedidoVendaDTO pedidoVendaDTO = new PedidoVendaDTO();
                    pedidoVendaDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    pedidoVendaDTO.DocNum = Convert.ToInt32(rdr["DocNum"].ToString());
                    pedidoVendaDTO.DocStatus = rdr["DocStatus"].ToString();
                    pedidoVendaDTO.DocDate = Convert.ToDateTime(rdr["DocDate"].ToString());
                    pedidoVendaDTO.DocDueDate = Convert.ToDateTime(rdr["DocDueDate"].ToString());
                    pedidoVendaDTO.DocTotalSy = Convert.ToDouble(rdr["DocTotalSy"].ToString());
                    pedidoVendaDTO.TaxDate = Convert.ToDateTime(rdr["TaxDate"]);
                    pedidoVendaDTO.Canceled = rdr["CANCELED"].ToString();
                    pedidoVendaDTO.JrnlMemo = rdr["JrnlMemo"].ToString();
                    pedidoVendaDTO.Address = rdr["Address"].ToString();
                    pedidoVendaDTO.Address2 = rdr["Address2"].ToString();
                    pedidoVendaDTO.Confirmed = rdr["Confirmed"].ToString();
                    pedidoVendaDTO.PartSupply = rdr["PartSupply"].ToString();
                    pedidoVendaDTO.PoPrss = rdr["PoPrss"].ToString();
                    pedidoVendaDTO.LangCode = rdr["LangCode"].ToString();
                    pedidoVendaDTO.Pick = rdr["Pick"].ToString();
                    pedidoVendaDTO.PickRmrk = rdr["PickRmrk"].ToString();
                    pedidoVendaDTO.AgentCode = rdr["AgentCode"].ToString();
                    pedidoVendaDTO.CardCode = rdr["CardCode"].ToString();
                    pedidoVendaDTO.CardName = rdr["CardName"].ToString();
                    pedidoVendaDTO.DocCur = rdr["DocCur"].ToString();
                    pedidoVendaDTO.OwnerCode = rdr["OwnerCode"].ToString();
                    pedidoVendaDTO.PeyMethod = rdr["PeyMethod"].ToString();
                    pedidoVendaDTO.GroupNum = rdr["GroupNum"].ToString();
                    pedidoVendaDTO.DocTotal = Convert.ToDouble(rdr["DocTotal"]);
                    pedidoVendaDTO.VatSum = Convert.ToDouble(rdr["VatSum"]);
                    pedidoVendaDTO.Comments = rdr["Comments"].ToString();
                    pedidoVendaDTO.TemFrete = rdr["U_S7_CobrarFrete"].ToString();
                    pedidoVendaDTO.PercentualFrete = rdr["U_S7_TaxaFrete"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_S7_TaxaFrete"]);
                    pedidoVendaDTO.ValorFreteCab = rdr["U_S7_ValorFrete"].ToString().Equals("") ? 0 : Convert.ToDouble(rdr["U_S7_ValorFrete"]);

                    FilialDTO filialDTO = new FilialDTO();
                    filialDTO.BPLId = Convert.ToInt32(rdr["BPLId"].ToString().Equals("") ? "0" : rdr["BPLId"].ToString());
                    filialDTO.TaxIdNum = rdr["VATRegNum"].ToString();
                    pedidoVendaDTO.Filial = filialDTO;

                    VendedorDTO vendedorDTO = new VendedorDTO();
                    vendedorDTO.SlpCode = Convert.ToInt32(rdr["SlpCode"].ToString());
                    pedidoVendaDTO.Vendedor = vendedorDTO;

                    TipoEnvioDTO tipoEnvioDTO = new TipoEnvioDTO();
                    tipoEnvioDTO.TrnspCode = Convert.ToInt32(rdr["TrnspCode"].ToString());
                    pedidoVendaDTO.TipoEnvio = tipoEnvioDTO;

                    pedidoVendaDTO.CardName = rdr["CardName"].ToString();
                    pedidoVendaDTO.U_CNPJ = rdr["U_CNPJ"].ToString();
                    listPedidos.Add(pedidoVendaDTO);
                }

                return listPedidos;
            }
            else
            {
                return new List<PedidoVendaDTO>();
            }
        }

        public double RetornarValorTotalPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT SUM(""DocTotal"") FROM ORDR WHERE ""DocStatus"" = 'C'";
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();

                    return Convert.ToDouble(conexaoHana.ExecuteScalar(query));
                }
                catch (Exception erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT SUM(DocTotal) FROM ORDR WHERE DocStatus = 'C'");

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);

                    return Convert.ToDouble(comando.ExecuteScalar());
                }
                catch (SqlException erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }

        }

        public IList<PedidoVendaDTO> BuscarPedidoVenda(PedidoVendaDTO pedidoVendaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT p.""DocEntry"", p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", p.""DocDate"", p.""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"",  p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", c.""U_CNPJ"", p.""PeyMethod"", p.""GroupNum"", p.""VatSum"", p.""DocTotal"", p.""Comments"", p.""U_S7_CobrarFrete"", p.""U_S7_TaxaFrete"", p.""U_S7_ValorFrete"" FROM ORDR p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" ";

                if (pedidoVendaDTO != null)
                {
                    if (pedidoVendaDTO.DocNum != 0 ||
                        (pedidoVendaDTO.DocDate != DateTime.MinValue && pedidoVendaDTO.DocDueDate != DateTime.MinValue) ||
                        !string.IsNullOrEmpty(pedidoVendaDTO.CardName) ||
                        !string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ) || pedidoVendaDTO.Vendedor != null)
                    {
                        query += "WHERE ";

                        if (pedidoVendaDTO.DocNum != 0)
                        {
                            query += $@"p.""DocNum"" = '{pedidoVendaDTO.DocNum}' ";

                            if ((pedidoVendaDTO.DocDate != DateTime.MinValue && pedidoVendaDTO.DocDueDate != DateTime.MinValue) ||
                                !string.IsNullOrEmpty(pedidoVendaDTO.CardName) ||
                                !string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ) || pedidoVendaDTO.Vendedor != null)
                            {
                                query += "AND ";
                            }
                        }

                        if (pedidoVendaDTO.DocDate != DateTime.MinValue && pedidoVendaDTO.DocDueDate != DateTime.MinValue)
                        {
                            query += $@"(p.""DocDate"" BETWEEN '{pedidoVendaDTO.DocDate.ToString("yyyy-MM-dd") + " 00:00:00"}' AND '{pedidoVendaDTO.DocDueDate.ToString("yyyy-MM-dd") + " 23:59:59"}') ";

                            if (!string.IsNullOrEmpty(pedidoVendaDTO.CardName) ||
                               !string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ))
                            {
                                query += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(pedidoVendaDTO.CardName))
                        {
                            query += $@"c.""CardName"" LIKE '{pedidoVendaDTO.CardName}%' ";

                            if (!string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ) || pedidoVendaDTO.Vendedor != null)
                            {
                                query += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ))
                        {
                            query += $@"c.""U_CNPJ"" = '{pedidoVendaDTO.U_CNPJ}' ";

                            if (pedidoVendaDTO.Vendedor != null)
                                query += "AND ";
                        }

                        if (pedidoVendaDTO.Vendedor != null)
                        {
                            query += $@"p.""SlpCode"" = '{pedidoVendaDTO.Vendedor.SlpCode}' ";
                        }

                        if (!string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                        {
                            query += $@"AND p.""OwnerCode"" = '{pedidoVendaDTO.OwnerCode}' ";
                        }
                    }
                }
                query += $@"ORDER BY p.""DocNum"" DESC";
                HanaConexao conexaoHana = new HanaConexao();

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
                stb.Append("SELECT p.DocEntry, p.DocNum, p.CardCode, p.CardName, DocCur, p.DocStatus, p.DocDate, p.DocDueDate, p.TaxDate, p.DocTotalSy, p.CANCELED, p.NumAtCard, p.BPLId, ");
                stb.Append("p.VATRegNum, p.SlpCode, p.JrnlMemo, p.Address, p.Address2, p.TrnspCode, p.Confirmed, p.PartSupply, p.PoPrss, p.LangCode, p.Pick, p.PickRmrk, p.AgentCode,");
                stb.Append("p.OwnerCode, c.CardName, c.U_CNPJ, p.PeyMethod, p.GroupNum, p.VatSum, p.DocTotal, p.Comments, p.U_S7_CobrarFrete, p.U_S7_TaxaFrete, p.U_S7_ValorFrete FROM ORDR p LEFT JOIN OCRD c ON c.CardCode = p.CardCode ");

                if (pedidoVendaDTO != null)
                {
                    if (pedidoVendaDTO.DocNum != 0 ||
                        (pedidoVendaDTO.DocDate != DateTime.MinValue && pedidoVendaDTO.DocDueDate != DateTime.MinValue) ||
                        !string.IsNullOrEmpty(pedidoVendaDTO.CardName) ||
                        !string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ) || pedidoVendaDTO.Vendedor != null)
                    {
                        stb.Append("WHERE ");

                        if (pedidoVendaDTO.DocNum != 0)
                        {
                            stb.Append("p.DocNum = @DocNum ");
                            cmd.Parameters.AddWithValue("@DocNum", pedidoVendaDTO.DocNum);

                            if ((pedidoVendaDTO.DocDate != DateTime.MinValue && pedidoVendaDTO.DocDueDate != DateTime.MinValue) ||
                                !string.IsNullOrEmpty(pedidoVendaDTO.CardName) ||
                                !string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ) || pedidoVendaDTO.Vendedor != null)
                            {
                                stb.Append("AND ");
                            }
                        }

                        if (pedidoVendaDTO.DocDate != DateTime.MinValue && pedidoVendaDTO.DocDueDate != DateTime.MinValue)
                        {
                            stb.Append("(p.DocDate BETWEEN @DataInicial AND @DataFinal) ");
                            cmd.Parameters.AddWithValue("@DataInicial", pedidoVendaDTO.DocDate.ToString("yyyy-MM-dd") + " 00:00:00");
                            cmd.Parameters.AddWithValue("@DataFinal", pedidoVendaDTO.DocDueDate.ToString("yyyy-MM-dd") + " 23:59:59");

                            if (!string.IsNullOrEmpty(pedidoVendaDTO.CardName) ||
                               !string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ))
                            {
                                stb.Append("AND ");
                            }
                        }

                        if (!string.IsNullOrEmpty(pedidoVendaDTO.CardName))
                        {
                            stb.Append("c.CardName LIKE @CardName ");
                            cmd.Parameters.AddWithValue("@CardName", pedidoVendaDTO.CardName + "%");

                            if (!string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ) || pedidoVendaDTO.Vendedor != null)
                            {
                                stb.Append("AND ");
                            }
                        }

                        if (!string.IsNullOrEmpty(pedidoVendaDTO.U_CNPJ))
                        {
                            stb.Append("c.U_CNPJ = @U_CNPJ ");
                            cmd.Parameters.AddWithValue("@U_CNPJ", pedidoVendaDTO.U_CNPJ);

                            if (pedidoVendaDTO.Vendedor != null)
                                stb.Append("AND ");
                        }

                        if (pedidoVendaDTO.Vendedor != null)
                        {
                            stb.Append("p.SlpCode = @SlpCode ");
                            cmd.Parameters.AddWithValue("@SlpCode", pedidoVendaDTO.Vendedor.SlpCode);
                        }

                        if (!string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                        {
                            stb.Append(" AND p.OwnerCode = @OwnerCode ");
                            cmd.Parameters.AddWithValue("@OwnerCode", pedidoVendaDTO.OwnerCode);
                        }
                    }
                }

                stb.Append("ORDER BY p.DocNum DESC");


                SqlServerConexao conexao = new SqlServerConexao();

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

        public string RetornarCodigoTransportadora(long docNum)
        {
            string codigoTransportadora = "";
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT ""Carrier"" FROM RDR12 WHERE ""DocEntry"" = '{docNum}'";
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            codigoTransportadora = dr["Carrier"].ToString();
                        }
                    }
                }
                catch (Exception erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT Carrier FROM RDR12 WHERE DocEntry = @DocEntry");

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);
                    comando.Parameters.AddWithValue("@DocEntry", docNum);
                    SqlDataReader rdr = comando.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            codigoTransportadora = rdr["Carrier"].ToString();
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                }
                catch (SqlException erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
            return codigoTransportadora;
        }


        public double RetornarValorDespesaFrete(long docNum)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string query = $@"SELECT COALESCE(""LineTotal"", 0) FROM RDR3 WHERE ""DocEntry"" = '{docNum}' AND ""ExpnsCode"" = 1";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();
                    return Convert.ToDouble(conexaoHana.ExecuteScalar(query));
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
                stb.Append("SELECT COALESCE(LineTotal, 0) FROM RDR3 WHERE DocEntry = @DocEntry AND ExpnsCode = 1");

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);
                    comando.Parameters.AddWithValue("@DocEntry", docNum);

                    return Convert.ToDouble(comando.ExecuteScalar());
                }
                catch (SqlException erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
        }
    }
}
