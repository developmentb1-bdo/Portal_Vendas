using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Estado;
using SAPB1.DALFactory.Estado;
using SAPB1.IDAL.Estado;

namespace SAPB1.BLL.Estado
{
    public class EstadoBLL
    {
        public IList<EstadoDTO> Listar(EstadoDTO estadoDTO)
        {
            IEstado estadoDAL = EstadoFactory.EstadoDAL();

            return estadoDAL.Listar(estadoDTO);
        }
    }
}
