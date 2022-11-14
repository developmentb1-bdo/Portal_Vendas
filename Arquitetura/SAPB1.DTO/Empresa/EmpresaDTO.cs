using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Empresa
{
    public class EmpresaDTO
    {
        public string CompnyName { get; set; }

        public string CompnyAddress { get; set; }

        /// <summary>
        /// Código da Filial
        /// </summary>
        public int UserSign { get; set; }

        /// <summary>
        /// Código da filial 2
        /// </summary>
        public int UserSign2 { get; set; }

        /// <summary>
        /// Moeda Principal
        /// </summary>
        public string MainCurrency { get; set; }

        public string Country { get; set; }

        public string E_Mail { get; set; }

        public string Manager { get; set; }

        public string CompType { get; set; }

        public string TaxIdNum { get; set; }
    }
}
