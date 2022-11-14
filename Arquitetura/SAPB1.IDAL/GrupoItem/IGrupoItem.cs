using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.GrupoItem;

namespace SAPB1.IDAL.GrupoItem
{
    public interface IGrupoItem
    {
        IList<GrupoItemDTO> ObterTodos();
    }
}
