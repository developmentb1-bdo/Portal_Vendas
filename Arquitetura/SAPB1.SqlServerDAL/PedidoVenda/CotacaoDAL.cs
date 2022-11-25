/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SAPB1.DTO.PedidoVenda;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.SqlServerDAL.PedidoVenda
{
    public sealed class CotacaoDAL : ICotacao
    {
        public CotacaoDAL() { }

        //string tSQLBase = @"SELECT TOP 500 * FROM OQUT ";
        string tSQLBase = "SELECT /*TOP 100*/ p.DocEntry, p.DocNum, p.CardCode, p.CardName, DocCur, p.DocStatus, p.DocDate, p.DocDueDate, p.TaxDate, p.DocTotalSy, p.DocTotal, p.CANCELED, p.NumAtCard, p.BPLId, p.VATRegNum, p.SlpCode, p.JrnlMemo, p.Address, p.Address2, p.TrnspCode, p.Confirmed, p.PartSupply, p.PoPrss, p.LangCode, p.Pick, p.PickRmrk, p.AgentCode, p.OwnerCode, c.CardName, c.U_CNPJ, p.PeyMethod, p.GroupNum, p.VatSum, p.Comments, " +
                             "p.U_S7_CobrarFrete, p.U_S7_TaxaFrete, p.U_S7_ValorFrete,p.DocType,p.Handwrtten,p.Printed,p.InvntSttus,p.ObjType FROM OQUT p LEFT JOIN OCRD c ON c.CardCode = p.CardCode ";


        public IList<CotacaoDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<CotacaoDTO> listCotacaoDTO = new List<CotacaoDTO>();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT /*TOP 100*/ p.""DocEntry"", p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", p.""DocDate"", p.""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""DocTotal"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"", p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", c.""U_CNPJ"", p.""PeyMethod"", p.""GroupNum"", p.""VatSum"", p.""Comments"", " +
                             $@"p.""U_S7_CobrarFrete"", p.""U_S7_TaxaFrete"", p.""U_S7_ValorFrete"", p.""DocType"", p.""Handwrtten"", p.""Printed"", p.""InvntSttus"", p.""ObjType"" FROM OQUT p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" ";
                try
                {
                    StringBuilder tSQL = new StringBuilder();
                    tSQL.Append(query);
                    tSQL.Append($@"ORDER BY p.""DocEntry"" DESC;");

                    conexaoHana.Connection();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    CotacaoDTO cotacaoDTO = new CotacaoDTO();
                    foreach (DataRow dr in dt.Rows)
                    {
                        cotacaoDTO = ObjetoCotacaoHanaDTO(dr);
                    }

                    listCotacaoDTO.Add(cotacaoDTO);

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
                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    StringBuilder tSQL = new StringBuilder();
                    tSQL.Append(tSQLBase);
                    tSQL.Append("ORDER BY p.DocEntry DESC;");

                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                    SqlDataReader dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        CotacaoDTO cotacaoDTO = new CotacaoDTO();
                        cotacaoDTO = ObjetoCotacaoDTO(dataReader);

                        listCotacaoDTO.Add(cotacaoDTO);
                    }
                    dataReader.Close();
                }
                catch (Exception erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
            return listCotacaoDTO;
        }

        public CotacaoDTO Selecionar(int docEntry)
        {
            CotacaoDTO cotacaoDTO = new CotacaoDTO();
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT /*TOP 100*/ p.""DocEntry"", p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", p.""DocDate"", p.""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""DocTotal"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"", p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", c.""U_CNPJ"", p.""PeyMethod"", p.""GroupNum"", p.""VatSum"", p.""Comments"", " +
                             $@"p.""U_S7_CobrarFrete"", p.""U_S7_TaxaFrete"", p.""U_S7_ValorFrete"", p.""DocType"", p.""Handwrtten"", p.""Printed"", p.""InvntSttus"", p.""ObjType"" FROM OQUT p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" WHERE p.""DocEntry"" = '{docEntry}'";

                try
                {
                    conexaoHana.Connection();
                    DataTable dataReader = conexaoHana.ExecuteDataTable(query);
                    if (dataReader.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dataReader.Rows)
                        {
                            cotacaoDTO = ObjetoCotacaoHanaDTO(dr);
                        }
                    }
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

                try
                {
                    StringBuilder tSQL = new StringBuilder();
                    tSQL.Append(tSQLBase);
                    tSQL.Append("WHERE p.DocEntry = @DocEntry;");

                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                    comando.Parameters.Add(new SqlParameter("@DocEntry", docEntry));
                    SqlDataReader dataReader = comando.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        cotacaoDTO = ObjetoCotacaoDTO(dataReader);
                    }
                    dataReader.Close();
                }
                catch (Exception erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }


            return cotacaoDTO;
        }

        private CotacaoDTO ObjetoCotacaoDTO(SqlDataReader dataReader)
        {
            CotacaoDTO cotacaoDTO = new CotacaoDTO();

            if (dataReader.HasRows)
            {
                cotacaoDTO.DocEntry = Convert.ToInt32(dataReader["DocEntry"]);
                cotacaoDTO.U_CNPJ = ((!DBNull.Value.Equals(dataReader["U_CNPJ"])) ? Convert.ToString(dataReader["U_CNPJ"]) : "");
                cotacaoDTO.DocNum = Convert.ToInt32(dataReader["DocNum"]);
                cotacaoDTO.DocType = ((!DBNull.Value.Equals(dataReader["DocType"])) ? Convert.ToChar(dataReader["DocType"]) : char.MinValue);
                cotacaoDTO.CANCELED = ((!DBNull.Value.Equals(dataReader["CANCELED"])) ? Convert.ToChar(dataReader["CANCELED"]) : char.MinValue);
                cotacaoDTO.Handwrtten = ((!DBNull.Value.Equals(dataReader["Handwrtten"])) ? Convert.ToChar(dataReader["Handwrtten"]) : char.MinValue);
                cotacaoDTO.Printed = ((!DBNull.Value.Equals(dataReader["Printed"])) ? Convert.ToChar(dataReader["Printed"]) : char.MinValue);
                cotacaoDTO.DocStatus = ((!DBNull.Value.Equals(dataReader["DocStatus"])) ? Convert.ToChar(dataReader["DocStatus"]) : char.MinValue);
                cotacaoDTO.InvntSttus = ((!DBNull.Value.Equals(dataReader["InvntSttus"])) ? Convert.ToChar(dataReader["InvntSttus"]) : char.MinValue);
                cotacaoDTO.ObjType = ((!DBNull.Value.Equals(dataReader["ObjType"])) ? Convert.ToString(dataReader["ObjType"]) : "");
                cotacaoDTO.DocDate = ((!DBNull.Value.Equals(dataReader["DocDate"])) ? Convert.ToDateTime(dataReader["DocDate"]) : DateTime.MinValue);
                cotacaoDTO.DocDueDate = ((!DBNull.Value.Equals(dataReader["DocDueDate"])) ? Convert.ToDateTime(dataReader["DocDueDate"]) : DateTime.MinValue);
                cotacaoDTO.TaxDate = ((!DBNull.Value.Equals(dataReader["TaxDate"])) ? Convert.ToDateTime(dataReader["TaxDate"]) : DateTime.MinValue);
                cotacaoDTO.CardCode = ((!DBNull.Value.Equals(dataReader["CardCode"])) ? Convert.ToString(dataReader["CardCode"]) : "");
                cotacaoDTO.CardName = ((!DBNull.Value.Equals(dataReader["CardName"])) ? Convert.ToString(dataReader["CardName"]) : "");
                cotacaoDTO.DocTotal = ((!DBNull.Value.Equals(dataReader["DocTotal"])) ? Convert.ToDecimal(dataReader["DocTotal"]) : 0m);
                cotacaoDTO.GroupNum = ((!DBNull.Value.Equals(dataReader["GroupNum"])) ? Convert.ToInt16(dataReader["GroupNum"]) : (short)0);
                cotacaoDTO.Comments = ((!DBNull.Value.Equals(dataReader["Comments"])) ? Convert.ToString(dataReader["Comments"]) : "");
            }
            return cotacaoDTO;
        }

        private CotacaoDTO ObjetoCotacaoHanaDTO(DataRow dataReader)
        {
            CotacaoDTO cotacaoDTO = new CotacaoDTO();


            cotacaoDTO.DocEntry = Convert.ToInt32(dataReader["DocEntry"]);
            cotacaoDTO.U_CNPJ = ((!DBNull.Value.Equals(dataReader["U_CNPJ"])) ? Convert.ToString(dataReader["U_CNPJ"]) : "");
            cotacaoDTO.DocNum = Convert.ToInt32(dataReader["DocNum"]);
            cotacaoDTO.DocType = ((!DBNull.Value.Equals(dataReader["DocType"])) ? Convert.ToChar(dataReader["DocType"]) : char.MinValue);
            cotacaoDTO.CANCELED = ((!DBNull.Value.Equals(dataReader["CANCELED"])) ? Convert.ToChar(dataReader["CANCELED"]) : char.MinValue);
            cotacaoDTO.Handwrtten = ((!DBNull.Value.Equals(dataReader["Handwrtten"])) ? Convert.ToChar(dataReader["Handwrtten"]) : char.MinValue);
            cotacaoDTO.Printed = ((!DBNull.Value.Equals(dataReader["Printed"])) ? Convert.ToChar(dataReader["Printed"]) : char.MinValue);
            cotacaoDTO.DocStatus = ((!DBNull.Value.Equals(dataReader["DocStatus"])) ? Convert.ToChar(dataReader["DocStatus"]) : char.MinValue);
            cotacaoDTO.InvntSttus = ((!DBNull.Value.Equals(dataReader["InvntSttus"])) ? Convert.ToChar(dataReader["InvntSttus"]) : char.MinValue);
            cotacaoDTO.ObjType = ((!DBNull.Value.Equals(dataReader["ObjType"])) ? Convert.ToString(dataReader["ObjType"]) : "");
            cotacaoDTO.DocDate = ((!DBNull.Value.Equals(dataReader["DocDate"])) ? Convert.ToDateTime(dataReader["DocDate"]) : DateTime.MinValue);
            cotacaoDTO.DocDueDate = ((!DBNull.Value.Equals(dataReader["DocDueDate"])) ? Convert.ToDateTime(dataReader["DocDueDate"]) : DateTime.MinValue);
            cotacaoDTO.TaxDate = ((!DBNull.Value.Equals(dataReader["TaxDate"])) ? Convert.ToDateTime(dataReader["TaxDate"]) : DateTime.MinValue);
            cotacaoDTO.CardCode = ((!DBNull.Value.Equals(dataReader["CardCode"])) ? Convert.ToString(dataReader["CardCode"]) : "");
            cotacaoDTO.CardName = ((!DBNull.Value.Equals(dataReader["CardName"])) ? Convert.ToString(dataReader["CardName"]) : "");
            cotacaoDTO.DocTotal = ((!DBNull.Value.Equals(dataReader["DocTotal"])) ? Convert.ToDecimal(dataReader["DocTotal"]) : 0m);
            cotacaoDTO.GroupNum = ((!DBNull.Value.Equals(dataReader["GroupNum"])) ? Convert.ToInt16(dataReader["GroupNum"]) : (short)0);
            cotacaoDTO.Comments = ((!DBNull.Value.Equals(dataReader["Comments"])) ? Convert.ToString(dataReader["Comments"]) : "");

            return cotacaoDTO;
        }
    }
}