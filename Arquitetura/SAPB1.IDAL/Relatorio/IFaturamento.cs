using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Relatorio;

namespace SAPB1.IDAL.Relatorio
{
    public interface IFaturamento
    {
        List<FaturamentoDTO> ObterFaturamentoMes(DateTime dataInicial, DateTime dataFinal);

        List<FaturamentoDTO> BuscaFaturamentoMes(DateTime dataInicial, DateTime dataFinal, string cliente, string grupoProduto);

        List<FaturamentoDTO> ObterFaturamentoPorCliente(DateTime dataInicial, DateTime dataFinal);

        List<FaturamentoDTO> ObterFaturamentoMesPorGrupoProduto(DateTime dataInicial, DateTime dataFinal);

        List<FaturamentoDTO> BuscarFaturamentoMesPorGrupoProduto(DateTime dataInicial, DateTime dataFinal, string cliente, string grupoProduto);
    }
}
