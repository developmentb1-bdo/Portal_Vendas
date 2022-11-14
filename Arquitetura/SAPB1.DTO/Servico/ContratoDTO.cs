using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    /// <summary>
    /// OCRT
    /// </summary>
    public class ContratoDTO
    {
        public int ContractID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TermDate { get; set; }
        public string SrvcType { get; set; }
    }
}
