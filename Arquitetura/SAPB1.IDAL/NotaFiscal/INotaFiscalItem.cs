using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.NotaFiscal;

namespace SAPB1.IDAL.NotaFiscal
{
    public interface INotaFiscalItem
    {
        IList<NotaFiscalItemDTO> ObterNotasFiscaisPorPedidoVenda(string codPedido);   
    }
}
