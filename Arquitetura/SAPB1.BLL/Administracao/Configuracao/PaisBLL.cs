using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.IDAL.Administracao.Configuracao;
using SAPB1.DALFactory.Administracao.Configuracao;

namespace SAPB1.BLL.Administracao.Configuracao
{
    public class PaisBLL
    {
        public IList<PaisDTO> Listar()
        {
            IPais paisDAL = PaisFactory.PaisDAL();

            return paisDAL.Listar();
        }

        public IList<PaisDTO> BuscarPorSigla(string sigla)
        {
            IPais paisDAL = PaisFactory.PaisDAL();

            return paisDAL.BuscarPorSigla(sigla);
        }
    }
}
