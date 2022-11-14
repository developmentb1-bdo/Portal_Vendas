using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.FormasPagamento;

namespace SAPB1.IDAL.FormasPagamento
{
    public interface IFormaPagamento
    {
        IList<FormaPagamentoDTO> Listar(FormaPagamentoDTO formaPagamentoDTO);
    }
}
