using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.DALFactory.PedidoVenda
{
    public class EnderecoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".PedidoVenda";

        public static IEndereco EnderecoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EnderecoDAL";
            return (IEndereco)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
