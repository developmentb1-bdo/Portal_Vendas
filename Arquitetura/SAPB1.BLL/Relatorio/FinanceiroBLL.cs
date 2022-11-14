using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Relatorio;
using SAPB1.DTO.Relatorio;
using SAPB1.DALFactory.Relatorio;

namespace SAPB1.BLL.Relatorio
{
    public class FinanceiroBLL
    {
        private readonly IFinanceiro _financeiro;

        public FinanceiroBLL()
        {
            _financeiro = FinanceiroFactory.FinanceiroDAL();
        }

        public FinanceiroDTO RetornarTotais(DateTime data)
        {
            decimal valorAberto = _financeiro.RetonarValorEmAberto(data);
            decimal valorVencimento = _financeiro.RetonarValorVencimento(data);
            FinanceiroDTO financeiroDTO = new FinanceiroDTO();
            financeiroDTO.ValorTotalAberto = valorAberto;
            financeiroDTO.ValorTotalVencimento = valorVencimento;
            financeiroDTO.ValorTotal = valorAberto + valorVencimento;

            return financeiroDTO;
        }

        public string RetonarGraficoRecebimentosEmAbertoPorParceiroNegocio()
        {
            List<FinanceiroDTO> listRecebimento = _financeiro.ObterRecimentosEmAbertoPorParceiroNegocio();

            if (listRecebimento.Count > 0)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");


                foreach (FinanceiroDTO i in listRecebimento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + i.Nome + "', valor: " + i.ValorTotal.ToString().Replace(",", ".") + " });");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divRecebimento',");
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

        public FinanceiroDTO RetornarTotaisPagamento(DateTime data)
        {
            decimal valorAberto = _financeiro.RetonarValorEmAbertoPagamento(data);
            decimal valorVencimento = _financeiro.RetonarValorVencimentoPagamento(data);
            decimal valorTotal = valorAberto + valorVencimento;

            if (valorTotal < 0)
                valorTotal = valorTotal * (-1);

            FinanceiroDTO financeiroDTO = new FinanceiroDTO();
            financeiroDTO.ValorTotalAberto = (valorAberto < 0 ? valorAberto * (-1) : valorAberto);
            financeiroDTO.ValorTotalVencimento = (valorVencimento < 0 ? valorVencimento * (-1) : valorVencimento);
            financeiroDTO.ValorTotal = valorAberto + valorVencimento;

            return financeiroDTO;
        }

        public string RetonarGraficoPagamentoEmAbertoPorParceiroNegocio()
        {
            List<FinanceiroDTO> listPagamento = _financeiro.ObterPagamentoEmAbertoPorParceiroNegocio();

            if (listPagamento.Count > 0)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");


                foreach (FinanceiroDTO i in listPagamento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + i.Nome + "', valor: " + i.ValorTotal.ToString().Replace(",", ".") + " });");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divPagamento',");
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

        public string RetornarGaraficoRecebimentoAbertoPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            List<FinanceiroDTO> listRecebimento = _financeiro.ObterRecebimentosPorMesEmAberto(dataInicial, dataFinal);

            if (listRecebimento.Count > 0)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");


                foreach (FinanceiroDTO i in listRecebimento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + FormartarData(i.Data) + "', valor: " + i.ValorTotal.ToString().Replace(",", ".") + " });");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divRecebimento',");
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

            switch (mes)
            {
                case "01":
                    mes = "Jan";
                    break;
                case "02":
                    mes = "Fev";
                    break;
                case "03":
                    mes = "Mar";
                    break;
                case "04":
                    mes = "Abr";
                    break;
                case "05":
                    mes = "Mai";
                    break;
                case "06":
                    mes = "Jun";
                    break;
                case "07":
                    mes = "Jul";
                    break;
                case "08":
                    mes = "Ago";
                    break;
                case "09":
                    mes = "Set";
                    break;
                case "10":
                    mes = "Out";
                    break;
                case "11":
                    mes = "Nov";
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

        public List<FinanceiroDTO> ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            List<FinanceiroDTO> listFinanceiro = _financeiro.ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(dataInicial, dataFinal);

            if (listFinanceiro.Count > 0)
            {
                foreach (FinanceiroDTO financeiro in listFinanceiro)
                {
                    financeiro.Data = FormartarData(financeiro.Data);
                }
            }

            return listFinanceiro;
        }

        public string ObterGraficoRecebimentoPagoPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            List<FinanceiroDTO> listRecebimento = _financeiro.ObterRecebimentoPagoPorMes(dataInicial, dataFinal);

            if (listRecebimento.Count > 0)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<script type=\"text/javascript\">");
                stb.Append("$(function () { ");
                stb.Append("\"use strict\";");
                stb.Append("var dadosValorMeses = [];");

                foreach (FinanceiroDTO i in listRecebimento)
                {
                    stb.Append("dadosValorMeses.push({ y: '" + FormartarData(i.Data) + "', valor: " + i.ValorTotal.ToString().Replace(",", ".") + " });");
                }

                stb.Append("var bars2 = new Morris.Bar({");
                stb.Append("element: 'divRecebimentoPagos',");
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

        public List<FinanceiroDTO> ObterRecebimentoPagoPorParceiroNegocioPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            List<FinanceiroDTO> listFinanceiro = _financeiro.ObterRecebimentoPagoPorParceiroNegocioPorMes(dataInicial, dataFinal);

            if (listFinanceiro.Count > 0)
            {
                foreach (FinanceiroDTO financeiro in listFinanceiro)
                {
                    financeiro.Data = FormartarData(financeiro.Data);
                }
            }

            return listFinanceiro;
        }
    }
}
