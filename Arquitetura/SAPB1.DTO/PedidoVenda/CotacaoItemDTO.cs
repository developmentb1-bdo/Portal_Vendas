/*
 * @author Victor Oliveira.
 */

namespace SAPB1.DTO.PedidoVenda
{
    public class CotacaoItemDTO
    {
        public CotacaoItemDTO() { }

        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string Dscription { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double DiscPrcnt { get; set; }
        public double LineTotal { get; set; }
        public int Usage { get; set; }
        public string UsageName { get; set; }
        public string unitMsr { get; set; }
        public double U_Peso { get; set; }
        public double QtdBarra { get; set; }
        public double Comprimento { get; set; }



        /*
        DelivrdQty
        UomCode
        PackQty
        Price
        DiscPrcnt
        Usage
        TaxCode
        CFOPCode
        CSTCode
        LineTotal
        LinePoPrss*/
    }
}