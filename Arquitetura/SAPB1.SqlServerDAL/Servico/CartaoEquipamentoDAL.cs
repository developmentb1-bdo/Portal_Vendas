/*
 * @author Victor Oliveira.
 */

using System;
using System.Data.SqlClient;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.IDAL.Servico;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SAPB1.SqlServerDAL.Servico
{
    /// <summary>
    /// Tabela do SAP Business One OINS.
    /// </summary>
    public sealed class CartaoEquipamentoDAL : ICartaoEquipamento
    {


        public CartaoEquipamentoDTO Selecionar(int insID)
        {
            string tSQLBase = $@"SELECT ""insID"", ""customer"", ""custmrName"", ""contactCod"", ""directCsmr"", ""drctCsmNam"", ""manufSN"", ""internalSN"", ""warranty"", ""wrrntyStrt"", ""wrrntyEnd"", ""responsVal"", ""responsUnt"", ""itemCode"", ""itemName"", ""itemGroup"", ""manufDate"", ""delivery"", ""deliveryNo"", ""invoice"", ""invoiceNum"", ""dlvryDate"", ""cntctPhone"", ""street"", ""block"", ""zip"", ""city"", ""county"", ""country"", ""state"", ""instLction"", ""contract"", ""cntrctStrt"", ""cntrctEnd"", ""attachment"", ""objType"", ""logInstanc"", ""userSign"", ""createDate"", ""userSign2"", ""updateDate"", ""Building"", ""status"", ""replcIns"", ""repByIns"", ""technician"", ""territory"", ""AtcEntry"", ""Transfered"", ""AddrType"", ""Instance"", ""StreetNo"" FROM OINS ";

            CartaoEquipamentoDTO cartaoEquipamentoDTO = new CartaoEquipamentoDTO();

            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();
            if (tipoBD == "Hana")
            {

                HanaConexao conexaoHana = new HanaConexao();
                tSQLBase += $@"WHERE ""insID"" = '{insID}'";

                try
                {
                    conexaoHana.Connection();

                    cartaoEquipamentoDTO = ObjetoCartaoEquipamentoHanaDTO(tSQLBase, conexaoHana);
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
                    tSQL.Append("WHERE insID = @insID");

                    conexao.Conectar();

                    SqlCommand comando = new SqlCommand(tSQL.ToString(), conexao.Conexao);
                    comando.Parameters.Add(new SqlParameter("@insID", insID));
                    SqlDataReader dataReader = comando.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        cartaoEquipamentoDTO = ObjetoCartaoEquipamentoDTO(dataReader);
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
            return cartaoEquipamentoDTO;
        }

        private CartaoEquipamentoDTO ObjetoCartaoEquipamentoDTO(SqlDataReader dataReader)
        {
            CartaoEquipamentoDTO cartaoEquipamentoDTO = new CartaoEquipamentoDTO();

            if (dataReader.HasRows)
            {
                cartaoEquipamentoDTO.insID = ((!dataReader["insID"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["insID"]) : 0);
                cartaoEquipamentoDTO.customer = ((!dataReader["customer"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["customer"]) : string.Empty);
                cartaoEquipamentoDTO.custmrName = ((!dataReader["custmrName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["custmrName"]) : string.Empty);
                cartaoEquipamentoDTO.contactCod = ((!dataReader["contactCod"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contactCod"]) : 0);
                cartaoEquipamentoDTO.directCsmr = ((!dataReader["directCsmr"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["directCsmr"]) : string.Empty);
                cartaoEquipamentoDTO.drctCsmNam = ((!dataReader["drctCsmNam"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["drctCsmNam"]) : string.Empty);
                cartaoEquipamentoDTO.manufSN = ((!dataReader["manufSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["manufSN"]) : string.Empty);
                cartaoEquipamentoDTO.internalSN = ((!dataReader["internalSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["internalSN"]) : string.Empty);
                cartaoEquipamentoDTO.warranty = ((!dataReader["warranty"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["warranty"]) : char.MinValue);
                cartaoEquipamentoDTO.wrrntyStrt = ((!dataReader["wrrntyStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyStrt"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.wrrntyEnd = ((!dataReader["wrrntyEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyEnd"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.responsVal = ((!dataReader["responsVal"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["responsVal"]) : (short)0);
                cartaoEquipamentoDTO.responsUnt = ((!dataReader["responsUnt"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["responsUnt"]) : char.MinValue);
                cartaoEquipamentoDTO.itemCode = ((!dataReader["itemCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemCode"]) : string.Empty);
                cartaoEquipamentoDTO.itemName = ((!dataReader["itemName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemName"]) : string.Empty);
                cartaoEquipamentoDTO.itemGroup = ((!dataReader["itemGroup"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["itemGroup"]) : (short)0);
                cartaoEquipamentoDTO.manufDate = ((!dataReader["manufDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["manufDate"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.delivery = ((!dataReader["delivery"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["delivery"]) : 0);
                cartaoEquipamentoDTO.deliveryNo = ((!dataReader["deliveryNo"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["deliveryNo"]) : 0);
                cartaoEquipamentoDTO.invoice = ((!dataReader["invoice"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoice"]) : 0);
                cartaoEquipamentoDTO.invoiceNum = ((!dataReader["invoiceNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoiceNum"]) : 0);
                cartaoEquipamentoDTO.dlvryDate = ((!dataReader["dlvryDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["dlvryDate"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.cntctPhone = ((!dataReader["cntctPhone"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["cntctPhone"]) : string.Empty);
                cartaoEquipamentoDTO.street = ((!dataReader["street"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["street"]) : string.Empty);
                cartaoEquipamentoDTO.block = ((!dataReader["block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["block"]) : string.Empty);
                cartaoEquipamentoDTO.zip = ((!dataReader["zip"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["zip"]) : string.Empty);
                cartaoEquipamentoDTO.city = ((!dataReader["city"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["city"]) : string.Empty);
                cartaoEquipamentoDTO.county = ((!dataReader["county"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["county"]) : string.Empty);
                cartaoEquipamentoDTO.country = ((!dataReader["country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["country"]) : string.Empty);
                cartaoEquipamentoDTO.state = ((!dataReader["state"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["state"]) : string.Empty);
                cartaoEquipamentoDTO.instLction = ((!dataReader["instLction"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["instLction"]) : string.Empty);
                cartaoEquipamentoDTO.contract = ((!dataReader["contract"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contract"]) : 0);
                cartaoEquipamentoDTO.cntrctStrt = ((!dataReader["cntrctStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctStrt"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.cntrctEnd = ((!dataReader["cntrctEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctEnd"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.attachment = ((!dataReader["attachment"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["attachment"]) : string.Empty);
                cartaoEquipamentoDTO.objType = ((!dataReader["objType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["objType"]) : string.Empty);
                cartaoEquipamentoDTO.logInstanc = ((!dataReader["logInstanc"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["logInstanc"]) : 0);
                cartaoEquipamentoDTO.userSign = ((!dataReader["userSign"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign"]) : (short)0);
                cartaoEquipamentoDTO.createDate = ((!dataReader["createDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["createDate"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.userSign2 = ((!dataReader["userSign2"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign2"]) : (short)0);
                cartaoEquipamentoDTO.updateDate = ((!dataReader["updateDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["updateDate"]) : DateTime.MinValue);
                cartaoEquipamentoDTO.Building = ((!dataReader["Building"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Building"]) : string.Empty);
                cartaoEquipamentoDTO.status = ((!dataReader["status"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["status"]) : char.MinValue);
                cartaoEquipamentoDTO.replcIns = ((!dataReader["replcIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["replcIns"]) : 0);
                cartaoEquipamentoDTO.repByIns = ((!dataReader["repByIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["repByIns"]) : 0);
                cartaoEquipamentoDTO.technician = ((!dataReader["technician"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["technician"]) : 0);
                cartaoEquipamentoDTO.territory = ((!dataReader["territory"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["territory"]) : 0);
                cartaoEquipamentoDTO.AtcEntry = ((!dataReader["AtcEntry"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["AtcEntry"]) : 0);
                cartaoEquipamentoDTO.Transfered = ((!dataReader["Transfered"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Transfered"]) : char.MinValue);
                cartaoEquipamentoDTO.AddrType = ((!dataReader["AddrType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddrType"]) : string.Empty);
                cartaoEquipamentoDTO.Instance = ((!dataReader["Instance"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["Instance"]) : (short)0);
                cartaoEquipamentoDTO.StreetNo = ((!dataReader["StreetNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["StreetNo"]) : string.Empty);
            }
            return cartaoEquipamentoDTO;
        }

        private CartaoEquipamentoDTO ObjetoCartaoEquipamentoHanaDTO(string query, HanaConexao conexaoHana)
        {
            CartaoEquipamentoDTO cartaoEquipamentoDTO = new CartaoEquipamentoDTO();
            DataTable dt = conexaoHana.ExecuteDataTable(query);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataReader in dt.Rows)
                {
                    cartaoEquipamentoDTO.insID = ((!dataReader["insID"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["insID"]) : 0);
                    cartaoEquipamentoDTO.customer = ((!dataReader["customer"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["customer"]) : string.Empty);
                    cartaoEquipamentoDTO.custmrName = ((!dataReader["custmrName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["custmrName"]) : string.Empty);
                    cartaoEquipamentoDTO.contactCod = ((!dataReader["contactCod"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contactCod"]) : 0);
                    cartaoEquipamentoDTO.directCsmr = ((!dataReader["directCsmr"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["directCsmr"]) : string.Empty);
                    cartaoEquipamentoDTO.drctCsmNam = ((!dataReader["drctCsmNam"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["drctCsmNam"]) : string.Empty);
                    cartaoEquipamentoDTO.manufSN = ((!dataReader["manufSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["manufSN"]) : string.Empty);
                    cartaoEquipamentoDTO.internalSN = ((!dataReader["internalSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["internalSN"]) : string.Empty);
                    cartaoEquipamentoDTO.warranty = ((!dataReader["warranty"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["warranty"]) : char.MinValue);
                    cartaoEquipamentoDTO.wrrntyStrt = ((!dataReader["wrrntyStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyStrt"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.wrrntyEnd = ((!dataReader["wrrntyEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyEnd"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.responsVal = ((!dataReader["responsVal"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["responsVal"]) : (short)0);
                    cartaoEquipamentoDTO.responsUnt = ((!dataReader["responsUnt"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["responsUnt"]) : char.MinValue);
                    cartaoEquipamentoDTO.itemCode = ((!dataReader["itemCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemCode"]) : string.Empty);
                    cartaoEquipamentoDTO.itemName = ((!dataReader["itemName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemName"]) : string.Empty);
                    cartaoEquipamentoDTO.itemGroup = ((!dataReader["itemGroup"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["itemGroup"]) : (short)0);
                    cartaoEquipamentoDTO.manufDate = ((!dataReader["manufDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["manufDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.delivery = ((!dataReader["delivery"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["delivery"]) : 0);
                    cartaoEquipamentoDTO.deliveryNo = ((!dataReader["deliveryNo"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["deliveryNo"]) : 0);
                    cartaoEquipamentoDTO.invoice = ((!dataReader["invoice"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoice"]) : 0);
                    cartaoEquipamentoDTO.invoiceNum = ((!dataReader["invoiceNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoiceNum"]) : 0);
                    cartaoEquipamentoDTO.dlvryDate = ((!dataReader["dlvryDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["dlvryDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.cntctPhone = ((!dataReader["cntctPhone"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["cntctPhone"]) : string.Empty);
                    cartaoEquipamentoDTO.street = ((!dataReader["street"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["street"]) : string.Empty);
                    cartaoEquipamentoDTO.block = ((!dataReader["block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["block"]) : string.Empty);
                    cartaoEquipamentoDTO.zip = ((!dataReader["zip"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["zip"]) : string.Empty);
                    cartaoEquipamentoDTO.city = ((!dataReader["city"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["city"]) : string.Empty);
                    cartaoEquipamentoDTO.county = ((!dataReader["county"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["county"]) : string.Empty);
                    cartaoEquipamentoDTO.country = ((!dataReader["country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["country"]) : string.Empty);
                    cartaoEquipamentoDTO.state = ((!dataReader["state"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["state"]) : string.Empty);
                    cartaoEquipamentoDTO.instLction = ((!dataReader["instLction"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["instLction"]) : string.Empty);
                    cartaoEquipamentoDTO.contract = ((!dataReader["contract"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contract"]) : 0);
                    cartaoEquipamentoDTO.cntrctStrt = ((!dataReader["cntrctStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctStrt"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.cntrctEnd = ((!dataReader["cntrctEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctEnd"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.attachment = ((!dataReader["attachment"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["attachment"]) : string.Empty);
                    cartaoEquipamentoDTO.objType = ((!dataReader["objType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["objType"]) : string.Empty);
                    cartaoEquipamentoDTO.logInstanc = ((!dataReader["logInstanc"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["logInstanc"]) : 0);
                    cartaoEquipamentoDTO.userSign = ((!dataReader["userSign"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign"]) : (short)0);
                    cartaoEquipamentoDTO.createDate = ((!dataReader["createDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["createDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.userSign2 = ((!dataReader["userSign2"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign2"]) : (short)0);
                    cartaoEquipamentoDTO.updateDate = ((!dataReader["updateDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["updateDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.Building = ((!dataReader["Building"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Building"]) : string.Empty);
                    cartaoEquipamentoDTO.status = ((!dataReader["status"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["status"]) : char.MinValue);
                    cartaoEquipamentoDTO.replcIns = ((!dataReader["replcIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["replcIns"]) : 0);
                    cartaoEquipamentoDTO.repByIns = ((!dataReader["repByIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["repByIns"]) : 0);
                    cartaoEquipamentoDTO.technician = ((!dataReader["technician"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["technician"]) : 0);
                    cartaoEquipamentoDTO.territory = ((!dataReader["territory"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["territory"]) : 0);
                    cartaoEquipamentoDTO.AtcEntry = ((!dataReader["AtcEntry"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["AtcEntry"]) : 0);
                    cartaoEquipamentoDTO.Transfered = ((!dataReader["Transfered"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Transfered"]) : char.MinValue);
                    cartaoEquipamentoDTO.AddrType = ((!dataReader["AddrType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddrType"]) : string.Empty);
                    cartaoEquipamentoDTO.Instance = ((!dataReader["Instance"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["Instance"]) : (short)0);
                    cartaoEquipamentoDTO.StreetNo = ((!dataReader["StreetNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["StreetNo"]) : string.Empty);
                }
            }
            return cartaoEquipamentoDTO;
        }

        public IList<CartaoEquipamentoDTO> Listar()
        {
            string tipoBD = ConfigurationManager.AppSettings["TipoBD"].ToString();

            string tSQLBase = $@"SELECT ""insID"", ""customer"", ""custmrName"", ""contactCod"", ""directCsmr"", ""drctCsmNam"", ""manufSN"", ""internalSN"", ""warranty"", ""wrrntyStrt"", ""wrrntyEnd"", ""responsVal"", ""responsUnt"", ""itemCode"", ""itemName"", ""itemGroup"", ""manufDate"", ""delivery"", ""deliveryNo"", ""invoice"", ""invoiceNum"", ""dlvryDate"", ""cntctPhone"", ""street"", ""block"", ""zip"", ""city"", ""county"", ""country"", ""state"", ""instLction"", ""contract"", ""cntrctStrt"", ""cntrctEnd"", ""attachment"", ""objType"", ""logInstanc"", ""userSign"", ""createDate"", ""userSign2"", ""updateDate"", ""Building"", ""status"", ""replcIns"", ""repByIns"", ""technician"", ""territory"", ""AtcEntry"", ""Transfered"", ""AddrType"", ""Instance"", ""StreetNo"" FROM OINS ";
            if (tipoBD == "Hana")
            {
                HanaConexao conexaoHana = new HanaConexao();

                try
                {
                    conexaoHana.Connection();

                    return RetornarVariosHana(tSQLBase, conexaoHana);
                }
                catch (Exception er)
                {
                    throw new Exception(er.Message);
                }
                finally
                {
                    conexaoHana.Dispose();
                }
            }
            else
            {

                StringBuilder stb = new StringBuilder();
                stb.Append(tSQLBase);
                SqlServerConexao conexao = new SqlServerConexao();
                SqlCommand cmd = new SqlCommand(stb.ToString(), conexao.Conexao);

                try
                {
                    conexao.Conectar();

                    return RetornarVarios(ref cmd);
                }
                catch (SqlException er)
                {
                    throw new Exception(er.Message);
                }
                finally
                {
                    conexao.Desconectar();
                }
            }
        }

        private IList<CartaoEquipamentoDTO> RetornarVarios(ref SqlCommand cmd)
        {
            SqlDataReader dataReader = cmd.ExecuteReader();

            IList<CartaoEquipamentoDTO> listCartaoEquipamento = new List<CartaoEquipamentoDTO>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    CartaoEquipamentoDTO cartaoEquipamentoDTO = new CartaoEquipamentoDTO();
                    cartaoEquipamentoDTO.insID = ((!dataReader["insID"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["insID"]) : 0);
                    cartaoEquipamentoDTO.customer = ((!dataReader["customer"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["customer"]) : string.Empty);
                    cartaoEquipamentoDTO.custmrName = ((!dataReader["custmrName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["custmrName"]) : string.Empty);
                    cartaoEquipamentoDTO.contactCod = ((!dataReader["contactCod"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contactCod"]) : 0);
                    cartaoEquipamentoDTO.directCsmr = ((!dataReader["directCsmr"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["directCsmr"]) : string.Empty);
                    cartaoEquipamentoDTO.drctCsmNam = ((!dataReader["drctCsmNam"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["drctCsmNam"]) : string.Empty);
                    cartaoEquipamentoDTO.manufSN = ((!dataReader["manufSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["manufSN"]) : string.Empty);
                    cartaoEquipamentoDTO.internalSN = ((!dataReader["internalSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["internalSN"]) : string.Empty);
                    cartaoEquipamentoDTO.warranty = ((!dataReader["warranty"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["warranty"]) : char.MinValue);
                    cartaoEquipamentoDTO.wrrntyStrt = ((!dataReader["wrrntyStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyStrt"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.wrrntyEnd = ((!dataReader["wrrntyEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyEnd"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.responsVal = ((!dataReader["responsVal"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["responsVal"]) : (short)0);
                    cartaoEquipamentoDTO.responsUnt = ((!dataReader["responsUnt"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["responsUnt"]) : char.MinValue);
                    cartaoEquipamentoDTO.itemCode = ((!dataReader["itemCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemCode"]) : string.Empty);
                    cartaoEquipamentoDTO.itemName = ((!dataReader["itemName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemName"]) : string.Empty);
                    cartaoEquipamentoDTO.itemGroup = ((!dataReader["itemGroup"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["itemGroup"]) : (short)0);
                    cartaoEquipamentoDTO.manufDate = ((!dataReader["manufDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["manufDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.delivery = ((!dataReader["delivery"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["delivery"]) : 0);
                    cartaoEquipamentoDTO.deliveryNo = ((!dataReader["deliveryNo"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["deliveryNo"]) : 0);
                    cartaoEquipamentoDTO.invoice = ((!dataReader["invoice"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoice"]) : 0);
                    cartaoEquipamentoDTO.invoiceNum = ((!dataReader["invoiceNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoiceNum"]) : 0);
                    cartaoEquipamentoDTO.dlvryDate = ((!dataReader["dlvryDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["dlvryDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.cntctPhone = ((!dataReader["cntctPhone"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["cntctPhone"]) : string.Empty);
                    cartaoEquipamentoDTO.street = ((!dataReader["street"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["street"]) : string.Empty);
                    cartaoEquipamentoDTO.block = ((!dataReader["block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["block"]) : string.Empty);
                    cartaoEquipamentoDTO.zip = ((!dataReader["zip"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["zip"]) : string.Empty);
                    cartaoEquipamentoDTO.city = ((!dataReader["city"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["city"]) : string.Empty);
                    cartaoEquipamentoDTO.county = ((!dataReader["county"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["county"]) : string.Empty);
                    cartaoEquipamentoDTO.country = ((!dataReader["country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["country"]) : string.Empty);
                    cartaoEquipamentoDTO.state = ((!dataReader["state"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["state"]) : string.Empty);
                    cartaoEquipamentoDTO.instLction = ((!dataReader["instLction"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["instLction"]) : string.Empty);
                    cartaoEquipamentoDTO.contract = ((!dataReader["contract"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contract"]) : 0);
                    cartaoEquipamentoDTO.cntrctStrt = ((!dataReader["cntrctStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctStrt"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.cntrctEnd = ((!dataReader["cntrctEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctEnd"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.attachment = ((!dataReader["attachment"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["attachment"]) : string.Empty);
                    cartaoEquipamentoDTO.objType = ((!dataReader["objType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["objType"]) : string.Empty);
                    cartaoEquipamentoDTO.logInstanc = ((!dataReader["logInstanc"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["logInstanc"]) : 0);
                    cartaoEquipamentoDTO.userSign = ((!dataReader["userSign"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign"]) : (short)0);
                    cartaoEquipamentoDTO.createDate = ((!dataReader["createDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["createDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.userSign2 = ((!dataReader["userSign2"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign2"]) : (short)0);
                    cartaoEquipamentoDTO.updateDate = ((!dataReader["updateDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["updateDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.Building = ((!dataReader["Building"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Building"]) : string.Empty);
                    cartaoEquipamentoDTO.status = ((!dataReader["status"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["status"]) : char.MinValue);
                    cartaoEquipamentoDTO.replcIns = ((!dataReader["replcIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["replcIns"]) : 0);
                    cartaoEquipamentoDTO.repByIns = ((!dataReader["repByIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["repByIns"]) : 0);
                    cartaoEquipamentoDTO.technician = ((!dataReader["technician"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["technician"]) : 0);
                    cartaoEquipamentoDTO.territory = ((!dataReader["territory"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["territory"]) : 0);
                    cartaoEquipamentoDTO.AtcEntry = ((!dataReader["AtcEntry"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["AtcEntry"]) : 0);
                    cartaoEquipamentoDTO.Transfered = ((!dataReader["Transfered"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Transfered"]) : char.MinValue);
                    cartaoEquipamentoDTO.AddrType = ((!dataReader["AddrType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddrType"]) : string.Empty);
                    cartaoEquipamentoDTO.Instance = ((!dataReader["Instance"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["Instance"]) : (short)0);
                    cartaoEquipamentoDTO.StreetNo = ((!dataReader["StreetNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["StreetNo"]) : string.Empty);

                    listCartaoEquipamento.Add(cartaoEquipamentoDTO);
                }
            }

            dataReader.Close();
            dataReader.Dispose();

            return listCartaoEquipamento;
        }

        private IList<CartaoEquipamentoDTO> RetornarVariosHana(string query, HanaConexao conexaoHana)
        {
            DataTable dt = conexaoHana.ExecuteDataTable(query);
            IList<CartaoEquipamentoDTO> listCartaoEquipamento = new List<CartaoEquipamentoDTO>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataReader in dt.Rows)
                {
                    CartaoEquipamentoDTO cartaoEquipamentoDTO = new CartaoEquipamentoDTO();
                    cartaoEquipamentoDTO.insID = ((!dataReader["insID"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["insID"]) : 0);
                    cartaoEquipamentoDTO.customer = ((!dataReader["customer"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["customer"]) : string.Empty);
                    cartaoEquipamentoDTO.custmrName = ((!dataReader["custmrName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["custmrName"]) : string.Empty);
                    cartaoEquipamentoDTO.contactCod = ((!dataReader["contactCod"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contactCod"]) : 0);
                    cartaoEquipamentoDTO.directCsmr = ((!dataReader["directCsmr"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["directCsmr"]) : string.Empty);
                    cartaoEquipamentoDTO.drctCsmNam = ((!dataReader["drctCsmNam"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["drctCsmNam"]) : string.Empty);
                    cartaoEquipamentoDTO.manufSN = ((!dataReader["manufSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["manufSN"]) : string.Empty);
                    cartaoEquipamentoDTO.internalSN = ((!dataReader["internalSN"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["internalSN"]) : string.Empty);
                    cartaoEquipamentoDTO.warranty = ((!dataReader["warranty"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["warranty"]) : char.MinValue);
                    cartaoEquipamentoDTO.wrrntyStrt = ((!dataReader["wrrntyStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyStrt"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.wrrntyEnd = ((!dataReader["wrrntyEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["wrrntyEnd"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.responsVal = ((!dataReader["responsVal"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["responsVal"]) : (short)0);
                    cartaoEquipamentoDTO.responsUnt = ((!dataReader["responsUnt"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["responsUnt"]) : char.MinValue);
                    cartaoEquipamentoDTO.itemCode = ((!dataReader["itemCode"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemCode"]) : string.Empty);
                    cartaoEquipamentoDTO.itemName = ((!dataReader["itemName"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["itemName"]) : string.Empty);
                    cartaoEquipamentoDTO.itemGroup = ((!dataReader["itemGroup"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["itemGroup"]) : (short)0);
                    cartaoEquipamentoDTO.manufDate = ((!dataReader["manufDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["manufDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.delivery = ((!dataReader["delivery"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["delivery"]) : 0);
                    cartaoEquipamentoDTO.deliveryNo = ((!dataReader["deliveryNo"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["deliveryNo"]) : 0);
                    cartaoEquipamentoDTO.invoice = ((!dataReader["invoice"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoice"]) : 0);
                    cartaoEquipamentoDTO.invoiceNum = ((!dataReader["invoiceNum"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["invoiceNum"]) : 0);
                    cartaoEquipamentoDTO.dlvryDate = ((!dataReader["dlvryDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["dlvryDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.cntctPhone = ((!dataReader["cntctPhone"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["cntctPhone"]) : string.Empty);
                    cartaoEquipamentoDTO.street = ((!dataReader["street"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["street"]) : string.Empty);
                    cartaoEquipamentoDTO.block = ((!dataReader["block"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["block"]) : string.Empty);
                    cartaoEquipamentoDTO.zip = ((!dataReader["zip"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["zip"]) : string.Empty);
                    cartaoEquipamentoDTO.city = ((!dataReader["city"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["city"]) : string.Empty);
                    cartaoEquipamentoDTO.county = ((!dataReader["county"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["county"]) : string.Empty);
                    cartaoEquipamentoDTO.country = ((!dataReader["country"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["country"]) : string.Empty);
                    cartaoEquipamentoDTO.state = ((!dataReader["state"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["state"]) : string.Empty);
                    cartaoEquipamentoDTO.instLction = ((!dataReader["instLction"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["instLction"]) : string.Empty);
                    cartaoEquipamentoDTO.contract = ((!dataReader["contract"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["contract"]) : 0);
                    cartaoEquipamentoDTO.cntrctStrt = ((!dataReader["cntrctStrt"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctStrt"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.cntrctEnd = ((!dataReader["cntrctEnd"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["cntrctEnd"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.attachment = ((!dataReader["attachment"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["attachment"]) : string.Empty);
                    cartaoEquipamentoDTO.objType = ((!dataReader["objType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["objType"]) : string.Empty);
                    cartaoEquipamentoDTO.logInstanc = ((!dataReader["logInstanc"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["logInstanc"]) : 0);
                    cartaoEquipamentoDTO.userSign = ((!dataReader["userSign"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign"]) : (short)0);
                    cartaoEquipamentoDTO.createDate = ((!dataReader["createDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["createDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.userSign2 = ((!dataReader["userSign2"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["userSign2"]) : (short)0);
                    cartaoEquipamentoDTO.updateDate = ((!dataReader["updateDate"].Equals(DBNull.Value)) ? Convert.ToDateTime(dataReader["updateDate"]) : DateTime.MinValue);
                    cartaoEquipamentoDTO.Building = ((!dataReader["Building"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["Building"]) : string.Empty);
                    cartaoEquipamentoDTO.status = ((!dataReader["status"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["status"]) : char.MinValue);
                    cartaoEquipamentoDTO.replcIns = ((!dataReader["replcIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["replcIns"]) : 0);
                    cartaoEquipamentoDTO.repByIns = ((!dataReader["repByIns"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["repByIns"]) : 0);
                    cartaoEquipamentoDTO.technician = ((!dataReader["technician"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["technician"]) : 0);
                    cartaoEquipamentoDTO.territory = ((!dataReader["territory"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["territory"]) : 0);
                    cartaoEquipamentoDTO.AtcEntry = ((!dataReader["AtcEntry"].Equals(DBNull.Value)) ? Convert.ToInt32(dataReader["AtcEntry"]) : 0);
                    cartaoEquipamentoDTO.Transfered = ((!dataReader["Transfered"].Equals(DBNull.Value)) ? Convert.ToChar(dataReader["Transfered"]) : char.MinValue);
                    cartaoEquipamentoDTO.AddrType = ((!dataReader["AddrType"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["AddrType"]) : string.Empty);
                    cartaoEquipamentoDTO.Instance = ((!dataReader["Instance"].Equals(DBNull.Value)) ? Convert.ToInt16(dataReader["Instance"]) : (short)0);
                    cartaoEquipamentoDTO.StreetNo = ((!dataReader["StreetNo"].Equals(DBNull.Value)) ? Convert.ToString(dataReader["StreetNo"]) : string.Empty);

                    listCartaoEquipamento.Add(cartaoEquipamentoDTO);
                }
            }

            return listCartaoEquipamento;
        }
    }
}