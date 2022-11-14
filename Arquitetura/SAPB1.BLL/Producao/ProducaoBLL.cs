using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.Previsao;
using SAPB1.DALFactory.OrdemProducao;
using SAPB1.DTO.Previsao;
using SAPB1.DTO.OrdemProducao;
using SAPB1.IDAL.Previsao;
using SAPB1.IDAL.OrdemProducao;
using SAPB1.BLL.EstruturaItem;
using SAPB1.DTO.EstruturaItem;

namespace SAPB1.BLL.Producao
{
    public class ProducaoBLL
    {
        public string GerarTxtPlanoProducao(string caminhoCompleto)
        {
            StringBuilder stb = new StringBuilder();

            IItemPrevisao itemPrevisao = ItemPrevisaoFactory.ItemPrevisaoDAL();
            IList<ItemPrevisaoDTO> listaItensPrevisao = itemPrevisao.ObeterTodosItensPrevisoes();

            string valorQtdPrevisao = string.Empty;

            if(listaItensPrevisao.Count > 0)
            {
                foreach(ItemPrevisaoDTO itensProducaoPrevisao in listaItensPrevisao)
                {
                    if (itensProducaoPrevisao.ItemCode.Length > 25)
                        stb.Append(itensProducaoPrevisao.ItemCode.Substring(0, 25).Trim().PadRight(25, ' '));
                    else
                        stb.Append(itensProducaoPrevisao.ItemCode.PadRight(25, ' '));

                    stb.Append(itensProducaoPrevisao.Date.ToString("ddMMyyyy").PadRight(8, ' '));

                    if (itensProducaoPrevisao.Quantity.ToString().Length > 1)
                    {
                        valorQtdPrevisao = itensProducaoPrevisao.Quantity.ToString().Replace(",", "").Trim();

                        stb.Append(valorQtdPrevisao.PadLeft(15, '0'));
                    }
                    else
                    {
                        valorQtdPrevisao = (itensProducaoPrevisao.Quantity * 1000000).ToString().Replace(",", "").Trim();

                        stb.Append(valorQtdPrevisao.PadLeft(15, '0'));
                    }

                    stb.Append("P");
                    stb.Append("\n");
                }
            }

            IOrdemProducao ordemProducao = OrdemProducaoFactory.OrdemProducaoDAL();

            IList<OrdemProducaoDTO> listOrdemOProducao = ordemProducao.ObterOrdemProducaoAbertas();

            string valorQtdProducao = string.Empty;

            if(listOrdemOProducao.Count > 0)
            {
                foreach (OrdemProducaoDTO itensProducao in listOrdemOProducao)
                {
                    if (itensProducao.ItemCode.Length > 25)
                        stb.Append(itensProducao.ItemCode.Substring(0, 25).Trim().PadRight(25, ' '));
                    else
                        stb.Append(itensProducao.ItemCode.PadRight(25, ' '));

                    stb.Append(itensProducao.DueDate.ToString("ddMMyyyy").PadRight(8, ' '));

                    if (itensProducao.PlannedQty.ToString().Length > 1)
                    {
                        valorQtdProducao = itensProducao.PlannedQty.ToString().Replace(",", "").Trim();

                        stb.Append(valorQtdProducao.PadLeft(15, '0'));
                    }
                    else
                    {
                        valorQtdProducao = (itensProducao.PlannedQty * 1000000).ToString().Replace(",", "").Trim();

                        stb.Append(valorQtdProducao.PadLeft(15, '0'));
                    }

                    stb.Append("F");
                    stb.Append("\n");
                }
            }

            if (!stb.ToString().Trim().Equals(""))
            {
                string[] matrizDados = stb.ToString().Split('\n');

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(caminhoCompleto))
                {
                    foreach (string i in matrizDados)
                        file.WriteLine(i);

                    file.Close();
                }

                return "";
            }
            else
                return "Não foi encontrado nenhum plano de produção para ser relacionado.";
        }

        public string GerarTxtArquivoFip(string caminhoCompleto)
        {
            EstruturaItemBLL estruturaItemBLL = new EstruturaItemBLL();
            IList<EstruturaItemDTO> list = estruturaItemBLL.ObterItensEstruturasProdutoPai();

            if(list.Count > 0)
            {
                int contador = 1;

                StringBuilder stb = new StringBuilder();

                foreach(EstruturaItemDTO i in list)
                {
                    stb.Append(contador.ToString().PadRight(25, ' '));
                    stb.Append(i.Codigo.PadRight(25, ' '));
                    stb.Append("\n");

                    contador += 1;
                }

                string[] matrizDados = stb.ToString().Split('\n');

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(caminhoCompleto))
                {
                    foreach (string i in matrizDados)
                        file.WriteLine(i);

                    file.Close();
                }

                return "";
            }
            else
                return "Não foi encontrado nenhum FIO para ser relacionado.";
        }
    }
}
