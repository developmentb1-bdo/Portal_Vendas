using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoVenda;

namespace SAPB1.IDAL.PedidoVenda
{
    public interface IItemCotacao
    {
        IList<CotacaoItemDTO> Listar(CotacaoItemDTO CotacaoItemDTO);
    }
}
