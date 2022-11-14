using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.TiposEnvio;
using SAPB1.IDAL.TiposEnvio;
using SAPB1.DALFactory.TiposEnvio;

namespace SAPB1.BLL.TiposEnvio
{
    public class TipoEnvioBLL
    {
        ITipoEnvio tipoEnvioDAL = TipoEnvioFacytory.TipoEnvioDAL();

        public IList<TipoEnvioDTO> Listar(TipoEnvioDTO tipoEnvioDTO)
        {
            return tipoEnvioDAL.Listar(tipoEnvioDTO);
        }
    }
}
