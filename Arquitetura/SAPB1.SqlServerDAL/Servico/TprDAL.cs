using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Servico
{
    public class TprDAL : ITpr
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public List<TprDTO> ObterTodos()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [@RSD_TPR]");

            try
            {
                SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                List<TprDTO> listTpr = new List<TprDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listTpr.Add(new TprDTO()
                        {
                            U_AumLev = rdr["U_U_AumLev"].ToString(),
                            U_Codigo = rdr["U_U_Codigo"].ToString(),
                            U_CompFal = rdr["U_U_CompFal"].ToString(),
                            U_ConSis = rdr["U_U_ConSis"].ToString(),
                            U_ForLev = rdr["U_U_ForLev"].ToString(),
                            U_ForLing = rdr["U_U_ForLing"].ToString(),
                            U_ItmMan = rdr["U_U_ItmMan"].ToString(),
                            U_OllLev = rdr["U_U_OilLev"].ToString(),
                            U_OlnExp = rdr["U_U_OlnExp"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listTpr;
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

        public TprDTO ObterDadosPorCodigo(string codigo)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [@RSD_TPR] WHERE U_U_Codigo = @Codigo");

            try
            {
                SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                TprDTO tprDados = new TprDTO();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        tprDados.U_AumLev = rdr["U_U_AumLev"].ToString();
                        tprDados.U_Codigo = rdr["U_U_Codigo"].ToString();
                        tprDados.U_CompFal = rdr["U_U_CompFal"].ToString();
                        tprDados.U_ConSis = rdr["U_U_ConSis"].ToString();
                        tprDados.U_ForLev = rdr["U_U_ForLev"].ToString();
                        tprDados.U_ForLing = rdr["U_U_ForLing"].ToString();
                        tprDados.U_ItmMan = rdr["U_U_ItmMan"].ToString();
                        tprDados.U_OllLev = rdr["U_U_OilLev"].ToString();
                        tprDados.U_OlnExp = rdr["U_U_OlnExp"].ToString();
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return tprDados;
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
