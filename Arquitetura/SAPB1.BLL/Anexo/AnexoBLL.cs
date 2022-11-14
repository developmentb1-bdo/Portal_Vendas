using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Anexo;
using SAPB1.DTO.Anexo;
using SAPB1.DALFactory.Anexo;

namespace SAPB1.BLL.Anexo
{
    public class AnexoBLL
    {
        private readonly IAnexo _anexo;

        public AnexoBLL()
        {
            _anexo = AnexoFactory.AnexoDAL();
        }

        public IList<AnexoDTO> ListarTodosAnexosPorAbsEntry(string absEntry)
        {
            return _anexo.ListarTodosAnexosPorAbsEntry(absEntry);
        }
    }
}
