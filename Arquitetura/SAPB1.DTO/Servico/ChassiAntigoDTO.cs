using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    /// <summary>
    /// Tabela de usuárop [RSD_CHASSIOLD]
    /// </summary>
    public class ChassiAntigoDTO
    {
        public string U_Chassi { get; set; }

        public string U_Motor { get; set; }

        public string U_Modelo { get; set; }

        public string U_Ano { get; set; }

        public DateTime U_ArrDate { get; set; }

        public string U_DataVenda { get; set; }

        public DateTime U_FinGaran { get; set; }

        public string U_Dealer { get; set; }

        public string U_Cliente { get; set; }

        public string U_InspEntr { get; set; }

        public string U_China { get; set; }

        public string U_EntreEixos { get; set; }

        public string U_ModeloMotor { get; set; }
    }
}
