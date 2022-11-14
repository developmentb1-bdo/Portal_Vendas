using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.PedidoVenda
{
    /// <summary>
    /// Referente a tabela RDR12
    /// </summary>
    public class EnderecoDTO
    {
        /// <summary>
        /// Pedido de Venda
        /// </summary>
        public PedidoVendaDTO PedidoVenda { get; set; }

        /// <summary>
        /// Tipo de Endereço
        /// </summary>
        public string AddrTypeS { get; set; }

        /// <summary>
        /// Tipo de Endereço
        /// </summary>
        public string AddrTypeB { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string ZipCodeS { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        public string ZipCodeB { get; set; }


        /// <summary>
        /// Endereço
        /// </summary>
        public string StreetS { get; set; }

        /// <summary>
        /// Endereço
        /// </summary>
        public string StreetB { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public string StreetNoS { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public string StreetNoB { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string BuildingS { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string BuildingB { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string BlockS { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string BlockB { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string CityS { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string CityB { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string StateS { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string StateB { get; set; }

        /// <summary>
        /// Município
        /// </summary>
        public string CountyS { get; set; }

        /// <summary>
        /// Município
        /// </summary>
        public string CountyB { get; set; }

        /// <summary>
        /// País
        /// </summary>
        public string CountryS { get; set; }

        /// <summary>
        /// País
        /// </summary>
        public string CountryB { get; set; }

        /// <summary>
        /// Endereço 2
        /// </summary>
        public string Address2S { get; set; }

        /// <summary>
        /// Endereço 2
        /// </summary>
        public string Address2B { get; set; }

        /// <summary>
        /// Endereço 3
        /// </summary>
        public string Address3S { get; set; }

        /// <summary>
        /// Endereço 3
        /// </summary>
        public string Address3B { get; set; }

        /// <summary>
        /// GLN
        /// </summary>
        public string GlbLocNumS { get; set; }


        /// <summary>
        /// GLN
        /// </summary>
        public string GlbLocNumB { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string Incoterms { get; set; }

        public string Vehicle { get; set; }

        public string VidState { get; set; }

        public string NfRef { get; set; }

        public string POSEqNum { get; set; }

        public string Carrier { get; set; }

        public string QoP { get; set; }

        public string PackDesc { get; set; }

        public string Brand { get; set; }

        public string NoSu { get; set; }

        public string POSManufSn { get; set; }

        public string POSCashN { get; set; }
    }
}
