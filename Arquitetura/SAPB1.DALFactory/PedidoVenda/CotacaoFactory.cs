/*
 * @author Victor Oliveira.
 */

using System.Configuration;
using System.Reflection;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.DALFactory.PedidoVenda
{
    public static class CotacaoFactory
    {
        static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"];
        static readonly string pasta = ".PedidoVenda";

        public static ICotacao CotacaoDAL()
        {
            string nameSpace = (arquivo + pasta + ".CotacaoDAL");
            return (ICotacao)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}