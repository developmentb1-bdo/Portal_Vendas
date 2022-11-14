using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Concessionario;
using SAPB1.IDAL.Concessionario;
using System.Data.SqlClient;

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
            catch(Exception er)
            {
                throw new Exception("Erro no banco de dados: " + er.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public ConcessionarioDTO ObterConcessionarioPorId(string cardCode)
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

        public IList<ConcessionarioDTO> ObterTodos()
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

        public IList<ConcessionarioDTO> ObterConcessionarioPorGrupoCliente(string groupCode)
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
