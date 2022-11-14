using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.OrdemProducao;

namespace SAPB1.IDAL.OrdemProducao
{
    public interface IOrdemProducao
    {
        IList<OrdemProducaoDTO> ObterOrdemProducaoAbertas();
    }
}
