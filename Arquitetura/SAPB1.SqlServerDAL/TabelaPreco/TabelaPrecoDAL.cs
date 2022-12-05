using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.TabelaPreco;
using SAPB1.DTO.TabelaPreco;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SAPB1.SqlServerDAL.TabelaPreco
{
    public class TabelaPrecoDAL : ITabelaPreco
    {

        /// <summary>
        /// Lista todas as tabelas de preço
        /// </summary>
        /// <param name="tabelaPrecoDTO">Classe TabelaPrecoDTO</param>
        /// <returns>Lista genérica da classe TabelaPrecoDTo</returns>
        public IList<TabelaPrecoDTO> Listar(TabelaPrecoDTO tabelaPrecoDTO)
        {
            string sSql = $@"SELECT ""ListNum"", ""ListName"", ""GroupCode"" FROM OPLN ";

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                StringBuilder stb = new StringBuilder();
                stb.Append(sSql);

                if (tabelaPrecoDTO != null)
                {
                    if (!string.IsNullOrEmpty(tabelaPrecoDTO.ListName) || tabelaPrecoDTO.ListNum > 0)
                    {
                        stb.Append("WHERE ");

                        if (!string.IsNullOrEmpty(tabelaPrecoDTO.ListName))
                        {
                            stb.Append($@"""ListName"" LIKE '%{tabelaPrecoDTO.ListName}%' ");
                            if (tabelaPrecoDTO.ListNum > 0)
                            {
                                stb.Append("AND ");
                            }
                        }

                        if (tabelaPrecoDTO.ListNum > 0)
                        {
                            stb.Append($@"""ListNum"" = '{tabelaPrecoDTO.ListNum}' ");
                        }
                    }
                }

                stb.Append($@"ORDER BY ""ListNum""");

                try
                {
                    conexaoHana.Connection();
                    string query = stb.ToString();
                    return PopularDadosHana(query, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no Banco de dados: " + er.Message);
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
                    SqlCommand comando = new SqlCommand();
                    StringBuilder stb = new StringBuilder();
                    stb.Append(sSql);

                    if (tabelaPrecoDTO != null)
                    {
                        if (!string.IsNullOrEmpty(tabelaPrecoDTO.ListName) || tabelaPrecoDTO.ListNum > 0)
                        {
                            stb.Append("WHERE ");

                            if (!string.IsNullOrEmpty(tabelaPrecoDTO.ListName))
                            {
                                stb.Append("ListName LIKE @ListName ");
                                comando.Parameters.AddWithValue("@ListName", "%" + tabelaPrecoDTO.ListName + "%");

                                if (tabelaPrecoDTO.ListNum > 0)
                                {
                                    stb.Append("AND ");
                                }
                            }

                            if (tabelaPrecoDTO.ListNum > 0)
                            {
                                stb.Append("ListNum = @ListNum ");
                                comando.Parameters.AddWithValue("@ListNum", tabelaPrecoDTO.ListNum);
                            }
                        }
                    }

                    stb.Append("ORDER BY ListNum");

                    conexao.Conectar();
                    comando.CommandText = stb.ToString();
                    comando.Connection = conexao.Conexao;

                    return PopularDados(ref comando);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no Banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
        }

        /// <summary>
        /// Popula os dados da consulta em uma lista genérica
        /// </summary>
        /// <param name="cmd">Classe SQLCommand</param>
        /// <returns>Lista genérica da classe TabelaPrecoDTo</returns>
        private IList<TabelaPrecoDTO> PopularDados(ref SqlCommand cmd)
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            IList<TabelaPrecoDTO> listTabelaPreco = new List<TabelaPrecoDTO>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                    tabelaPrecoDTO.ListName = dataReader["ListName"].ToString();
                    tabelaPrecoDTO.ListNum = Convert.ToInt32(dataReader["ListNum"].ToString());
                    tabelaPrecoDTO.GroupCode = Convert.ToInt32(dataReader["GroupCode"].ToString());

                    listTabelaPreco.Add(tabelaPrecoDTO);
                }
            }

            dataReader.Close();
            dataReader.Dispose();
            cmd.Dispose();

            return listTabelaPreco;
        }

        private IList<TabelaPrecoDTO> PopularDadosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            IList<TabelaPrecoDTO> listTabelaPreco = new List<TabelaPrecoDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataReader in dt.Rows)
                {
                    TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                    tabelaPrecoDTO.ListName = dataReader["ListName"].ToString();
                    tabelaPrecoDTO.ListNum = Convert.ToInt32(dataReader["ListNum"].ToString());
                    tabelaPrecoDTO.GroupCode = Convert.ToInt32(dataReader["GroupCode"].ToString());

                    listTabelaPreco.Add(tabelaPrecoDTO);
                }
            }

            return listTabelaPreco;
        }

        public IList<TabelaPrecoDTO> ListarTabelaPrecoConcessionario(int idTabela)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""ListNum"", ""ListName"", ""GroupCode"" FROM OPLN WHERE ""ListNum"" = '{idTabela}'";

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
                SqlServerConexao conexao = new SqlServerConexao();
                try
                {
                    SqlCommand comando = new SqlCommand();
                    StringBuilder stb = new StringBuilder();
                    stb.Append("SELECT ListNum, ListName, GroupCode FROM OPLN (NOLOCK) ");
                    stb.Append("WHERE ListNum = @ListNum");

                    comando.Parameters.AddWithValue("@ListNum", idTabela);

                    conexao.Conectar();
                    comando.CommandText = stb.ToString();
                    comando.Connection = conexao.Conexao;

                    return PopularDados(ref comando);
                }
                catch (SqlException er)
                {
                    throw new Exception("Erro no Banco de dados: " + er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
        }
    }
}
