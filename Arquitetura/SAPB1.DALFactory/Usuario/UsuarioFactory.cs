using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Usuario;
using System.Reflection;
namespace SAPB1.DALFactory.Usuario
{
    public class UsuarioFactory
    {
        readonly static string arquivo = System.Configuration.ConfigurationManager.AppSettings["CamadaDAL"];
        readonly static string nameSpace = ".Usuario";

        public static IUsuario UsuarioDAL()
        {
            string nomeClasse = arquivo + nameSpace + ".UsuarioDAL";
            return (IUsuario)Assembly.Load(arquivo).CreateInstance(nomeClasse);
        }
    }
}
