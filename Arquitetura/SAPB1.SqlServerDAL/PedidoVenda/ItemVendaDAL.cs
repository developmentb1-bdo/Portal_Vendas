using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoVenda;
using System.Data.SqlClient;
using SAPB1.IDAL.PedidoVenda;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.PedidoVenda
{
    public class ItemVendaDAL : IItemVenda
    {

        public IList<ItemVendaDTO> Listar(ItemVendaDTO itemVendaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string query = $@"SELECT t0.""DocEntry"", t0.""LineNum"", t0.""ItemCode"", t0.""Quantity"", t0.""DelivrdQty"", t0.""UomCode"", t0.""PackQty"", t0.""DiscPrcnt"", t0.""Usage"", t0.""TaxCode"", t0.""CFOPCode"", t0.""CSTCode"", t0.""LinePoPrss"", t0.""Price"", t0.""LineTotal"",COALESCE(t0.""U_Lote"",'') AS ""U_Lote"",COALESCE(t0.""U_Comprimento2"",0) AS ""U_Comprimento2"",COALESCE(t0.""U_Pecas"",0) AS ""U_Pecas"",COALESCE(t0.""U_Metros"",0) AS ""U_Metros"",COALESCE(t0.""U_Norma"",'') AS ""U_Norma"",COALESCE(t0.""U_Peso"",0) AS ""U_Peso"", COALESCE(t1.""U_ComprFixo"",0) AS ""DescricaoAuxiliar"", COALESCE(t0.""unitMsr"",'') AS ""unitMsr"", COALESCE(t0.""U_SKILL_NP"",'') AS ""U_SKILL_NP"", COALESCE(t0.""U_SKILL_IP"",'') AS ""U_SKILL_IP"", t0.""ShipDate"", COALESCE(t2.""WhsName"",'') AS ""WhsName"" FROM RDR1 t0 INNER JOIN OITM t1 ON t0.""ItemCode"" = t1.""ItemCode"" LEFT JOIN OWHS t2 ON t1.""DfltWH"" = t2.""WhsCode"" ";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                query += $@"WHERE t0.""DocEntry"" = '{itemVendaDTO.DocEntry}' ORDER BY ""LineNum"" ASC";

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
                string queryPadrao = "SELECT t0.DocEntry, t0.LineNum, t0.ItemCode, t0.Quantity, t0.DelivrdQty, t0.UomCode, t0.PackQty, t0.DiscPrcnt, t0.Usage, t0.TaxCode, t0.CFOPCode, t0.CSTCode, t0.LinePoPrss, t0.Price, t0.LineTotal,COALESCE(t0.U_Lote,'') AS 'U_Lote',COALESCE(t0.U_Comprimento2,0) AS 'U_Comprimento2',COALESCE(t0.U_Pecas,0) AS 'U_Pecas',COALESCE(t0.U_Metros,0) AS 'U_Metros',COALESCE(t0.U_Norma,'') AS 'U_Norma',COALESCE(t0.U_Peso,0) AS 'U_Peso',COALESCE(t1.U_ComprFixo,0) AS 'DescricaoAuxiliar',COALESCE(t0.unitMsr,'') AS 'unitMsr', COALESCE(t0.U_SKILL_NP,'') AS 'U_SKILL_NP', COALESCE(t0.U_SKILL_IP,'') AS 'U_SKILL_IP',t0.ShipDate,COALESCE(t2.WhsName,'') AS 'WhsName' FROM RDR1 t0 INNER JOIN OITM t1 ON t0.ItemCode = t1.ItemCode LEFT JOIN OWHS t2 ON t1.DfltWH = t2.WhsCode ";
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);
                stb.Append("WHERE t0.DocEntry = @DocEntry ");
                stb.Append("ORDER BY LineNum ASC");

                cmd.Parameters.AddWithValue("@DocEntry", itemVendaDTO.DocEntry);

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

        private IList<ItemVendaDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<ItemVendaDTO> listItens = new List<ItemVendaDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ItemVendaDTO itemDTO = new ItemVendaDTO();
                    itemDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    itemDTO.LineNum = Convert.ToInt32(rdr["LineNum"]);
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.Quantity = Convert.ToDouble(rdr["Quantity"]);
                    itemDTO.DelivrdQty = Convert.ToDouble(rdr["DelivrdQty"]);
                    itemDTO.UomCode = rdr["UomCode"].ToString();
                    itemDTO.PackQty = Convert.ToDouble(rdr["PackQty"]);
                    itemDTO.DiscPrcnt = Convert.ToDouble(rdr["DiscPrcnt"]);
                    itemDTO.Usage = Convert.ToInt32(rdr["Usage"]);
                    itemDTO.TaxCode = rdr["TaxCode"].ToString();
                    itemDTO.CFOPCode = rdr["CFOPCode"].ToString();
                    itemDTO.CSTCode = rdr["CSTCode"].ToString();
                    itemDTO.LinePoPrss = rdr["LinePoPrss"].ToString();
                    itemDTO.Price = Convert.ToDouble(rdr["Price"]);
                    itemDTO.LineTotal = Convert.ToInt32(rdr["LineTotal"]);
                    itemDTO.Comprimento = Convert.ToInt32(rdr["U_Comprimento2"]);
                    itemDTO.Lote = rdr["U_Lote"].ToString();
                    itemDTO.Norma = rdr["U_Norma"].ToString();
                    itemDTO.QtdBarra = Convert.ToDouble(rdr["U_Pecas"]);
                    itemDTO.QtdMetro = Convert.ToDouble(rdr["U_Metros"]);
                    itemDTO.Peso = Convert.ToDouble(rdr["U_Peso"]);
                    itemDTO.DescricaoAuxiliar = rdr["DescricaoAuxiliar"].ToString();
                    itemDTO.DataEntrega = Convert.ToDateTime(rdr["ShipDate"]);
                    itemDTO.nomeDeposito = rdr["WhsName"].ToString();

                    itemDTO.UnidadeMedida = rdr["unitMsr"].ToString();
                    itemDTO.NumeroPedidoCompra = rdr["U_SKILL_NP"].ToString();
                    itemDTO.ItemPedidoCompra = rdr["U_SKILL_IP"].ToString();

                    listItens.Add(itemDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listItens;
        }

        private IList<ItemVendaDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<ItemVendaDTO> listItens = new List<ItemVendaDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rdr in dt.Rows)
                {
                    ItemVendaDTO itemDTO = new ItemVendaDTO();
                    itemDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    itemDTO.LineNum = Convert.ToInt32(rdr["LineNum"]);
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.Quantity = Convert.ToDouble(rdr["Quantity"]);
                    itemDTO.DelivrdQty = Convert.ToDouble(rdr["DelivrdQty"]);
                    itemDTO.UomCode = rdr["UomCode"].ToString();
                    itemDTO.PackQty = Convert.ToDouble(rdr["PackQty"]);
                    itemDTO.DiscPrcnt = Convert.ToDouble(rdr["DiscPrcnt"]);
                    itemDTO.Usage = Convert.ToInt32(rdr["Usage"]);
                    itemDTO.TaxCode = rdr["TaxCode"].ToString();
                    itemDTO.CFOPCode = rdr["CFOPCode"].ToString();
                    itemDTO.CSTCode = rdr["CSTCode"].ToString();
                    itemDTO.LinePoPrss = rdr["LinePoPrss"].ToString();
                    itemDTO.Price = Convert.ToDouble(rdr["Price"]);
                    itemDTO.LineTotal = Convert.ToInt32(rdr["LineTotal"]);
                    itemDTO.Comprimento = Convert.ToInt32(rdr["U_Comprimento2"]);
                    itemDTO.Lote = rdr["U_Lote"].ToString();
                    itemDTO.Norma = rdr["U_Norma"].ToString();
                    itemDTO.QtdBarra = Convert.ToDouble(rdr["U_Pecas"]);
                    itemDTO.QtdMetro = Convert.ToDouble(rdr["U_Metros"]);
                    itemDTO.Peso = Convert.ToDouble(rdr["U_Peso"]);
                    itemDTO.DescricaoAuxiliar = rdr["DescricaoAuxiliar"].ToString();
                    itemDTO.DataEntrega = Convert.ToDateTime(rdr["ShipDate"]);
                    itemDTO.nomeDeposito = rdr["WhsName"].ToString();

                    itemDTO.UnidadeMedida = rdr["unitMsr"].ToString();
                    itemDTO.NumeroPedidoCompra = rdr["U_SKILL_NP"].ToString();
                    itemDTO.ItemPedidoCompra = rdr["U_SKILL_IP"].ToString();

                    listItens.Add(itemDTO);
                }

            }

            return listItens;
        }
    }
}
