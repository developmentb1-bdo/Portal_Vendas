using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Funcionario;
using System.Reflection;

namespace SAPB1.DALFactory.Funcionario
{
    public class ColaboradorFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Funcionario";

        public static IColaborador ColaboradorDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ColaboradorDAL";
            return (IColaborador)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
