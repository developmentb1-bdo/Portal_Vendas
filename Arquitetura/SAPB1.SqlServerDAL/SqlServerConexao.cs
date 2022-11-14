/*
 * @author Victor Oliveira.
 */

using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL
{
    internal class SqlServerConexao
    {
        internal SqlServerConexao() { }

        SqlConnection conexao = new SqlConnection(/*Criptografia.Decriptar(*/ConfigurationManager.ConnectionStrings["SqlServerConexao"].ToString()/*, "UE9846MB")*/);

        internal SqlConnection Conexao
        {
            get
            {
                return conexao;
            }
        }

        internal void Conectar()
        {
            try
            {
                if (Conexao.State == 0)
                    Conexao.Open();
                else
                    Conexao.Close();
            }
            catch (SqlException erro)
            {
                throw new Exception("Erro ao conectar com o banco de dados SQL Server!\n" + erro.Message);
            }
        }

        internal void Desconectar()
        {
            try
            {
                if (Conexao.State != 0)
                    Conexao.Close();
            }
            catch (SqlException erro)
            {
                throw new Exception("Erro ao desconectar com o banco de dados SQL Server!\n" + erro.Message);
            }
        }
    }
}