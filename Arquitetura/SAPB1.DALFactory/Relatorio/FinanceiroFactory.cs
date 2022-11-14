using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Relatorio;
using System.Reflection;

namespace SAPB1.DALFactory.Relatorio
{
    public class FinanceiroFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Relatorio";

        public static IFinanceiro FinanceiroDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".FinanceiroDAL";
            return (IFinanceiro)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
