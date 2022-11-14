/*
 * @author Victor Oliveira.
 */

using System.Configuration;
using System.Reflection;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.DALFactory.ParceiroNegocio
{
    public static class ParceiroNegocioFactory
    {
        readonly static string arquivo = ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".ParceiroNegocio";

        public static IContato ContatoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ContatoDAL";
            return (IContato)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IEndereco EnderecoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EnderecoDAL";
            return (IEndereco)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IGrupo GrupoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".GrupoDAL";
            return (IGrupo)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IIdentificacaoFiscal IdentificacaoFiscalDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".IdentificacaoFiscalDAL";
            return (IIdentificacaoFiscal)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IMoeda MoedaDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".MoedaDAL";
            return (IMoeda)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }

        public static IParceiroNegocio ParceiroNegocioDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ParceiroNegocioDAL";
            return (IParceiroNegocio)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}