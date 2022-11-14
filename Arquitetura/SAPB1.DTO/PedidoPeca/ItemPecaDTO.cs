using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.PedidoPeca
{
    [Serializable]
    public class ItemPecaDTO
    {
        public int DocEntry { get; set; }

        public int LineNum { get; set; }

        public string ItemCode { get; set; }

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

        public string ItemName { get; set; }

        public decimal Disponivel { get; set; }

        public string Modelo { get; set; }

        public string AnoModelo { get; set; }

        public string EntreEixos { get; set; }

        public string Deposito { get; set; }

        public string Filial { get; set; }

        public string CodigoGenerico { get; set; }

        public string Dscription { get; set; }
    }
}
