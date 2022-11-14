using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Compras;
using System.Data.SqlClient;

namespace SAPB1.SqlServerDAL.Compras
{
    public class ComprasDAL:ICompra
    {
        public double RetornarValorCompras(DateTime dataInicial, DateTime dataFinal)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT SUM(DocTotal) FROM OPOR WHERE DocStatus = 'C'");

            SqlServerConexao conexao = new SqlServerConexao();

            try
            {
                conexao.Conectar();

                SqlCommand comando = new SqlCommand(stb.ToString(), conexao.Conexao);

                return Convert.ToDouble(comando.ExecuteScalar());
            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
        }
    }
}
