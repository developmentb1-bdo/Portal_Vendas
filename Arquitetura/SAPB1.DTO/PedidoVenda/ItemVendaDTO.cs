using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.PedidoVenda
{
    [Serializable]
    /// <summary>
    /// Tabela RDR1 - Itens de pedido de venda
    /// </summary>
    public class ItemVendaDTO
    {
        public int DocEntry { get; set; }

        public int LineNum { get; set; }

        public string ItemCode { get; set; }
        public string Dscription { get; set; }

        public double Quantity { get; set; }

        public double DelivrdQty { get; set; }

        public string UomCode { get; set; }

        public double PackQty { get; set; }

        public double DiscPrcnt { get; set; }

        public int Usage { get; set; }

        public string TaxCode { get; set; }

        public string CFOPCode { get; set; }

        public string CSTCode { get; set; }

        public string LinePoPrss { get; set; }

        public double Price { get; set; }

        public double LineTotal { get; set; }

        public double Comprimento { get; set; }
        public string Lote { get; set; }
        public string Norma { get; set; }
        public double QtdBarra { get; set; }
        public double QtdMetro { get; set; }

        public double Peso { get; set; }
        public string DescricaoAuxiliar { get; set; }

        public string UnidadeMedida { get; set; }
        public string ItemPedidoCompra { get; set; }
        public string NumeroPedidoCompra { get; set; }

        public string nomeDeposito { get; set; }

        public DateTime DataEntrega { get; set; }

    }
}
