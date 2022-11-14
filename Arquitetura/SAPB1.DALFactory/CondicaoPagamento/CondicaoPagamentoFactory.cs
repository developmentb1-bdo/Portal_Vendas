using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SAPB1.IDAL.CondicaoPagamento;

namespace SAPB1.DALFactory.CondicaoPagamento
{
    public class CondicaoPagamentoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".CondicaoPagamento";

        public static ICondicaoPagamento CondicaoPagamentoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".CondicaoPagamentoDAL";
            return (ICondicaoPagamento)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
