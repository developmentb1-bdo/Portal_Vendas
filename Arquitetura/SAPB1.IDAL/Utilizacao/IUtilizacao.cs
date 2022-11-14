using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Utilizacao;

namespace SAPB1.IDAL.Utilizacao
{
    public interface IUtilizacao
    {
        IList<UtilizacaoDTO> Listar(UtilizacaoDTO utlizacaoDTO);
    }
}
