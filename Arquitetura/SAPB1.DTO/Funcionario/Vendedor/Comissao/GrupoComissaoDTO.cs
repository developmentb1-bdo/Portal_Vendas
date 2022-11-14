using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Funcionario.Vendedor.Comissao
{
    public class GrupoComissaoDTO
    {
        /// <summary>
        /// Chave primária
        /// </summary>
        public int GroupCode { get; set; }

        /// <summary>
        /// Nome do grupo de comissão
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Valor da comissão
        /// </summary>
        public double Comission { get; set; }

        /// <summary>
        /// Disponível. Y-Sim N-Não
        /// </summary>
        public string Locked { get; set; }
    }
}
