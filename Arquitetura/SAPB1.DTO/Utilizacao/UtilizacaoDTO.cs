using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Utilizacao.Cfop;

namespace SAPB1.DTO.Utilizacao
{
    /// <summary>
    /// Domínio de Utilização(Uso Principal) do pedido de venda do SB1
    /// </summary>
    public class UtilizacaoDTO
    {
        /// <summary>
        /// Id da Utilização
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome da Utilização
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// Disponível Y-Sim N-Não
        /// </summary>
        public string Locked { get; set; }

        /// <summary>
        /// Código da Empresa
        /// </summary>
        public string UserSign { get; set; }

        /// <summary>
        /// Somente Imposto Y-Sim N-Não
        /// </summary>
        public string TaxOnly { get; set; }

        /// <summary>
        /// Lançar imposto no preço ao estoque/Ativo Fixo. 1-Sim 0-Não
        /// </summary>
        public int PostTax { get; set; }

        /// <summary>
        /// Descrição da utilização
        /// </summary>
        public string Descr { get; set; }

        /// <summary>
        /// Entrada de CFOP no estado
        /// </summary>
        public CfopDTO CFOPIIS { get; set; }

        /// <summary>
        /// Entrada de CFOP fora do estado
        /// </summary>
        public CfopDTO CFOPIOS { get; set; }

        /// <summary>
        /// Importação de entrada de CFOP
        /// </summary>
        public CfopDTO CFOPII { get; set; }

        /// <summary>
        /// Saída de CFOP no estado
        /// </summary>
        public CfopDTO CFOPOIS { get; set; }

        /// <summary>
        /// Saída de CFOP fora do estado
        /// </summary>
        public CfopDTO CFOPOOS { get; set; }

        /// <summary>
        /// Exportação de saída de CFOP
        /// </summary>
        public CfopDTO CFOPOE { get; set; }

        /// <summary>
        /// Terceiros. Y-Sim N-Não
        /// </summary>
        public string ThirdParty { get; set; }

        /// <summary>
        /// Apropriação de Crédito. Y-Sim N-Não
        /// </summary>
        public string U_ApropCred { get; set; }

        /// <summary>
        /// Gratuito(Parceiro de negócio)
        /// </summary>
        public string FreeChrgBP { get; set; }

        /// <summary>
        /// Soma PIS e Confins. Y-Sim N-Não
        /// </summary>
        public string U_SomaPisCofins { get; set; }

        //public string U_LG_OperTerc{ get; set; }

        //public string U_FinNfe { get; set; }

    }
}
