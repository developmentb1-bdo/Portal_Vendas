using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoPeca;

namespace SAPB1.IDAL.PedidoPeca
{
    public interface IPedidoPeca
    {
        IList<PedidoPecaDTO> Listar(PedidoPecaDTO pedidoPecaDTO);

        IList<PedidoPecaDTO> ListarPedidoPorConcessionario(string cardCode);

        IList<PedidoPecaDTO> BuscarPedidoPorConcessionario(PedidoPecaDTO pedidoPecaDTO);
    }
}
