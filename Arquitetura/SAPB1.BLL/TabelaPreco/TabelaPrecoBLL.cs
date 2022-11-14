using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.TabelaPreco;
using SAPB1.DTO.TabelaPreco;
using SAPB1.IDAL.TabelaPreco;

namespace SAPB1.BLL.TabelaPreco
{
    public class TabelaPrecoBLL
    {
        ITabelaPreco tabelaPrecoDAL = TabelaPrecoFactory.TabelaPrecoDAL();

        /// <summary>
        /// Classe de Regra de Negócio da clase TabelaPrecoDTO para listar as tabelas de preço
        /// </summary>
        /// <param name="tabelaPrecoDTO">classe TabelaprecoDTO</param>
        /// <returns>Lista genérica da classe TabelPrecoDTO</returns>
        public IList<TabelaPrecoDTO> Listar(TabelaPrecoDTO tabelaPrecoDTO)
        {
            return tabelaPrecoDAL.Listar(tabelaPrecoDTO);
        }

        public IList<TabelaPrecoDTO> ListarTabelaPrecoConcessionario(int idTabela)
        {
            return tabelaPrecoDAL.ListarTabelaPrecoConcessionario(idTabela);
        }
    }
}
