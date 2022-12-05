using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.NotaFiscal;
using SAPB1.DTO.NotaFiscal;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.NotaFiscal
{
    public class NotaFiscalItemDAL : INotaFiscalItem
    {

        public IList<NotaFiscalItemDTO> ObterNotasFiscaisPorPedidoVenda(string codPedido)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            List<NotaFiscalItemDTO> listNotas = new List<NotaFiscalItemDTO>();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT DISTINCT nfi.""DocEntry"", nfi.""BaseRef"", nf.""Serial"" FROM INV1 nfi INNER JOIN OINV nf ON nf.""DocEntry"" = nfi.""DocEntry"" WHERE nfi.""BaseRef"" = '{codPedido}'";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow rdr in dt.Rows)
                        {
                            listNotas.Add(new NotaFiscalItemDTO()
                            {
                                DocEntry = rdr["DocEntry"].ToString(),
                                BaseRef = rdr["BaseRef"].ToString(),
                                Serial = rdr["Serial"].ToString()
                            });

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
                SqlServerConexao _conexao = new SqlServerConexao();
                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT DISTINCT nfi.DocEntry, nfi.BaseRef, nf.Serial FROM INV1 nfi (NOLOCK) ");
                stb.Append("INNER JOIN OINV nf ON nf.DocEntry = nfi.DocEntry ");
                stb.Append("WHERE nfi.BaseRef = @CodigoPedido");

                SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
                cmd.Parameters.AddWithValue("@CodigoPedido", codPedido);

                try
                {
                    _conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            listNotas.Add(new NotaFiscalItemDTO()
                            {
                                DocEntry = rdr["DocEntry"].ToString(),
                                BaseRef = rdr["BaseRef"].ToString(),
                                Serial = rdr["Serial"].ToString()
                            });
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    _conexao.Desconectar();
                }
            }
            return listNotas;
        }
    }
}
