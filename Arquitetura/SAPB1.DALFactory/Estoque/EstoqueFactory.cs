using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Estoque;
using System.Reflection;

namespace SAPB1.DALFactory.Estoque
{
    public class EstoqueFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Estoque";

        public static IEstoque EstoqueDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EstoqueDAL";
            return (IEstoque)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IEstoqueConsulta EstoqueConsultaDal()
        {
            string nomeClasse = arquivo + nameSpace + ".EstoqueDAL2";
            return (IEstoqueConsulta)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IDeposito DepositoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".DepositoDAL";
            return (IDeposito)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
