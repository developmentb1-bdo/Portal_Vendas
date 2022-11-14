using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;

namespace SAPB1.IDAL.Servico
{
    public interface ITpr
    {
        List<TprDTO> ObterTodos();

        TprDTO ObterDadosPorCodigo(string codigo);
    }
}
