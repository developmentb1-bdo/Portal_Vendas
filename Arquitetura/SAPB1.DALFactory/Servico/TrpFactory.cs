using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using SAPB1.IDAL.Servico;

namespace SAPB1.DALFactory.Servico
{
    public class TrpFactory
    {
        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static ITpr TprDAL()
        {
            string nameSpace = arquivo + pasta + ".TprDAL";
            return (ITpr)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}
