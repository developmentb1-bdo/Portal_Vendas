/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;

namespace SAPB1.DTO.PedidoVenda
{
    public class CotacaoDTO
    {
        public CotacaoDTO() { }

        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public char DocType { get; set; }
        public char CANCELED { get; set; }
        public char Handwrtten { get; set; }
        public char Printed { get; set; }
        public char DocStatus { get; set; }
        public char InvntSttus { get; set; }
        public string ObjType { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocDueDate { get; set; }
        public DateTime TaxDate { get; set; }
        public string PaymentGroupCode { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string Comments { get; set; }
        public int BPLId { get; set; }
        public List<CotacaoItemDTO> Itens { get; set; }
        public decimal DocTotal { get; set; }
        public short GroupNum { get; set; }
        public string Carrier { get; set; }
        public int TrnspCode { get; set; }
        public string U_S7_CobrarFrete { get; set; }
        public decimal U_S7_TaxaFrete { get; set; }
        public decimal U_S7_ValorFrete { get; set; }
        public string U_CNPJ { get; set; }
    }
}