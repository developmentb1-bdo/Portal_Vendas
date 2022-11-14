using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Utilizacao.Cfop;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.DALFactory.Utilizacao.Cfop;

namespace SAPB1.BLL.Utilizacao.Cfop
{
    public class CfopBLL
    {
        public IList<CfopDTO> Listar(CfopDTO cfopDTO)
        {
            ICfop cfopDAL = CfopFactory.ICfopDAL();

            return cfopDAL.Listar(cfopDTO);
        }
    }
}
