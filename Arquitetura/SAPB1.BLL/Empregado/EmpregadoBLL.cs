using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.Empregado;
using SAPB1.DTO.Empregado;
using SAPB1.IDAL.Empregado;

namespace SAPB1.BLL.Empregado
{
    public class EmpregadoBLL
    {
        public IList<EmpregadoDTO> Listar(EmpregadoDTO empregadoDTO)
        {
            IEmpregado empregadoDAL = EmpregadoFactory.EmpregadoDAL();

            return empregadoDAL.Listar(empregadoDTO);
        }
    }
}
