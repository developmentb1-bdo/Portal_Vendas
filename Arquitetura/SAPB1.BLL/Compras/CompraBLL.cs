using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Compras;
using SAPB1.DALFactory.Compras;

namespace SAPB1.BLL.Compras
{
    public class CompraBLL
    {
        
        public double RetornarValorCompras(DateTime dataInicial, DateTime dataFinal)
        {
            ICompra compraDAL = CompraFactory.CompraDAL();

            return compraDAL.RetornarValorCompras(dataInicial, dataFinal);
        }
    }
}
