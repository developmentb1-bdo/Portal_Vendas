using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.TabelaPreco;

namespace SAPB1.IDAL.TabelaPreco
{
    public interface ITabelaPreco
    {
        IList<TabelaPrecoDTO> Listar(TabelaPrecoDTO tabelaPrecoDTO);

        IList<TabelaPrecoDTO> ListarTabelaPrecoConcessionario(int idTabela);
    }
}
