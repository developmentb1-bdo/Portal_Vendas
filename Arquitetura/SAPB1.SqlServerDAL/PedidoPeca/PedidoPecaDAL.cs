using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoPeca;
using SAPB1.DTO.PedidoPeca;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.PedidoPeca
{
    public class PedidoPecaDAL : IPedidoPeca
    {


        public IList<PedidoPecaDTO> Listar(PedidoPecaDTO pedidoPecaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                string query = $@"SELECT p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", COALESCE(p.""DocDate"",'2000-01-01') AS ""DocDate"", COALESCE(p.""DocDueDate"", '2000-01-01') AS ""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"", p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", p.""U_UND_PARADA"", p.""U_ST_CONCESS"", p.""U_NomeCliente"", COALESCE(p.""U_KmAtual"", 0) AS ""U_KmAtual"", p.""U_FalhasApresent"", p.""U_ObsAdc"", p.""U_TstRealizado"", COALESCE(""U_QtdDiasParado"", 0) AS ""U_QtdDiasParado"", ""U_Chassi"", ""U_ModVei"", ""U_AnoModelo"", ""U_EntreEixos"" FROM ORDR p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" ";

                if (pedidoPecaDTO != null)
                {
                    if (pedidoPecaDTO.DocNum != 0)
                    {
                        query += "WHERE ";

                        if (pedidoPecaDTO.DocNum != 0)
                        {
                            query += $@"p.""DocNum"" = '{pedidoPecaDTO.DocNum}' ";
                        }
                    }
                }
                query += $@"ORDER BY p.""DocDate"" DESC";
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
                string queryPadrao = "SELECT p.DocNum, p.CardCode, p.CardName, DocCur, p.DocStatus, COALESCE(p.DocDate,'2000-01-01') AS 'DocDate', COALESCE(p.DocDueDate, '2000-01-01') AS 'DocDueDate', p.TaxDate, p.DocTotalSy, p.CANCELED, p.NumAtCard, p.BPLId, p.VATRegNum, p.SlpCode, p.JrnlMemo, p.Address, p.Address2, p.TrnspCode, p.Confirmed, p.PartSupply, p.PoPrss, p.LangCode, p.Pick, p.PickRmrk, p.AgentCode, p.OwnerCode, c.CardName, p.U_UND_PARADA, p.U_ST_CONCESS, p.U_NomeCliente, COALESCE(p.U_KmAtual, 0) AS 'U_KmAtual', p.U_FalhasApresent, p.U_ObsAdc, p.U_TstRealizado, COALESCE(U_QtdDiasParado, 0) AS 'U_QtdDiasParado', U_Chassi, U_ModVei, U_AnoModelo, U_EntreEixos FROM ORDR p LEFT JOIN OCRD c ON c.CardCode = p.CardCode ";
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);

                if (pedidoPecaDTO != null)
                {
                    if (pedidoPecaDTO.DocNum != 0)
                    {
                        stb.Append("WHERE ");

                        if (pedidoPecaDTO.DocNum != 0)
                        {
                            stb.Append("p.DocNum = @DocNum ");

                            cmd.Parameters.AddWithValue("@DocNum", pedidoPecaDTO.DocNum);
                        }
                    }
                }

                stb.Append("ORDER BY p.DocDate DESC");

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

        private IList<PedidoPecaDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                IList<PedidoPecaDTO> listPedidos = new List<PedidoPecaDTO>();

                while (rdr.Read())
                {
                    PedidoPecaDTO pedidoVendaDTO = new PedidoPecaDTO();
                    pedidoVendaDTO.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"].ToString()));
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
                    pedidoVendaDTO.U_ST_CONCESS = rdr["U_ST_CONCESS"].ToString();
                    pedidoVendaDTO.U_UND_PARADA = rdr["U_UND_PARADA"].ToString();
                    pedidoVendaDTO.CardName = rdr["CardName"].ToString();
                    pedidoVendaDTO.U_NomeCliente = rdr["U_NomeCliente"].ToString();
                    pedidoVendaDTO.U_ObsAdc = rdr["U_ObsAdc"].ToString();
                    pedidoVendaDTO.U_TstRealizado = rdr["U_TstRealizado"].ToString();
                    pedidoVendaDTO.U_KmAtual = Convert.ToDouble(rdr["U_KmAtual"].ToString().Equals("") ? "0" : rdr["U_KmAtual"].ToString());
                    pedidoVendaDTO.U_FalhasApresent = rdr["U_FalhasApresent"].ToString();
                    pedidoVendaDTO.U_QtdDiasParado = Convert.ToDouble(rdr["U_QtdDiasParado"].ToString().Equals("") ? "0" : rdr["U_QtdDiasParado"].ToString());
                    pedidoVendaDTO.U_Chassi = rdr["U_Chassi"].ToString();
                    pedidoVendaDTO.U_EntreEixos = rdr["U_EntreEixos"].ToString();
                    pedidoVendaDTO.U_ModVei = rdr["U_ModVei"].ToString();
                    pedidoVendaDTO.U_AnoModelo = rdr["U_AnoModelo"].ToString();

                    listPedidos.Add(pedidoVendaDTO);
                }

                rdr.Close();
                rdr.Dispose();

                return listPedidos;
            }
            else
            {
                return new List<PedidoPecaDTO>();
            }
        }

        private IList<PedidoPecaDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<PedidoPecaDTO> listPedidos = new List<PedidoPecaDTO>();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow rdr in dt.Rows)
                {
                    PedidoPecaDTO pedidoVendaDTO = new PedidoPecaDTO();
                    pedidoVendaDTO.DocNum = (rdr["DocNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["DocNum"].ToString()));
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
                    pedidoVendaDTO.U_ST_CONCESS = rdr["U_ST_CONCESS"].ToString();
                    pedidoVendaDTO.U_UND_PARADA = rdr["U_UND_PARADA"].ToString();
                    pedidoVendaDTO.CardName = rdr["CardName"].ToString();
                    pedidoVendaDTO.U_NomeCliente = rdr["U_NomeCliente"].ToString();
                    pedidoVendaDTO.U_ObsAdc = rdr["U_ObsAdc"].ToString();
                    pedidoVendaDTO.U_TstRealizado = rdr["U_TstRealizado"].ToString();
                    pedidoVendaDTO.U_KmAtual = Convert.ToDouble(rdr["U_KmAtual"].ToString().Equals("") ? "0" : rdr["U_KmAtual"].ToString());
                    pedidoVendaDTO.U_FalhasApresent = rdr["U_FalhasApresent"].ToString();
                    pedidoVendaDTO.U_QtdDiasParado = Convert.ToDouble(rdr["U_QtdDiasParado"].ToString().Equals("") ? "0" : rdr["U_QtdDiasParado"].ToString());
                    pedidoVendaDTO.U_Chassi = rdr["U_Chassi"].ToString();
                    pedidoVendaDTO.U_EntreEixos = rdr["U_EntreEixos"].ToString();
                    pedidoVendaDTO.U_ModVei = rdr["U_ModVei"].ToString();
                    pedidoVendaDTO.U_AnoModelo = rdr["U_AnoModelo"].ToString();

                    listPedidos.Add(pedidoVendaDTO);
                }

                return listPedidos;
            }
            else
            {
                return new List<PedidoPecaDTO>();
            }
        }

        public IList<PedidoPecaDTO> ListarPedidoPorConcessionario(string cardCode)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", COALESCE(p.""DocDate"",'2000-01-01') AS ""DocDate"", COALESCE(p.""DocDueDate"", '2000-01-01') AS ""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"", p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", p.""U_UND_PARADA"", p.""U_ST_CONCESS"", p.""U_NomeCliente"", COALESCE(p.""U_KmAtual"", 0) AS ""U_KmAtual"", p.""U_FalhasApresent"", p.""U_ObsAdc"", p.""U_TstRealizado"", COALESCE(""U_QtdDiasParado"", 0) AS ""U_QtdDiasParado"", ""U_Chassi"", ""U_ModVei"", ""U_AnoModelo"", ""U_EntreEixos"" FROM ORDR p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" WHERE p.""CardCode"" = '{cardCode}' ORDER BY p.""DocNum"" DESC";

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
                string queryPadrao = "SELECT p.DocNum, p.CardCode, p.CardName, DocCur, p.DocStatus, COALESCE(p.DocDate,'2000-01-01') AS 'DocDate', COALESCE(p.DocDueDate, '2000-01-01') AS 'DocDueDate', p.TaxDate, p.DocTotalSy, p.CANCELED, p.NumAtCard, p.BPLId, p.VATRegNum, p.SlpCode, p.JrnlMemo, p.Address, p.Address2, p.TrnspCode, p.Confirmed, p.PartSupply, p.PoPrss, p.LangCode, p.Pick, p.PickRmrk, p.AgentCode, p.OwnerCode, c.CardName, p.U_UND_PARADA, p.U_ST_CONCESS, p.U_NomeCliente, COALESCE(p.U_KmAtual, 0) AS 'U_KmAtual', p.U_FalhasApresent, p.U_ObsAdc, p.U_TstRealizado, COALESCE(U_QtdDiasParado, 0) AS 'U_QtdDiasParado', U_Chassi, U_ModVei, U_AnoModelo, U_EntreEixos FROM ORDR p LEFT JOIN OCRD c ON c.CardCode = p.CardCode ";
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);
                stb.Append("WHERE p.CardCode = @CardCode ");
                stb.Append("ORDER BY p.DocNum DESC");

                cmd.Parameters.AddWithValue("@CardCode", cardCode);

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

        public IList<PedidoPecaDTO> BuscarPedidoPorConcessionario(PedidoPecaDTO pedidoPecaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                string query = $@"SELECT p.""DocNum"", p.""CardCode"", p.""CardName"", ""DocCur"", p.""DocStatus"", COALESCE(p.""DocDate"",'2000-01-01') AS ""DocDate"", COALESCE(p.""DocDueDate"", '2000-01-01') AS ""DocDueDate"", p.""TaxDate"", p.""DocTotalSy"", p.""CANCELED"", p.""NumAtCard"", p.""BPLId"", p.""VATRegNum"", p.""SlpCode"", p.""JrnlMemo"", p.""Address"", p.""Address2"", p.""TrnspCode"", p.""Confirmed"", p.""PartSupply"", p.""PoPrss"", p.""LangCode"", p.""Pick"", p.""PickRmrk"", p.""AgentCode"", p.""OwnerCode"", c.""CardName"", p.""U_UND_PARADA"", p.""U_ST_CONCESS"", p.""U_NomeCliente"", COALESCE(p.""U_KmAtual"", 0) AS ""U_KmAtual"", p.""U_FalhasApresent"", p.""U_ObsAdc"", p.""U_TstRealizado"", COALESCE(""U_QtdDiasParado"", 0) AS ""U_QtdDiasParado"", ""U_Chassi"", ""U_ModVei"", ""U_AnoModelo"", ""U_EntreEixos"" FROM ORDR p LEFT JOIN OCRD c ON c.""CardCode"" = p.""CardCode"" WHERE p.""CardCode"" = '{pedidoPecaDTO.CardCode}' ";

                if (pedidoPecaDTO.DocNum > 0 || (pedidoPecaDTO.DocDate != DateTime.MinValue && pedidoPecaDTO.DocDueDate != DateTime.MinValue))
                {
                    query += "AND ";

                    if (pedidoPecaDTO.DocNum > 0)
                    {
                        query += $@"p.""DocNum"" = {pedidoPecaDTO.DocNum}";

                        if (pedidoPecaDTO.DocDate != DateTime.MinValue && pedidoPecaDTO.DocDueDate != DateTime.MinValue)
                            query += "AND ";
                    }

                    if (pedidoPecaDTO.DocDate != DateTime.MinValue && pedidoPecaDTO.DocDueDate != DateTime.MinValue)
                    {
                        query += $@"(""DocDate"" BETWEEN '{pedidoPecaDTO.DocDate.ToString("yyyy-MM-dd")}' AND '{pedidoPecaDTO.DocDueDate.ToString("yyyy-MM-dd")}') ";

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
                string queryPadrao = "SELECT p.DocNum, p.CardCode, p.CardName, DocCur, p.DocStatus, COALESCE(p.DocDate,'2000-01-01') AS 'DocDate', COALESCE(p.DocDueDate, '2000-01-01') AS 'DocDueDate', p.TaxDate, p.DocTotalSy, p.CANCELED, p.NumAtCard, p.BPLId, p.VATRegNum, p.SlpCode, p.JrnlMemo, p.Address, p.Address2, p.TrnspCode, p.Confirmed, p.PartSupply, p.PoPrss, p.LangCode, p.Pick, p.PickRmrk, p.AgentCode, p.OwnerCode, c.CardName, p.U_UND_PARADA, p.U_ST_CONCESS, p.U_NomeCliente, COALESCE(p.U_KmAtual, 0) AS 'U_KmAtual', p.U_FalhasApresent, p.U_ObsAdc, p.U_TstRealizado, COALESCE(U_QtdDiasParado, 0) AS 'U_QtdDiasParado', U_Chassi, U_ModVei, U_AnoModelo, U_EntreEixos FROM ORDR p LEFT JOIN OCRD c ON c.CardCode = p.CardCode ";
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);
                stb.Append("WHERE p.CardCode = @CardCode ");
                cmd.Parameters.AddWithValue("@CardCode", pedidoPecaDTO.CardCode);

                if (pedidoPecaDTO.DocNum > 0 || (pedidoPecaDTO.DocDate != DateTime.MinValue && pedidoPecaDTO.DocDueDate != DateTime.MinValue))
                {
                    stb.Append("AND ");

                    if (pedidoPecaDTO.DocNum > 0)
                    {
                        stb.Append("p.DocNum = @DocNum ");
                        cmd.Parameters.AddWithValue("@DocNum", pedidoPecaDTO.DocNum);

                        if (pedidoPecaDTO.DocDate != DateTime.MinValue && pedidoPecaDTO.DocDueDate != DateTime.MinValue)
                            stb.Append("AND ");
                    }

                    if (pedidoPecaDTO.DocDate != DateTime.MinValue && pedidoPecaDTO.DocDueDate != DateTime.MinValue)
                    {
                        stb.Append("(DocDate BETWEEN @DataInicial ANd @DataFinal) ");
                        cmd.Parameters.AddWithValue("@DataInicial", pedidoPecaDTO.DocDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@DataFinal", pedidoPecaDTO.DocDueDate.ToString("yyyy-MM-dd"));
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
    }
}
