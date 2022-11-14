using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Municipio;
using System.Reflection;

namespace SAPB1.DALFactory.Municipio
{
    public class MunicipioFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Municipio";

        public static IMunicipio MunicipioDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".MunicipioDAL";
            return (IMunicipio)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
