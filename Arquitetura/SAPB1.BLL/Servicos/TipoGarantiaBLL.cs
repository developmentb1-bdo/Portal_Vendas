using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using SAPB1.DALFactory.Servico;

namespace SAPB1.BLL.Servicos
{
    public class TipoGarantiaBLL
    {
        private readonly ITipoGarantia _tipoGarantia;

        public TipoGarantiaBLL()
        {
            _tipoGarantia = TipoGarantiaFactory.TipoGarantiaDAL();
        }

        public IList<TipoGarantiaDTO> ObterTipoGarantiaAtivas()
        {
            return _tipoGarantia.ObterTipoGarantiaAtivas();
        }
    }
}
