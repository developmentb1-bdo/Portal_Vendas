using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Funcionario.Vendedor;
using System.Reflection;

namespace SAPB1.DALFactory.Funcionario.Vendedor
{
    public class VendedorFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Funcionario.Vendedor";

        public static IVendedor VendedorDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".VendedorDAL";
            return (IVendedor)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
