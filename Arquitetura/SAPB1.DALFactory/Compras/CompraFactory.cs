using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Compras;
using System.Reflection;

namespace SAPB1.DALFactory.Compras
{
    public class CompraFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Compras";

        public static ICompra CompraDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ComprasDAL";
            return (ICompra)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
