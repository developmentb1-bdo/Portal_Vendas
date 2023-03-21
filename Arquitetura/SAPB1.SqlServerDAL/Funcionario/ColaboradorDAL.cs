using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Funcionario;
using SAPB1.IDAL.Funcionario;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Funcionario
{
    public class ColaboradorDAL : IColaborador
    {
        public ColaboradorDTO SelecionarColaboradorPorId(int empId)
        {
            string query = $@"SELECT * FROM OHEM WHERE ""empID"" = '{empId}'";
            ColaboradorDTO colaborador = new ColaboradorDTO();

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    var retornoQuery = conexaoHana.ExecuteDataTable(query);

                    foreach (DataRow dr in retornoQuery.Rows)
                    {
                        if (dr["empID"] != DBNull.Value)
                        {
                            colaborador.EmpId = Convert.ToInt32(dr["empID"]);
                            colaborador.FirstName = dr["firstName"].ToString();
                            colaborador.LastName = dr["lastName"].ToString();
                            colaborador.MiddleName = dr["middleName"].ToString();

                            if (dr["position"] != DBNull.Value)
                                colaborador.Position = Convert.ToInt32(dr["position"]);

                            colaborador.SalesPrson = Convert.ToInt32((dr["salesPrson"].ToString().Equals("") ? "0" : dr["salesPrson"]));
                            colaborador.U_AcessoPortal = ((!DBNull.Value.Equals(dr["U_AcessoPortal"])) ? dr["U_AcessoPortal"].ToString() : "");
                        }
                    }

                    return colaborador;

                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }

            }
            else
            {

                SqlCommand cmd = new SqlCommand();
                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    cmd.CommandText = query;

                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            colaborador.EmpId = Convert.ToInt32(rdr["empID"]);
                            colaborador.FirstName = rdr["firstName"].ToString();
                            colaborador.LastName = rdr["lastName"].ToString();
                            colaborador.MiddleName = rdr["middleName"].ToString();
                            colaborador.Position = Convert.ToInt32(rdr["position"]);
                            colaborador.SalesPrson = Convert.ToInt32((rdr["salesPrson"].ToString().Equals("") ? "0" : rdr["salesPrson"]));
                            colaborador.U_AcessoPortal = ((!DBNull.Value.Equals(rdr["U_AcessoPortal"])) ? rdr["U_AcessoPortal"].ToString() : "");
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

                    return colaborador;
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

        public ColaboradorDTO SelecionarColaboradorPorUsuarioESenha(string usuario, string senha)
        {
            ColaboradorDTO colaborador = new ColaboradorDTO();
            //Tipo do banco de dados utilizado 
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string query = $@"SELECT * FROM OHEM WHERE ""U_usuario"" = '{usuario}' AND ""U_senha"" = '{senha}'";

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    var retornoQuery = conexaoHana.ExecuteDataTable(query);

                    foreach (DataRow dr in retornoQuery.Rows)
                    {
                        if (dr["empID"] != DBNull.Value)
                        {
                            colaborador.EmpId = Convert.ToInt32(dr["empID"]);
                            colaborador.FirstName = dr["firstName"].ToString();
                            colaborador.LastName = dr["lastName"].ToString();
                            colaborador.MiddleName = dr["middleName"].ToString();

                            if (dr["position"] != DBNull.Value)
                                colaborador.Position = Convert.ToInt32(dr["position"]);

                            colaborador.SalesPrson = Convert.ToInt32((dr["salesPrson"].ToString().Equals("") ? "0" : dr["salesPrson"]));
                            colaborador.U_AcessoPortal = ((!DBNull.Value.Equals(dr["U_AcessoPortal"])) ? dr["U_AcessoPortal"].ToString() : "");
                        }
                    }
                    return colaborador;
                }
                catch (Exception er)
                {
                    throw new Exception("Erro no banco de dados: " + er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                SqlServerConexao conexao = new SqlServerConexao();

                try
                {
                    cmd.CommandText = query;
                    cmd.Connection = conexao.Conexao;

                    conexao.Conectar();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            colaborador.EmpId = Convert.ToInt32(rdr["empID"]);
                            colaborador.FirstName = rdr["firstName"].ToString();
                            colaborador.LastName = rdr["lastName"].ToString();
                            colaborador.MiddleName = rdr["middleName"].ToString();
                            colaborador.Position = Convert.ToInt32(rdr["position"]);
                            colaborador.SalesPrson = Convert.ToInt32((rdr["salesPrson"].ToString().Equals("") ? "0" : rdr["salesPrson"]));
                            colaborador.U_AcessoPortal = ((!DBNull.Value.Equals(rdr["U_AcessoPortal"])) ? rdr["U_AcessoPortal"].ToString() : "");
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();

                    return colaborador;
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
}
