/*
 * @author Victor Oliveira.
 */

using System.Configuration;
using System.Reflection;
using SAPB1.IDAL.Servico;

namespace SAPB1.DALFactory.Servico
{
    public static class ServicoFactory
    {
        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static ICartaoEquipamento CartaoEquipamentoDAL()
        {
            string nameSpace = arquivo + pasta + ".CartaoEquipamentoDAL";
            return (ICartaoEquipamento)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}