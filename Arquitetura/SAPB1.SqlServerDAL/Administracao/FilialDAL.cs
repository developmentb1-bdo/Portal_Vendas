/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SAPB1.DTO.Administracao;
using SAPB1.IDAL.Administracao;

namespace SAPB1.SqlServerDAL.Administracao
{
    public sealed class FilialDAL : IFilial
    {
        public FilialDAL() { }

        string tSQLBase = @"SELECT BPLId, BPLName, [Disabled] FROM OBPL WHERE [Disabled] = 'N';";

        public IList<FilialDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<FilialDTO> listFilialDTO = new List<FilialDTO>();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""BPLId"", ""BPLName"", ""Disabled"" FROM OBPL WHERE ""Disabled"" = 'N';";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            FilialDTO filialDTO = new FilialDTO();
                            filialDTO = ObterFiliaHanalDTO(dr);
                            listFilialDTO.Add(filialDTO);
                        }
                    }
                    return listFilialDTO;
                }
                catch (Exception err)
                {
                    throw new Exception("Erro no banco de Dados: " + err.Message);
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
                        FilialDTO filialDTO = new FilialDTO();
                        filialDTO = ObterFilialDTO(dataReader);

                        listFilialDTO.Add(filialDTO);
                    }
                    dataReader.Close();

                    return listFilialDTO;
                }
                catch (Exception erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }

        }

        private FilialDTO ObterFilialDTO(SqlDataReader dataReader)
        {
            FilialDTO filialDTO = new FilialDTO();

            if (dataReader.HasRows)
            {
                filialDTO.BPLId = Convert.ToInt32(dataReader["BPLId"]);
                filialDTO.BPLName = Convert.ToString(dataReader["BPLName"]);
                filialDTO.Disabled = Convert.ToChar(dataReader["Disabled"]);
            }
            return filialDTO;
        }

        private FilialDTO ObterFiliaHanalDTO(DataRow dr)
        {
            FilialDTO filialDTO = new FilialDTO();

            filialDTO.BPLId = Convert.ToInt32(dr["BPLId"]);
            filialDTO.BPLName = Convert.ToString(dr["BPLName"]);
            filialDTO.Disabled = Convert.ToChar(dr["Disabled"]);

            return filialDTO;
        }
    }
}