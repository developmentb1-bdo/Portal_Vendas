using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoPeca;
using SAPB1.DALFactory.PedidoPeca;
using SAPB1.DTO.PedidoPeca;
using SAPB1.IDAL.ItensTabelaPreco;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DALFactory.ItensTabelaPreco;

namespace SAPB1.BLL.PedidoPeca
{
    public class PedidoPecaBLL
    {
        private IPedidoPeca _pedidoPeca;

        public PedidoPecaBLL()
        {
            _pedidoPeca = PedidoPecaFactory.PedidoPecaDAL();
        }

        public IList<PedidoPecaDTO> Listar(PedidoPecaDTO pedidoPecaDTO)
        {
            return _pedidoPeca.Listar(pedidoPecaDTO);
        }

        /// <summary>
        /// Lista os pedidos de acordo com o id da concessionária
        /// </summary>
        /// <param name="cardCode">Código do Concessionário</param>
        /// <returns></returns>
        public IList<PedidoPecaDTO> ListarPedidosPorIdConcessionario(string cardCode)
        {
            return _pedidoPeca.ListarPedidoPorConcessionario(cardCode);
        }

        /// <summary>
        /// Lista os pedidos do conessionário, modificando os preços da lista de preço de peça de reposição já com impostos
        /// </summary>
        /// <param name="cardCode">Código da concessionária</param>
        /// <param name="listNum">Código da lista de preço</param>
        /// <returns></returns>
        public IList<PedidoPecaDTO> ListarPedidosPorIdConcessionario(string cardCode, string listNum)
        {
            IList<PedidoPecaDTO> listPedidos = _pedidoPeca.ListarPedidoPorConcessionario(cardCode);

            if(listPedidos.Count > 0)
            {
                double totalPedido = 0;
                double qtdItem = 0;
                double precoModificado = 0;

                IItensTabelaPreco itensTabelaPreco = ItensTabelaPrecoFactory.ItensTabelaPrecoDAL();
                IList<ItensTabelaPrecoDTO> listItensTabelaPreco = itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(listNum);

                IItemPeca itensPedidos = ItemPecaFactory.ItemPecaDAL();
                IList<ItemPecaDTO> listItensPedido = itensPedidos.ListarTodosItensPedidoPecaPorConcessionario(cardCode);

                foreach(PedidoPecaDTO pedidoPeca in listPedidos)
                {
                    var itensPedidoPeca = listItensPedido.Where(p => p.DocEntry == pedidoPeca.DocNum);

                    foreach(var item in itensPedidoPeca)
                    {
                        qtdItem = item.Quantity;
                        var precoTabela = listItensTabelaPreco.Where(pi => pi.CodigoItem == item.ItemCode);

                        foreach(var preco in precoTabela)
                        {
                            precoModificado = preco.Price;

                            totalPedido += (precoModificado * qtdItem);
                        }
                    }

                    pedidoPeca.DocTotalSy = totalPedido;

                    totalPedido = 0;
                    qtdItem = 0;
                    precoModificado = 0;
                }
            }

            return listPedidos;
        }

        public IList<PedidoPecaDTO> BuscarPedidoPorConcessionario(PedidoPecaDTO pedidoPecaDTO)
        {
            return _pedidoPeca.BuscarPedidoPorConcessionario(pedidoPecaDTO);
        }

        public IList<PedidoPecaDTO> BuscarPedidoPorConcessionario(PedidoPecaDTO pedidoPecaDTO, string listNum)
        {
            IList<PedidoPecaDTO> listPedidos = _pedidoPeca.BuscarPedidoPorConcessionario(pedidoPecaDTO);

            if (listPedidos.Count > 0)
            {
                double totalPedido = 0;
                double qtdItem = 0;
                double precoModificado = 0;

                IItensTabelaPreco itensTabelaPreco = ItensTabelaPrecoFactory.ItensTabelaPrecoDAL();
                IList<ItensTabelaPrecoDTO> listItensTabelaPreco = itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(listNum);

                IItemPeca itensPedidos = ItemPecaFactory.ItemPecaDAL();
                IList<ItemPecaDTO> listItensPedido = itensPedidos.ListarTodosItensPedidoPecaPorConcessionario(pedidoPecaDTO.CardCode);


                foreach (PedidoPecaDTO pedidoPeca in listPedidos)
                {
                    var itensPedidoPeca = listItensPedido.Where(p => p.DocEntry == pedidoPeca.DocNum);

                    foreach (var item in itensPedidoPeca)
                    {
                        qtdItem = item.Quantity;
                        var precoTabela = listItensTabelaPreco.Where(pi => pi.CodigoItem == item.ItemCode);

                        foreach (var preco in precoTabela)
                        {
                            precoModificado = preco.Price;

                            totalPedido += (precoModificado * qtdItem);
                        }
                    }

                    pedidoPeca.DocTotalSy = totalPedido;

                    totalPedido = 0;
                    qtdItem = 0;
                    precoModificado = 0;
                }
            }

            return listPedidos;
        }
    }
}
