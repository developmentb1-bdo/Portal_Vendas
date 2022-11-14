using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.DiasDesconto;

namespace SAPB1.DTO.Desconto
{
    public class DescontoDTO
    {
        public string Code { get; set; }

        public string TableDesc { get; set; }

        public string ByDate { get; set; }

        public string Freight { get; set; }

        public string BaseDate { get; set; }

        public IList<DiasDescontoDTO> DiasDesconto { get; set; }
    }
}
