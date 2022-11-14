/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SAPB1.DTO.Administracao;
using SAPB1.IDAL.Administracao;

namespace SAPB1.SqlServerDAL.Administracao
{
    public sealed class FilialDAL : IFilial
    {
        public FilialDAL() { }

        string tSQLBase = @"SELECT BPLId, BPLName, [Disabled] FROM OBPL WHERE [Disabled] = 'N';";
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<FilialDTO> Listar()
        {
            IList<FilialDTO> listFilialDTO = new List<FilialDTO>();

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
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconectar();
            }
            return listFilialDTO;
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
    }
}