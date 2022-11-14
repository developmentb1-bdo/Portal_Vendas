using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Representante;
using System.Reflection;

namespace SAPB1.DALFactory.Representante
{
    public class RepresentanteFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Representante";

        public static IRepresentante RepresentanteDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".RepresentanteDAL";
            return (IRepresentante)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
