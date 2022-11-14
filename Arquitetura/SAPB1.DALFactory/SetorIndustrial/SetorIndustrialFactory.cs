using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.SetorIndustrial;
using System.Reflection;

namespace SAPB1.DALFactory.SetorIndustrial
{
    public class SetorIndustrialFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".SetorIndustrial";

        public static ISetorIndustrial SetorIndustrialDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".SetorIndustrialDAL";
            return (ISetorIndustrial)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
