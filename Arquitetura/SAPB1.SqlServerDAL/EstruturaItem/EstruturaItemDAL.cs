using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.EstruturaItem;
using SAPB1.IDAL.EstruturaItem;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.EstruturaItem
{
    public class EstruturaItemDAL : IEstruturaItem
    {
        public IList<EstruturaItemDTO> ObterTodasItensEstrutura()
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

        public IList<EstruturaItemDTO> ObterItensEstruturasProdutos()
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

                if(rdr.HasRows)
                {
                    while(rdr.Read())
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

        public IList<EstruturaItemDTO> ObterItensEstruturasProdutoPai()
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

                if(rdr.HasRows)
                {
                    while(rdr.Read())
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
