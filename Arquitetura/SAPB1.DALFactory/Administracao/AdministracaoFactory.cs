/*
 * @author Victor Oliveira.
 */

using System.Configuration;
using System.Reflection;
using SAPB1.IDAL.Administracao;

namespace SAPB1.DALFactory.Administracao
{
    public static class AdministracaoFactory
    {
        readonly static string arquivo = ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Administracao";

        public static IFilial FilialDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".FilialDAL";
            return (IFilial)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}