/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.SqlServerDAL.ParceiroNegocio
{
    public sealed class ContatoDAL : IContato
    {
        public ContatoDAL() { }

        string tSQLBase = "SELECT CardCode, Name, E_MailL, Tel1, Notes1 FROM OCPR ";
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<ContatoDTO> Listar(string cardCode)
        {
            IList<ContatoDTO> listContatoDTO = new List<ContatoDTO>();

            try
            {
                StringBuilder tSQL = new StringBuilder();
                tSQL.Append(tSQLBase);
                tSQL.Append("WHERE CardCode = @CardCode");

                conexao.Conectar();

                SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                comando.Parameters.Add(new SqlParameter("@CardCode", cardCode));
                SqlDataReader dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    ContatoDTO contatoDTO = new ContatoDTO();
                    contatoDTO = ObterContatoDTO(dataReader);

                    listContatoDTO.Add(contatoDTO);
                }
                dataReader.Close();

                return listContatoDTO;
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

        private ContatoDTO ObterContatoDTO(SqlDataReader dataReader)
        {
            ContatoDTO contatoDTO = new ContatoDTO();

            if (dataReader.HasRows)
            {
                contatoDTO.CardCode = ((!dataReader["CardCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CardCode"]) : string.Empty);
                contatoDTO.Name = ((!dataReader["Name"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Name"]) : string.Empty);
                contatoDTO.E_MailL = ((!dataReader["E_MailL"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["E_MailL"]) : string.Empty);
                contatoDTO.Tel1 = ((!dataReader["Tel1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Tel1"]) : string.Empty);
                contatoDTO.Notes1 = ((!dataReader["Notes1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Notes1"]) : string.Empty);
            }
            return contatoDTO;
        }
    }
}