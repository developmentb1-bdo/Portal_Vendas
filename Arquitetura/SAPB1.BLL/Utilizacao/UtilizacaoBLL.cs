using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Utilizacao;
using SAPB1.DTO.Utilizacao;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.DALFactory.Utilizacao;

namespace SAPB1.BLL.Utilizacao
{
    public class UtilizacaoBLL
    {
        public IList<UtilizacaoDTO> Listar(UtilizacaoDTO utilizacaoDTO)
        {
            IUtilizacao utilizacaoDAL = UtilizacaoFactory.UtilizacaoDAL();

            return utilizacaoDAL.Listar(utilizacaoDTO);
        }
    }
}
