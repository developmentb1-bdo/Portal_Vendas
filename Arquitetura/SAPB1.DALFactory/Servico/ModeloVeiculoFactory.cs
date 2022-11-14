using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using System.Reflection;

namespace SAPB1.DALFactory.Servico
{
    public class ModeloVeiculoFactory
    {
        private static readonly string arquivo = ConfigurationManager.AppSettings["CamadaDAL"].ToString();
        private static readonly string pasta = ".Servico";

        public static IModeloVeiculo ModeloVeiculoDAL()
        {
            string nameSpace = arquivo + pasta + ".ModeloVeiculoDAL";
            return (IModeloVeiculo)Assembly.Load(arquivo).CreateInstance(nameSpace);
        }
    }
}
