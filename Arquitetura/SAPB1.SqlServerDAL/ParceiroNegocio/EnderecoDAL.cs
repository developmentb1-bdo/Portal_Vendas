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
    public sealed class EnderecoDAL : IEndereco
    {
        public EnderecoDAL() { }

        string tSQLBase = "SELECT Address, Street, Block, ZipCode, City, County, Country, State, Building, AdresType, AddrType, StreetNo, CardCode FROM CRD1 ";

        public IList<EnderecoDTO> Listar(string cardCode)
        {
            IList<EnderecoDTO> listEnderecoDTO = new List<EnderecoDTO>();
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                string query = $@"SELECT ""Address"", ""Street"", ""Block"", ""ZipCode"", ""City"", ""County"", ""Country"", ""State"", ""Building"", ""AdresType"", ""AddrType"", ""StreetNo"", ""CardCode"" FROM CRD1 WHERE ""CardCode"" = '{cardCode}'";
                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(query);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            EnderecoDTO enderecoDTO = new EnderecoDTO();
                            enderecoDTO = ObterEnderecoHanaDTO(dr);
                            listEnderecoDTO.Add(enderecoDTO);
                        }
                    }
                    return listEnderecoDTO;
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
                        EnderecoDTO enderecoDTO = new EnderecoDTO();
                        enderecoDTO = ObterEnderecoDTO(dataReader);

                        listEnderecoDTO.Add(enderecoDTO);
                    }
                    dataReader.Close();

                    return listEnderecoDTO;
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

        private EnderecoDTO ObterEnderecoDTO(SqlDataReader dataReader)
        {
            EnderecoDTO enderecoDTO = new EnderecoDTO();

            if (dataReader.HasRows)
            {
                enderecoDTO.Address = ((!dataReader["Address"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Address"]) : string.Empty);
                enderecoDTO.Street = ((!dataReader["Street"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Street"]) : string.Empty);
                enderecoDTO.Block = ((!dataReader["Block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Block"]) : string.Empty);
                enderecoDTO.ZipCode = ((!dataReader["ZipCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ZipCode"]) : string.Empty);
                enderecoDTO.City = ((!dataReader["City"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["City"]) : string.Empty);
                enderecoDTO.County = ((!dataReader["County"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["County"]) : string.Empty);
                enderecoDTO.Country = ((!dataReader["Country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Country"]) : string.Empty);
                enderecoDTO.State = ((!dataReader["State"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["State"]) : string.Empty);
                enderecoDTO.Building = ((!dataReader["Building"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Building"]) : string.Empty);
                enderecoDTO.AdresType = ((!dataReader["AdresType"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["AdresType"]) : char.MinValue);
                enderecoDTO.AddrType = ((!dataReader["AddrType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddrType"]) : string.Empty);
                enderecoDTO.StreetNo = ((!dataReader["StreetNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["StreetNo"]) : string.Empty);
                enderecoDTO.CardCode = ((!dataReader["CardCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CardCode"]) : string.Empty);
            }
            return enderecoDTO;
        }

        private EnderecoDTO ObterEnderecoHanaDTO(DataRow dr)
        {
            EnderecoDTO enderecoDTO = new EnderecoDTO();

         
                enderecoDTO.Address = ((!dr["Address"].Equals(DBNull.Value)) ? Convert.ToString(dr["Address"]) : string.Empty);
                enderecoDTO.Street = ((!dr["Street"].Equals(DBNull.Value)) ? Convert.ToString(dr["Street"]) : string.Empty);
                enderecoDTO.Block = ((!dr["Block"].Equals(DBNull.Value)) ? Convert.ToString(dr["Block"]) : string.Empty);
                enderecoDTO.ZipCode = ((!dr["ZipCode"].Equals(DBNull.Value)) ? Convert.ToString(dr["ZipCode"]) : string.Empty);
                enderecoDTO.City = ((!dr["City"].Equals(DBNull.Value)) ? Convert.ToString(dr["City"]) : string.Empty);
                enderecoDTO.County = ((!dr["County"].Equals(DBNull.Value)) ? Convert.ToString(dr["County"]) : string.Empty);
                enderecoDTO.Country = ((!dr["Country"].Equals(DBNull.Value)) ? Convert.ToString(dr["Country"]) : string.Empty);
                enderecoDTO.State = ((!dr["State"].Equals(DBNull.Value)) ? Convert.ToString(dr["State"]) : string.Empty);
                enderecoDTO.Building = ((!dr["Building"].Equals(DBNull.Value)) ? Convert.ToString(dr["Building"]) : string.Empty);
                enderecoDTO.AdresType = ((!dr["AdresType"].Equals(DBNull.Value)) ? Convert.ToChar(dr["AdresType"]) : char.MinValue);
                enderecoDTO.AddrType = ((!dr["AddrType"].Equals(DBNull.Value)) ? Convert.ToString(dr["AddrType"]) : string.Empty);
                enderecoDTO.StreetNo = ((!dr["StreetNo"].Equals(DBNull.Value)) ? Convert.ToString(dr["StreetNo"]) : string.Empty);
                enderecoDTO.CardCode = ((!dr["CardCode"].Equals(DBNull.Value)) ? Convert.ToString(dr["CardCode"]) : string.Empty);
 
            return enderecoDTO;
        }
    }
}