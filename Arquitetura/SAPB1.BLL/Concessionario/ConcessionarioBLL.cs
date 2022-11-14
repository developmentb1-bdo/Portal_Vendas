using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Concessionario;
using SAPB1.DTO.Concessionario;
using SAPB1.DALFactory.Concessionario;

namespace SAPB1.BLL.Concessionario
{
    public class ConcessionarioBLL
    {
        private readonly IConcessionario _concessionario;

        public ConcessionarioBLL()
        {
            _concessionario = ConcessionarioFactory.ConcessionarioDAL();
        }

        public ConcessionarioDTO RetornarDadosConcessionarioPorLogin(string usuario, string senha)
        {
            return _concessionario.RetornarDadosConcessionarioPorLogin(usuario, senha);
        }

        public ConcessionarioDTO ObterConcessionarioPorId(string cardCode)
        {
            return _concessionario.ObterConcessionarioPorId(cardCode);
        }

        public IList<ConcessionarioDTO> ObterTodos()
        {
            return _concessionario.ObterTodos();
        }

        public IList<ConcessionarioDTO> ObterConcessionarioPorGrupoCliente(string groupCode)
        {
            return _concessionario.ObterConcessionarioPorGrupoCliente(groupCode);
        }
    }
}
