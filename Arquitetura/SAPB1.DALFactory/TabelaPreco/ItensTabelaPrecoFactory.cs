using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.ItensTabelaPreco;
using System.Reflection;

namespace SAPB1.DALFactory.ItensTabelaPreco
{
    public class ItensTabelaPrecoFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".ItensTabelaPreco";

        /// <summary>
        /// Interface da classe ItensTabelaPrecoDAL
        /// </summary>
        /// <returns>Interface(IDAL) da classe ItensTabelaPrecoDAL</returns>
        public static IItensTabelaPreco ItensTabelaPrecoDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".ItensTabelaPrecoDAL";
            return (IItensTabelaPreco)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
