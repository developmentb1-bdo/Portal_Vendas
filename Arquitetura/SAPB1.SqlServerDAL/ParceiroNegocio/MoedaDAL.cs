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
    /// <summary>
    /// Tabela OCRN SAP B1.
    /// </summary>
    public sealed class MoedaDAL : IMoeda
    {
        public IList<MoedaDTO> Listar()
        {
            IList<MoedaDTO> listMoedaDTO = new List<MoedaDTO>();
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            string tSQLBase = $@"SELECT ""CurrCode"", ""CurrName"", ""ChkName"", ""Chk100Name"", ""DocCurrCod"", ""FrgnName"", ""F100Name"", ""Locked"", ""DataSource"", ""UserSign"", ""RoundSys"", ""UserSign2"", ""Decimals"", ""ISRCalc"", ""RoundPym"", ""ConvUnit"", ""BaseCurr"", ""Factor"", ""ChkNamePl"", ""Chk100NPl"", ""FrgnNamePl"", ""F100NamePl"", ""ISOCurrCod"", ""MaxInDiff"", ""MaxOutDiff"", ""MaxInPcnt"", ""MaxOutPcnt"", ""ISOCurrNum"" FROM OCRN";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();
                    DataTable dt = conexaoHana.ExecuteDataTable(tSQLBase);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            MoedaDTO moedaDTO = new MoedaDTO();
                            moedaDTO = ObterMoedaHanaDTO(dr);

                            listMoedaDTO.Add(moedaDTO);
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

                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                    SqlDataReader dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        MoedaDTO moedaDTO = new MoedaDTO();
                        moedaDTO = ObterMoedaDTO(dataReader);

                        listMoedaDTO.Add(moedaDTO);
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

            return listMoedaDTO;
        }

        private MoedaDTO ObterMoedaDTO(SqlDataReader dataReader)
        {
            MoedaDTO moedaDTO = new MoedaDTO();

            if (dataReader.HasRows)
            {
                moedaDTO.CurrCode = Convert.ToString(dataReader["CurrCode"]);
                moedaDTO.CurrName = Convert.ToString(dataReader["CurrName"]);
                moedaDTO.ChkName = Convert.ToString(dataReader["ChkName"]);
                moedaDTO.Chk100Name = Convert.ToString(dataReader["Chk100Name"]);
                moedaDTO.DocCurrCod = Convert.ToString(dataReader["DocCurrCod"]);
                moedaDTO.FrgnName = Convert.ToString(dataReader["FrgnName"]);
                moedaDTO.F100Name = Convert.ToString(dataReader["F100Name"]);
                moedaDTO.Locked = Convert.ToChar(dataReader["Locked"]);
                moedaDTO.DataSource = Convert.ToChar(dataReader["DataSource"]);
                moedaDTO.UserSign = Convert.ToInt32(dataReader["UserSign"]);
                moedaDTO.RoundSys = Convert.ToInt32(dataReader["RoundSys"]);
                moedaDTO.UserSign2 = ((!dataReader["UserSign2"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["UserSign2"]) : 0);
                moedaDTO.Decimals = Convert.ToInt32(dataReader["Decimals"]);
                moedaDTO.ISRCalc = Convert.ToChar(dataReader["ISRCalc"]);
                moedaDTO.RoundPym = Convert.ToChar(dataReader["RoundPym"]);
                moedaDTO.RoundPym = Convert.ToChar(dataReader["RoundPym"]);
                moedaDTO.ConvUnit = Convert.ToChar(dataReader["ConvUnit"]);
                moedaDTO.BaseCurr = ((!dataReader["BaseCurr"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["BaseCurr"]) : ' ');
                moedaDTO.Factor = Convert.ToDecimal(dataReader["Factor"]);
                moedaDTO.ChkNamePl = ((!dataReader["ChkNamePl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ChkNamePl"]) : string.Empty);
                moedaDTO.Chk100NPl = ((!dataReader["Chk100NPl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Chk100NPl"]) : string.Empty);
                moedaDTO.FrgnNamePl = ((!dataReader["FrgnNamePl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["FrgnNamePl"]) : string.Empty);
                moedaDTO.F100NamePl = ((!dataReader["F100NamePl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["F100NamePl"]) : string.Empty);
                moedaDTO.ISOCurrCod = Convert.ToString(dataReader["ISOCurrCod"]);
                moedaDTO.MaxInDiff = ((!dataReader["MaxInDiff"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["MaxInDiff"]) : decimal.Zero);
                moedaDTO.MaxOutDiff = ((!dataReader["MaxOutDiff"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["MaxOutDiff"]) : decimal.Zero);
                moedaDTO.MaxInPcnt = Convert.ToDecimal(dataReader["MaxInPcnt"]);
                moedaDTO.MaxOutPcnt = Convert.ToDecimal(dataReader["MaxOutPcnt"]);
                moedaDTO.ISOCurrNum = ((!dataReader["ISOCurrNum"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ISOCurrNum"]) : string.Empty);
            }
            return moedaDTO;
        }

        private MoedaDTO ObterMoedaHanaDTO(DataRow dataReader)
        {
            MoedaDTO moedaDTO = new MoedaDTO();


            moedaDTO.CurrCode = Convert.ToString(dataReader["CurrCode"]);
            moedaDTO.CurrName = Convert.ToString(dataReader["CurrName"]);
            moedaDTO.ChkName = Convert.ToString(dataReader["ChkName"]);
            moedaDTO.Chk100Name = Convert.ToString(dataReader["Chk100Name"]);
            moedaDTO.DocCurrCod = Convert.ToString(dataReader["DocCurrCod"]);
            moedaDTO.FrgnName = Convert.ToString(dataReader["FrgnName"]);
            moedaDTO.F100Name = Convert.ToString(dataReader["F100Name"]);
            moedaDTO.Locked = Convert.ToChar(dataReader["Locked"]);
            moedaDTO.DataSource = Convert.ToChar(dataReader["DataSource"]);
            moedaDTO.UserSign = Convert.ToInt32(dataReader["UserSign"]);
            moedaDTO.RoundSys = Convert.ToInt32(dataReader["RoundSys"]);
            moedaDTO.UserSign2 = ((!dataReader["UserSign2"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["UserSign2"]) : 0);
            moedaDTO.Decimals = Convert.ToInt32(dataReader["Decimals"]);
            moedaDTO.ISRCalc = Convert.ToChar(dataReader["ISRCalc"]);
            moedaDTO.RoundPym = Convert.ToChar(dataReader["RoundPym"]);
            moedaDTO.RoundPym = Convert.ToChar(dataReader["RoundPym"]);
            moedaDTO.ConvUnit = Convert.ToChar(dataReader["ConvUnit"]);
            moedaDTO.BaseCurr = ((!dataReader["BaseCurr"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["BaseCurr"]) : ' ');
            moedaDTO.Factor = Convert.ToDecimal(dataReader["Factor"]);
            moedaDTO.ChkNamePl = ((!dataReader["ChkNamePl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ChkNamePl"]) : string.Empty);
            moedaDTO.Chk100NPl = ((!dataReader["Chk100NPl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Chk100NPl"]) : string.Empty);
            moedaDTO.FrgnNamePl = ((!dataReader["FrgnNamePl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["FrgnNamePl"]) : string.Empty);
            moedaDTO.F100NamePl = ((!dataReader["F100NamePl"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["F100NamePl"]) : string.Empty);
            moedaDTO.ISOCurrCod = Convert.ToString(dataReader["ISOCurrCod"]);
            moedaDTO.MaxInDiff = ((!dataReader["MaxInDiff"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["MaxInDiff"]) : decimal.Zero);
            moedaDTO.MaxOutDiff = ((!dataReader["MaxOutDiff"].Equals(DBNull.Value)) ? Convert.ToDecimal(dataReader["MaxOutDiff"]) : decimal.Zero);
            moedaDTO.MaxInPcnt = Convert.ToDecimal(dataReader["MaxInPcnt"]);
            moedaDTO.MaxOutPcnt = Convert.ToDecimal(dataReader["MaxOutPcnt"]);
            moedaDTO.ISOCurrNum = ((!dataReader["ISOCurrNum"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["ISOCurrNum"]) : string.Empty);

            return moedaDTO;
        }
    }
}