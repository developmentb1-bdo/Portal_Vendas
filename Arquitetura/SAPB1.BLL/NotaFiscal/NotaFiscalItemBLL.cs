using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.NotaFiscal;
using SAPB1.DTO.NotaFiscal;
using SAPB1.IDAL.NotaFiscal;

namespace SAPB1.BLL.NotaFiscal
{
    public class NotaFiscalItemBLL
    {
        private readonly INotaFiscalItem _notaFiscalItem;

        public NotaFiscalItemBLL()
        {
            _notaFiscalItem = NotaFiscalItemFactory.NotaFiscalItemDAL();
        }

        public IList<NotaFiscalItemDTO> ObterNotasFiscaisPorPedidoVenda(string codPedido)
        {
            return _notaFiscalItem.ObterNotasFiscaisPorPedidoVenda(codPedido);
        }
    }
}
