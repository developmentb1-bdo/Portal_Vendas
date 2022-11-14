using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Desconto;

namespace SAPB1.DTO.DiasDesconto
{
    public class DiasDescontoDTO
    {
        public DescontoDTO Desconto { get; set; }

        public string CdcCode { get; set; }

        public int NumOfDays { get; set; }

        public double Discount { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

    }
}
