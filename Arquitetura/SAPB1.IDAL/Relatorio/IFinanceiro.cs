using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Relatorio;

namespace SAPB1.IDAL.Relatorio
{
    public interface IFinanceiro
    {
        decimal RetonarValorEmAberto(DateTime data);

        decimal RetonarValorVencimento(DateTime data);

        List<FinanceiroDTO> ObterRecimentosEmAbertoPorParceiroNegocio();

        decimal RetonarValorEmAbertoPagamento(DateTime data);

        decimal RetonarValorVencimentoPagamento(DateTime data);

        List<FinanceiroDTO> ObterPagamentoEmAbertoPorParceiroNegocio();

        List<FinanceiroDTO> ObterRecebimentosPorMesEmAberto(DateTime dataInicial, DateTime dataFinal);

        List<FinanceiroDTO> ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(DateTime dataInicial, DateTime dataFinal);

        List<FinanceiroDTO> ObterRecebimentoPagoPorMes(DateTime dataInicial, DateTime dataFinal);

        List<FinanceiroDTO> ObterRecebimentoPagoPorParceiroNegocioPorMes(DateTime dataInicial, DateTime dataFinal);
    }
}
