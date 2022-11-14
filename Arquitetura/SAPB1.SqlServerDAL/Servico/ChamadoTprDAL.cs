using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.IDAL.Servico;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ChamadoTprDAL : IChamadoTpr
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ChamadoTprDTO> ObterTprPorChamado(int callId)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT ct.Code, ct.U_CallId, ct.U_CodTpr, tpr.U_U_ItmMan, ct.U_Qtd, ct.U_Total ");
            stb.Append("FROM [@@RSD_CALLTPR] ct ");
            stb.Append("INNER JOIN [@RSD_TPR] tpr ON tpr.U_U_Codigo = ct.U_CodTpr ");
            stb.Append("WHERE ct.U_CallId = @CallId");

            SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);
            cmd.Parameters.AddWithValue("@CallId", callId);

            try
            {
                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ChamadoTprDTO> listChamadoTpr = new List<ChamadoTprDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listChamadoTpr.Add(new ChamadoTprDTO()
                        {
                            Code = Convert.ToInt32(rdr["Code"]),
                            U_CallId = (rdr["U_CallId"].ToString().Equals("") ? 0 : Convert.ToInt32(rdr["U_CallId"])),
                            U_CodTpr = rdr["U_CodTpr"].ToString(),
                            U_ItmMan = rdr["U_U_ItmMan"].ToString(),
                            U_Qtd = (rdr["U_Qtd"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["U_Qtd"])),
                            U_Total = (rdr["U_Total"].ToString().Equals("") ? 0 : Convert.ToDecimal(rdr["U_Total"]))
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listChamadoTpr;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }

        public bool InserirChamadoTpr(ChamadoTprDTO chamadoTprDTO)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("INSERT [@@RSD_CALLTPR] (Code, U_CallId, U_CodTpr, U_Qtd, U_Total) ");
            stb.Append("VALUES(@Code, @U_CallId, @U_CodTpr, @U_Qtd, @U_Total)");

            SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);
            cmd.Parameters.AddWithValue("@Code", chamadoTprDTO.Code);
            cmd.Parameters.AddWithValue("@U_CallId", chamadoTprDTO.U_CallId);
            cmd.Parameters.AddWithValue("@U_CodTpr", chamadoTprDTO.U_CodTpr);
            cmd.Parameters.AddWithValue("@U_Qtd", chamadoTprDTO.U_Qtd);
            cmd.Parameters.AddWithValue("@U_Total", chamadoTprDTO.U_Total);

            try
            {
                conexao.Conectar();

                if (cmd.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }


    }
}
