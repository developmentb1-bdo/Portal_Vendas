using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.IDAL.Compras
{
    public interface ICompra
    {
        double RetornarValorCompras(DateTime dataInicial, DateTime dataFinal);
    }
}
