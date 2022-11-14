using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Empregado;

namespace SAPB1.IDAL.Empregado
{
    public interface IEmpregado
    {
        IList<EmpregadoDTO> Listar(EmpregadoDTO empregadoDTO);
    }
}
