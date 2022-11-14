using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.EstruturaItem;

namespace SAPB1.IDAL.EstruturaItem
{
    public interface IEstruturaItem
    {
        IList<EstruturaItemDTO> ObterTodasItensEstrutura();

        IList<EstruturaItemDTO> ObterItensEstruturasProdutos();

        IList<EstruturaItemDTO> ObterItensEstruturasProdutoPai();
    }
}
