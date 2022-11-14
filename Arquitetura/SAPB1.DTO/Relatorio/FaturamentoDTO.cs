using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Relatorio
{
    public class FaturamentoDTO
    {
        public string NomeCliente { get; set; }

        public decimal Valor { get; set; }

        public string Data { get; set; }

        public string GrupoProdutoNome { get; set; }

        public string CodigoGrupo { get; set; }

        public decimal Pecas { get; set; }

        public decimal Caminhoes { get; set; }
    }
}
