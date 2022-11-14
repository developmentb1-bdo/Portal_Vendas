using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.EstruturaItem;
using System.Reflection;

namespace SAPB1.DALFactory.EstruturaItem
{
    public class EstruturaItemFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".EstruturaItem";

        public static IEstruturaItem EstruturaItemDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EstruturaItemDAL";
            return (IEstruturaItem)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
