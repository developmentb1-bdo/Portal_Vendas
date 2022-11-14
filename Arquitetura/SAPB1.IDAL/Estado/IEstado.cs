using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Estado;

namespace SAPB1.IDAL.Estado
{
    public interface IEstado
    {
        IList<EstadoDTO> Listar(EstadoDTO estadoDTO);
    }
}
