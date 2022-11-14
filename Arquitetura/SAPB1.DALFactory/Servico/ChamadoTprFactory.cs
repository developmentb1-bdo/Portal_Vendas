using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using SAPB1.IDAL.Servico;

namespace SAPB1.DALFactory.Servico
{
    public class ChamadoTprFactory
    {
        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static IChamadoTpr ChamadoTprDAL()
        {
            string nameSpace = arquivo + pasta + ".ChamadoTprDAL";
            return (IChamadoTpr)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}
