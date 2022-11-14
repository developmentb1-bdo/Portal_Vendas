using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Territorio;
using System.Reflection;

namespace SAPB1.DALFactory.Territorio
{
    public class TerritorioFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Territorio";

        public static ITerritorio TerritorioDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".TerritorioDAL";
            return (ITerritorio)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
