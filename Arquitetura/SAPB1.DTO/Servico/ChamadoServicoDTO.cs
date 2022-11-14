using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    public class ChamadoServicoDTO
    {
        public string status { get; set; }
        public string customer { get; set; }
        public DateTime createDate { get; set; }
        public int DocNum { get; set; }
        public int callID { get; set; }
        public string itemCode { get; set; }
        public DateTime closeDate { get; set; }

        public DateTime U_DataFalha { get; set; }
        public string U_Chassi { get; set; }
        public string U_Modelo { get; set; }
        public double U_KmFal { get; set; }
        public string U_Placa { get; set; }
        public string U_NumMoto { get; set; }
        public string U_ModelMoto { get; set; }
        public string U_DescFal { get; set; }
        public string U_CausaFal { get; set; }
        public string U_CorrecaoFal { get; set; }
        public string U_ObsGerais { get; set; }
        public string U_OrdemServ { get; set; }
        public string U_NomResp { get; set; }
        public string U_Funcao { get; set; }
        public DateTime U_DtVenda { get; set; }
        public DateTime U_DtAbertFal { get; set; }
        public string U_NomCli { get; set; }
        public double U_KmAt { get; set; }

        public string AtcEntry { get; set; }

        public string U_Status { get; set; }

        public string U_TpGarant { get; set; }

        public string U_SubTipoGarant { get; set; }
    }
}
