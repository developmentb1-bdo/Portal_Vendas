using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.EstruturaItem
{
    public class EstruturaItemDTO
    {
        /// <summary>
        /// Código do Produto
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Unidade de Medida do Produto
        /// </summary>
        public string UnidadeMedida { get; set; }

        /// <summary>
        /// Peso do Produto
        /// </summary>
        public decimal Peso { get; set; }

        public string UnidadeMedidaPeso { get; set; }

        /// <summary>
        /// P - Produtos C - Componente
        /// </summary>
        public string TipoItem { get; set; }

        public decimal LeadTime { get; set; }

        public string ItemFantasma { get; set; }

        /// <summary>
        /// validFrom
        /// </summary>
        public DateTime DataValidadeInicial { get; set; }

        /// <summary>
        /// validTo
        /// </summary>
        public DateTime DataValidadeFinal { get; set; }

        public string CodigoPai { get; set; }

        public decimal Quantity { get; set; }
    }
}
