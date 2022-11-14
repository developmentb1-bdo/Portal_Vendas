using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.Estoque;
using SAPB1.DTO.Estoque;
using SAPB1.IDAL.Estoque;

namespace SAPB1.BLL.Estoque
{
    public class EstoqueBLL
    {
        IEstoque estoqueDAL = EstoqueFactory.EstoqueDAL();
        IEstoqueConsulta estoqueDALC = EstoqueFactory.EstoqueConsultaDal();

        public IList<EstoqueConsulta> Listar(EstoqueDTO estoqueDTO,bool tipo)
        {
            IList<EstoqueConsulta> listaProdutosEstoque = estoqueDALC.Listar(estoqueDTO);


            if (listaProdutosEstoque.Count > 0)
            {
                //foreach(EstoqueDTO estoque in listaProdutosEstoque)
                //{
                //    estoque.Disponivel = ((estoque.OnHand - estoque.IsCommited)) + estoque.OnOrder;
                //}

                return listaProdutosEstoque;
            }
            else
            {
                return listaProdutosEstoque;
            }
        }

        public IList<EstoqueDTO> Listar(EstoqueDTO estoqueDTO)
        {
            IList<EstoqueDTO> listaProdutosEstoque = estoqueDAL.Listar(estoqueDTO);


            if (listaProdutosEstoque.Count > 0)
            {
                foreach(EstoqueDTO estoque in listaProdutosEstoque)
                {
                    estoque.Disponivel = ((estoque.OnHand - estoque.IsCommited)) + estoque.OnOrder;
                }

                return listaProdutosEstoque;
            }
            else
            {
                return listaProdutosEstoque;
            }
        }

        public double RetornarTotalValorEstoque()
        {
            return estoqueDAL.RetornarTotalValorEstoque();
        }

        public EstoqueDTO RetornarDadosEstoqueProduto(string itemCode)
        {
            EstoqueDTO estoqueDTO = new EstoqueDTO();

            IList<EstoqueDTO> listEstoque = estoqueDAL.ListarEstoquePorProduto(itemCode);

            if (listEstoque.Count > 0)
            {
                foreach (EstoqueDTO estoque in listEstoque)
                {
                    estoque.Disponivel = estoque.OnHand - estoque.IsCommited;
                }

                double maiorValor = listEstoque.Max(e => e.Disponivel);

                if(maiorValor > 0)
                {
                    IList<EstoqueDTO> listEstoqueParaReposicao = listEstoque.Where(e => e.Disponivel >= maiorValor).ToList();

                    if(listEstoqueParaReposicao.Count > 1)
                    {
                        estoqueDTO.BPLid = "4";
                        estoqueDTO.Deposito = new DTO.Deposito.DepositoDTO();
                        estoqueDTO.Deposito.WhsCode = "DPR002";
                    }
                    else
                    {
                        estoqueDTO.BPLid = listEstoqueParaReposicao[0].BPLid;
                        estoqueDTO.Deposito = new DTO.Deposito.DepositoDTO();
                        estoqueDTO.Deposito.WhsCode = listEstoqueParaReposicao[0].Deposito.WhsCode;
                    }
                }
                else
                {
                    estoqueDTO.BPLid = "4";
                    estoqueDTO.Deposito = new DTO.Deposito.DepositoDTO();
                    estoqueDTO.Deposito.WhsCode = "DPR002";
                }
            }

            return estoqueDTO;
        }
    }
}
