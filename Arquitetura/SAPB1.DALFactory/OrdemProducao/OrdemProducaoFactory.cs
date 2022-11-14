using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.OrdemProducao;
using System.Reflection;

namespace SAPB1.DALFactory.OrdemProducao
{
    public class OrdemProducaoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".OrdemProducao";

        public static IOrdemProducao OrdemProducaoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".OrdemProducaoDAL";
            return (IOrdemProducao)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
