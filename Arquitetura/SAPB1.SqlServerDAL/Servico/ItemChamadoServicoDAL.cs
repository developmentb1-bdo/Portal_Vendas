using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ItemChamadoServicoDAL : IItemChamadoServico
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ItemChamadoServicoDTO> ListarPorIdChamado(int callId)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [dbo].[@RSDITMCALL] WHERE U_CallID = @callId");

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@callId", callId);

            try
            {
                cmd.CommandText = stb.ToString();
                cmd.Connection = conexao.Conexao;

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ItemChamadoServicoDTO> listItens = new List<ItemChamadoServicoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        ItemChamadoServicoDTO item = new ItemChamadoServicoDTO();
                        item.U_CallID = (rdr["U_CallID"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["U_CallID"]));
                        item.Code = rdr["Code"].ToString();
                        item.U_dscription = rdr["U_dscription"].ToString();
                        item.U_ItemAlt = rdr["U_ItemAlt"].ToString();
                        item.U_LineNum = (rdr["U_LineNum"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["U_LineNum"]));
                        item.U_Quantity = (rdr["U_Quantity"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["U_Quantity"]));
                        item.U_Price = (rdr["U_Price"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["U_Price"]));

                        listItens.Add(item);
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listItens;
            }
            catch (SqlException er)
            {
                throw new Exception(er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }
    }
}
