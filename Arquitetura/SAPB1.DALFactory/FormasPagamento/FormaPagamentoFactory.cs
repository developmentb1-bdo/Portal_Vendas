using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.FormasPagamento;
using System.Reflection;

namespace SAPB1.DALFactory.FormasPagamento
{
    public class FormaPagamentoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".FormasPagamento";

        public static IFormaPagamento FormaPagamentoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".FormaPagamentoDAL";
            return (IFormaPagamento)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
