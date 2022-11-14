using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoPeca;

namespace SAPB1.IDAL.PedidoPeca
{
    public interface IItemPeca
    {
        IList<ItemPecaDTO> Listar(ItemPecaDTO itemPecaDTO);

        IList<ItemPecaDTO> ListarTodosItensPedidoPecaPorConcessionario(string cardCode);
    }
}
