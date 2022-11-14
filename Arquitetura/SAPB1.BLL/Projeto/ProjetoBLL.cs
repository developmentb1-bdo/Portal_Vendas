using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Projeto;
using SAPB1.IDAL.Projeto;
using SAPB1.DALFactory.Projeto;

namespace SAPB1.BLL.Projeto
{
    public class ProjetoBLL
    {
        public IList<ProjetoDTO> Listar(ProjetoDTO projetoDTO)
        {
            IProjeto projetoDAL = ProjetoFactory.ProjetoDAL();

            return projetoDAL.Listar(projetoDTO);
        }
    }
}
