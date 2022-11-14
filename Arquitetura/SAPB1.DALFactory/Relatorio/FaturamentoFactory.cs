using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Relatorio;
using System.Reflection;

namespace SAPB1.DALFactory.Relatorio
{
    public class FaturamentoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Relatorio";

        public static IFaturamento FaturamentoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".FaturamentoDAL";
            return (IFaturamento)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
