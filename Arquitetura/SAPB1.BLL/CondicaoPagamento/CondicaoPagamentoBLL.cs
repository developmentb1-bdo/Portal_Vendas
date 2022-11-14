using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.CondicaoPagamento;
using SAPB1.IDAL.CondicaoPagamento;
using SAPB1.DALFactory.CondicaoPagamento;

namespace SAPB1.BLL.CondicaoPagamento
{
    public class CondicaoPagamentoBLL
    {
        ICondicaoPagamento condicaoPagamentoDAL = CondicaoPagamentoFactory.CondicaoPagamentoDAL();

        public IList<CondicaoPagamentoDTO> Listar(CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            return condicaoPagamentoDAL.Listar(condicaoPagamentoDTO);
        }
    }
}
