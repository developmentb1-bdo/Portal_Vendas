using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Utilizacao.Cfop
{
    public class CfopDTO
    {
        /// <summary>
        /// Id da CFOP(Chave primária)
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Código da CFOP
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Descrição da CFOP
        /// </summary>
        public string Descrip { get; set; }

        /// <summary>
        /// Aplicação da CFOP(Explicação)
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// Disponível. Y-Sim N-Não
        /// </summary>
        public string Locked { get; set; }

        public CfopType TipoCfop { get; set; }
    }
}
