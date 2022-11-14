using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Concessionario
{
    public class ConcessionarioDTO
    {
        /// <summary>
        /// Código
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// Razão Social
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Tabela de preço padrão
        /// </summary>
        public int ListNum { get; set; }

        /// <summary>
        /// CNPJ
        /// </summary>
        public string U_Tsystem { get; set; }

        /// <summary>
        /// Tabela de preço de garantia
        /// </summary>
        public string U_TabGarant { get; set; }

        /// <summary>
        /// Tabela de Preço Sugerida
        /// </summary>
        public string U_TabSuger { get; set; } 
    }
}
