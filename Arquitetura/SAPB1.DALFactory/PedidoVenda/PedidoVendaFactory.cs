using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.DALFactory.PedidoVenda
{
    public class PedidoVendaFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".PedidoVenda";

        public static IPedidoVenda PedidoVendaDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".PedidoVendaDAL";
            return (IPedidoVenda)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
