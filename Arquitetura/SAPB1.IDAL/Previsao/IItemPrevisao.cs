using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Previsao;

namespace SAPB1.IDAL.Previsao
{
    public interface IItemPrevisao
    {
        IList<ItemPrevisaoDTO> ObeterTodosItensPrevisoes();
    }
}
