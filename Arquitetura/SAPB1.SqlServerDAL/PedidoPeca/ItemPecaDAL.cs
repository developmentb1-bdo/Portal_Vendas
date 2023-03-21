using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoPeca;
using SAPB1.IDAL.PedidoPeca;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SAPB1.SqlServerDAL.PedidoPeca
{
    public class ItemPecaDAL : IItemPeca
    {
        public IList<ItemPecaDTO> Listar(ItemPecaDTO itemVendaDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                string query = $@"SELECT r.""DocEntry"", r.""LineNum"", r.""ItemCode"", r.""Quantity"", r.""DelivrdQty"", r.""UomCode"", r.""PackQty"", r.""DiscPrcnt"", COALESCE(r.""Usage"", 0) AS ""Usage"", r.""TaxCode"", r.""CFOPCode"", r.""CSTCode"", r.""LinePoPrss"", ""Price"", ""LineTotal"", i.""ItemName"", r.""Dscription"", r.""U_Modelo"", r.""U_AnoModel"", r.""U_EntreEix"" FROM RDR1 r INNER JOIN OITM i ON i.""ItemCode"" = r.""ItemCode"" WHERE r.""DocEntry"" = '{itemVendaDTO.DocEntry}' ORDER BY r.""LineNum"" ASC";
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
                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT r.DocEntry, r.LineNum, r.ItemCode, r.Quantity, r.DelivrdQty, r.UomCode, r.PackQty, r.DiscPrcnt, COALESCE(r.Usage, 0) AS 'Usage', r.TaxCode, r.CFOPCode, ");
                stb.Append("r.CSTCode, r.LinePoPrss, ");
                stb.Append("Price, LineTotal,  i.ItemName, r.Dscription, r.U_Modelo, r.U_AnoModel, r.U_EntreEix ");
                stb.Append("FROM RDR1 r ");
                stb.Append("INNER JOIN OITM i ON i.ItemCode = r.ItemCode ");

                stb.Append("WHERE r.DocEntry = @DocEntry ");
                stb.Append("ORDER BY r.LineNum ASC");

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

        private IList<ItemPecaDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<ItemPecaDTO> listItens = new List<ItemPecaDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    ItemPecaDTO itemDTO = new ItemPecaDTO();
                    itemDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    itemDTO.LineNum = Convert.ToInt32(rdr["LineNum"]);
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.Quantity = Convert.ToDouble(rdr["Quantity"]);
                    itemDTO.DelivrdQty = Convert.ToDouble((rdr["DelivrdQty"].ToString().Equals("") ? "0" : rdr["DelivrdQty"].ToString()));
                    itemDTO.UomCode = rdr["UomCode"].ToString();
                    itemDTO.PackQty = Convert.ToDouble(rdr["PackQty"]);
                    itemDTO.DiscPrcnt = Convert.ToDouble((rdr["DiscPrcnt"].ToString().Equals("") ? "0" : rdr["DiscPrcnt"].ToString()));
                    itemDTO.Usage = Convert.ToInt32(rdr["Usage"]);
                    itemDTO.TaxCode = rdr["TaxCode"].ToString();
                    itemDTO.CFOPCode = rdr["CFOPCode"].ToString();
                    itemDTO.CSTCode = rdr["CSTCode"].ToString();
                    itemDTO.LinePoPrss = rdr["LinePoPrss"].ToString();
                    itemDTO.Price = Convert.ToDouble(rdr["Price"]);
                    itemDTO.LineTotal = Convert.ToDouble(rdr["LineTotal"]);
                    itemDTO.ItemName = rdr["ItemName"].ToString();
                    itemDTO.Dscription = rdr["Dscription"].ToString();
                    itemDTO.Modelo = rdr["U_Modelo"].ToString();
                    itemDTO.AnoModelo = rdr["U_AnoModel"].ToString();
                    itemDTO.EntreEixos = rdr["U_EntreEix"].ToString();

                    listItens.Add(itemDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listItens;
        }

        private IList<ItemPecaDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);
            IList<ItemPecaDTO> listItens = new List<ItemPecaDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rdr in dt.Rows)
                {
                    ItemPecaDTO itemDTO = new ItemPecaDTO();
                    itemDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    itemDTO.LineNum = Convert.ToInt32(rdr["LineNum"]);
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.Quantity = Convert.ToDouble(rdr["Quantity"]);
                    itemDTO.DelivrdQty = Convert.ToDouble((rdr["DelivrdQty"].ToString().Equals("") ? "0" : rdr["DelivrdQty"].ToString()));
                    itemDTO.UomCode = rdr["UomCode"].ToString();
                    itemDTO.PackQty = Convert.ToDouble(rdr["PackQty"]);
                    itemDTO.DiscPrcnt = Convert.ToDouble((rdr["DiscPrcnt"].ToString().Equals("") ? "0" : rdr["DiscPrcnt"].ToString()));
                    itemDTO.Usage = Convert.ToInt32(rdr["Usage"]);
                    itemDTO.TaxCode = rdr["TaxCode"].ToString();
                    itemDTO.CFOPCode = rdr["CFOPCode"].ToString();
                    itemDTO.CSTCode = rdr["CSTCode"].ToString();
                    itemDTO.LinePoPrss = rdr["LinePoPrss"].ToString();
                    itemDTO.Price = Convert.ToDouble(rdr["Price"]);
                    itemDTO.LineTotal = Convert.ToDouble(rdr["LineTotal"]);
                    itemDTO.ItemName = rdr["ItemName"].ToString();
                    itemDTO.Dscription = rdr["Dscription"].ToString();
                    itemDTO.Modelo = rdr["U_Modelo"].ToString();
                    itemDTO.AnoModelo = rdr["U_AnoModel"].ToString();
                    itemDTO.EntreEixos = rdr["U_EntreEix"].ToString();

                    listItens.Add(itemDTO);
                }

            }

            return listItens;
        }

        public IList<ItemPecaDTO> ListarTodosItensPedidoPecaPorConcessionario(string cardCode)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                string query = $@"SELECT COALESCE(r.""DocEntry"", 0) AS ""DocEntry"", COALESCE(r.""LineNum"", 0) AS ""LineNum"", r.""ItemCode"", COALESCE(r.""Quantity"", 0) AS ""Quantity"", COALESCE(r.""DelivrdQty"", 0) AS ""DelivrdQty"", r.""UomCode"", COALESCE(r.""PackQty"", 0) AS ""PackQty"", COALESCE(r.""DiscPrcnt"", 0) AS ""DiscPrcnt"", COALESCE(r.""Usage"", 0) AS ""Usage"", r.""TaxCode"", r.""CFOPCode"", r.""CSTCode"", r.""LinePoPrss"", ""Price"", ""LineTotal"", i.""ItemName"", r.""Dscription"", r.""U_Modelo"", r.""U_AnoModel"", r.""U_EntreEix"" FROM RDR1 r INNER JOIN OITM i ON i.""ItemCode"" = r.""ItemCode"" WHERE ""BaseCard"" = '{cardCode}'";
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
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
                stb.Append("SELECT COALESCE(r.DocEntry, 0) AS 'DocEntry', COALESCE(r.LineNum, 0) AS 'LineNum', r.ItemCode, COALESCE(r.Quantity, 0) AS 'Quantity', COALESCE(r.DelivrdQty, 0) AS 'DelivrdQty', r.UomCode, COALESCE(r.PackQty, 0) AS 'PackQty', COALESCE(r.DiscPrcnt, 0) AS 'DiscPrcnt', COALESCE(r.Usage, 0) AS 'Usage', r.TaxCode, r.CFOPCode, ");
                stb.Append("r.CSTCode, r.LinePoPrss, ");
                stb.Append("Price, LineTotal,  i.ItemName, r.Dscription, r.U_Modelo, r.U_AnoModel, r.U_EntreEix ");
                stb.Append("FROM RDR1 r ");
                stb.Append("INNER JOIN OITM i ON i.ItemCode = r.ItemCode ");
                stb.Append("WHERE BaseCard = @CardCode");

                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;
                    cmd.Parameters.AddWithValue("@CardCode", cardCode);

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
