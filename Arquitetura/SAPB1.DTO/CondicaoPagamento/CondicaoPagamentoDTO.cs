using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Desconto;
using SAPB1.DTO.Prestacoes;

namespace SAPB1.DTO.CondicaoPagamento
{
    /// <summary>
    /// Domínio da Condição de pagamento
    /// </summary>
    public class CondicaoPagamentoDTO
    {
        public int GroupNum { get; set; }

        public string PymntGroup { get; set; }

        public string PayDuMonth { get; set; }

        public int ExtraMonth { get; set; }

        public int ExtraDays { get; set; }

        public int TolDays { get; set; }

        public string OpenRcpt { get; set; }

        public double VolumDsct { get; set; }

        public double CredLimit { get; set; }

        public double ObligLimit { get; set; }

        public double VolumDscnt { get; set; }

        public double LatePyChrg { get; set; }

        public DescontoDTO Desconto { get; set; }

        public IList<PrestacaoDTO> Prestacoes { get; set; }

        public string CrdMthd { get; set; }

        public int UserSign { get; set; }

        public string BslineDate { get; set; }
    }
}
