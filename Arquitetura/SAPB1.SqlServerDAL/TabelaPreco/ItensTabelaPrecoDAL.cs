using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.TabelaPreco;
using SAPB1.IDAL.ItensTabelaPreco;
using SAPB1.DTO.Item;
using System.Data;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.ItensTabelaPreco
{
    public class ItensTabelaPrecoDAL:IItensTabelaPreco
    {
        SqlServerConexao conexao = new SqlServerConexao();

        /// <summary>
        /// Retorna um a lista genérica da classe ItensTabelaPrecoDTo
        /// </summary>
        /// <param name="itensTabelaPrecoDTO">Classe ItensTabelaPrecoDTO</param>
        /// <returns>Lista genérica da classe ItensTabelaPrecoDTO</returns>
        public IList<ItensTabelaPrecoDTO> Listar(ItensTabelaPrecoDTO itensTabelaPrecoDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("tp.ListName, ");
            stb.Append("p.ItemName, ");
            stb.Append("i.PriceList, ");
            stb.Append("i.ItemCode, ");
            stb.Append("i.Currency, ");
            stb.Append("i.Price, COALESCE(t1.WhsName,'') AS 'WhsName' ");
            stb.Append("FROM ITM1 i ");
            stb.Append("INNER JOIN OPLN tp ON tp.ListNum = i.PriceList ");
            stb.Append("INNER JOIN OITM p ON p.ItemCode = i.ItemCode ");

            if (itensTabelaPrecoDTO.Item !=null)
            {
                stb.Append("AND ");

                if(!string.IsNullOrEmpty(itensTabelaPrecoDTO.Item.ItemCode))
                {
                    stb.Append("p.ItemCode LIKE @ItemCode ");
                    cmd.Parameters.AddWithValue("@ItemCode", "%" + itensTabelaPrecoDTO.Item.ItemCode + "%");

                    if(!string.IsNullOrEmpty(itensTabelaPrecoDTO.Item.ItemName))
                    {
                        stb.Append("AND ");
                    }
                }

                if(!string.IsNullOrEmpty(itensTabelaPrecoDTO.Item.ItemName))
                {
                    stb.Append("p.ItemName LIKE @ItemName ");
                    cmd.Parameters.AddWithValue("@ItemName", "%" + itensTabelaPrecoDTO.Item.ItemName + "%");
                }
            }

            stb.Append("LEFT JOIN OWHS t1 ON p.DfltWH = t1.WhsCode ");

            if (itensTabelaPrecoDTO.TabelaPreco != null)
            {
                if (itensTabelaPrecoDTO.TabelaPreco.ListNum != 0)
                {
                    stb.Append("WHERE ");

                    stb.Append("i.PriceList = @PriceList ");
                    cmd.Parameters.AddWithValue("@PriceList", itensTabelaPrecoDTO.TabelaPreco.ListNum);
                }
            }

            stb.Append("ORDER BY i.ItemCode");

            cmd.CommandText = stb.ToString();
            cmd.Connection = conexao.Conexao;

            try
            {
                conexao.Conectar();

                return PopularDados(ref cmd);
            }
            catch(SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }

        /// <summary>
        /// Popula os dados em uma lista genérica da classe ItensTabelaPrecoDTO
        /// </summary>
        /// <param name="cmd">classe SQLCommand</param>
        /// <returns>Lista genérica da classe ItensTabelaPrecoDTO</returns>
        private IList<ItensTabelaPrecoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader rdr = cmd.ExecuteReader();

            IList<ItensTabelaPrecoDTO> listItens = new List<ItensTabelaPrecoDTO>();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                    tabelaPrecoDTO.ListNum = Convert.ToInt32(rdr["PriceList"].ToString());
                    tabelaPrecoDTO.ListName = rdr["ListName"].ToString();

                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemCode = rdr["ItemCode"].ToString();
                    itemDTO.ItemName = rdr["ItemName"].ToString();
                    itemDTO.WareHouseName = rdr["WhsName"].ToString();

                    ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                    itensTabelaPrecoDTO.TabelaPreco = tabelaPrecoDTO;
                    itensTabelaPrecoDTO.Item = itemDTO;
                    itensTabelaPrecoDTO.Currency = rdr["Currency"].ToString();
                    itensTabelaPrecoDTO.Price = Convert.ToDouble((rdr["Price"].ToString().Equals("") ? "0" : rdr["Price"].ToString()));

                    listItens.Add(itensTabelaPrecoDTO);
                }
            }

            rdr.Close();
            rdr.Dispose();
            cmd.Dispose();

            return listItens;
        }

        public IList<ItensTabelaPrecoDTO> ListarItensDeMaisDeUmaTabelapreco(List<string> codTabelas)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("tp.ListName, ");
            stb.Append("p.ItemName, ");
            stb.Append("i.PriceList, ");
            stb.Append("i.ItemCode, ");
            stb.Append("i.Currency, ");
            stb.Append("i.Price, ");
            stb.Append("n.NcmCode ");
            stb.Append("FROM ITM1 i (NOLOCK) ");
            stb.Append("INNER JOIN OPLN tp (NOLOCK) ON tp.ListNum = i.PriceList ");
            stb.Append("INNER JOIN OITM p (NOLOCK) ON p.ItemCode = i.ItemCode ");
            stb.Append("LEFT JOIN ONCM n (NOLOCK) ON n.AbsEntry = p.NCMCode ");
            stb.Append("WHERE i.Price > 0 AND ");

            stb.Append("(");
            for (int i = 0; i < codTabelas.Count; i++)
            {
                stb.Append("tp.ListNum = @Tabela" + i + " ");
                cmd.Parameters.AddWithValue("@Tabela" + i, codTabelas[i]);

                if (i < (codTabelas.Count - 1))
                    stb.Append("OR ");
            }

            stb.Append(") ");

            stb.Append("ORDER BY i.ItemCode");

            cmd.CommandText = stb.ToString();
            cmd.Connection = conexao.Conexao;

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ItensTabelaPrecoDTO> listItens = new List<ItensTabelaPrecoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                        itensTabelaPrecoDTO.Lista = Convert.ToInt32(rdr["PriceList"].ToString());
                        itensTabelaPrecoDTO.CodigoItem = rdr["ItemCode"].ToString();
                        itensTabelaPrecoDTO.NomeItem = rdr["ItemName"].ToString();
                        itensTabelaPrecoDTO.Currency = rdr["Currency"].ToString();
                        itensTabelaPrecoDTO.Price = Convert.ToDouble((rdr["Price"].ToString().Equals("") ? "0" : rdr["Price"].ToString()));
                        itensTabelaPrecoDTO.NcmCode = rdr["NcmCode"].ToString();

                        listItens.Add(itensTabelaPrecoDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();
                cmd.Dispose();

                return listItens;
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }

        public IList<ItensTabelaPrecoDTO> BuscarItensDeMaisDeUmaTabelapreco(List<string> codTabelas, ItensTabelaPrecoDTO itensDTO)
        {
            SqlCommand cmd = new SqlCommand();

            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("tp.ListName, ");
            stb.Append("p.ItemName, ");
            stb.Append("i.PriceList, ");
            stb.Append("i.ItemCode, ");
            stb.Append("i.Currency, ");
            stb.Append("i.Price, ");
            stb.Append("n.NcmCode ");
            stb.Append("FROM ITM1 i (NOLOCK) ");
            stb.Append("INNER JOIN OPLN tp (NOLOCK) ON tp.ListNum = i.PriceList ");
            stb.Append("INNER JOIN OITM p (NOLOCK) ON p.ItemCode = i.ItemCode ");
            stb.Append("LEFT JOIN ONCM n (NOLOCK) ON n.AbsEntry = p.NCMCode ");
            stb.Append("WHERE  i.Price > 0 AND (");

            for (int i = 0; i < codTabelas.Count; i++)
            {
                stb.Append("tp.ListNum = @Tabela" + i + " ");
                cmd.Parameters.AddWithValue("@Tabela" + i, codTabelas[i]);

                if (i < (codTabelas.Count - 1))
                    stb.Append("OR ");
            }

            stb.Append(") ");

            if (!string.IsNullOrEmpty(itensDTO.Item.ItemCode))
            {
                stb.Append("AND p.ItemCode LIKE @ItemCode ");
                cmd.Parameters.AddWithValue("@ItemCode", "%" + itensDTO.Item.ItemCode + "%");
            }

            if (!string.IsNullOrEmpty(itensDTO.Item.ItemName))
            {
                stb.Append("AND p.ItemName LIKE @ItemName ");
                cmd.Parameters.AddWithValue("@ItemName", "%" + itensDTO.Item.ItemName + "%");
            }

            stb.Append("ORDER BY i.ItemCode");

            cmd.CommandText = stb.ToString();
            cmd.Connection = conexao.Conexao;

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ItensTabelaPrecoDTO> listItens = new List<ItensTabelaPrecoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                        itensTabelaPrecoDTO.Lista = Convert.ToInt32(rdr["PriceList"].ToString());
                        itensTabelaPrecoDTO.CodigoItem = rdr["ItemCode"].ToString();
                        itensTabelaPrecoDTO.NomeItem = rdr["ItemName"].ToString();
                        itensTabelaPrecoDTO.Currency = rdr["Currency"].ToString();
                        itensTabelaPrecoDTO.Price = Convert.ToDouble((rdr["Price"].ToString().Equals("") ? "0" : rdr["Price"].ToString()));
                        itensTabelaPrecoDTO.NcmCode = rdr["NcmCode"].ToString();

                        listItens.Add(itensTabelaPrecoDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();
                cmd.Dispose();

                return listItens;
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }

        public IList<ItensTabelaPrecoDTO> ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(string codTabela)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ");
            stb.Append("tp.ListName, ");
            stb.Append("p.ItemName, ");
            stb.Append("i.PriceList, ");
            stb.Append("i.ItemCode, ");
            stb.Append("i.Currency, ");
            stb.Append("i.Price, ");
            stb.Append("n.NcmCode ");
            stb.Append("FROM ITM1 i (NOLOCK) ");
            stb.Append("INNER JOIN OPLN tp (NOLOCK) ON tp.ListNum = i.PriceList ");
            stb.Append("INNER JOIN OITM p (NOLOCK) ON p.ItemCode = i.ItemCode ");
            stb.Append("LEFT JOIN ONCM n (NOLOCK) ON n.AbsEntry = p.NCMCode ");
            stb.Append("WHERE ");
            stb.Append("i.Price > 0 AND i.PriceList = @CodTabela");

            SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);
            cmd.Parameters.AddWithValue("@CodTabela", codTabela);

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ItensTabelaPrecoDTO> listItens = new List<ItensTabelaPrecoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                        itensTabelaPrecoDTO.Lista = Convert.ToInt32(rdr["PriceList"].ToString());
                        itensTabelaPrecoDTO.CodigoItem = rdr["ItemCode"].ToString();
                        itensTabelaPrecoDTO.NomeItem = rdr["ItemName"].ToString();
                        itensTabelaPrecoDTO.Currency = rdr["Currency"].ToString();
                        itensTabelaPrecoDTO.Price = Convert.ToDouble((rdr["Price"].ToString().Equals("") ? "0" : rdr["Price"].ToString()));
                        itensTabelaPrecoDTO.NcmCode = rdr["NcmCode"].ToString();

                        listItens.Add(itensTabelaPrecoDTO);
                    }
                }

                rdr.Close();
                rdr.Dispose();
                cmd.Dispose();

                return listItens;
            }
            catch (SqlException er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                cmd.Dispose();
                conexao.Desconectar();
            }
        }
    }
}
