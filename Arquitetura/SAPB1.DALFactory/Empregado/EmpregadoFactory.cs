using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Empregado;
using System.Reflection;

namespace SAPB1.DALFactory.Empregado
{
    public class EmpregadoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Empregado";

        public static IEmpregado EmpregadoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EmpregadoDAL";
            return (IEmpregado)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
