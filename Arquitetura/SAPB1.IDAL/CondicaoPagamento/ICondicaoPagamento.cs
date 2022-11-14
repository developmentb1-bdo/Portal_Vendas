using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.CondicaoPagamento;

namespace SAPB1.IDAL.CondicaoPagamento
{
    public interface ICondicaoPagamento
    {
        IList<CondicaoPagamentoDTO> Listar(CondicaoPagamentoDTO condicaoPagamentoDTO);
    }
}
