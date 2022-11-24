using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.EstruturaItem;
using SAPB1.IDAL.EstruturaItem;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.EstruturaItem
{
    public class EstruturaItemDAL : IEstruturaItem
    {
        public IList<EstruturaItemDTO> ObterTodasItensEstrutura()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                string query = $@"SELECT ITEMPAI.""Code"" AS CODIGO, ITM.""ItemName"" AS ITEM, ITM.""SWeight1"" AS PESO, ITM.""BuyUnitMsr"" AS ""UNIDADE DE MEDIDA"", ITM.""LeadTime"" AS ""LEAD TIME"", 'P' AS TIPOITEM, '' AS ITEMPAI FROM OITT AS ITEMPAI INNER JOIN OITM AS ITM ON ITM.""ItemCode"" = ITEMPAI.""Code"" UNION ALL(SELECT ITM.""ItemCode"" AS CODIGO, ITM.""ItemName"" AS ITEM, ITM.""SWeight1"" AS PESO, ITM.""BuyUnitMsr"" AS ""UNIDADE DE MEDIDA"", ITM.""LeadTime"" AS ""LEAD TIME"", 'C' AS TIPOITEM, '0' AS ITEMPAI FROM OITM AS ITM WHERE ITM.""ItemCode"" NOT IN (SELECT ""Code"" FROM OITT) AND ITM.""validFor"" = 'Y') ORDER BY CODIGO";
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();
                    return PopularDadosHana(query);

                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de dados: " + err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT ");
                stb.Append("ITEMPAI.Code AS 'CODIGO', ");
                stb.Append("ITM.ItemName AS 'ITEM', ");
                stb.Append("ITM.SWeight1 AS 'PESO', ");
                stb.Append("ITM.BuyUnitMsr AS 'UNIDADE DE MEDIDA', ");
                stb.Append("ITM.LeadTime AS 'LEAD TIME', ");
                stb.Append("'P' AS 'TIPOITEM', ");
                stb.Append("'' AS 'ITEMPAI' ");
                stb.Append("FROM OITT ITEMPAI ");
                stb.Append("INNER JOIN OITM ITM ON ITM.ItemCode = ITEMPAI.Code ");
                stb.Append("UNION ALL ");
                stb.Append("( ");
                stb.Append("SELECT ");
                stb.Append("ITM.ItemCode AS 'CODIGO', ");
                stb.Append("ITM.ItemName AS 'ITEM', ");
                stb.Append("ITM.SWeight1 AS 'PESO', ");
                stb.Append("ITM.BuyUnitMsr AS 'UNIDADE DE MEDIDA', ");
                stb.Append("ITM.LeadTime AS 'LEAD TIME', ");
                stb.Append("'C' AS 'TIPOITEM', ");
                stb.Append("'0' AS 'ITEMPAI' ");
                stb.Append("FROM OITM ITM ");
                //stb.Append("WHERE ITM.ItemCode NOT IN (SELECT Code FROM OITT) AND (ITM.BuyUnitMsr IS NOT NULL AND ITM.BuyUnitMsr <> '') AND ITM.validFor = 'Y'");
                stb.Append("WHERE ITM.ItemCode NOT IN (SELECT Code FROM OITT) AND ITM.validFor = 'Y'");
                stb.Append(") ");
                stb.Append("ORDER BY ITEMPAI.Code");

                SqlServerConexao conexao = new SqlServerConexao();
                SqlCommand cmd = new SqlCommand();

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

        private IList<EstruturaItemDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<EstruturaItemDTO> listItemEstrutura = new List<EstruturaItemDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    EstruturaItemDTO estruturaDTO = new EstruturaItemDTO();
                    estruturaDTO.Codigo = rdr["CODIGO"].ToString();
                    estruturaDTO.Descricao = rdr["ITEM"].ToString();
                    estruturaDTO.Peso = Convert.ToDecimal((rdr["PESO"].ToString().Equals("") ? "0" : rdr["PESO"].ToString()));
                    estruturaDTO.UnidadeMedida = rdr["UNIDADE DE MEDIDA"].ToString();
                    estruturaDTO.TipoItem = rdr["TIPOITEM"].ToString();
                    estruturaDTO.LeadTime = Convert.ToDecimal(rdr["LEAD TIME"].ToString().Equals("") ? "0" : rdr["LEAD TIME"].ToString());
                    estruturaDTO.UnidadeMedidaPeso = "KG";
                    estruturaDTO.ItemFantasma = "N";

                    listItemEstrutura.Add(estruturaDTO);
                }
            }

            rdr.Close();

            return listItemEstrutura;
        }

        private IList<EstruturaItemDTO> PopularDadosHana(string query)
        {
            HanaConexao conexaoHana = new HanaConexao();

            IList<EstruturaItemDTO> listItemEstrutura = new List<EstruturaItemDTO>();

            DataTable dt = conexaoHana.ExecuteDataTable(query);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EstruturaItemDTO estruturaDTO = new EstruturaItemDTO();
                    estruturaDTO.Codigo = dr["CODIGO"].ToString();
                    estruturaDTO.Descricao = dr["ITEM"].ToString();
                    estruturaDTO.Peso = Convert.ToDecimal((dr["PESO"].ToString().Equals("") ? "0" : dr["PESO"].ToString()));
                    estruturaDTO.UnidadeMedida = dr["UNIDADE DE MEDIDA"].ToString();
                    estruturaDTO.TipoItem = dr["TIPOITEM"].ToString();
                    estruturaDTO.LeadTime = Convert.ToDecimal(dr["LEAD TIME"].ToString().Equals("") ? "0" : dr["LEAD TIME"].ToString());
                    estruturaDTO.UnidadeMedidaPeso = "KG";
                    estruturaDTO.ItemFantasma = "N";

                    listItemEstrutura.Add(estruturaDTO);
                }
            }

            return listItemEstrutura;
        }

        public IList<EstruturaItemDTO> ObterItensEstruturasProdutos()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ITEMFILHO.""Code"" AS CODIGO, ITM.""ItemName"" AS ITEM, ITEMFILHO.""Quantity"", ITEMFILHO.""Father"" AS ITEMPAI, ITM.""validFrom"", ITM.""validTo"" FROM OITT AS ITEMPAI INNER JOIN ITT1 AS ITEMFILHO ON ITEMFILHO.""Father"" = ITEMPAI.""Code"" INNER JOIN OITM AS ITM ON ITM.""ItemCode"" = ITEMFILHO.""Code""";
                try
                {
                    conexaoHana.Connection();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    IList<EstruturaItemDTO> listProdutos = new List<EstruturaItemDTO>();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            EstruturaItemDTO estruturaDTO = new EstruturaItemDTO();
                            estruturaDTO.Codigo = dr["CODIGO"].ToString();
                            estruturaDTO.Descricao = dr["ITEM"].ToString();
                            estruturaDTO.Quantity = Convert.ToDecimal((dr["Quantity"].ToString().Equals("") ? "0" : dr["Quantity"].ToString()));
                            estruturaDTO.CodigoPai = dr["ITEMPAI"].ToString();
                            estruturaDTO.DataValidadeInicial = (dr["validFrom"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(dr["validFrom"]));
                            estruturaDTO.DataValidadeFinal = (dr["validTo"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(dr["validTo"]));

                            listProdutos.Add(estruturaDTO);
                        }
                    }

                    return listProdutos;
                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de dados: " + err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT ");
                stb.Append("ITEMFILHO.Code AS 'CODIGO', ");
                stb.Append("ITM.ItemName AS 'ITEM', ");
                stb.Append("ITEMFILHO.Quantity, ");
                stb.Append("ITEMFILHO.Father AS 'ITEMPAI', ");
                stb.Append("ITM.validFrom, ");
                stb.Append("ITM.validTo ");
                stb.Append("FROM OITT ITEMPAI ");
                stb.Append("INNER JOIN ITT1 ITEMFILHO ON ITEMFILHO.Father = ITEMPAI.Code ");
                stb.Append("INNER JOIN OITM ITM ON ITM.ItemCode = ITEMFILHO.Code ");

                SqlServerConexao conexao = new SqlServerConexao();
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    IList<EstruturaItemDTO> listProdutos = new List<EstruturaItemDTO>();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            EstruturaItemDTO estruturaDTO = new EstruturaItemDTO();
                            estruturaDTO.Codigo = rdr["CODIGO"].ToString();
                            estruturaDTO.Descricao = rdr["ITEM"].ToString();
                            estruturaDTO.Quantity = Convert.ToDecimal((rdr["Quantity"].ToString().Equals("") ? "0" : rdr["Quantity"].ToString()));
                            estruturaDTO.CodigoPai = rdr["ITEMPAI"].ToString();
                            estruturaDTO.DataValidadeInicial = (rdr["validFrom"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["validFrom"]));
                            estruturaDTO.DataValidadeFinal = (rdr["validTo"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["validTo"]));

                            listProdutos.Add(estruturaDTO);
                        }
                    }

                    return listProdutos;
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

        public IList<EstruturaItemDTO> ObterItensEstruturasProdutoPai()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ITEMPAI.""Code"" AS CODIGO FROM OITT AS ITEMPAI";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    IList<EstruturaItemDTO> list = new List<EstruturaItemDTO>();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new EstruturaItemDTO()
                            {
                                Codigo = dr["CODIGO"].ToString()
                            });
                        }
                    }

                    return list;
                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de dados: " + err.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT ");
                stb.Append("ITEMPAI.Code AS 'CODIGO' ");
                stb.Append("FROM OITT ITEMPAI ");

                SqlServerConexao conexao = new SqlServerConexao();
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    IList<EstruturaItemDTO> list = new List<EstruturaItemDTO>();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            list.Add(new EstruturaItemDTO()
                            {
                                Codigo = rdr["CODIGO"].ToString()
                            });
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

                    return list;
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
