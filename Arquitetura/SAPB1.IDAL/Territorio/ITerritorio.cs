using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Territorio;

namespace SAPB1.IDAL.Territorio
{
    public interface ITerritorio
    {
        IList<TerritorioDTO> Listar();
    }
}
