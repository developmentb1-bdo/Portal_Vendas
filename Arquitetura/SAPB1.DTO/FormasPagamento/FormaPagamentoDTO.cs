using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.FormasPagamento
{
    public class FormaPagamentoDTO
    {
        public string PayMethCod { get; set; }

        public string Descript { get; set; }

        public string Active { get; set; }

        public string Type { get; set; }
    }
}
