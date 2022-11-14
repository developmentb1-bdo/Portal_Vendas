using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Item;
using System.Reflection;

namespace SAPB1.DALFactory.Item
{
    public class ItemFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Item";

        public static IItem ItemDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ItemDAL";
            return (IItem)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
