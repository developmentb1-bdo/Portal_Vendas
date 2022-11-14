using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.DeskPararelo.Estoque;
using System.Reflection;

namespace SAPB1.DALFactory.DeskPararelo.Estoque
{
    public class EstoqueDadosFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".DeskPararelo.Estoque";

        public static IEstoqueDados EstoqueDadosDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".EstoqueDadosDAL";
            return (IEstoqueDados)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
