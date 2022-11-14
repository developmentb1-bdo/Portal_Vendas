using SAPB1.DTO.Servico;
using SAPB1.IDAL.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.Servico;

namespace SAPB1.BLL.Servicos
{
    public class ChassiAntigoBLL
    {
        private readonly IChassiAntigo _chassiAntigo;

        public ChassiAntigoBLL()
        {
            _chassiAntigo = ChassiAntigoFactory.ChassiAntigoDAL();
        }

        public IList<ChassiAntigoDTO> ObterTodosChassi()
        {
            return _chassiAntigo.ObterTodosChassi();
        }

        public ChassiAntigoDTO ObterDadosPeloChassi(string chassi)
        {
            return _chassiAntigo.ObterDadosPeloChassi(chassi);
        }
    }
}
