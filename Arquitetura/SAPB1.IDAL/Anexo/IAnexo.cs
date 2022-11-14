using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Anexo;

namespace SAPB1.IDAL.Anexo
{
    public interface IAnexo
    {
        IList<AnexoDTO> ListarTodosAnexosPorAbsEntry(string absEntry);
    }
}
