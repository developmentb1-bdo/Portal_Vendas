using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Relatorio;
using SAPB1.DALFactory.Relatorio;
using SAPB1.DTO.Relatorio;

namespace SAPB1.BLL.Relatorio
{
    public class FaturamentoBLL
    {
        IFaturamento _faturamento;

        public FaturamentoBLL()
        {
            _faturamento = FaturamentoFactory.FaturamentoDAL();
        }
        public string RetornarGraficoFaturmentoMes(DateTime dataInicial, DateTime dataFinal)
        {
            List<FaturamentoDTO> listFaturamento = _faturamento.ObterFaturamentoMes(dataInicial, dataFinal);

            if (listFaturamento.Count > 0)
            {
                List<FaturamentoDTO> listGruposProdutosFaturamento = _faturamento.ObterFaturamentoMesPorGrupoProduto(dataInicial, dataFinal);

                decimal totalPecas = 0;
                decimal totalCaminhoes = 0;

                foreach (FaturamentoDTO faturamento in listFaturamento)
                {
                    var dadosPeca = listGruposProdutosFaturamento.Where(e => e.Data == faturamento.Data &&
                                                                       (e.CodigoGrupo == "100" || e.CodigoGrupo == "103" || e.CodigoGrupo == "129"));

                    if (dadosPeca != null)
                    {
                        foreach (var fatPecas in dadosPeca)
                            totalPecas += fatPecas.Valor;

                        faturamento.Pecas = totalPecas;
                    }

                    var dadosCaminhao = listGruposProdutosFaturamento.Where(e => e.Data == faturamento.Data &&
                                                                           (e.CodigoGrupo == "107" || e.CodigoGrupo == "127"));

                    if (dadosCaminhao != null)
                    {
                        foreach (var fatCaminhao in dadosCaminhao)
                            totalCaminhoes += fatCaminhao.Valor;

                        faturamento.Caminhoes = totalCaminhoes;
                    }

                    totalCaminhoes = 0;
                    totalPecas = 0;
                }

                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");


                foreach (FaturamentoDTO i in listFaturamento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + FormartarData(i.Data) + "', valor: " + i.Valor.ToString().Replace(",", ".") + ",b:" + i.Pecas.ToString().Replace(",", ".") + ",c:" + i.Caminhoes.ToString().Replace(",", ".") +"});");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divFaturamento',");
                stb.Append("data: dadosValorMeses,");
                stb.Append("xkey: 'y',");
                stb.Append("ykeys: ['valor', 'b', 'c'],");
                stb.Append("labels: ['Valor', 'Peças', 'Caminhões'],");
                stb.Append("hideHover: 'auto',");
                stb.Append("parseTime: false");
                stb.Append("});");

                stb.Append("});");
                stb.Append("</script>");

                return stb.ToString();
            }
            else
                return "";
        }

        public string RetonarGraficoBuscaFaturamentoPorCliente(DateTime dataInicial, DateTime dataFinal, string cliente, string grupoProduto)
        {
            List<FaturamentoDTO> listFaturamento = _faturamento.BuscaFaturamentoMes(dataInicial, dataFinal, cliente, grupoProduto);

            if (listFaturamento.Count > 0)
            {
                List<FaturamentoDTO> listGruposProdutosFaturamento = _faturamento.BuscarFaturamentoMesPorGrupoProduto(dataInicial, dataFinal, cliente, grupoProduto);

                decimal totalPecas = 0;
                decimal totalCaminhoes = 0;

                foreach (FaturamentoDTO faturamento in listFaturamento)
                {
                    var dadosPeca = listGruposProdutosFaturamento.Where(e => e.Data == faturamento.Data &&
                                                                       (e.CodigoGrupo == "100" || e.CodigoGrupo == "103" || e.CodigoGrupo == "129"));

                    if (dadosPeca != null)
                    {
                        foreach (var fatPecas in dadosPeca)
                            totalPecas += fatPecas.Valor;

                        faturamento.Pecas = totalPecas;
                    }

                    var dadosCaminhao = listGruposProdutosFaturamento.Where(e => e.Data == faturamento.Data &&
                                                                           (e.CodigoGrupo == "107" || e.CodigoGrupo == "127"));

                    if (dadosCaminhao != null)
                    {
                        foreach (var fatCaminhao in dadosCaminhao)
                            totalCaminhoes += fatCaminhao.Valor;

                        faturamento.Caminhoes = totalCaminhoes;
                    }

                    totalCaminhoes = 0;
                    totalPecas = 0;
                }

                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");


                foreach (FaturamentoDTO i in listFaturamento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + FormartarData(i.Data) + "', valor: " + i.Valor.ToString().Replace(",", ".") + ",b:" + i.Pecas.ToString().Replace(",", ".") + ",c:" + i.Caminhoes.ToString().Replace(",", ".") + "});");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divFaturamento',");
                stb.Append("data: dadosValorMeses,");
                stb.Append("xkey: 'y',");
                stb.Append("ykeys: ['valor','b', 'c'],");
                stb.Append("labels: ['Valor', 'Peças', 'Caminhões'],");
                stb.Append("hideHover: 'auto',");
                stb.Append("parseTime: false");
                stb.Append("});");

                stb.Append("});");
                stb.Append("</script>");

                return stb.ToString();
            }
            else
                return "";
        }

        public string ObterGraficoFaturamentoPorCliente(DateTime dataInicial, DateTime dataFinal)
        {
            List<FaturamentoDTO> listFaturamento = _faturamento.ObterFaturamentoPorCliente(dataInicial, dataFinal);

            if (listFaturamento.Count > 0)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");


                foreach (FaturamentoDTO i in listFaturamento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + i.NomeCliente + "', valor: " + i.Valor.ToString().Replace(",", ".") + " });");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divFaturamentoCliente',");
                stb.Append("data: dadosValorMeses,");
                stb.Append("xkey: 'y',");
                stb.Append("ykeys: ['valor'],");
                stb.Append("labels: ['Valor'],");
                stb.Append("hideHover: 'auto',");
                stb.Append("parseTime: false");
                stb.Append("});");

                stb.Append("});");
                stb.Append("</script>");

                return stb.ToString();
            }
            else
                return "";
        }

        private string FormartarData(string data)
        {
            string ano = data.Substring(0, 4);
            string mes = data.Substring(4, 2);

            switch(mes)
            {
                case "01":
                    mes = "Jan";
                    break;
                case "02":
                    mes ="Fev";
                    break;
                case "03":
                    mes ="Mar";
                    break;
                case "04":
                    mes ="Abr";
                    break;
                case "05":
                    mes ="Mai";
                    break;
                case "06":
                    mes ="Jun";
                    break;
                case "07":
                    mes ="Jul";
                    break;
                case "08":
                    mes ="Ago";
                    break;
                case "09":
                    mes ="Set";
                    break;
                case "10":
                    mes ="Out";
                    break;
                case "11":
                    mes ="Nov";
                    break;
                case "12":
                    mes = "Dez";
                    break;
                default:
                    mes = "";
                    break;
            }

            return mes + "-" + ano;
        }
    }
}
