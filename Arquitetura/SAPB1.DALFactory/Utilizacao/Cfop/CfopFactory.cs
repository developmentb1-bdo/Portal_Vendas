using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Utilizacao.Cfop;
using System.Reflection;

namespace SAPB1.DALFactory.Utilizacao.Cfop
{
    public class CfopFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Utilizacao.Cfop";

        public static ICfop ICfopDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".CfopDAL";
            return (ICfop)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
