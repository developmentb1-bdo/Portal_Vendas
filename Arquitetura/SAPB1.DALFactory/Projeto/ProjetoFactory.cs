using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Projeto;
using System.Reflection;

namespace SAPB1.DALFactory.Projeto
{
    public class ProjetoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Projeto";

        public static IProjeto ProjetoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ProjetoDAL";
            return (IProjeto)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
