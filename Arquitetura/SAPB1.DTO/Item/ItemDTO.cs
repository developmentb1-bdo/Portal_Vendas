using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Item
{
    [Serializable]
    public class ItemDTO
    {
        /// <summary>
        /// Código do Item
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Nome do Item
        /// </summary>
        public string ItemName { get; set; }
        public string DfltWH { get; set; }
        public string WareHouseName { get; set; }

        /// <summary>
        /// Item é de venda (Y - Sim N - Não
        /// </summary>
        public string SellItem { get; set; }

        public string validFor { get; set; }

        public double Comprimento { get; set; }
        public double Pecas { get; set; }
        public string Lote { get; set; }
        public string Norma { get; set; }      
        public double QtdMetro { get; set; }
        public double Peso { get; set; }
        public double DescricaoAuxiliar { get; set; }

    }
}
