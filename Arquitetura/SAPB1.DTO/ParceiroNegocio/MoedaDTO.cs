/*
 * @author Victor Oliveira.
 */

namespace SAPB1.DTO.ParceiroNegocio
{
    /// <summary>
    /// Tabela OCRN SAP B1.
    /// </summary>
    public class MoedaDTO
    {
        public MoedaDTO() { }

        public string CurrCode { get; set; }
        public string CurrName { get; set; }
        public string ChkName { get; set; }
        public string Chk100Name { get; set; }
        public string DocCurrCod { get; set; }
        public string FrgnName { get; set; }
        public string F100Name { get; set; }
        public char Locked { get; set; }
        public char DataSource { get; set; }
        public int UserSign { get; set; }
        public int RoundSys { get; set; }
        public int UserSign2 { get; set; }
        public int Decimals { get; set; }
        public char ISRCalc { get; set; }
        public char RoundPym { get; set; }
        public char ConvUnit { get; set; }
        public char BaseCurr { get; set; }
        public decimal Factor { get; set; }
        public string ChkNamePl { get; set; }
        public string Chk100NPl { get; set; }
        public string FrgnNamePl { get; set; }
        public string F100NamePl { get; set; }
        public string ISOCurrCod { get; set; }
        public decimal MaxInDiff { get; set; }
        public decimal MaxOutDiff { get; set; }
        public decimal MaxInPcnt { get; set; }
        public decimal MaxOutPcnt { get; set; }
        public string ISOCurrNum { get; set; }
    }
}