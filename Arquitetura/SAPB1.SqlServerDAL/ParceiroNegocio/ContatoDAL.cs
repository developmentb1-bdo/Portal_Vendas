/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.SqlServerDAL.ParceiroNegocio
{
    public sealed class ContatoDAL : IContato
    {
        public ContatoDAL() { }



        public IList<ContatoDTO> Listar(string cardCode)
        {
            IList<ContatoDTO> listContatoDTO = new List<ContatoDTO>();
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""CardCode"", ""Name"", ""E_MailL"", ""Tel1"", ""Notes1"" FROM OCPR WHERE ""CardCode"" = '{cardCode}'";

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ContatoDTO contatoDTO = new ContatoDTO();
                            contatoDTO = ObterContatoHanaDTO(dr);

                            listContatoDTO.Add(contatoDTO);
                        }
                    }

                    return listContatoDTO;
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
                string tSQLBase = "SELECT CardCode, Name, E_MailL, Tel1, Notes1 FROM OCPR ";
                SqlServerConexao conexao = new SqlServerConexao();
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

        private ContatoDTO ObterContatoHanaDTO(DataRow dr)
        {
            ContatoDTO contatoDTO = new ContatoDTO();

            contatoDTO.CardCode = ((!dr["CardCode"].Equals(DBNull.Value)) ? Convert.ToString(dr["CardCode"]) : string.Empty);
            contatoDTO.Name = ((!dr["Name"].Equals(DBNull.Value)) ? Convert.ToString(dr["Name"]) : string.Empty);
            contatoDTO.E_MailL = ((!dr["E_MailL"].Equals(DBNull.Value)) ? Convert.ToString(dr["E_MailL"]) : string.Empty);
            contatoDTO.Tel1 = ((!dr["Tel1"].Equals(DBNull.Value)) ? Convert.ToString(dr["Tel1"]) : string.Empty);
            contatoDTO.Notes1 = ((!dr["Notes1"].Equals(DBNull.Value)) ? Convert.ToString(dr["Notes1"]) : string.Empty);

            return contatoDTO;
        }
    }
}