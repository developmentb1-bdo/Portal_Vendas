using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Estoque;

namespace SAPB1.IDAL.Estoque
{
    public interface IEstoque
    {
        IList<EstoqueDTO> Listar(EstoqueDTO estoqueDTO);

        double RetornarTotalValorEstoque();

        IList<EstoqueDTO> ListarEstoquePorProduto(string itemCode);
    }

    public interface IEstoqueConsulta
    {
        IList<EstoqueConsulta> Listar(EstoqueDTO estoqueDTO);

        double RetornarTotalValorEstoque();

        IList<EstoqueDTO> ListarEstoquePorProduto(string itemCode);
    }

}
