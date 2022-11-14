using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.EstruturaItem;
using SAPB1.DALFactory.EstruturaItem;
using SAPB1.IDAL.EstruturaItem;

namespace SAPB1.BLL.EstruturaItem
{
    public class EstruturaItemBLL
    {
        private readonly IEstruturaItem _estrutraItem;

        public EstruturaItemBLL()
        {
            _estrutraItem = EstruturaItemFactory.EstruturaItemDAL();
        }

        public string GerarArquivoCadastroProdutoEstrutura(string caminho)
        {
            IList<EstruturaItemDTO> listProdutos = _estrutraItem.ObterTodasItensEstrutura();


            /*
             * **********************************************Layout do arquivo txt******************************************************************************************
             * *************************************************************************************************************************************************************
             */

            if (listProdutos.Count > 0)
            {
                StringBuilder stb = new StringBuilder();

                string peso = string.Empty;
                string leadTime = string.Empty;
                string produto = string.Empty;

                foreach (EstruturaItemDTO produtos in listProdutos)
                {
                    if (produtos.Codigo.Length <= 25)
                        stb.Append(produtos.Codigo.PadRight(25, ' '));
                    else
                        stb.Append(produtos.Codigo.Substring(0, 25).PadRight(25, ' '));

                    produto = RetirarAcentos(produtos.Descricao);

                    if (produto.Length <= 40)
                        stb.Append(produto.PadRight(40, ' '));
                    else
                        stb.Append(produto.Substring(0, 40).PadRight(40, ' '));

                    if (produtos.UnidadeMedida.Length <= 2)
                        stb.Append(produtos.UnidadeMedida.PadRight(2, ' '));
                    else
                        stb.Append(produtos.UnidadeMedida.Substring(0, 2).PadRight(2, ' '));

                    if (produtos.Peso > 0)
                    {
                        peso = (produtos.Peso * 1000000).ToString().PadLeft(15, '0').Replace(",", "");

                        if (peso.Length < 15)
                            peso = peso.PadLeft(15, '0');
                        else
                        {
                            peso = peso.Substring(0, 15);
                        }

                        //peso = 
                        //peso = (produtos.Peso * 1000000).ToString().Trim().PadLeft(15, '0').Replace(",", "");
                        //peso = produtos.Peso.ToString("n6").PadLeft(15, '0').Replace(",", "");
                    }
                    else
                        peso = "1000000".PadLeft(15, '0');

                    stb.Append(peso);

                    stb.Append(produtos.UnidadeMedidaPeso.PadRight(2, ' '));

                    if (produtos.LeadTime > 0)
                        leadTime = (produtos.LeadTime * 1000000).ToString().Replace(",", "").Trim();
                    else
                        leadTime = "1000000";

                    stb.Append(leadTime.PadLeft(15,'0'));

                    stb.Append(produtos.ItemFantasma.PadLeft(1, ' '));
                    stb.Append(produtos.TipoItem);
                      
                    stb.Append("\n");
                }

                string[] matrizDados = stb.ToString().Split('\n');

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(caminho))
                {
                    foreach (string i in matrizDados)
                        file.WriteLine(i);

                    file.Close();
                }

                return "";
            }
            else
                return "Não tem nenhuma estrutura de produtos para ser relacionada.";
        }

        public string GerarListaMateriasTxt(string caminho)
        {
            IList<EstruturaItemDTO> listProdutos = _estrutraItem.ObterItensEstruturasProdutos();

            StringBuilder stb = new StringBuilder();

            string quatity = string.Empty;

            if (listProdutos.Count > 0)
            {
                foreach(EstruturaItemDTO produto in listProdutos)
                {
                    if(produto.CodigoPai.Length > 25)
                        stb.Append(produto.CodigoPai.Substring(0,25).Trim().PadRight(25,' '));
                    else
                        stb.Append(produto.CodigoPai.PadRight(25, ' '));

                    if(produto.Codigo.Length > 25)
                        stb.Append(produto.Codigo.Substring(0, 25).Trim().PadRight(25, ' '));
                    else
                        stb.Append(produto.Codigo.PadRight(25, ' '));

                    if (produto.Quantity.ToString().Length > 1)
                    {
                        quatity = produto.Quantity.ToString().Replace(",", "").Trim();

                        stb.Append(quatity.PadLeft(15, '0'));
                    }
                    else
                    {
                        quatity = (produto.Quantity * 1000000).ToString().Replace(",", "").Trim();

                        stb.Append(quatity.PadLeft(15, '0'));
                    }

                    if (produto.DataValidadeInicial != DateTime.MinValue)
                        stb.Append(produto.DataValidadeInicial.ToString("ddMMyyyy").PadRight(8, ' '));
                    else
                        stb.Append("01012016".PadRight(8, ' '));

                    if (produto.DataValidadeFinal != DateTime.MinValue)
                        stb.Append(produto.DataValidadeFinal.ToString("ddMMyyyy").PadRight(8, ' '));
                    else
                        stb.Append(DateTime.Now.ToString("30129999").PadRight(8, ' '));

                    stb.Append("\n");
                }

                string[] matrizDados = stb.ToString().Split('\n');

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(caminho))
                {
                    foreach (string i in matrizDados)
                        file.WriteLine(i);

                    file.Close();
                }

                return "";
            }
            else
                return "não foi possível gerar a lista de materias pois não existe produto para ser relacionado.";
        }

        private  string RetirarAcentos(string texto)
        {
            string ComAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç'";

            string SemAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc ";

            int i = 0;

            foreach(Char c in ComAcentos)
            {
                texto = texto.Replace(c.ToString().Trim(), SemAcentos[i].ToString().Trim());
                i++;
            }

            return texto;
        }

         public IList<EstruturaItemDTO> ObterItensEstruturasProdutoPai()
        {
            return _estrutraItem.ObterItensEstruturasProdutoPai();
        }
    }
}
