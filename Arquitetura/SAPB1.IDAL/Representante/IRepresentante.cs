using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Representante;

namespace SAPB1.IDAL.Representante
{
    public interface IRepresentante
    {
        IList<RepresentanteDTO> Listar();
    }
}
