using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;

namespace SAPB1.IDAL.Servico
{
    public interface ITipoGarantia
    {
        IList<TipoGarantiaDTO> ObterTipoGarantiaAtivas();
    }
}
