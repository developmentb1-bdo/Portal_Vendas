using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoVenda;
using System.Reflection;

namespace SAPB1.DALFactory.PedidoVenda
{
    public class ItemVendaFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".PedidoVenda";

        public static IItemVenda ItemVendaDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ItemVendaDAL";
            return (IItemVenda)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
