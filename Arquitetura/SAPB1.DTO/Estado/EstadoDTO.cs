using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;

namespace SAPB1.DTO.Estado
{
    public class EstadoDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public PaisDTO Pais { get; set; }
    }
}
