using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.NotaFiscal;
using SAPB1.DTO.NotaFiscal;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.NotaFiscal
{
    public class NotaFiscalItemDAL : INotaFiscalItem
    {
        SqlServerConexao _conexao;

        public NotaFiscalItemDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public IList<NotaFiscalItemDTO> ObterNotasFiscaisPorPedidoVenda(string codPedido)
        {
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

                List<NotaFiscalItemDTO> listNotas = new List<NotaFiscalItemDTO>();

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

                return listNotas;
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
    }
}
