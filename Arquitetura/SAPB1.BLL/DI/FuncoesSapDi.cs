using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SAPbobsCOM;
using System.Configuration;

namespace SAPB1.BLL.DI
{
    public class FuncoesSapDi
    {
        //public static SAPbobsCOM.Company _oCompany;
       
        public static string ConnectarDi()
        {
            //_oCompany = new Company();
            //_oCompany.Server = System.Configuration.ConfigurationManager.AppSettings["ServerSql"].ToString();
            //_oCompany.UserName = System.Configuration.ConfigurationManager.AppSettings["UsuarioSap"].ToString();
            //_oCompany.Password = System.Configuration.ConfigurationManager.AppSettings["SenhaSap"].ToString();
            //_oCompany.LicenseServer = System.Configuration.ConfigurationManager.AppSettings["LicencaServerSap"].ToString();
            //_oCompany.CompanyDB = System.Configuration.ConfigurationManager.AppSettings["BancoSap"].ToString();
            //_oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
            //_oCompany.language = BoSuppLangs.ln_Portuguese_Br;

            //if (_oCompany.Connected)
            //    return "0";
            //else
            //{
            //    int retorno = _oCompany.Connect();

            //    if (retorno != 0)
            //    {
            //        string erroDi = "";
            //        _oCompany.GetLastError(out retorno, out erroDi);

            //        return "Erro: " + erroDi;
            //    }
            //    else
            //        return "0";
            //}

            return "";
        }

        public static string DesConnDI()
        {

            //try
            //{
            //    _oCompany.Disconnect();
            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(_oCompany);
            //    return "0";
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
            //finally
            //{
            //    _oCompany = null;
            //}

            return "";
        }
    }
}
