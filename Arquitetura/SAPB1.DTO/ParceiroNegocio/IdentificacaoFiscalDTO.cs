/*
 * @author Victor Oliveira.
 */

using System;

namespace SAPB1.DTO.ParceiroNegocio
{
    public class IdentificacaoFiscalDTO
    {
        public IdentificacaoFiscalDTO() { }

        public string CardCode { get; set; }
        public string Address { get; set; }
        public string TaxId0 { get; set; }
        public string TaxId1 { get; set; }
        public string TaxId2 { get; set; }
        public string TaxId3 { get; set; }
        public string TaxId4 { get; set; }
        public string TaxId5 { get; set; }
        public string TaxId6 { get; set; }
        public string TaxId7 { get; set; }
        public string TaxId8 { get; set; }
        public string TaxId9 { get; set; }
        public string TaxId10 { get; set; }
        public string TaxId11 { get; set; }
        public string TaxId12 { get; set; }
        public string TaxId13 { get; set; }
        public int CNAEId { get; set; }
        public char AddrType { get; set; }
        public string ECCNo { get; set; }
        public string CERegNo { get; set; }
        public string CERange { get; set; }
        public string CEDivis { get; set; }
        public string CEComRate { get; set; }
        public int LogInstanc { get; set; }
        public DateTime SefazDate { get; set; }
    }
}