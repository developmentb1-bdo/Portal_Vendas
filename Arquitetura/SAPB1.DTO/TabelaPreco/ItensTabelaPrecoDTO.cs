using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Item;
using SAPB1.DTO.TabelaPreco;

namespace SAPB1.DTO.ItensTabelaPreco
{
    public class ItensTabelaPrecoDTO
    {
        /// <summary>
        /// DTO do Item
        /// </summary>
        public ItemDTO Item { get; set; }

        /// <summary>
        /// DTO da Tabela de Preço
        /// </summary>
        /// 
        public TabelaPrecoDTO TabelaPreco { get; set; }

        /// <summary>
        /// Moeda
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Preço Padrão - Preço tabela reposição
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Preço 1
        /// </summary>
        public double AddPrice1 { get; set; }

        /// <summary>
        /// Moeda 1
        /// </summary>
        public string Currency1 { get; set; }

        /// <summary>
        /// Preço 2
        /// </summary>
        public double AddPrice2 { get; set; }

        /// <summary>
        /// Moeda 2
        /// </summary>
        public string Currency2 { get; set; }

        public double PrecoGarantia { get; set; }

        public double PrecoSugerido { get; set; }

        public double PrecoReposicao { get; set; }

        public string NomeItem { get; set; }

        public string CodigoItem { get; set; }

        public int Lista { get; set; }

        public string NcmCode { get; set; }

        public double Comprimento { get; set; }
        public double Pecas { get; set; }
        public string Lote { get; set; }
        public string Norma { get; set; }
        public double QtdMetro { get; set; }
        public double Peso { get; set; }
        public double DescricaoAuxiliar { get; set; }

    }
}
