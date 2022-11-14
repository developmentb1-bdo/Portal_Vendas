using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoPeca;
using System.Reflection;

namespace SAPB1.DALFactory.PedidoPeca
{
    public class ItemPecaFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".PedidoPeca";

        public static IItemPeca ItemPecaDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ItemPecaDAL";
            return (IItemPeca)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
