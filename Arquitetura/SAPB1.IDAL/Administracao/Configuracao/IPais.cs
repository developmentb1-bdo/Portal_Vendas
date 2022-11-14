using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Administracao.Configuracao;

namespace SAPB1.IDAL.Administracao.Configuracao
{
    public interface IPais
    {
        IList<PaisDTO> Listar();

        IList<PaisDTO> BuscarPorSigla(string sigla);
    }
}
