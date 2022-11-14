using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Empresa.Filial;
using SAPB1.DTO.Empresa.Filial;
using SAPB1.DALFactory.Empresa.Filial;

namespace SAPB1.BLL.Empresa.Filial
{
    public class FilialBLL
    {
        public IList<FilialDTO>Listar(FilialDTO filialDTO)
        {
            IFilial filialDAL = FilialFactory.FilialDAL();

            return filialDAL.Listar(filialDTO);
        }
    }
}
