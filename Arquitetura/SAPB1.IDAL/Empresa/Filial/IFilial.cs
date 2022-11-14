using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Empresa.Filial;

namespace SAPB1.IDAL.Empresa.Filial
{
    public interface IFilial
    {
        IList<FilialDTO> Listar(FilialDTO filialDTO);
    }
}
