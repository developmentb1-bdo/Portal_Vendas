using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.NotaFiscal;

namespace SAPB1.DTO.PedidoPeca
{
    public class PedidoPecaDTO
    {
        /*
        * CardCode - Código do Parceiro de negócio
        * CardName - Nome do Parceiro de negócio
        * Address - Endereço do parceiro de negócio
        */

        /// <summary>
        /// Código da entrada do Número do Pedido de venda. 1-automático
        /// </summary>
        public int DocEntry { get; set; }

        /// <summary>
        /// Número do Pedido
        /// </summary>
        public int DocNum { get; set; }

        /// <summary>
        /// Número de refência do cliente
        /// </summary>
        public string NumAtCard { get; set; }

        /// <summary>
        /// Número Manual ou automático
        /// </summary>
        public int Series { get; set; }

        /// <summary>
        /// Data de Lançamento
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Data de entrega
        /// </summary>
        public DateTime DocDueDate { get; set; }

        /// <summary>
        /// Data do documento
        /// </summary>
        public DateTime TaxDate { get; set; }

        /// <summary>
        /// Status do Pedido de Venda
        /// </summary>
        public string DocStatus { get; set; }

        /// <summary>
        /// Pedido Cancelado
        /// </summary>
        public string Canceled { get; set; }

        /// <summary>
        /// Número do documento Manual - N-Não S-Sim
        /// </summary>
        public string HandWrtten { get; set; }

        /// <summary>
        /// Imprimido
        /// </summary>
        public string Printed { get; set; }

        public double DocTotalSy { get; set; }

        public double DiscPrcnt { get; set; }

        public string Rounding { get; set; }

        public string Comments { get; set; }

        public string JrnlMemo { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string Confirmed { get; set; }

        public string PartSupply { get; set; }

        public string PoPrss { get; set; }

        public string LangCode { get; set; }

        public string Pick { get; set; }

        public string PickRmrk { get; set; }

        public string AgentCode { get; set; }

        public string CardCode { get; set; }

        public string CardName { get; set; }

        public string DocCur { get; set; }

        public string OwnerCode { get; set; }

        public string PaymentGroupCode { get; set; }

        public string U_UND_PARADA { get; set; }

        public string U_ST_CONCESS { get; set; }

        public string U_NomeCliente { get; set; }
        public double U_KmAtual { get; set; }
        public string U_FalhasApresent { get; set; }
        public string U_ObsAdc { get; set; }
        public string U_TstRealizado { get; set; }
        public double U_QtdDiasParado { get; set; }
        public string U_Chassi { get; set; }
        public string U_ModVei { get; set; }
        public string U_AnoModelo { get; set; }
        public string U_EntreEixos { get; set; }
        public string ListNum { get; set; }
    }
}
