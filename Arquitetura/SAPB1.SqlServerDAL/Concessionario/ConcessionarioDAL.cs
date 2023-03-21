using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Concessionario;
using SAPB1.IDAL.Concessionario;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SAPB1.SqlServerDAL.Concessionario
{
    public class ConcessionarioDAL : IConcessionario
    {
        private readonly SqlServerConexao _conexao;

        public ConcessionarioDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public ConcessionarioDTO RetornarDadosConcessionarioPorLogin(string usuario, string senha)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OCRD WHERE ""U_LOGIN_PORTAL"" = '{usuario}' AND ""U_SENHA_PORTAL"" = '{senha}'";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    ConcessionarioDTO concessionarioDTO = new ConcessionarioDTO();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            concessionarioDTO.CardCode = dr["CardCode"].ToString();
                            concessionarioDTO.CardName = dr["CardName"].ToString();
                            concessionarioDTO.City = dr["City"].ToString();
                            concessionarioDTO.State = dr["State1"].ToString();
                            concessionarioDTO.ListNum = Convert.ToInt32((dr["ListNum"].ToString().Trim().Equals("") ? "-2" : dr["ListNum"].ToString()));
                            concessionarioDTO.U_TabGarant = dr["U_TabGarant"].ToString();
                            concessionarioDTO.U_TabSuger = dr["U_TabSuger"].ToString();
                        }
                    }
                    return concessionarioDTO;
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
                stb.Append("SELECT * FROM OCRD (NOLOCK) WHERE U_LOGIN_PORTAL = @Usuario AND U_SENHA_PORTAL = @Senha");

                try
                {
                    _conexao.Conectar();

                    SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    ConcessionarioDTO concessionarioDTO = new ConcessionarioDTO();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            concessionarioDTO.CardCode = rdr["CardCode"].ToString();
                            concessionarioDTO.CardName = rdr["CardName"].ToString();
                            concessionarioDTO.City = rdr["City"].ToString();
                            concessionarioDTO.State = rdr["State1"].ToString();
                            concessionarioDTO.ListNum = Convert.ToInt32((rdr["ListNum"].ToString().Trim().Equals("") ? "-2" : rdr["ListNum"].ToString()));
                            concessionarioDTO.U_TabGarant = rdr["U_TabGarant"].ToString();
                            concessionarioDTO.U_TabSuger = rdr["U_TabSuger"].ToString();
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                    cmd.Dispose();

                    return concessionarioDTO;
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

        public ConcessionarioDTO ObterConcessionarioPorId(string cardCode)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    string query = $@"SELECT * FROM OCRD WHERE ""CardCode"" = '{cardCode}'";
                    ConcessionarioDTO concessionarioDTO = new ConcessionarioDTO();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            concessionarioDTO.CardCode = dr["CardCode"].ToString();
                            concessionarioDTO.CardName = dr["CardName"].ToString();
                            concessionarioDTO.City = dr["City"].ToString();
                            concessionarioDTO.State = dr["State1"].ToString();
                            concessionarioDTO.ListNum = Convert.ToInt32((dr["ListNum"].ToString().Trim().Equals("") ? "-2" : dr["ListNum"].ToString()));
                            concessionarioDTO.U_Tsystem = dr["U_Tsystem"].ToString();
                            concessionarioDTO.U_TabGarant = dr["U_TabGarant"].ToString();
                            concessionarioDTO.U_TabSuger = dr["U_TabSuger"].ToString();
                        }
                    }
                    return concessionarioDTO;
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
                stb.Append("SELECT * FROM OCRD (NOLOCK) WHERE CardCode = @CardCode");

                try
                {
                    _conexao.Conectar();

                    SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
                    cmd.Parameters.AddWithValue("@CardCode", cardCode);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    ConcessionarioDTO concessionarioDTO = new ConcessionarioDTO();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            concessionarioDTO.CardCode = rdr["CardCode"].ToString();
                            concessionarioDTO.CardName = rdr["CardName"].ToString();
                            concessionarioDTO.City = rdr["City"].ToString();
                            concessionarioDTO.State = rdr["State1"].ToString();
                            concessionarioDTO.ListNum = Convert.ToInt32((rdr["ListNum"].ToString().Trim().Equals("") ? "-2" : rdr["ListNum"].ToString()));
                            concessionarioDTO.U_Tsystem = rdr["U_Tsystem"].ToString();
                            concessionarioDTO.U_TabGarant = rdr["U_TabGarant"].ToString();
                            concessionarioDTO.U_TabSuger = rdr["U_TabSuger"].ToString();
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                    cmd.Dispose();

                    return concessionarioDTO;
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

        public IList<ConcessionarioDTO> ObterTodos()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                string query = $@"SELECT * FROM OCRD ORDER BY ""CardName""";
                try
                {
                    conexaoHana.Connection();

                    IList<ConcessionarioDTO> listConcessionario = new List<ConcessionarioDTO>();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listConcessionario.Add(new ConcessionarioDTO()
                            {
                                CardCode = dr["CardCode"].ToString(),
                                CardName = dr["CardName"].ToString()
                            });
                        }
                    }

                    return listConcessionario;
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


                StringBuilder stb = new StringBuilder();
                stb.Append("SELECT * FROM OCRD (NOLOCK) ORDER BY CardName");

                try
                {
                    _conexao.Conectar();

                    SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    IList<ConcessionarioDTO> listConcessionario = new List<ConcessionarioDTO>();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            listConcessionario.Add(new ConcessionarioDTO()
                            {
                                CardCode = rdr["CardCode"].ToString(),
                                CardName = rdr["CardName"].ToString()
                            });
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                    cmd.Dispose();

                    return listConcessionario;
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

        public IList<ConcessionarioDTO> ObterConcessionarioPorGrupoCliente(string groupCode)
        {

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT * FROM OCRD WHERE ""GroupCode"" = '{groupCode}' ORDER BY ""CardName""";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    IList<ConcessionarioDTO> listConcessionario = new List<ConcessionarioDTO>();

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listConcessionario.Add(new ConcessionarioDTO()
                            {
                                CardCode = dr["CardCode"].ToString(),
                                CardName = dr["CardFName"].ToString()
                            });
                        }
                    }
                    return listConcessionario;
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
                stb.Append("SELECT * FROM OCRD (NOLOCK) WHERE GroupCode = @GroupCode ORDER BY CardName");

                try
                {
                    _conexao.Conectar();

                    SqlCommand cmd = new SqlCommand(stb.ToString(), _conexao.Conexao);
                    cmd.Parameters.AddWithValue("@GroupCode", groupCode);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    IList<ConcessionarioDTO> listConcessionario = new List<ConcessionarioDTO>();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            listConcessionario.Add(new ConcessionarioDTO()
                            {
                                CardCode = rdr["CardCode"].ToString(),
                                CardName = rdr["CardFName"].ToString()
                            });
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                    cmd.Dispose();

                    return listConcessionario;
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
}
