using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Concessionario;
using System.Reflection;

namespace SAPB1.DALFactory.Concessionario
{
    public class ConcessionarioFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Concessionario";

        public static IConcessionario ConcessionarioDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ConcessionarioDAL";
            return (IConcessionario)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
