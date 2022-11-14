using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoVenda;

namespace SAPB1.IDAL.PedidoVenda
{
    public interface IPedidoVenda
    {
        IList<PedidoVendaDTO> Listar(PedidoVendaDTO pedidoVendaDTO);

        double RetornarValorTotalPorMes(DateTime dataInicial, DateTime dataFinal);

        IList<PedidoVendaDTO> BuscarPedidoVenda(PedidoVendaDTO pedidoVendaDTO);

        string RetornarCodigoTransportadora(long docNum);

        double RetornarValorDespesaFrete(long docNum);
    }
}
