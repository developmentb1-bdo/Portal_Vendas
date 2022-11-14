using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Empregado
{
    /// <summary>
    /// Tabela OHEM
    /// </summary>
    public class EmpregadoDTO
    {
        public int EmpID { get; set; }

        public string Active { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public PosicaoDTO Posicao { get; set; }
    }
}
