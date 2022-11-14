using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.CondicaoPagamento;

namespace SAPB1.DTO.Prestacoes
{
    /// <summary>
    /// Domínio das Prestações
    /// </summary>
    public class PrestacaoDTO
    {
        public CondicaoPagamentoDTO CondicaoPagamento { get; set; }

        public int InstsNo { get; set; }

        public int InstMonth { get; set; }

        public int InstDays { get; set; }

        public double InstPrcnt { get; set; }
    }
}
