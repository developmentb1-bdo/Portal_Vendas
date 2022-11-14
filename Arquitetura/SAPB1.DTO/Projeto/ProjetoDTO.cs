using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Projeto
{
    /// <summary>
    /// Tabela OPRJ
    /// </summary>
    public class ProjetoDTO
    {
        public string PrjCode { get; set; }

        public string PrjName { get; set; }

        public string Active { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
