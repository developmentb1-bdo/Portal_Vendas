using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.FormasPagamento;
using SAPB1.DALFactory.FormasPagamento;
using SAPB1.DTO.FormasPagamento;

namespace SAPB1.BLL.FormasPagamento
{
    public class FormaPagamentoBLL
    {
        IFormaPagamento formaPagamentoDAL = FormaPagamentoFactory.FormaPagamentoDAL();

        public IList<FormaPagamentoDTO> Listar(FormaPagamentoDTO formaPagamentoDTO)
        {
            return formaPagamentoDAL.Listar(formaPagamentoDTO);
        }
    }
}
