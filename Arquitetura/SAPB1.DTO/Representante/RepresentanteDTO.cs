using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Representante
{
    /// <summary>
    /// Tabela OAGP
    /// </summary>
    public class RepresentanteDTO
    {
        public int AgentCode { get; set; }

        public string AgentName { get; set; }

        public string Locked { get; set; }
    }
}
