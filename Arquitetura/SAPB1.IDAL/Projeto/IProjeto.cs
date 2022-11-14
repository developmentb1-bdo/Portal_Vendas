using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Projeto;

namespace SAPB1.IDAL.Projeto
{
    public interface IProjeto
    {
        IList<ProjetoDTO> Listar(ProjetoDTO projetoDTO);
    }
}
