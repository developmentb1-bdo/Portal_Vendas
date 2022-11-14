using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Usuario;
using SAPB1.IDAL.Usuario;
using System.Data.SqlClient;
using System.Configuration;

namespace SAPB1.SqlServerDAL.Usuario
{
    public class UsuarioDAL : IUsuario
    {
        private SqlServerConexao _conexao;

        public UsuarioDAL()
        {
            _conexao = new SqlServerConexao();
        }

        public int RetornarCodigoUsuarioPorNomeUsuario(string usuario)
        {
           
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT COALESCE(USERID, 0) AS 'Codigo' FROM OUSR WHERE USER_CODE = @UserCode");

            try
            {
                _conexao.Conectar();

                SqlCommand comando = new SqlCommand(stb.ToString(), _conexao.Conexao);
                comando.Parameters.AddWithValue("@UserCode", usuario);

                return Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }

        public string RetornarCodigoVideoYoutubeDoUsuarioPortal(string usuario)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT U_LinkVideo FROM OUSR WHERE USER_CODE = @UserCode");

            try
            {
                _conexao.Conectar();

                SqlCommand comando = new SqlCommand(stb.ToString(), _conexao.Conexao);
                comando.Parameters.AddWithValue("@UserCode", usuario);

                SqlDataReader rdr = comando.ExecuteReader();

                string retorno = "";

                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        retorno = rdr["U_LinkVideo"].ToString();
                    }
                }

                rdr.Close();
                rdr.Dispose();

                return retorno;
            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                _conexao.Desconectar();
            }
        }
    }
}
