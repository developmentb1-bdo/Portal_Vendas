using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.GrupoItem;
using SAPB1.DALFactory.GrupoItem;
using SAPB1.IDAL.GrupoItem;

namespace SAPB1.BLL.GrupoItem
{
    public class GrupoItemBLL
    {
        private readonly IGrupoItem _grupoItem;

        public GrupoItemBLL()
        {
            _grupoItem = GrupoItemFactory.GrupoItemDAL();
        }

        public IList<GrupoItemDTO> ObterTodos()
        {
            return _grupoItem.ObterTodos();
        }
    }
}
