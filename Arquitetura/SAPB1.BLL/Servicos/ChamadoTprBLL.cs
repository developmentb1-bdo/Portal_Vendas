using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using SAPB1.DALFactory.Servico;

namespace SAPB1.BLL.Servicos
{
    public class ChamadoTprBLL
    {
        private readonly IChamadoTpr _chamadoTpr;

        public ChamadoTprBLL()
        {
            _chamadoTpr = ChamadoTprFactory.ChamadoTprDAL();
        }

        public IList<ChamadoTprDTO> ObterTprPorChamado(int callId)
        {
            return _chamadoTpr.ObterTprPorChamado(callId);
        }

        public bool InserirChamadoTpr(ChamadoTprDTO chamadoTprDTO)
        {
            return _chamadoTpr.InserirChamadoTpr(chamadoTprDTO);
        }
    }
}
