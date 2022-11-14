using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.TabelaPreco;
using System.Configuration;
using System.Reflection;

namespace SAPB1.DALFactory.TabelaPreco
{
    public class TabelaPrecoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".TabelaPreco";

        /// <summary>
        /// Interface da classe TabelaPrecoDAL
        /// </summary>
        /// <returns>Interace da classe TabelaPrecoDAL</returns>
        public static ITabelaPreco TabelaPrecoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".TabelaPrecoDAL";
            return (ITabelaPreco)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
