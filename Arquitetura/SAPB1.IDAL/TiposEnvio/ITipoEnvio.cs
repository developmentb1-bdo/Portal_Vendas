using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.TiposEnvio;

namespace SAPB1.IDAL.TiposEnvio
{
    public interface ITipoEnvio
    {
        IList<TipoEnvioDTO> Listar(TipoEnvioDTO tipoEnvioDTO);
    }
}
