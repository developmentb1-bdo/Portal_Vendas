using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Administracao.Configuracao;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.DALFactory.Administracao.Configuracao;

namespace SAPB1.BLL.Administracao.Configuracao
{
    public class IdiomaBLL
    {
        public IList<IdiomaDTO> Listar()
        {
            IIdioma idiomaDAL = IdiomaFactory.IdiomaDAL();

            return idiomaDAL.Listar();
        }
    }
}
