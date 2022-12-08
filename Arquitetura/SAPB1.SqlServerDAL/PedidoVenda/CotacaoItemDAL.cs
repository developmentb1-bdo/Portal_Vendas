/*
 * @author Victor Oliveira.
 */
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
    public class CotacaoItemDAL : IItemCotacao
    {

        public IList<CotacaoItemDTO> Listar(CotacaoItemDTO CotacaoItemDTO)
        {
            string query = $@"SELECT t0.""DocEntry"", t0.""Dscription"", t0.""LineNum"", t0.""ItemCode"", t0.""Quantity"", t0.""DelivrdQty"", t0.""UomCode"", t0.""PackQty"", t0.""DiscPrcnt"", t2.""Usage"", t0.""TaxCode"", t0.""CFOPCode"", t0.""CSTCode"", t0.""LinePoPrss"", t0.""Price"", t0.""LineTotal"", COALESCE(t0.""U_Lote"",'0') AS ""U_Lote"", COALESCE(t0.""U_Comprimento2"",'0') AS ""U_Comprimento2"", COALESCE(t0.""U_Pecas"", '0') AS ""U_Pecas"", COALESCE(t0.""U_Metros"", '0') AS ""U_Metros"", COALESCE(t0.""U_Norma"",'0') AS ""U_Norma"", COALESCE(t0.""U_Peso"",'0') AS ""U_Peso"", COALESCE(t1.""U_ComprFixo"", '0') AS ""DescricaoAuxiliar"", COALESCE(t0.""unitMsr"", '0') AS ""unitMsr"", COALESCE(t0.""U_SKILL_NP"", '0') AS ""U_SKILL_NP"", COALESCE(t0.""U_SKILL_IP"", '0') AS ""U_SKILL_IP"", t0.""ShipDate"" FROM QUT1 t0 INNER JOIN OITM t1 ON t0.""ItemCode"" = t1.""ItemCode"" LEFT JOIN OUSG t2 ON t0.""Usage"" = t2.""ID"" WHERE t0.""DocEntry"" = '{CotacaoItemDTO.DocEntry}' ORDER BY ""LineNum"" ASC";
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {

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
                string queryPadrao = "SELECT t0.DocEntry, t0.Dscription,t0.LineNum, t0.ItemCode, t0.Quantity, t0.DelivrdQty, t0.UomCode, t0.PackQty, t0.DiscPrcnt, t2.Usage, t0.TaxCode, t0.CFOPCode, t0.CSTCode, t0.LinePoPrss, t0.Price, t0.LineTotal,COALESCE(t0.U_Lote,'') AS 'U_Lote',COALESCE(t0.U_Comprimento2,0) AS 'U_Comprimento2',COALESCE(t0.U_Pecas,0) AS 'U_Pecas',COALESCE(t0.U_Metros,0) AS 'U_Metros',COALESCE(t0.U_Norma,'') AS 'U_Norma',COALESCE(t0.U_Peso,0) AS 'U_Peso',COALESCE(t1.U_ComprFixo,0) AS 'DescricaoAuxiliar',COALESCE(t0.unitMsr,'') AS 'unitMsr', COALESCE(t0.U_SKILL_NP,'') AS 'U_SKILL_NP', COALESCE(t0.U_SKILL_IP,'') AS 'U_SKILL_IP',t0.ShipDate FROM QUT1 t0 INNER JOIN OITM t1 ON t0.ItemCode = t1.ItemCode LEFT JOIN OUSG t2 ON t0.Usage = t2.ID ";

                SqlCommand cmd = new SqlCommand();

                StringBuilder stb = new StringBuilder();
                stb.Append(queryPadrao);
                stb.Append("WHERE t0.DocEntry = @DocEntry ");
                stb.Append("ORDER BY LineNum ASC");

                cmd.Parameters.AddWithValue("@DocEntry", CotacaoItemDTO.DocEntry);

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

        private IList<CotacaoItemDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<CotacaoItemDTO> listItens = new List<CotacaoItemDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    CotacaoItemDTO itemDTO = new CotacaoItemDTO();
                    itemDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    itemDTO.Dscription = rdr["Dscription"].ToString();
                    itemDTO.LineNum = Convert.ToInt32(rdr["LineNum"]) + 1;
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.Quantity = Convert.ToDouble(rdr["Quantity"]);
                    itemDTO.DiscPrcnt = Convert.ToDouble(rdr["DiscPrcnt"]);
                    itemDTO.UsageName = rdr["Usage"].ToString();
                    //itemDTO.Usage = Convert.ToInt32(rdr["Usage"]);
                    itemDTO.Price = Convert.ToDouble(rdr["Price"]);
                    itemDTO.LineTotal = Convert.ToInt32(rdr["LineTotal"]);
                    itemDTO.unitMsr = rdr["unitMsr"].ToString();
                    itemDTO.U_Peso = Convert.ToDouble(rdr["U_Peso"]);
                    itemDTO.Comprimento = Convert.ToDouble(rdr["U_Comprimento2"]);
                    itemDTO.QtdBarra = Convert.ToDouble(rdr["U_Pecas"]);
                    //itemDTO.


                    listItens.Add(itemDTO);
                }

                rdr.Close();
                rdr.Dispose();
            }

            return listItens;
        }

        private IList<CotacaoItemDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<CotacaoItemDTO> listItens = new List<CotacaoItemDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow rdr in dt.Rows)
                {
                    CotacaoItemDTO itemDTO = new CotacaoItemDTO();
                    itemDTO.DocEntry = Convert.ToInt32(rdr["DocEntry"]);
                    itemDTO.Dscription = rdr["Dscription"].ToString();
                    itemDTO.LineNum = Convert.ToInt32(rdr["LineNum"]) + 1;
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.Quantity = Convert.ToDouble(rdr["Quantity"]);
                    itemDTO.DiscPrcnt = Convert.ToDouble(rdr["DiscPrcnt"]);
                    itemDTO.UsageName = rdr["Usage"].ToString();
                    itemDTO.Price = Convert.ToDouble(rdr["Price"]);
                    itemDTO.LineTotal = Convert.ToInt32(rdr["LineTotal"]);
                    itemDTO.unitMsr = rdr["unitMsr"].ToString();
                    itemDTO.U_Peso = Convert.ToDouble(rdr["U_Peso"]);
                    itemDTO.Comprimento = Convert.ToDouble(rdr["U_Comprimento2"]);
                    itemDTO.QtdBarra = Convert.ToDouble(rdr["U_Pecas"]);

                    listItens.Add(itemDTO);
                }

            }
            else
            {
                return listItens;
            }

            return listItens;
        }
    }
}