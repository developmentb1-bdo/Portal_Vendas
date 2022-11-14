using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.TabelaPreco
{
    /// <summary>
    /// DTO da Tabela de Preço
    /// </summary>
    public class TabelaPrecoDTO
    {
        /// <summary>
        /// Código da Lista de Preco
        /// </summary>
        public int ListNum { get; set; }

        /// <summary>
        /// Nome da Lista de Preço
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// Código do Grupo
        /// </summary>
        public int GroupCode { get; set; }
    }
}
