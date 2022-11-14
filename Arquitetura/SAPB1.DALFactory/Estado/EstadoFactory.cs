using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Estado;
using System.Reflection;

namespace SAPB1.DALFactory.Estado
{
    public class EstadoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Estado";

        public static IEstado EstadoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EstadoDAL";
            return (IEstado)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
