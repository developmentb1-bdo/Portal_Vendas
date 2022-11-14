using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.GrupoItem;
using System.Reflection;

namespace SAPB1.DALFactory.GrupoItem
{
    public class GrupoItemFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".GrupoItem";

        public static IGrupoItem GrupoItemDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".GrupoItemDAL";
            return (IGrupoItem)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
