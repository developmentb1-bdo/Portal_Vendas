using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using System.Data.SqlClient;
using SAPB1.IDAL.Servico;

namespace SAPB1.SqlServerDAL.Servico
{
    public class ChassiAntigoDAL : IChassiAntigo
    {
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ChassiAntigoDTO> ObterTodosChassi()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [@RSD_CHASSIOLD]");

            try
            {
                SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                IList<ChassiAntigoDTO> listChassi = new List<ChassiAntigoDTO>();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listChassi.Add(new ChassiAntigoDTO()
                        {
                            U_Ano = rdr["U_Ano"].ToString(),
                            U_ArrDate = (rdr["U_ArrDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_ArrDate"])),
                            U_Chassi = rdr["U_Chassi"].ToString(),
                            U_China = rdr["U_China"].ToString(),
                            U_Cliente = rdr["U_Cliente"].ToString(),
                            U_DataVenda = (rdr["U_DataVenda"].ToString().Equals("") ? DateTime.MinValue.ToString("dd/MM/yyyy") : Convert.ToDateTime(rdr["U_DataVenda"]).ToString("dd/MM/yyyy")),
                            U_Dealer = rdr["U_Dealer"].ToString(),
                            U_FinGaran = (rdr["U_FinGaran"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_FinGaran"])),
                            U_InspEntr = rdr["U_InspEntr"].ToString(),
                            U_Modelo = rdr["U_Modelo"].ToString(),
                            U_Motor = rdr["U_Motor"].ToString(),
                            U_EntreEixos = rdr["U_EntreEixos"].ToString(),
                            U_ModeloMotor = rdr["U_ModeloMotor"].ToString()
                        });
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return listChassi;
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

        public ChassiAntigoDTO ObterDadosPeloChassi(string chassi)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM [@RSD_CHASSIOLD] WHERE U_Chassi = @Chassi");

            try
            {
                SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);
                cmd.Parameters.AddWithValue("@Chassi", chassi);

                conexao.Conectar();

                SqlDataReader rdr = cmd.ExecuteReader();

                ChassiAntigoDTO dados = new ChassiAntigoDTO();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        dados.U_Ano = rdr["U_Ano"].ToString();
                        dados.U_ArrDate = (rdr["U_ArrDate"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_ArrDate"]));
                        dados.U_Chassi = rdr["U_Chassi"].ToString();
                        dados.U_China = rdr["U_China"].ToString();
                        dados.U_Cliente = rdr["U_Cliente"].ToString();
                        dados.U_DataVenda = (rdr["U_DataVenda"].ToString().Equals("") ? DateTime.MinValue.ToString("dd/MM/yyyy") : Convert.ToDateTime(rdr["U_DataVenda"]).ToString("dd/MM/yyyy"));
                        dados.U_Dealer = rdr["U_Dealer"].ToString();
                        dados.U_FinGaran = (rdr["U_FinGaran"].ToString().Equals("") ? DateTime.MinValue : Convert.ToDateTime(rdr["U_FinGaran"]));
                        dados.U_InspEntr = rdr["U_InspEntr"].ToString();
                        dados.U_Modelo = rdr["U_Modelo"].ToString();
                        dados.U_Motor = rdr["U_Motor"].ToString();
                        dados.U_EntreEixos = rdr["U_EntreEixos"].ToString();
                        dados.U_ModeloMotor = rdr["U_ModeloMotor"].ToString();
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return dados;
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
