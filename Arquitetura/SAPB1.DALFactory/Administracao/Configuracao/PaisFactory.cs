using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Administracao.Configuracao;
using System.Reflection;

namespace SAPB1.DALFactory.Administracao.Configuracao
{
    public class PaisFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Administracao.Configuracao";

        public static IPais PaisDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".PaisDAL";
            return (IPais)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
