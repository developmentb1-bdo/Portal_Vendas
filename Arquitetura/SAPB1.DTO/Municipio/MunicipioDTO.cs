using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.DTO.Estado;

namespace SAPB1.DTO.Municipio
{
    public class MunicipioDTO
    {
        public int AbsId { get; set; }

        public int Code { get; set; }

        public PaisDTO Pais { get; set; }

        public EstadoDTO Estado { get; set; }

        public string Name { get; set; }

        public string TaxZone { get; set; }

        public int IbgeCode { get; set; }

        public int GiaCode { get; set; }
    }
}
