using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    /// <summary>
    /// Tabela [@@RSD_MODVEI]
    /// </summary>
    public class ModeloVeiculoDTO
    {
        public int Code { get; set; }

        public string Modelo { get; set; }

        public string AnoModelo { get; set; }

        public string EntreEixos { get; set; }
    }
}
