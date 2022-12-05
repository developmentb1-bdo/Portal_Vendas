using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Usuario;
using SAPB1.IDAL.Usuario;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Usuario
{
    public class UsuarioDAL : IUsuario
    {

        public int RetornarCodigoUsuarioPorNomeUsuario(string usuario)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT COALESCE(""USERID"", 0) AS ""Codigo"" FROM OUSR WHERE ""USER_CODE"" = '{usuario}'";

                try
                {
                    conexaoHana.Connection();
                    return Convert.ToInt32(conexaoHana.ExecuteScalar(query));
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
        }

        public string RetornarCodigoVideoYoutubeDoUsuarioPortal(string usuario)
        {
            string retorno = "";

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""U_LinkVideo"" FROM OUSR WHERE ""USER_CODE"" = '{usuario}'";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            retorno = dr["U_LinkVideo"].ToString();
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
                stb.Append("SELECT U_LinkVideo FROM OUSR WHERE USER_CODE = @UserCode");
                try
                {
                    _conexao.Conectar();

                    SqlCommand comando = new SqlCommand(stb.ToString(), _conexao.Conexao);
                    comando.Parameters.AddWithValue("@UserCode", usuario);

                    SqlDataReader rdr = comando.ExecuteReader();


                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
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
            return retorno;
        }
    }
}
