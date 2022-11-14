using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    [Serializable]
    public class ItemChamadoServicoDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int U_CallID { get; set; }

        public int U_LineNum { get; set; }

        public string U_ItemAlt { get; set; }

        public string U_dscription { get; set; }

        public decimal U_Price { get; set; }

        public decimal U_Quantity { get; set; }

        public decimal Total { get; set; }
    }
}
