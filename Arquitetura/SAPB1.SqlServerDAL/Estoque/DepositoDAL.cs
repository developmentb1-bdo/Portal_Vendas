/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SAPB1.DTO.Deposito;
using SAPB1.IDAL.Estoque;

namespace SAPB1.SqlServerDAL.Estoque
{
    public sealed class DepositoDAL : IDeposito
    {
        public DepositoDAL() { }

        string tSQLBase = @"SELECT WhsCode, WhsName FROM OWHS;";
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<DepositoDTO> Listar()
        {
            IList<DepositoDTO> listDepositoDTO = new List<DepositoDTO>();

            try
            {
                conexao.Conectar();

                SqlCommand comando = new SqlCommand(tSQLBase, conexao.Conexao);
                SqlDataReader dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    DepositoDTO depositoDTO = new DepositoDTO();
                    depositoDTO = ObterDepositoDTO(dataReader);

                    listDepositoDTO.Add(depositoDTO);
                }
                dataReader.Close();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
            return listDepositoDTO;
        }

        DepositoDTO ObterDepositoDTO(SqlDataReader dataReader)
        {
            DepositoDTO depositoDTO = new DepositoDTO();

            if (dataReader.HasRows)
            {
                depositoDTO.WhsCode = Convert.ToString(dataReader["WhsCode"]);
                depositoDTO.WhsName = Convert.ToString(dataReader["WhsName"]);
            }
            return depositoDTO;
        }
    }
}