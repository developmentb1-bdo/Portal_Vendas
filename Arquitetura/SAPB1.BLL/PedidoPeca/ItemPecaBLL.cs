using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.PedidoPeca;
using SAPB1.DALFactory.PedidoPeca;
using SAPB1.DTO.PedidoPeca;
using SAPB1.DTO.Estoque;
using SAPB1.DALFactory.Estoque;
using SAPB1.IDAL.Estoque;

namespace SAPB1.BLL.PedidoPeca
{
    public class ItemPecaBLL
    {
        private readonly IItemPeca _itemPeca;

        public ItemPecaBLL()
        {
            _itemPeca = ItemPecaFactory.ItemPecaDAL();
        }

        public IList<ItemPecaDTO> Listar(ItemPecaDTO itemPecaDTO)
        {
            IList<ItemPecaDTO> listPecas = _itemPeca.Listar(itemPecaDTO);

            if(listPecas.Count > 0)
            {
                IEstoque estoque = EstoqueFactory.EstoqueDAL();

                IList<EstoqueDTO> listEstoqueProdutos = estoque.Listar(null);

                if(listEstoqueProdutos.Count > 0)
                {
                    foreach(ItemPecaDTO peca in listPecas)
                    {
                        var retorno  = listEstoqueProdutos.Where(e => e.Item.ItemCode == peca.ItemCode);

                        if(retorno !=null)
                        {
                            foreach(var dadosEstoque in retorno)
                            {
                                peca.Disponivel = Convert.ToDecimal(dadosEstoque.Disponivel);

                                break;
                            }
                        }
                    }
                }
            }

            return listPecas;
        }
    }
}
