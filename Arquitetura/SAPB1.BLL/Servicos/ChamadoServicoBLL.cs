using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Servico;
using SAPB1.DTO.Servico;
using SAPB1.DALFactory.Servico;

namespace SAPB1.BLL.Servicos
{
    public class ChamadoServicoBLL
    {
        private readonly IChamadoServico _chamado;

        public ChamadoServicoBLL()
        {
            _chamado = ChamadoServicoFactory.ChamadoServicoDAL();
        }

        public IList<ChamadoServicoDTO> ListarChamadoPorCustomer(string customer)
        {
            return _chamado.ListarChamadoPorCustomer(customer);
        }

        public ChamadoServicoDTO ListarChamadoPorIdPorCustomer(int callId, string customer)
        {
            return _chamado.ListarChamadoPorIdPorCustomer(callId, customer);
        }

        public IList<ChamadoServicoDTO> BuscarChamadoPorCustomer(string customer, ChamadoServicoDTO chamadoDTO)
        {
            return _chamado.BuscarChamadoPorCustomer(customer, chamadoDTO);
        }
    }
}
