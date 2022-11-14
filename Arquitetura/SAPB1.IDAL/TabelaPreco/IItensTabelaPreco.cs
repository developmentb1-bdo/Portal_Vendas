using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.ItensTabelaPreco;

namespace SAPB1.IDAL.ItensTabelaPreco
{
    public interface IItensTabelaPreco
    {
        IList<ItensTabelaPrecoDTO> Listar(ItensTabelaPrecoDTO itensTabelaPrecoDTO);

        IList<ItensTabelaPrecoDTO> ListarItensDeMaisDeUmaTabelapreco(List<string> codTabelas);

        IList<ItensTabelaPrecoDTO> BuscarItensDeMaisDeUmaTabelapreco(List<string> codTabelas, ItensTabelaPrecoDTO itensDTO);

        IList<ItensTabelaPrecoDTO> ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(string codTabela);
    }
}
