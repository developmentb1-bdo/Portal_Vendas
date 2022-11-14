using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Relatorio
{
    public class FinanceiroDTO
    {
        public decimal ValorTotalAberto { get; set; }

        public decimal ValorTotalVencimento { get; set; }

        public decimal ValorTotal { get; set; }

        public string Nome { get; set; }

        public string Data { get; set; }

        public string CodigoPn { get; set; }
    }
}
