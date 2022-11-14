using SAPB1.DTO.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.IDAL.Servico
{
    public interface IChamadoServico
    {
        IList<ChamadoServicoDTO> ListarChamadoPorCustomer(string customer);

        ChamadoServicoDTO ListarChamadoPorIdPorCustomer(int callId, string customer);

        IList<ChamadoServicoDTO> BuscarChamadoPorCustomer(string customer, ChamadoServicoDTO chamadoDTO);
    }
}
