using SAPB1.DALFactory.Territorio;
using SAPB1.DTO.Territorio;
using SAPB1.IDAL.Territorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.BLL.Territorio
{
    public class TerritorioBLL
    {
        private readonly ITerritorio _territorio;

        public TerritorioBLL()
        {
            _territorio = TerritorioFactory.TerritorioDAL();
        }

        public IList<TerritorioDTO> Listar()
        {
            return _territorio.Listar();
        }
    }
}
