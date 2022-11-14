using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.ParceiroNegocio;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.BLL.ParceiroNegocio
{
    public class GrupoBLL
    {
        public IList<GrupoDTO> Listar(GroupType groupType)
        {
            IGrupo grupoDAL = ParceiroNegocioFactory.GrupoDAL();

            return grupoDAL.Listar(groupType);
        }
    }
}
