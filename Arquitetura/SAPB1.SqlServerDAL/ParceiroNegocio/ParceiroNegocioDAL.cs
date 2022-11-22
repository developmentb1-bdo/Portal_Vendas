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
    public sealed class ParceiroNegocioDAL : IParceiroNegocio
    {
        public ParceiroNegocioDAL() { }

        string tSQLBase = "SELECT * FROM OCRD ";
        HanaConexao conexaoHana = new HanaConexao();
        SqlServerConexao conexao = new SqlServerConexao();


        public IList<ParceiroNegocioDTO> ListarHana(string query)
        {
            IList<ParceiroNegocioDTO> listParceiroNegocioDTO = new List<ParceiroNegocioDTO>();
            HanaConexao conexaoHana = new HanaConexao();
            try
            {
                conexaoHana.Connection();
                var dataReader = conexaoHana.ExecuteDataTable(query);

                foreach (DataRow dr in dataReader.Rows)
                {
                    ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                    parceiroNegocioDTO = ObterParceiroNegocioHanaDTO(dr);

                    listParceiroNegocioDTO.Add(parceiroNegocioDTO);
                }

            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexaoHana.Dispose();
            }

            return listParceiroNegocioDTO;
        }

        public IList<ParceiroNegocioDTO> Listar()
        {
            // tSQLBase += $@"WHERE ""CardType"" <> 'S' ";
            // tSQLBase += $@"ORDER BY ""CardName""";
            string query = $@"SELECT * FROM OCRD WHERE ""CardType"" <> 'S' ORDER BY ""CardName""";
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {
                IList<ParceiroNegocioDTO> listParceiroNegocioDTO = new List<ParceiroNegocioDTO>();
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    conexaoHana.Connection();
                    var dataReader = conexaoHana.ExecuteDataTable(query);

                    foreach (DataRow dr in dataReader.Rows)
                    {
                        ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                        parceiroNegocioDTO = ObterParceiroNegocioHanaDTO(dr);

                        listParceiroNegocioDTO.Add(parceiroNegocioDTO);
                    }

                }
                catch (SqlException erro)
                {
                    throw new Exception(erro.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }

                return listParceiroNegocioDTO;
            }
            else
            {
                IList<ParceiroNegocioDTO> listParceiroNegocioDTO = new List<ParceiroNegocioDTO>();
                try
                {
                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(tSQLBase, conexao.Conexao);
                    SqlDataReader dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                        parceiroNegocioDTO = ObterParceiroNegocioDTO(dataReader);

                        listParceiroNegocioDTO.Add(parceiroNegocioDTO);
                    }
                    dataReader.Close();

                    return listParceiroNegocioDTO;
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

        public ParceiroNegocioDTO Selecionar(string cardCode)
        {
            ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();

            try
            {
                StringBuilder tSQL = new StringBuilder();
                tSQL.Append(tSQLBase);
                tSQL.Append("WHERE CardCode = @CardCode");

                conexao.Conectar();

                SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                comando.Parameters.Add(new SqlParameter("@CardCode", cardCode));
                SqlDataReader dataReader = comando.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    parceiroNegocioDTO = ObterParceiroNegocioDTO(dataReader);
                }
                dataReader.Close();

                return parceiroNegocioDTO;
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

        private ParceiroNegocioDTO ObterParceiroNegocioHanaDTO(DataRow dataReader)
        {
            ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();


            parceiroNegocioDTO.CardCode = Convert.ToString(dataReader["CardCode"]);
            parceiroNegocioDTO.CardName = Convert.ToString(dataReader["CardName"]);
            parceiroNegocioDTO.CardFName = Convert.ToString(dataReader["CardFName"]);
            parceiroNegocioDTO.CardType = Convert.ToString(dataReader["CardType"]);
            parceiroNegocioDTO.GroupCode = Convert.ToInt32(dataReader["GroupCode"]);
            parceiroNegocioDTO.CmpPrivate = Convert.ToChar(dataReader["CmpPrivate"]);
            parceiroNegocioDTO.Address = ((!dataReader["Address"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Address"]) : string.Empty);
            parceiroNegocioDTO.ZipCode = ((!dataReader["ZipCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ZipCode"]) : string.Empty);
            parceiroNegocioDTO.MailAddres = ((!dataReader["MailAddres"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailAddres"]) : string.Empty);
            parceiroNegocioDTO.MailZipCod = ((!dataReader["MailZipCod"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailZipCod"]) : string.Empty);
            parceiroNegocioDTO.Phone1 = ((!dataReader["Phone1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Phone1"]) : string.Empty);
            parceiroNegocioDTO.Phone2 = ((!dataReader["Phone2"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Phone2"]) : string.Empty);
            parceiroNegocioDTO.Fax = ((!dataReader["Fax"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Fax"]) : string.Empty);
            parceiroNegocioDTO.CntctPrsn = ((!dataReader["CntctPrsn"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CntctPrsn"]) : string.Empty);
            parceiroNegocioDTO.Notes = ((!dataReader["Notes"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Notes"]) : string.Empty);
            parceiroNegocioDTO.Balance = ((!dataReader["Balance"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["Balance"]) : decimal.Zero);
            parceiroNegocioDTO.ChecksBal = ((!dataReader["ChecksBal"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["ChecksBal"]) : decimal.Zero);
            parceiroNegocioDTO.DNotesBal = ((!dataReader["DNotesBal"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DNotesBal"]) : decimal.Zero);
            parceiroNegocioDTO.OrdersBal = ((!dataReader["OrdersBal"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["OrdersBal"]) : decimal.Zero);
            parceiroNegocioDTO.GroupNum = ((!dataReader["GroupNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["GroupNum"]) : 0);
            parceiroNegocioDTO.CreditLine = ((!dataReader["CreditLine"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["CreditLine"]) : decimal.Zero);
            parceiroNegocioDTO.DebtLine = ((!dataReader["DebtLine"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DebtLine"]) : decimal.Zero);
            parceiroNegocioDTO.Discount = ((!dataReader["Discount"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["Discount"]) : decimal.Zero);
            parceiroNegocioDTO.VatStatus = ((!dataReader["VatStatus"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["VatStatus"]) : char.MinValue);
            parceiroNegocioDTO.LicTradNum = ((!dataReader["LicTradNum"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["LicTradNum"]) : string.Empty);
            parceiroNegocioDTO.DdctStatus = ((!dataReader["DdctStatus"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DdctStatus"]) : char.MinValue);
            parceiroNegocioDTO.DdctPrcnt = ((!dataReader["DdctPrcnt"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DdctPrcnt"]) : decimal.Zero);
            parceiroNegocioDTO.ValidUntil = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
            parceiroNegocioDTO.Chrctrstcs = ((!dataReader["Chrctrstcs"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["Chrctrstcs"]) : 0);
            parceiroNegocioDTO.ExMatchNum = ((!dataReader["ExMatchNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["ExMatchNum"]) : 0);
            parceiroNegocioDTO.InMatchNum = ((!dataReader["InMatchNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["InMatchNum"]) : 0);
            parceiroNegocioDTO.ListNum = ((!dataReader["ListNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["ListNum"]) : 0);
            parceiroNegocioDTO.DNoteBalFC = ((!dataReader["DNoteBalFC"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DNoteBalFC"]) : decimal.Zero);
            parceiroNegocioDTO.OrderBalFC = ((!dataReader["OrderBalFC"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["OrderBalFC"]) : decimal.Zero);
            parceiroNegocioDTO.DNoteBalSy = ((!dataReader["DNoteBalSy"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DNoteBalSy"]) : decimal.Zero);
            parceiroNegocioDTO.OrderBalSy = ((!dataReader["OrderBalSy"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["OrderBalSy"]) : decimal.Zero);
            parceiroNegocioDTO.Transfered = ((!dataReader["Transfered"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Transfered"]) : char.MinValue);
            parceiroNegocioDTO.BalTrnsfrd = ((!dataReader["BalTrnsfrd"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["BalTrnsfrd"]) : char.MinValue);
            parceiroNegocioDTO.IntrstRate = ((!dataReader["IntrstRate"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["IntrstRate"]) : decimal.Zero);
            parceiroNegocioDTO.Commission = ((!dataReader["Commission"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["Commission"]) : decimal.Zero);
            parceiroNegocioDTO.CommGrCode = ((!dataReader["InMatchNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["InMatchNum"]) : 0);
            parceiroNegocioDTO.Free_Text = ((!dataReader["Free_Text"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Free_Text"]) : string.Empty);
            parceiroNegocioDTO.SlpCode = ((!dataReader["SlpCode"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SlpCode"]) : 0);
            parceiroNegocioDTO.PrevYearAc = ((!dataReader["PrevYearAc"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["PrevYearAc"]) : char.MinValue);
            parceiroNegocioDTO.Currency = ((!dataReader["Currency"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Currency"]) : string.Empty);
            parceiroNegocioDTO.RateDifAct = ((!dataReader["RateDifAct"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["RateDifAct"]) : string.Empty);
            parceiroNegocioDTO.BalanceSys = ((!dataReader["BalanceSys"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["BalanceSys"]) : decimal.Zero);
            parceiroNegocioDTO.BalanceFC = ((!dataReader["BalanceFC"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["BalanceFC"]) : decimal.Zero);
            parceiroNegocioDTO.Protected = ((!dataReader["Protected"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Protected"]) : char.MinValue);
            parceiroNegocioDTO.Cellular = ((!dataReader["Cellular"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Cellular"]) : string.Empty);
            parceiroNegocioDTO.AvrageLate = ((!dataReader["AvrageLate"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["AvrageLate"]) : 0);
            parceiroNegocioDTO.City = ((!dataReader["City"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["City"]) : string.Empty);
            parceiroNegocioDTO.County = ((!dataReader["County"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["County"]) : string.Empty);
            parceiroNegocioDTO.Country = ((!dataReader["Country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Country"]) : string.Empty);
            parceiroNegocioDTO.MailCity = ((!dataReader["MailCity"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailCity"]) : string.Empty);
            parceiroNegocioDTO.MailCounty = ((!dataReader["MailCounty"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailCounty"]) : string.Empty);
            parceiroNegocioDTO.MailCountr = ((!dataReader["MailCountr"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailCountr"]) : string.Empty);
            parceiroNegocioDTO.E_Mail = ((!dataReader["E_Mail"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["E_Mail"]) : string.Empty);
            parceiroNegocioDTO.Picture = ((!dataReader["Picture"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Picture"]) : string.Empty);
            parceiroNegocioDTO.DflAccount = ((!dataReader["DflAccount"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DflAccount"]) : string.Empty);
            parceiroNegocioDTO.DflBranch = ((!dataReader["DflBranch"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DflBranch"]) : string.Empty);
            parceiroNegocioDTO.BankCode = ((!dataReader["BankCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["BankCode"]) : string.Empty);
            parceiroNegocioDTO.AddID = ((!dataReader["AddID"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddID"]) : string.Empty);
            parceiroNegocioDTO.Pager = ((!dataReader["Pager"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Pager"]) : string.Empty);
            parceiroNegocioDTO.FatherCard = ((!dataReader["FatherCard"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["FatherCard"]) : string.Empty);
            parceiroNegocioDTO.FatherType = ((!dataReader["FatherType"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["FatherType"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup1 = ((!dataReader["QryGroup1"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup1"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup2 = ((!dataReader["QryGroup2"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup2"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup3 = ((!dataReader["QryGroup3"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup3"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup4 = ((!dataReader["QryGroup4"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup4"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup5 = ((!dataReader["QryGroup5"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup5"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup6 = ((!dataReader["QryGroup6"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup6"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup7 = ((!dataReader["QryGroup7"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup7"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup8 = ((!dataReader["QryGroup8"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup8"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup9 = ((!dataReader["QryGroup9"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup9"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup10 = ((!dataReader["QryGroup10"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup10"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup11 = ((!dataReader["QryGroup11"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup11"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup12 = ((!dataReader["QryGroup12"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup12"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup13 = ((!dataReader["QryGroup13"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup13"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup14 = ((!dataReader["QryGroup14"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup14"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup15 = ((!dataReader["QryGroup15"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup15"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup16 = ((!dataReader["QryGroup16"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup16"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup17 = ((!dataReader["QryGroup17"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup17"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup18 = ((!dataReader["QryGroup18"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup18"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup19 = ((!dataReader["QryGroup19"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup19"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup20 = ((!dataReader["QryGroup20"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup20"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup21 = ((!dataReader["QryGroup21"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup21"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup22 = ((!dataReader["QryGroup22"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup22"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup23 = ((!dataReader["QryGroup23"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup23"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup24 = ((!dataReader["QryGroup24"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup24"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup25 = ((!dataReader["QryGroup25"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup25"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup26 = ((!dataReader["QryGroup26"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup26"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup27 = ((!dataReader["QryGroup27"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup27"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup28 = ((!dataReader["QryGroup28"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup28"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup29 = ((!dataReader["QryGroup29"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup29"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup30 = ((!dataReader["QryGroup30"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup30"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup31 = ((!dataReader["QryGroup31"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup31"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup32 = ((!dataReader["QryGroup32"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup32"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup33 = ((!dataReader["QryGroup33"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup33"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup34 = ((!dataReader["QryGroup34"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup34"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup35 = ((!dataReader["QryGroup35"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup35"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup36 = ((!dataReader["QryGroup36"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup36"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup37 = ((!dataReader["QryGroup37"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup37"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup38 = ((!dataReader["QryGroup38"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup38"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup39 = ((!dataReader["QryGroup39"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup39"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup40 = ((!dataReader["QryGroup40"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup40"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup41 = ((!dataReader["QryGroup41"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup41"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup42 = ((!dataReader["QryGroup42"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup42"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup43 = ((!dataReader["QryGroup43"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup43"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup44 = ((!dataReader["QryGroup44"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup44"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup45 = ((!dataReader["QryGroup45"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup45"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup46 = ((!dataReader["QryGroup46"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup46"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup47 = ((!dataReader["QryGroup47"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup47"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup48 = ((!dataReader["QryGroup48"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup48"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup49 = ((!dataReader["QryGroup49"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup49"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup50 = ((!dataReader["QryGroup50"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup50"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup51 = ((!dataReader["QryGroup51"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup51"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup52 = ((!dataReader["QryGroup52"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup52"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup53 = ((!dataReader["QryGroup53"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup53"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup54 = ((!dataReader["QryGroup54"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup54"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup55 = ((!dataReader["QryGroup55"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup55"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup56 = ((!dataReader["QryGroup56"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup56"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup57 = ((!dataReader["QryGroup57"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup57"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup58 = ((!dataReader["QryGroup58"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup58"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup59 = ((!dataReader["QryGroup59"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup59"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup60 = ((!dataReader["QryGroup60"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup60"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup61 = ((!dataReader["QryGroup61"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup61"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup62 = ((!dataReader["QryGroup62"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup62"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup63 = ((!dataReader["QryGroup63"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup63"]) : char.MinValue);
            parceiroNegocioDTO.QryGroup64 = ((!dataReader["QryGroup64"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup64"]) : char.MinValue);
            parceiroNegocioDTO.DdctOffice = ((!dataReader["DdctOffice"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DdctOffice"]) : string.Empty);
            parceiroNegocioDTO.CreateDate = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
            parceiroNegocioDTO.UpdateDate = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
            parceiroNegocioDTO.ExportCode = ((!dataReader["ExportCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ExportCode"]) : string.Empty);
            parceiroNegocioDTO.DscntObjct = ((!dataReader["DscntObjct"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["DscntObjct"]) : 0);
            parceiroNegocioDTO.DscntRel = ((!dataReader["DscntRel"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DscntRel"]) : char.MinValue);
            parceiroNegocioDTO.SPGCounter = ((!dataReader["SPGCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SPGCounter"]) : 0);
            parceiroNegocioDTO.SPPCounter = ((!dataReader["SPPCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SPPCounter"]) : 0);
            parceiroNegocioDTO.DdctFileNo = ((!dataReader["DdctFileNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DdctFileNo"]) : string.Empty);
            parceiroNegocioDTO.SCNCounter = ((!dataReader["SCNCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SCNCounter"]) : 0);
            parceiroNegocioDTO.MinIntrst = ((!dataReader["MinIntrst"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["MinIntrst"]) : decimal.Zero);
            parceiroNegocioDTO.DataSource = ((!dataReader["DataSource"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DataSource"]) : char.MinValue);
            parceiroNegocioDTO.OprCount = ((!dataReader["OprCount"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["OprCount"]) : 0);
            parceiroNegocioDTO.ExemptNo = ((!dataReader["ExemptNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ExemptNo"]) : string.Empty);
            parceiroNegocioDTO.Priority = ((!dataReader["Priority"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["Priority"]) : 0);
            parceiroNegocioDTO.CreditCard = ((!dataReader["CreditCard"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["CreditCard"]) : 0);
            parceiroNegocioDTO.CrCardNum = ((!dataReader["CrCardNum"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CrCardNum"]) : string.Empty);
            parceiroNegocioDTO.CardValid = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
            parceiroNegocioDTO.UserSign = ((!dataReader["UserSign"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["UserSign"]) : 0);
            parceiroNegocioDTO.LocMth = ((!dataReader["LocMth"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["LocMth"]) : char.MinValue);
            parceiroNegocioDTO.validFor = ((!dataReader["validFor"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["validFor"]) : char.MinValue);
            parceiroNegocioDTO.validFrom = ((!dataReader["validFrom"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["validFrom"]) : DateTime.MinValue);
            parceiroNegocioDTO.validTo = ((!dataReader["validTo"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["validTo"]) : DateTime.MinValue);
            parceiroNegocioDTO.frozenFor = ((!dataReader["frozenFor"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["frozenFor"]) : char.MinValue);
            parceiroNegocioDTO.frozenFrom = ((!dataReader["frozenFrom"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["frozenFrom"]) : DateTime.MinValue);
            parceiroNegocioDTO.frozenTo = ((!dataReader["frozenTo"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["frozenTo"]) : DateTime.MinValue);
            parceiroNegocioDTO.sEmployed = ((!dataReader["sEmployed"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["sEmployed"]) : char.MinValue);
            parceiroNegocioDTO.MTHCounter = ((!dataReader["MTHCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["MTHCounter"]) : 0);
            parceiroNegocioDTO.BNKCounter = ((!dataReader["BNKCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["BNKCounter"]) : 0);
            parceiroNegocioDTO.DdgKey = ((!dataReader["DdgKey"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["DdgKey"]) : 0);
            parceiroNegocioDTO.DdtKey = ((!dataReader["DdtKey"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["DdtKey"]) : 0);
            parceiroNegocioDTO.ValidComm = ((!dataReader["ValidComm"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ValidComm"]) : string.Empty);
            parceiroNegocioDTO.FrozenComm = ((!dataReader["FrozenComm"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["FrozenComm"]) : string.Empty);
            parceiroNegocioDTO.chainStore = ((!dataReader["chainStore"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["chainStore"]) : char.MinValue);
            parceiroNegocioDTO.DiscInRet = ((!dataReader["DiscInRet"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DiscInRet"]) : char.MinValue);
            parceiroNegocioDTO.State1 = ((!dataReader["State1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["State1"]) : string.Empty);
            parceiroNegocioDTO.State2 = ((!dataReader["State2"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["State2"]) : string.Empty);
            parceiroNegocioDTO.VatGroup = ((!dataReader["VatGroup"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["VatGroup"]) : string.Empty);
            parceiroNegocioDTO.Block = ((!dataReader["Block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Block"]) : string.Empty);
            parceiroNegocioDTO.Series = ((!dataReader["Series"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["Series"]) : 0);
            parceiroNegocioDTO.IntrntSite = ((!dataReader["IntrntSite"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["IntrntSite"]) : string.Empty);
            parceiroNegocioDTO.SinglePaym = ((!dataReader["SinglePaym"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["SinglePaym"]) : char.MinValue);
            parceiroNegocioDTO.IndustryC = (dataReader["IndustryC"].Equals(DBNull.Value) ? string.Empty : dataReader["IndustryC"].ToString());
            parceiroNegocioDTO.PymCode = (dataReader["PymCode"].Equals(DBNull.Value) ? string.Empty : dataReader["PymCode"].ToString());
            parceiroNegocioDTO.AgentCode = (dataReader["AgentCode"].Equals(DBNull.Value) ? string.Empty : dataReader["AgentCode"].ToString());
            parceiroNegocioDTO.U_CNPJ = (dataReader["U_CNPJ"].Equals(DBNull.Value) ? string.Empty : dataReader["U_CNPJ"].ToString());
            parceiroNegocioDTO.MainUsage = (dataReader["MainUsage"].Equals(DBNull.Value) ? string.Empty : dataReader["MainUsage"].ToString());

            return parceiroNegocioDTO;
        }

        private ParceiroNegocioDTO ObterParceiroNegocioDTO(SqlDataReader dataReader)
        {
            ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();

            if (dataReader.HasRows)
            {
                parceiroNegocioDTO.CardCode = Convert.ToString(dataReader["CardCode"]);
                parceiroNegocioDTO.CardName = Convert.ToString(dataReader["CardName"]);
                parceiroNegocioDTO.CardFName = Convert.ToString(dataReader["CardFName"]);
                parceiroNegocioDTO.CardType = Convert.ToString(dataReader["CardType"]);
                parceiroNegocioDTO.GroupCode = Convert.ToInt32(dataReader["GroupCode"]);
                parceiroNegocioDTO.CmpPrivate = Convert.ToChar(dataReader["CmpPrivate"]);
                parceiroNegocioDTO.Address = ((!dataReader["Address"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Address"]) : string.Empty);
                parceiroNegocioDTO.ZipCode = ((!dataReader["ZipCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ZipCode"]) : string.Empty);
                parceiroNegocioDTO.MailAddres = ((!dataReader["MailAddres"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailAddres"]) : string.Empty);
                parceiroNegocioDTO.MailZipCod = ((!dataReader["MailZipCod"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailZipCod"]) : string.Empty);
                parceiroNegocioDTO.Phone1 = ((!dataReader["Phone1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Phone1"]) : string.Empty);
                parceiroNegocioDTO.Phone2 = ((!dataReader["Phone2"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Phone2"]) : string.Empty);
                parceiroNegocioDTO.Fax = ((!dataReader["Fax"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Fax"]) : string.Empty);
                parceiroNegocioDTO.CntctPrsn = ((!dataReader["CntctPrsn"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CntctPrsn"]) : string.Empty);
                parceiroNegocioDTO.Notes = ((!dataReader["Notes"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Notes"]) : string.Empty);
                parceiroNegocioDTO.Balance = ((!dataReader["Balance"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["Balance"]) : decimal.Zero);
                parceiroNegocioDTO.ChecksBal = ((!dataReader["ChecksBal"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["ChecksBal"]) : decimal.Zero);
                parceiroNegocioDTO.DNotesBal = ((!dataReader["DNotesBal"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DNotesBal"]) : decimal.Zero);
                parceiroNegocioDTO.OrdersBal = ((!dataReader["OrdersBal"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["OrdersBal"]) : decimal.Zero);
                parceiroNegocioDTO.GroupNum = ((!dataReader["GroupNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["GroupNum"]) : 0);
                parceiroNegocioDTO.CreditLine = ((!dataReader["CreditLine"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["CreditLine"]) : decimal.Zero);
                parceiroNegocioDTO.DebtLine = ((!dataReader["DebtLine"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DebtLine"]) : decimal.Zero);
                parceiroNegocioDTO.Discount = ((!dataReader["Discount"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["Discount"]) : decimal.Zero);
                parceiroNegocioDTO.VatStatus = ((!dataReader["VatStatus"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["VatStatus"]) : char.MinValue);
                parceiroNegocioDTO.LicTradNum = ((!dataReader["LicTradNum"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["LicTradNum"]) : string.Empty);
                parceiroNegocioDTO.DdctStatus = ((!dataReader["DdctStatus"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DdctStatus"]) : char.MinValue);
                parceiroNegocioDTO.DdctPrcnt = ((!dataReader["DdctPrcnt"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DdctPrcnt"]) : decimal.Zero);
                parceiroNegocioDTO.ValidUntil = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
                parceiroNegocioDTO.Chrctrstcs = ((!dataReader["Chrctrstcs"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["Chrctrstcs"]) : 0);
                parceiroNegocioDTO.ExMatchNum = ((!dataReader["ExMatchNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["ExMatchNum"]) : 0);
                parceiroNegocioDTO.InMatchNum = ((!dataReader["InMatchNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["InMatchNum"]) : 0);
                parceiroNegocioDTO.ListNum = ((!dataReader["ListNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["ListNum"]) : 0);
                parceiroNegocioDTO.DNoteBalFC = ((!dataReader["DNoteBalFC"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DNoteBalFC"]) : decimal.Zero);
                parceiroNegocioDTO.OrderBalFC = ((!dataReader["OrderBalFC"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["OrderBalFC"]) : decimal.Zero);
                parceiroNegocioDTO.DNoteBalSy = ((!dataReader["DNoteBalSy"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["DNoteBalSy"]) : decimal.Zero);
                parceiroNegocioDTO.OrderBalSy = ((!dataReader["OrderBalSy"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["OrderBalSy"]) : decimal.Zero);
                parceiroNegocioDTO.Transfered = ((!dataReader["Transfered"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Transfered"]) : char.MinValue);
                parceiroNegocioDTO.BalTrnsfrd = ((!dataReader["BalTrnsfrd"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["BalTrnsfrd"]) : char.MinValue);
                parceiroNegocioDTO.IntrstRate = ((!dataReader["IntrstRate"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["IntrstRate"]) : decimal.Zero);
                parceiroNegocioDTO.Commission = ((!dataReader["Commission"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["Commission"]) : decimal.Zero);
                parceiroNegocioDTO.CommGrCode = ((!dataReader["InMatchNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["InMatchNum"]) : 0);
                parceiroNegocioDTO.Free_Text = ((!dataReader["Free_Text"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Free_Text"]) : string.Empty);
                parceiroNegocioDTO.SlpCode = ((!dataReader["SlpCode"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SlpCode"]) : 0);
                parceiroNegocioDTO.PrevYearAc = ((!dataReader["PrevYearAc"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["PrevYearAc"]) : char.MinValue);
                parceiroNegocioDTO.Currency = ((!dataReader["Currency"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Currency"]) : string.Empty);
                parceiroNegocioDTO.RateDifAct = ((!dataReader["RateDifAct"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["RateDifAct"]) : string.Empty);
                parceiroNegocioDTO.BalanceSys = ((!dataReader["BalanceSys"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["BalanceSys"]) : decimal.Zero);
                parceiroNegocioDTO.BalanceFC = ((!dataReader["BalanceFC"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["BalanceFC"]) : decimal.Zero);
                parceiroNegocioDTO.Protected = ((!dataReader["Protected"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Protected"]) : char.MinValue);
                parceiroNegocioDTO.Cellular = ((!dataReader["Cellular"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Cellular"]) : string.Empty);
                parceiroNegocioDTO.AvrageLate = ((!dataReader["AvrageLate"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["AvrageLate"]) : 0);
                parceiroNegocioDTO.City = ((!dataReader["City"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["City"]) : string.Empty);
                parceiroNegocioDTO.County = ((!dataReader["County"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["County"]) : string.Empty);
                parceiroNegocioDTO.Country = ((!dataReader["Country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Country"]) : string.Empty);
                parceiroNegocioDTO.MailCity = ((!dataReader["MailCity"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailCity"]) : string.Empty);
                parceiroNegocioDTO.MailCounty = ((!dataReader["MailCounty"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailCounty"]) : string.Empty);
                parceiroNegocioDTO.MailCountr = ((!dataReader["MailCountr"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["MailCountr"]) : string.Empty);
                parceiroNegocioDTO.E_Mail = ((!dataReader["E_Mail"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["E_Mail"]) : string.Empty);
                parceiroNegocioDTO.Picture = ((!dataReader["Picture"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Picture"]) : string.Empty);
                parceiroNegocioDTO.DflAccount = ((!dataReader["DflAccount"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DflAccount"]) : string.Empty);
                parceiroNegocioDTO.DflBranch = ((!dataReader["DflBranch"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DflBranch"]) : string.Empty);
                parceiroNegocioDTO.BankCode = ((!dataReader["BankCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["BankCode"]) : string.Empty);
                parceiroNegocioDTO.AddID = ((!dataReader["AddID"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddID"]) : string.Empty);
                parceiroNegocioDTO.Pager = ((!dataReader["Pager"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Pager"]) : string.Empty);
                parceiroNegocioDTO.FatherCard = ((!dataReader["FatherCard"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["FatherCard"]) : string.Empty);
                parceiroNegocioDTO.FatherType = ((!dataReader["FatherType"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["FatherType"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup1 = ((!dataReader["QryGroup1"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup1"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup2 = ((!dataReader["QryGroup2"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup2"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup3 = ((!dataReader["QryGroup3"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup3"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup4 = ((!dataReader["QryGroup4"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup4"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup5 = ((!dataReader["QryGroup5"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup5"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup6 = ((!dataReader["QryGroup6"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup6"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup7 = ((!dataReader["QryGroup7"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup7"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup8 = ((!dataReader["QryGroup8"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup8"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup9 = ((!dataReader["QryGroup9"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup9"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup10 = ((!dataReader["QryGroup10"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup10"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup11 = ((!dataReader["QryGroup11"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup11"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup12 = ((!dataReader["QryGroup12"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup12"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup13 = ((!dataReader["QryGroup13"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup13"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup14 = ((!dataReader["QryGroup14"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup14"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup15 = ((!dataReader["QryGroup15"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup15"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup16 = ((!dataReader["QryGroup16"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup16"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup17 = ((!dataReader["QryGroup17"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup17"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup18 = ((!dataReader["QryGroup18"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup18"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup19 = ((!dataReader["QryGroup19"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup19"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup20 = ((!dataReader["QryGroup20"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup20"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup21 = ((!dataReader["QryGroup21"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup21"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup22 = ((!dataReader["QryGroup22"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup22"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup23 = ((!dataReader["QryGroup23"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup23"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup24 = ((!dataReader["QryGroup24"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup24"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup25 = ((!dataReader["QryGroup25"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup25"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup26 = ((!dataReader["QryGroup26"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup26"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup27 = ((!dataReader["QryGroup27"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup27"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup28 = ((!dataReader["QryGroup28"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup28"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup29 = ((!dataReader["QryGroup29"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup29"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup30 = ((!dataReader["QryGroup30"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup30"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup31 = ((!dataReader["QryGroup31"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup31"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup32 = ((!dataReader["QryGroup32"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup32"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup33 = ((!dataReader["QryGroup33"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup33"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup34 = ((!dataReader["QryGroup34"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup34"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup35 = ((!dataReader["QryGroup35"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup35"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup36 = ((!dataReader["QryGroup36"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup36"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup37 = ((!dataReader["QryGroup37"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup37"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup38 = ((!dataReader["QryGroup38"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup38"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup39 = ((!dataReader["QryGroup39"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup39"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup40 = ((!dataReader["QryGroup40"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup40"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup41 = ((!dataReader["QryGroup41"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup41"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup42 = ((!dataReader["QryGroup42"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup42"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup43 = ((!dataReader["QryGroup43"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup43"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup44 = ((!dataReader["QryGroup44"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup44"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup45 = ((!dataReader["QryGroup45"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup45"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup46 = ((!dataReader["QryGroup46"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup46"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup47 = ((!dataReader["QryGroup47"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup47"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup48 = ((!dataReader["QryGroup48"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup48"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup49 = ((!dataReader["QryGroup49"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup49"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup50 = ((!dataReader["QryGroup50"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup50"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup51 = ((!dataReader["QryGroup51"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup51"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup52 = ((!dataReader["QryGroup52"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup52"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup53 = ((!dataReader["QryGroup53"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup53"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup54 = ((!dataReader["QryGroup54"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup54"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup55 = ((!dataReader["QryGroup55"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup55"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup56 = ((!dataReader["QryGroup56"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup56"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup57 = ((!dataReader["QryGroup57"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup57"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup58 = ((!dataReader["QryGroup58"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup58"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup59 = ((!dataReader["QryGroup59"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup59"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup60 = ((!dataReader["QryGroup60"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup60"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup61 = ((!dataReader["QryGroup61"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup61"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup62 = ((!dataReader["QryGroup62"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup62"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup63 = ((!dataReader["QryGroup63"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup63"]) : char.MinValue);
                parceiroNegocioDTO.QryGroup64 = ((!dataReader["QryGroup64"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["QryGroup64"]) : char.MinValue);
                parceiroNegocioDTO.DdctOffice = ((!dataReader["DdctOffice"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DdctOffice"]) : string.Empty);
                parceiroNegocioDTO.CreateDate = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
                parceiroNegocioDTO.UpdateDate = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
                parceiroNegocioDTO.ExportCode = ((!dataReader["ExportCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ExportCode"]) : string.Empty);
                parceiroNegocioDTO.DscntObjct = ((!dataReader["DscntObjct"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["DscntObjct"]) : 0);
                parceiroNegocioDTO.DscntRel = ((!dataReader["DscntRel"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DscntRel"]) : char.MinValue);
                parceiroNegocioDTO.SPGCounter = ((!dataReader["SPGCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SPGCounter"]) : 0);
                parceiroNegocioDTO.SPPCounter = ((!dataReader["SPPCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SPPCounter"]) : 0);
                parceiroNegocioDTO.DdctFileNo = ((!dataReader["DdctFileNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["DdctFileNo"]) : string.Empty);
                parceiroNegocioDTO.SCNCounter = ((!dataReader["SCNCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["SCNCounter"]) : 0);
                parceiroNegocioDTO.MinIntrst = ((!dataReader["MinIntrst"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["MinIntrst"]) : decimal.Zero);
                parceiroNegocioDTO.DataSource = ((!dataReader["DataSource"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DataSource"]) : char.MinValue);
                parceiroNegocioDTO.OprCount = ((!dataReader["OprCount"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["OprCount"]) : 0);
                parceiroNegocioDTO.ExemptNo = ((!dataReader["ExemptNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ExemptNo"]) : string.Empty);
                parceiroNegocioDTO.Priority = ((!dataReader["Priority"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["Priority"]) : 0);
                parceiroNegocioDTO.CreditCard = ((!dataReader["CreditCard"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["CreditCard"]) : 0);
                parceiroNegocioDTO.CrCardNum = ((!dataReader["CrCardNum"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["CrCardNum"]) : string.Empty);
                parceiroNegocioDTO.CardValid = ((!dataReader["ValidUntil"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["ValidUntil"]) : DateTime.MinValue);
                parceiroNegocioDTO.UserSign = ((!dataReader["UserSign"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["UserSign"]) : 0);
                parceiroNegocioDTO.LocMth = ((!dataReader["LocMth"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["LocMth"]) : char.MinValue);
                parceiroNegocioDTO.validFor = ((!dataReader["validFor"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["validFor"]) : char.MinValue);
                parceiroNegocioDTO.validFrom = ((!dataReader["validFrom"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["validFrom"]) : DateTime.MinValue);
                parceiroNegocioDTO.validTo = ((!dataReader["validTo"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["validTo"]) : DateTime.MinValue);
                parceiroNegocioDTO.frozenFor = ((!dataReader["frozenFor"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["frozenFor"]) : char.MinValue);
                parceiroNegocioDTO.frozenFrom = ((!dataReader["frozenFrom"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["frozenFrom"]) : DateTime.MinValue);
                parceiroNegocioDTO.frozenTo = ((!dataReader["frozenTo"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["frozenTo"]) : DateTime.MinValue);
                parceiroNegocioDTO.sEmployed = ((!dataReader["sEmployed"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["sEmployed"]) : char.MinValue);
                parceiroNegocioDTO.MTHCounter = ((!dataReader["MTHCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["MTHCounter"]) : 0);
                parceiroNegocioDTO.BNKCounter = ((!dataReader["BNKCounter"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["BNKCounter"]) : 0);
                parceiroNegocioDTO.DdgKey = ((!dataReader["DdgKey"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["DdgKey"]) : 0);
                parceiroNegocioDTO.DdtKey = ((!dataReader["DdtKey"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["DdtKey"]) : 0);
                parceiroNegocioDTO.ValidComm = ((!dataReader["ValidComm"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ValidComm"]) : string.Empty);
                parceiroNegocioDTO.FrozenComm = ((!dataReader["FrozenComm"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["FrozenComm"]) : string.Empty);
                parceiroNegocioDTO.chainStore = ((!dataReader["chainStore"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["chainStore"]) : char.MinValue);
                parceiroNegocioDTO.DiscInRet = ((!dataReader["DiscInRet"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["DiscInRet"]) : char.MinValue);
                parceiroNegocioDTO.State1 = ((!dataReader["State1"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["State1"]) : string.Empty);
                parceiroNegocioDTO.State2 = ((!dataReader["State2"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["State2"]) : string.Empty);
                parceiroNegocioDTO.VatGroup = ((!dataReader["VatGroup"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["VatGroup"]) : string.Empty);
                parceiroNegocioDTO.Block = ((!dataReader["Block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Block"]) : string.Empty);
                parceiroNegocioDTO.Series = ((!dataReader["Series"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["Series"]) : 0);
                parceiroNegocioDTO.IntrntSite = ((!dataReader["IntrntSite"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["IntrntSite"]) : string.Empty);
                parceiroNegocioDTO.SinglePaym = ((!dataReader["SinglePaym"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["SinglePaym"]) : char.MinValue);
                parceiroNegocioDTO.IndustryC = (dataReader["IndustryC"].Equals(DBNull.Value) ? string.Empty : dataReader["IndustryC"].ToString());
                parceiroNegocioDTO.PymCode = (dataReader["PymCode"].Equals(DBNull.Value) ? string.Empty : dataReader["PymCode"].ToString());
                parceiroNegocioDTO.AgentCode = (dataReader["AgentCode"].Equals(DBNull.Value) ? string.Empty : dataReader["AgentCode"].ToString());
                parceiroNegocioDTO.U_CNPJ = (dataReader["U_CNPJ"].Equals(DBNull.Value) ? string.Empty : dataReader["U_CNPJ"].ToString());
                parceiroNegocioDTO.MainUsage = (dataReader["MainUsage"].Equals(DBNull.Value) ? string.Empty : dataReader["MainUsage"].ToString());
            }
            return parceiroNegocioDTO;
        }

        public IList<ParceiroNegocioDTO> Listar(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            IList<ParceiroNegocioDTO> listParceiroNegocioDTO = new List<ParceiroNegocioDTO>();

            SqlCommand comando = new SqlCommand();
            try
            {
                conexao.Conectar();

                StringBuilder stb = new StringBuilder();
                stb.Append(tSQLBase);

                if (!parceiroNegocioDTO.CardType.Contains("-"))
                {
                    stb.Append("WHERE CardType = @CardType ");
                    comando.Parameters.AddWithValue("@CardType", parceiroNegocioDTO.CardType);
                }
                else
                {
                    stb.Append("WHERE (");

                    string[] dadosTipos = parceiroNegocioDTO.CardType.Split('-');

                    for (int i = 0; i < dadosTipos.Length; i++)
                    {
                        stb.Append("CardType = @CardType" + i + " ");
                        comando.Parameters.AddWithValue("@CardType" + i, dadosTipos[i]);

                        if (i < dadosTipos.Length - 1)
                        {
                            stb.Append("OR ");
                        }
                    }

                    stb.Append(") ");
                }

                if (parceiroNegocioDTO.validFor != null)
                {
                    stb.Append("AND validFor = @ValidFor ");
                    comando.Parameters.AddWithValue("@ValidFor", parceiroNegocioDTO.validFor);
                }

                if (parceiroNegocioDTO.SlpCode > 0)
                {
                    stb.Append("AND SlpCode = @SlpCode ");
                    comando.Parameters.AddWithValue("@SlpCode", parceiroNegocioDTO.SlpCode);
                }

                stb.Append("ORDER BY CardName");

                comando.CommandText = stb.ToString();
                comando.Connection = conexao.Conexao;

                SqlDataReader dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    ParceiroNegocioDTO parceiroNegocioDTOParametro = new ParceiroNegocioDTO();
                    parceiroNegocioDTOParametro = ObterParceiroNegocioDTO(dataReader);

                    listParceiroNegocioDTO.Add(parceiroNegocioDTOParametro);
                }
                dataReader.Close();

                return listParceiroNegocioDTO;
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

        public int RetornarQtdParceiroNegocio(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string query = $@"SELECT COALESCE(COUNT(*), 0) FROM OCRD WHERE ""CardType"" = '{parceiroNegocioDTO.CardType}'";
            try
            {

                if (tipoBD == "Hana")
                {
                    return Convert.ToInt32(conexaoHana.ExecuteScalar(query));
                }
                else
                {
                    conexao.Conectar();
                    SqlCommand comando = new SqlCommand(query, conexao.Conexao);

                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
            catch (SqlException erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                if (tipoBD == "Hana")
                {
                    conexaoHana.Dispose();
                }
                else
                {
                    conexao.Desconectar();
                }
            }
        }

        public IList<ParceiroNegocioDTO> Buscar(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            IList<ParceiroNegocioDTO> listParceiroNegocioDTO = new List<ParceiroNegocioDTO>();

            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();
                try
                {
                    string query = "SELECT * FROM OCRD ";

                    if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardCode) ||
                    !string.IsNullOrEmpty(parceiroNegocioDTO.CardName) ||
                    !string.IsNullOrEmpty(parceiroNegocioDTO.CardType) ||
                    !string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                    !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                    parceiroNegocioDTO.GroupCode > 0 ||
                    parceiroNegocioDTO.SlpCode > 0)
                    {
                        query += "WHERE ";

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardCode))
                        {
                            query += $@"""CardCode"" LIKE '%{parceiroNegocioDTO.CardCode}%' ";

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardName) ||
                                !string.IsNullOrEmpty(parceiroNegocioDTO.CardType) ||
                                !string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                                !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                                parceiroNegocioDTO.GroupCode > 0 ||
                                parceiroNegocioDTO.SlpCode > 0)
                            {
                                query += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardName))
                        {
                            query += $@"""CardName"" LIKE '%{parceiroNegocioDTO.CardName}%' ";

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardType) ||
                                !string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                                !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                                parceiroNegocioDTO.GroupCode > 0 ||
                                parceiroNegocioDTO.SlpCode > 0)
                            {
                                query += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardType))
                        {
                            if (parceiroNegocioDTO.CardType.Contains("-"))
                            {
                                string[] dados = parceiroNegocioDTO.CardType.Split('-');

                                query += "(";

                                for (int i = 0; i < dados.Length; i++)
                                {
                                    query += $@"""CardType"" = '{dados[i].Trim()}' ";

                                    if (i < (dados.Length - 1))
                                    {
                                        query += "OR ";
                                    }
                                }
                                query += ") ";
                            }
                            else
                            {
                                query += $@"""CardType"" = {parceiroNegocioDTO.CardType} ";
                            }

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                                !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                                parceiroNegocioDTO.GroupCode > 0 ||
                                parceiroNegocioDTO.SlpCode > 0)
                            {
                                query += "AND ";
                            }
                        }

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail))
                        {
                            query += $@"""E_Mail"" LIKE '%{parceiroNegocioDTO.E_Mail}%' ";

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) || parceiroNegocioDTO.GroupCode > 0 || parceiroNegocioDTO.SlpCode > 0)
                                query += "AND ";
                        }

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ))
                        {
                            query += $@"""U_CNPJ"" LIKE '%{parceiroNegocioDTO.U_CNPJ}%'";

                            if (parceiroNegocioDTO.GroupCode > 0 || parceiroNegocioDTO.SlpCode > 0)
                                query += "AND ";
                        }

                        if (parceiroNegocioDTO.GroupCode > 0)
                        {
                            query = $@"""GroupCode"" = {parceiroNegocioDTO.GroupCode} ";

                            if (parceiroNegocioDTO.SlpCode > 0)
                                query += "AND ";
                        }

                        if (parceiroNegocioDTO.SlpCode > 0)
                        {
                            query += $@"""SlpCode"" = {parceiroNegocioDTO.SlpCode} ";
                        }
                    }

                    query += $@"ORDER BY ""CardName""";

                    conexaoHana.Connection();

                    DataTable dt = conexaoHana.ExecuteDataTable(query);

                    foreach (DataRow dr in dt.Rows)
                    {
                        ParceiroNegocioDTO parceiroNegocioDTOParametro = new ParceiroNegocioDTO();
                        parceiroNegocioDTOParametro = ObterParceiroNegocioHanaDTO(dr);

                        listParceiroNegocioDTO.Add(parceiroNegocioDTOParametro);
                    }

                    return listParceiroNegocioDTO;
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

            SqlCommand comando = new SqlCommand();
            try
            {
                conexao.Conectar();

                StringBuilder stb = new StringBuilder();
                stb.Append(tSQLBase);

                if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardCode) ||
                   !string.IsNullOrEmpty(parceiroNegocioDTO.CardName) ||
                   !string.IsNullOrEmpty(parceiroNegocioDTO.CardType) ||
                   !string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                   !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                   parceiroNegocioDTO.GroupCode > 0 ||
                   parceiroNegocioDTO.SlpCode > 0)
                {
                    stb.Append("WHERE ");

                    if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardCode))
                    {
                        stb.Append("CardCode LIKE @CardCode ");
                        comando.Parameters.AddWithValue("@CardCode", "%" + parceiroNegocioDTO.CardCode + "%");

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardName) ||
                            !string.IsNullOrEmpty(parceiroNegocioDTO.CardType) ||
                            !string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                            !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                            parceiroNegocioDTO.GroupCode > 0 ||
                            parceiroNegocioDTO.SlpCode > 0)
                        {
                            stb.Append("AND ");
                        }
                    }

                    if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardName))
                    {
                        stb.Append("CardName LIKE @CardName ");

                        comando.Parameters.AddWithValue("@CardName", "%" + parceiroNegocioDTO.CardName + "%");

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardType) ||
                            !string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                            !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                            parceiroNegocioDTO.GroupCode > 0 ||
                            parceiroNegocioDTO.SlpCode > 0)
                        {
                            stb.Append("AND ");
                        }
                    }

                    if (!string.IsNullOrEmpty(parceiroNegocioDTO.CardType))
                    {
                        if (parceiroNegocioDTO.CardType.Contains("-"))
                        {
                            string[] dados = parceiroNegocioDTO.CardType.Split('-');

                            stb.Append("(");

                            for (int i = 0; i < dados.Length; i++)
                            {
                                stb.Append("CardType = @CardType" + i + " ");

                                comando.Parameters.AddWithValue("@CardType" + i, dados[i].Trim());

                                if (i < (dados.Length - 1))
                                {
                                    stb.Append("OR ");
                                }
                            }

                            stb.Append(") ");
                        }
                        else
                        {
                            stb.Append("CardType = @CardType ");
                            comando.Parameters.AddWithValue("@CardType", parceiroNegocioDTO.CardType);
                        }

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail) ||
                            !string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) ||
                            parceiroNegocioDTO.GroupCode > 0 ||
                            parceiroNegocioDTO.SlpCode > 0)
                        {
                            stb.Append("AND ");
                        }
                    }

                    if (!string.IsNullOrEmpty(parceiroNegocioDTO.E_Mail))
                    {
                        stb.Append("E_Mail LIKE @E_Mail ");

                        comando.Parameters.AddWithValue("@E_Mail", "%" + parceiroNegocioDTO.E_Mail + "%");

                        if (!string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ) || parceiroNegocioDTO.GroupCode > 0 || parceiroNegocioDTO.SlpCode > 0)
                            stb.Append("AND ");
                    }

                    if (!string.IsNullOrEmpty(parceiroNegocioDTO.U_CNPJ))
                    {
                        stb.Append("U_CNPJ LIKE @Cnpj ");
                        comando.Parameters.AddWithValue("@Cnpj", "%" + parceiroNegocioDTO.U_CNPJ + "%");

                        if (parceiroNegocioDTO.GroupCode > 0 || parceiroNegocioDTO.SlpCode > 0)
                            stb.Append("AND ");
                    }

                    if (parceiroNegocioDTO.GroupCode > 0)
                    {
                        stb.Append("GroupCode = @GroupCode ");
                        comando.Parameters.AddWithValue("@GroupCode", parceiroNegocioDTO.GroupCode);

                        if (parceiroNegocioDTO.SlpCode > 0)
                            stb.Append("AND ");
                    }

                    if (parceiroNegocioDTO.SlpCode > 0)
                    {
                        stb.Append("SlpCode = @SlpCode ");
                        comando.Parameters.AddWithValue("@SlpCode", parceiroNegocioDTO.SlpCode);
                    }
                }


                stb.Append("ORDER BY CardName");

                comando.CommandText = stb.ToString();
                comando.Connection = conexao.Conexao;

                SqlDataReader dataReader = comando.ExecuteReader();

                while (dataReader.Read())
                {
                    ParceiroNegocioDTO parceiroNegocioDTOParametro = new ParceiroNegocioDTO();
                    parceiroNegocioDTOParametro = ObterParceiroNegocioDTO(dataReader);

                    listParceiroNegocioDTO.Add(parceiroNegocioDTOParametro);
                }
                dataReader.Close();

                return listParceiroNegocioDTO;
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

        public ParceiroNegocioDTO RetornarParceiroNegocioPorCnpjESenha(string cpnj, string senha)
        {
            ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();

            try
            {
                StringBuilder tSQL = new StringBuilder();
                tSQL.Append(tSQLBase);
                tSQL.Append("WHERE U_CNPJ = @U_CNPJ AND U_SenhaPortal = @U_SenhaPortal");

                conexao.Conectar();

                SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                comando.Parameters.Add(new SqlParameter("@U_CNPJ", cpnj));
                comando.Parameters.Add(new SqlParameter("@U_SenhaPortal", senha));

                SqlDataReader dataReader = comando.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    parceiroNegocioDTO = ObterParceiroNegocioDTO(dataReader);
                }
                dataReader.Close();

                return parceiroNegocioDTO;
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

        public ParceiroNegocioDTO RetornarParceiroNegocioPorCpfESenha(string cpf, string senha)
        {
            ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();

            try
            {
                StringBuilder tSQL = new StringBuilder();
                tSQL.Append(tSQLBase);
                tSQL.Append("WHERE U_CPF = @U_CPF AND U_SenhaPortal = @U_SenhaPortal");

                conexao.Conectar();

                SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                comando.Parameters.Add(new SqlParameter("@U_CPF", cpf));
                comando.Parameters.Add(new SqlParameter("@U_SenhaPortal", senha));

                SqlDataReader dataReader = comando.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    parceiroNegocioDTO = ObterParceiroNegocioDTO(dataReader);
                }
                dataReader.Close();

                return parceiroNegocioDTO;
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