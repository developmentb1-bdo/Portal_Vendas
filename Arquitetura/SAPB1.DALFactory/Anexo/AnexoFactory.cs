using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SAPB1.IDAL.Anexo;

namespace SAPB1.DALFactory.Anexo
{
    public class AnexoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Anexo";

        public static IAnexo AnexoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".AnexoDAL";
            return (IAnexo)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
