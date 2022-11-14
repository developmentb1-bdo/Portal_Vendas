using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Funcionario.Vendedor.Comissao;

namespace SAPB1.DTO.Funcionario.Vendedor
{
    /// <summary>
    /// Domínio do Vendedor
    /// </summary>
    public class VendedorDTO
    {
        /// <summary>
        /// Chave primária
        /// </summary>
        public int SlpCode { get; set; }

        /// <summary>
        /// Nome do vendedor
        /// </summary>
        public string SlpName { get; set; }

        /// <summary>
        /// Disponível. Y-Sim N-Não
        /// </summary>
        public string Locked { get; set; }

        /// <summary>
        /// Ativo. Y-Sim N-Não
        /// </summary>
        public string Active { get; set; }

        /// <summary>
        /// Memo
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Grupo de comissão (classe GrupoComissaoDTO)
        /// </summary>
        public GrupoComissaoDTO GrupoComissao { get; set; }
    }
}
