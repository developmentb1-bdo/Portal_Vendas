using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Concessionario;

namespace SAPB1.IDAL.Concessionario
{
    public interface IConcessionario
    {
        ConcessionarioDTO RetornarDadosConcessionarioPorLogin(string usuario, string senha);

        ConcessionarioDTO ObterConcessionarioPorId(string cardCode);

        IList<ConcessionarioDTO> ObterTodos();

        IList<ConcessionarioDTO> ObterConcessionarioPorGrupoCliente(string groupCode);
    }
}
