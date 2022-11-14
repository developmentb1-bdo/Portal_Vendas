using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.SetorIndustrial;

namespace SAPB1.IDAL.SetorIndustrial
{
    public interface ISetorIndustrial
    {
        IList<SetorIndustrialDTO> Listar();
    }
}
