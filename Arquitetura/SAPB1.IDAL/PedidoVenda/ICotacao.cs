/*
 * @author Victor Oliveira.
 */

using System.Collections.Generic;
using SAPB1.DTO.PedidoVenda;

namespace SAPB1.IDAL.PedidoVenda
{
    public interface ICotacao
    {
        IList<CotacaoDTO> Listar();
        CotacaoDTO Selecionar(int docEntry);
    }
}