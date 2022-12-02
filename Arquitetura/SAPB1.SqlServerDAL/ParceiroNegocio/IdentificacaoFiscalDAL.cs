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
    public sealed class IdentificacaoFiscalDAL : IIdentificacaoFiscal
    {
        public IdentificacaoFiscalDAL() { }


        public IList<IdentificacaoFiscalDTO> Listar(string cardCode)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string tSQLBase = $@"SELECT ""CardCode"", ""Address"", ""TaxId0"", ""TaxId1"", ""TaxId2"", ""TaxId3"", ""TaxId4"", ""TaxId5"", ""TaxId6"", ""TaxId7"", ""TaxId8"", ""TaxId9"", ""TaxId10"", ""TaxId11"", ""TaxId12"", ""TaxId13"", ""CNAEId"", ""AddrType"", ""ECCNo"", ""CERegNo"", ""CERange"", ""CEDivis"", ""CEComRate"", ""LogInstanc"", ""SefazDate"" FROM CRD7 ";
            IList<IdentificacaoFiscalDTO> listIdentificacaoFiscalDTO = new List<IdentificacaoFiscalDTO>();
            if (tipoBD == "Hana")
            {

                tSQLBase += $@"WHERE ""CardCode"" = '{cardCode}'";
                HanaConexao conexaoHana = new HanaConexao();
                DataTable dt = conexaoHana.ExecuteDataTable(tSQLBase);

                try
                {
                    conexaoHana.Connection();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            IdentificacaoFiscalDTO identificacaoFiscalDTO = new IdentificacaoFiscalDTO();
                            identificacaoFiscalDTO = ObterIdentificacaoFiscalHanaDTO(dr, conexaoHana);

                            listIdentificacaoFiscalDTO.Add(identificacaoFiscalDTO);
                        }
                    }
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
                        IdentificacaoFiscalDTO identificacaoFiscalDTO = new IdentificacaoFiscalDTO();
                        identificacaoFiscalDTO = ObterIdentificacaoFiscalDTO(dataReader);

                        listIdentificacaoFiscalDTO.Add(identificacaoFiscalDTO);
                    }
                    dataReader.Close();

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
            return listIdentificacaoFiscalDTO;
        }

        private IdentificacaoFiscalDTO ObterIdentificacaoFiscalDTO(SqlDataReader dataReader)
        {
            IdentificacaoFiscalDTO identificacaoFiscalDTO = new IdentificacaoFiscalDTO();

            if (dataReader.HasRows)
            {
                identificacaoFiscalDTO.CardCode = ((!dataReader["CardCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CardCode"]) : string.Empty);
                identificacaoFiscalDTO.Address = ((!dataReader["Address"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Address"]) : string.Empty);
                identificacaoFiscalDTO.TaxId0 = ((!dataReader["TaxId0"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId0"]) : string.Empty);
                identificacaoFiscalDTO.TaxId1 = ((!dataReader["TaxId1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId1"]) : string.Empty);
                identificacaoFiscalDTO.TaxId2 = ((!dataReader["TaxId2"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId2"]) : string.Empty);
                identificacaoFiscalDTO.TaxId3 = ((!dataReader["TaxId3"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId3"]) : string.Empty);
                identificacaoFiscalDTO.TaxId4 = ((!dataReader["TaxId4"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId4"]) : string.Empty);
                identificacaoFiscalDTO.TaxId5 = ((!dataReader["TaxId5"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId5"]) : string.Empty);
                identificacaoFiscalDTO.TaxId6 = ((!dataReader["TaxId6"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId6"]) : string.Empty);
                identificacaoFiscalDTO.TaxId7 = ((!dataReader["TaxId7"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId7"]) : string.Empty);
                identificacaoFiscalDTO.TaxId8 = ((!dataReader["TaxId8"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId8"]) : string.Empty);
                identificacaoFiscalDTO.TaxId9 = ((!dataReader["TaxId9"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId9"]) : string.Empty);
                identificacaoFiscalDTO.TaxId10 = ((!dataReader["TaxId10"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId10"]) : string.Empty);
                identificacaoFiscalDTO.TaxId11 = ((!dataReader["TaxId11"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId11"]) : string.Empty);
                identificacaoFiscalDTO.TaxId12 = ((!dataReader["TaxId12"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId12"]) : string.Empty);
                identificacaoFiscalDTO.TaxId13 = ((!dataReader["TaxId13"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["TaxId13"]) : string.Empty);
                identificacaoFiscalDTO.CNAEId = ((!dataReader["CNAEId"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["CNAEId"]) : 0);
                identificacaoFiscalDTO.AddrType = ((!dataReader["AddrType"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["AddrType"]) : char.MinValue);
                identificacaoFiscalDTO.ECCNo = ((!dataReader["ECCNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ECCNo"]) : string.Empty);
                identificacaoFiscalDTO.CERegNo = ((!dataReader["CERegNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CERegNo"]) : string.Empty);
                identificacaoFiscalDTO.CERange = ((!dataReader["CERange"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CERange"]) : string.Empty);
                identificacaoFiscalDTO.CEDivis = ((!dataReader["CEDivis"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CEDivis"]) : string.Empty);
                identificacaoFiscalDTO.CEComRate = ((!dataReader["CEComRate"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CEComRate"]) : string.Empty);
                identificacaoFiscalDTO.LogInstanc = ((!dataReader["LogInstanc"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["LogInstanc"]) : 0);
                identificacaoFiscalDTO.SefazDate = ((!dataReader["SefazDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["SefazDate"]) : DateTime.MinValue);
            }
            return identificacaoFiscalDTO;
        }

        private IdentificacaoFiscalDTO ObterIdentificacaoFiscalHanaDTO(DataRow dr, HanaConexao conexaoHana)
        {
            IdentificacaoFiscalDTO identificacaoFiscalDTO = new IdentificacaoFiscalDTO();


            identificacaoFiscalDTO.CardCode = ((!dr["CardCode"].Equals(DBNull.Value)) ? Convert.ToString(dr["CardCode"]) : string.Empty);
            identificacaoFiscalDTO.Address = ((!dr["Address"].Equals(DBNull.Value)) ? Convert.ToString(dr["Address"]) : string.Empty);
            identificacaoFiscalDTO.TaxId0 = ((!dr["TaxId0"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId0"]) : string.Empty);
            identificacaoFiscalDTO.TaxId1 = ((!dr["TaxId1"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId1"]) : string.Empty);
            identificacaoFiscalDTO.TaxId2 = ((!dr["TaxId2"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId2"]) : string.Empty);
            identificacaoFiscalDTO.TaxId3 = ((!dr["TaxId3"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId3"]) : string.Empty);
            identificacaoFiscalDTO.TaxId4 = ((!dr["TaxId4"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId4"]) : string.Empty);
            identificacaoFiscalDTO.TaxId5 = ((!dr["TaxId5"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId5"]) : string.Empty);
            identificacaoFiscalDTO.TaxId6 = ((!dr["TaxId6"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId6"]) : string.Empty);
            identificacaoFiscalDTO.TaxId7 = ((!dr["TaxId7"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId7"]) : string.Empty);
            identificacaoFiscalDTO.TaxId8 = ((!dr["TaxId8"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId8"]) : string.Empty);
            identificacaoFiscalDTO.TaxId9 = ((!dr["TaxId9"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId9"]) : string.Empty);
            identificacaoFiscalDTO.TaxId10 = ((!dr["TaxId10"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId10"]) : string.Empty);
            identificacaoFiscalDTO.TaxId11 = ((!dr["TaxId11"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId11"]) : string.Empty);
            identificacaoFiscalDTO.TaxId12 = ((!dr["TaxId12"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId12"]) : string.Empty);
            identificacaoFiscalDTO.TaxId13 = ((!dr["TaxId13"].Equals(DBNull.Value)) ? Convert.ToString(dr["TaxId13"]) : string.Empty);
            identificacaoFiscalDTO.CNAEId = ((!dr["CNAEId"].Equals(DBNull.Value)) ? Convert.ToInt32(dr["CNAEId"]) : 0);
            identificacaoFiscalDTO.AddrType = ((!dr["AddrType"].Equals(DBNull.Value)) ? Convert.ToChar(dr["AddrType"]) : char.MinValue);
            identificacaoFiscalDTO.ECCNo = ((!dr["ECCNo"].Equals(DBNull.Value)) ? Convert.ToString(dr["ECCNo"]) : string.Empty);
            identificacaoFiscalDTO.CERegNo = ((!dr["CERegNo"].Equals(DBNull.Value)) ? Convert.ToString(dr["CERegNo"]) : string.Empty);
            identificacaoFiscalDTO.CERange = ((!dr["CERange"].Equals(DBNull.Value)) ? Convert.ToString(dr["CERange"]) : string.Empty);
            identificacaoFiscalDTO.CEDivis = ((!dr["CEDivis"].Equals(DBNull.Value)) ? Convert.ToString(dr["CEDivis"]) : string.Empty);
            identificacaoFiscalDTO.CEComRate = ((!dr["CEComRate"].Equals(DBNull.Value)) ? Convert.ToString(dr["CEComRate"]) : string.Empty);
            identificacaoFiscalDTO.LogInstanc = ((!dr["LogInstanc"].Equals(DBNull.Value)) ? Convert.ToInt32(dr["LogInstanc"]) : 0);
            identificacaoFiscalDTO.SefazDate = ((!dr["SefazDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dr["SefazDate"]) : DateTime.MinValue);

            return identificacaoFiscalDTO;
        }
    }
}