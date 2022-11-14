using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoVenda;
using SAPB1.DALFactory.PedidoVenda;
using SAPB1.DTO.PedidoVenda;

namespace SAPB1.BLL.PedidoVenda
{
    public class ItemCotacaoBLL
    {
        public IList<CotacaoItemDTO> Listar(CotacaoItemDTO CotacaoItemDTO)
        {
            IItemCotacao itemCotacaoDAL = ItemCotacaoFactory.ItemCotacaoDAL();

            return itemCotacaoDAL.Listar(CotacaoItemDTO);
        }
    }
}
