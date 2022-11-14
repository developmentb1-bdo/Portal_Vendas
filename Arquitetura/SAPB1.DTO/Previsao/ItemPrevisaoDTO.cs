using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Previsao
{
    public class ItemPrevisaoDTO
    {
        /// <summary>
        /// Código do item (produto)
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Quantidade planejada
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Data planejada
        /// </summary>
        public DateTime Date { get; set; }
    }
}
