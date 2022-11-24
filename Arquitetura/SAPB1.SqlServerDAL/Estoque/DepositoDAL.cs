/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SAPB1.DTO.Deposito;
using SAPB1.IDAL.Estoque;

namespace SAPB1.SqlServerDAL.Estoque
{
    public sealed class DepositoDAL : IDeposito
    {
        public DepositoDAL() { }

        string tSQLBase = @"SELECT WhsCode, WhsName FROM OWHS;";

        public IList<DepositoDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<DepositoDTO> listDepositoDTO = new List<DepositoDTO>();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""WhsCode"", ""WhsName"" FROM OWHS";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            DepositoDTO depositoDTO = new DepositoDTO();
                            depositoDTO = ObterDepositoHanaDTO(dr);

                            listDepositoDTO.Add(depositoDTO);
                        }
                    }

                    return listDepositoDTO;
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
                SqlServerConexao conexao = new SqlServerConexao();
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

        DepositoDTO ObterDepositoHanaDTO(DataRow dr)
        {
            DepositoDTO depositoDTO = new DepositoDTO();
            depositoDTO.WhsCode = Convert.ToString(dr["WhsCode"]);
            depositoDTO.WhsName = Convert.ToString(dr["WhsName"]);

            return depositoDTO;
        }
    }
}