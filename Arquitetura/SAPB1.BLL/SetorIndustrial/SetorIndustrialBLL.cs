using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.SetorIndustrial;
using SAPB1.DALFactory.SetorIndustrial;
using SAPB1.DTO.SetorIndustrial;

namespace SAPB1.BLL.SetorIndustrial
{
    public class SetorIndustrialBLL
    {
        public IList<SetorIndustrialDTO> Listar()
        {
            ISetorIndustrial setorIndustrialDAL = SetorIndustrialFactory.SetorIndustrialDAL();

            return setorIndustrialDAL.Listar();
        }
    }
}
