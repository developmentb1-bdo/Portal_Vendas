using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.DeskPararelo.Estoque
{
    public class EstoqueDadosDTO
    {
        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public decimal EstoqueTransito { get; set; }

        public decimal NfsEmitidas { get; set; }

        public decimal EstoqueReal { get; set; }

        public decimal SaldoEstoque { get; set; }
    }
}
