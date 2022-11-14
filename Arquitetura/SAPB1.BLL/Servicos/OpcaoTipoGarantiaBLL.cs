using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.IDAL.Servico;
using SAPB1.DALFactory.Servico;

namespace SAPB1.BLL.Servicos
{
    public class OpcaoTipoGarantiaBLL
    {
        private readonly IOpcaoTipoGarantia _opcaoTipoGarantia;

        public OpcaoTipoGarantiaBLL()
        {
            _opcaoTipoGarantia = OpcaoTipoGarantiaFactory.OpcaoTipoGarantiaDAL();
        }

        public IList<OpcaoTipoGarantiaDTO> ObterOpcoesTipoGaratiaPorGarantia(int codTpGarantia)
        {
            return _opcaoTipoGarantia.ObterOpcoesTipoGaratiaPorGarantia(codTpGarantia);
        }
    }
}
