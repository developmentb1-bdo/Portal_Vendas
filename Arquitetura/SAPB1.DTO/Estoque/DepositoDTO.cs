using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Deposito
{
    [Serializable]
    public class DepositoDTO
    {
        /// <summary>
        /// Código do depósito
        /// </summary>
        public string WhsCode { get; set; }

        /// <summary>
        /// Nome do depósito
        /// </summary>
        public string WhsName { get; set; }
    }
}
