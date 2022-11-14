using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.OrdemProducao
{
    public class OrdemProducaoDTO
    {
        /// <summary>
        /// Código do item (Produto)
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Quantidade Planejada
        /// </summary>
        public decimal PlannedQty { get; set; }

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        public DateTime DueDate { get; set; }
    }
}
