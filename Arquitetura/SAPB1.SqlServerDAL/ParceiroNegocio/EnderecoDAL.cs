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
    public sealed class EnderecoDAL : IEndereco
    {
        public EnderecoDAL() { }

        string tSQLBase = "SELECT Address, Street, Block, ZipCode, City, County, Country, State, Building, AdresType, AddrType, StreetNo, CardCode FROM CRD1 ";
        SqlServerConexao conexao = new SqlServerConexao();

        public IList<EnderecoDTO> Listar(string cardCode)
        {
            IList<EnderecoDTO> listEnderecoDTO = new List<EnderecoDTO>();

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
    }
}