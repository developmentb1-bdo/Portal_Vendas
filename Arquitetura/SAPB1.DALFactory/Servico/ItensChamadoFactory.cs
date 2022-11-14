using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using System.Reflection;
using System.Configuration;

namespace SAPB1.DALFactory.Servico
{
    public class ItensChamadoFactory
    {

        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static IItemChamadoServico ItensChamadoServicoDAL()
        {
            string nameSpace = arquivo + pasta + ".ItemChamadoServicoDAL";
            return (IItemChamadoServico)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}
