using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.DeskPararelo.Estoque;

namespace SAPB1.IDAL.DeskPararelo.Estoque
{
    public interface IEstoqueDados
    {
        IList<EstoqueDadosDTO> RetornarDadosEstoque();
    }
}
