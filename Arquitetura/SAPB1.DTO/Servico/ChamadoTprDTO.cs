using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    /// <summary>
    /// Tabela [@@RSD_CALLTPR]
    /// </summary>
    [Serializable]
    public class ChamadoTprDTO
    {
        public int Code { get; set; }

        public int U_CallId { get; set; }

        public string U_CodTpr { get; set; }

        public string U_ItmMan { get; set; }

        public decimal U_Qtd { get; set; }

        public decimal U_Total { get; set; }
    }
}
