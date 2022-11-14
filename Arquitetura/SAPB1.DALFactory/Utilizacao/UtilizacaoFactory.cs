using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Utilizacao;
using System.Reflection;

namespace SAPB1.DALFactory.Utilizacao
{
    public class UtilizacaoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Utilizacao";

        public static IUtilizacao UtilizacaoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".UtilizacaoDAL";
            return (IUtilizacao)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
