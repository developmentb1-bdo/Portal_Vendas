using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Empresa.Filial;
using System.Reflection;

namespace SAPB1.DALFactory.Empresa.Filial
{
    public class FilialFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Empresa.Filial";

        public static IFilial FilialDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".FilialDAL";
            return (IFilial)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
