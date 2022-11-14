/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;

namespace SAPB1.DTO.ParceiroNegocio
{
    [Serializable]
    public class ParceiroNegocioDTO
    {
        public ParceiroNegocioDTO() { }

        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string CardFName { get; set; }
        public string CardType { get; set; }
        public int GroupCode { get; set; }
        public char CmpPrivate { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string MailAddres { get; set; }
        public string MailZipCod { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string CntctPrsn { get; set; }
        public string Notes { get; set; }
        public decimal Balance { get; set; }
        public decimal ChecksBal { get; set; }
        public decimal DNotesBal { get; set; }
        public decimal OrdersBal { get; set; }
        public int GroupNum { get; set; }
        public decimal CreditLine { get; set; }
        public decimal DebtLine { get; set; }
        public decimal Discount { get; set; }
        public char VatStatus { get; set; }
        public string LicTradNum { get; set; }
        public char DdctStatus { get; set; }
        public decimal DdctPrcnt { get; set; }
        public DateTime ValidUntil { get; set; }
        public int Chrctrstcs { get; set; }
        public int ExMatchNum { get; set; }
        public int InMatchNum { get; set; }
        public int ListNum { get; set; }
        public decimal DNoteBalFC { get; set; }
        public decimal OrderBalFC { get; set; }
        public decimal DNoteBalSy { get; set; }
        public decimal OrderBalSy { get; set; }
        public char Transfered { get; set; }
        public char BalTrnsfrd { get; set; }
        public decimal IntrstRate { get; set; }
        public decimal Commission { get; set; }
        public int CommGrCode { get; set; }
        public string Free_Text { get; set; }
        public int SlpCode { get; set; }
        public char PrevYearAc { get; set; }
        public string Currency { get; set; }
        public string RateDifAct { get; set; }
        public decimal BalanceSys { get; set; }
        public decimal BalanceFC { get; set; }
        public char Protected { get; set; }
        public string Cellular { get; set; }
        public int AvrageLate { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string MailCity { get; set; }
        public string MailCounty { get; set; }
        public string MailCountr { get; set; }
        public string E_Mail { get; set; }
        public string Picture { get; set; }
        public string DflAccount { get; set; }
        public string DflBranch { get; set; }
        public string BankCode { get; set; }
        public string AddID { get; set; }
        public string Pager { get; set; }
        public string FatherCard { get; set; }
        public char FatherType { get; set; }
        public char QryGroup1 { get; set; }
        public char QryGroup2 { get; set; }
        public char QryGroup3 { get; set; }
        public char QryGroup4 { get; set; }
        public char QryGroup5 { get; set; }
        public char QryGroup6 { get; set; }
        public char QryGroup7 { get; set; }
        public char QryGroup8 { get; set; }
        public char QryGroup9 { get; set; }
        public char QryGroup10 { get; set; }
        public char QryGroup11 { get; set; }
        public char QryGroup12 { get; set; }
        public char QryGroup13 { get; set; }
        public char QryGroup14 { get; set; }
        public char QryGroup15 { get; set; }
        public char QryGroup16 { get; set; }
        public char QryGroup17 { get; set; }
        public char QryGroup18 { get; set; }
        public char QryGroup19 { get; set; }
        public char QryGroup20 { get; set; }
        public char QryGroup21 { get; set; }
        public char QryGroup22 { get; set; }
        public char QryGroup23 { get; set; }
        public char QryGroup24 { get; set; }
        public char QryGroup25 { get; set; }
        public char QryGroup26 { get; set; }
        public char QryGroup27 { get; set; }
        public char QryGroup28 { get; set; }
        public char QryGroup29 { get; set; }
        public char QryGroup30 { get; set; }
        public char QryGroup31 { get; set; }
        public char QryGroup32 { get; set; }
        public char QryGroup33 { get; set; }
        public char QryGroup34 { get; set; }
        public char QryGroup35 { get; set; }
        public char QryGroup36 { get; set; }
        public char QryGroup37 { get; set; }
        public char QryGroup38 { get; set; }
        public char QryGroup39 { get; set; }
        public char QryGroup40 { get; set; }
        public char QryGroup41 { get; set; }
        public char QryGroup42 { get; set; }
        public char QryGroup43 { get; set; }
        public char QryGroup44 { get; set; }
        public char QryGroup45 { get; set; }
        public char QryGroup46 { get; set; }
        public char QryGroup47 { get; set; }
        public char QryGroup48 { get; set; }
        public char QryGroup49 { get; set; }
        public char QryGroup50 { get; set; }
        public char QryGroup51 { get; set; }
        public char QryGroup52 { get; set; }
        public char QryGroup53 { get; set; }
        public char QryGroup54 { get; set; }
        public char QryGroup55 { get; set; }
        public char QryGroup56 { get; set; }
        public char QryGroup57 { get; set; }
        public char QryGroup58 { get; set; }
        public char QryGroup59 { get; set; }
        public char QryGroup60 { get; set; }
        public char QryGroup61 { get; set; }
        public char QryGroup62 { get; set; }
        public char QryGroup63 { get; set; }
        public char QryGroup64 { get; set; }
        public string DdctOffice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ExportCode { get; set; }
        public int DscntObjct { get; set; }
        public char DscntRel { get; set; }
        public int SPGCounter { get; set; }
        public int SPPCounter { get; set; }
        public string DdctFileNo { get; set; }
        public int SCNCounter { get; set; }
        public decimal MinIntrst { get; set; }
        public char DataSource { get; set; }
        public int OprCount { get; set; }
        public string ExemptNo { get; set; }
        public int Priority { get; set; }
        public int CreditCard { get; set; }
        public string CrCardNum { get; set; }
        public DateTime CardValid { get; set; }
        public int UserSign { get; set; }
        public char LocMth { get; set; }
        public char validFor { get; set; }
        public DateTime validFrom { get; set; }
        public DateTime validTo { get; set; }
        public char frozenFor { get; set; }
        public DateTime frozenFrom { get; set; }
        public DateTime frozenTo { get; set; }
        public char sEmployed { get; set; }
        public int MTHCounter { get; set; }
        public int BNKCounter { get; set; }
        public int DdgKey { get; set; }
        public int DdtKey { get; set; }
        public string ValidComm { get; set; }
        public string FrozenComm { get; set; }
        public char chainStore { get; set; }
        public char DiscInRet { get; set; }
        public string State1 { get; set; }
        public string State2 { get; set; }
        public string VatGroup { get; set; }
        public string Block { get; set; }
        public int Series { get; set; }
        public string IntrntSite { get; set; }
        public IList<EnderecoDTO> ListEndereco { get; set; }
        public IList<ContatoDTO> ListContato { get; set; }
        public IList<IdentificacaoFiscalDTO> ListIdentificacaoFiscal { get; set; }
        public char SinglePaym { get; set; }

        public string IndustryC { get; set; }

        public string PymCode { get; set; }

        public string AgentCode { get; set; }

        public string U_CNPJ { get; set; }

        public string MainUsage { get; set; }
    }
}