using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.DALFactory.PedidoVenda
{
    public class ItemCotacaoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".PedidoVenda";

        public static IItemCotacao ItemCotacaoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".CotacaoItemDAL";
            return (IItemCotacao)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
