using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using System.Configuration;
using System.Reflection;

namespace SAPB1.DALFactory.Servico
{
    public class OpcaoTipoGarantiaFactory
    {
        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static IOpcaoTipoGarantia OpcaoTipoGarantiaDAL()
        {
            string nameSpace = arquivo + pasta + ".OpcaoTipoGarantiaDAL";
            return (IOpcaoTipoGarantia)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}
