using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.IDAL.Servico;
using SAPB1.DALFactory.Servico;

namespace SAPB1.BLL.Servicos
{
    public class TprBLL
    {
        private readonly ITpr _tpr;

        public TprBLL()
        {
            _tpr = TrpFactory.TprDAL();
        }

        public List<TprDTO> ObterTodos()
        {
            return _tpr.ObterTodos();
        }

        public TprDTO ObterDadosPorCodigo(string codigo)
        {
            return _tpr.ObterDadosPorCodigo(codigo);
        }
    }
}
