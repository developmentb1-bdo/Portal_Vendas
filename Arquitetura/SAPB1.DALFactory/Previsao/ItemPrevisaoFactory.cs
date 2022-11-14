using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Previsao;
using System.Reflection;

namespace SAPB1.DALFactory.Previsao
{
    public class ItemPrevisaoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Previsao";

        public static IItemPrevisao ItemPrevisaoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ItemPrevisaoDAL";
            return (IItemPrevisao)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
