using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.TiposEnvio;
using System.Reflection;

namespace SAPB1.DALFactory.TiposEnvio
{
    public class TipoEnvioFacytory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".TiposEnvio";

        public static ITipoEnvio TipoEnvioDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".TipoEnvioDAL";
            return (ITipoEnvio)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
