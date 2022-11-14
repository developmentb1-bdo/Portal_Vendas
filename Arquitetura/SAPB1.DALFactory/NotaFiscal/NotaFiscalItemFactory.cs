using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.NotaFiscal;
using System.Reflection;

namespace SAPB1.DALFactory.NotaFiscal
{
    public class NotaFiscalItemFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".NotaFiscal";

        public static INotaFiscalItem NotaFiscalItemDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".NotaFiscalItemDAL";
            return (INotaFiscalItem)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
