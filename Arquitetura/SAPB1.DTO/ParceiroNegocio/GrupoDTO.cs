/*
 * @author Victor Oliveira.
 */

namespace SAPB1.DTO.ParceiroNegocio
{
    /// <summary>
    /// Tabela OCRG do SAP B1.
    /// </summary>
    public class GrupoDTO
    {
        public GrupoDTO() { }

        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public char GroupType { get; set; }
        public char Locked { get; set; }
        public char DataSource { get; set; }
        public int UserSign { get; set; }
        public int PriceList { get; set; }
        public char DiscRel { get; set; }
    }
}