using SAPB1.IDAL.Servico;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SAPB1.DALFactory.Servico
{
    public class TransacaoFactory
    {
        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static ITransacao TransacaoDAL()
        {
            string nameSpace = arquivo + pasta + ".TransacaoDAL";
            return (ITransacao)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}
