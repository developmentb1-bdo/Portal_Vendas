
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.DeskPararelo.Estoque;
using SAPB1.IDAL.DeskPararelo.Estoque;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.DeskPararelo.Estoque
{
    public class EstoqueDadosDAL : IEstoqueDados
    {
        private readonly SqlServerConexao _conexao;

        public EstoqueDadosDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public IList<EstoqueDadosDTO> RetornarDadosEstoque()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT IT.""ItemCode"", IT.""ItemName"", (SELECT SUM(IFNULL(""OnHand"", 0)) FROM OITW AS DEP WHERE DEP.""ItemCode"" = IT.""ItemCode"" AND DEP.""WhsCode"" = '203') AS EstoqueTransito, (SELECT SUM(IFNULL(""OnHand"", 0)) FROM OITW AS DEP1 WHERE DEP1.""ItemCode"" = IT.""ItemCode"" AND DEP1.""WhsCode"" IN('201','202') ) AS Saldo, (SELECT SUM(IFNULL(""Quantity"", 0)) FROM INV1 AS NF1, OINV AS NF WHERE NF.""DocEntry"" = NF1.""DocEntry"" AND NF.""CANCELED"" <> 'Y' AND NF1.""ItemCode"" = IT.""ItemCode"") AS NFS, (SELECT SUM(IFNULL(""OnHand"", 0)) FROM OITW AS DEP1 WHERE DEP1.""ItemCode"" = IT.""ItemCode"" AND DEP1.""WhsCode"" IN('201','202', '203')) - ((SELECT SUM(IFNULL(""Quantity"", 0)) FROM INV1 AS NF1, OINV AS NF WHERE NF.""DocEntry"" = NF1.""DocEntry"" AND NF.""CANCELED"" <> 'Y' AND NF1.""ItemCode"" = IT.""ItemCode"")) AS EstoqueReal FROM OITM AS IT WHERE IT.""SellItem"" = 'Y'";

                try
                {
                    conexaoHana.Connection();
                    IList<EstoqueDadosDTO> list = new List<EstoqueDadosDTO>();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new EstoqueDadosDTO()
                            {
                                ItemCode = dr["ItemCode"].ToString(),
                                ItemName = dr["ItemName"].ToString(),
                                EstoqueTransito = (dr["EstoqueTransito"].ToString().Equals("") ? 0 : Convert.ToDecimal(dr["EstoqueTransito"])),
                                EstoqueReal = (dr["EstoqueReal"].ToString().Equals("") ? 0 : Convert.ToDecimal(dr["EstoqueReal"])),
                                NfsEmitidas = (dr["NFs"].ToString().Equals("") ? 0 : Convert.ToDecimal(dr["NFs"])),
                                SaldoEstoque = (dr["Saldo"].ToString().Equals("") ? 0 : Convert.ToDecimal(dr["Saldo"]))
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
                stb.Append("SELECT IT.ItemCode");
                stb.Append(", IT.ItemName");
                stb.Append(", (SELECT SUM(ISNULL(OnHand, 0))FROM OITW DEP WHERE DEP.ItemCode = IT.ItemCode AND DEP.WhsCode = '203' ) AS [EstoqueTransito]");
                stb.Append(", (SELECT SUM(ISNULL(OnHand, 0))FROM OITW DEP1 WHERE DEP1.ItemCode = IT.ItemCode AND DEP1.WhsCode IN('201','202') ) as [Saldo]");
                stb.Append(", (SELECT SUM(ISNULL(Quantity, 0))FROM INV1 NF1, OINV NF WHERE NF.DocEntry = nf1.DocEntry AND NF.CANCELED <> 'Y' AND NF1.ItemCode = IT.ItemCode) AS [Nfs]");
                stb.Append(", (SELECT SUM(ISNULL(OnHand, 0))FROM OITW DEP1 WHERE DEP1.ItemCode = IT.ItemCode AND DEP1.WhsCode IN('201','202', '203')) -(");
                stb.Append("(SELECT SUM(ISNULL(Quantity, 0))FROM INV1 NF1, OINV NF WHERE NF.DocEntry = nf1.DocEntry AND NF.CANCELED <> 'Y' AND NF1.ItemCode = IT.ItemCode)");
                stb.Append(")");
                stb.Append("AS [EstoqueReal] ");
                stb.Append("FROM OITM IT WHERE IT.SellItem = 'Y'");

                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = stb.ToString();
                    cmd.Connection = _conexao.Conexao;

                    _conexao.Conectar();

                    IList<EstoqueDadosDTO> list = new List<EstoqueDadosDTO>();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            list.Add(new EstoqueDadosDTO()
                            {
                                ItemCode = rdr["ItemCode"].ToString(),
                                ItemName = rdr["ItemName"].ToString(),
                                EstoqueTransito = (rdr["EstoqueTransito"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["EstoqueTransito"])),
                                EstoqueReal = (rdr["EstoqueReal"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["EstoqueReal"])),
                                NfsEmitidas = (rdr["NFs"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["NFs"])),
                                SaldoEstoque = (rdr["Saldo"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["Saldo"]))
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
                    _conexao.Desconectar();
                    cmd.Dispose();
                }
            }
        }
    }
}
