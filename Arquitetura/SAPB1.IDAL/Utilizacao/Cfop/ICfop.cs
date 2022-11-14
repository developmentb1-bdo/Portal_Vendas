using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Utilizacao.Cfop;

namespace SAPB1.IDAL.Utilizacao.Cfop
{
    public interface ICfop
    {
        IList<CfopDTO> Listar(CfopDTO cfopDTO);
    }
}
