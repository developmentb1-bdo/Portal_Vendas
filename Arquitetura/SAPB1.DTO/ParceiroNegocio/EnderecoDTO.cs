/*
 * @author Victor Oliveira.
 */

namespace SAPB1.DTO.ParceiroNegocio
{
    public class EnderecoDTO
    {
        public EnderecoDTO() { }

        public string Address { get; set; }
        public string Street { get; set; }
        public string Block { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Building { get; set; }
        public char AdresType { get; set; }
        public string AddrType { get; set; }
        public string StreetNo { get; set; }
        public string CardCode { get; set; }
    }
}