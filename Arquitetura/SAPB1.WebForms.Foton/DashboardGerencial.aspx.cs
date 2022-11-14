using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Relatorio;
using SAPB1.DTO.Relatorio;
using SAPB1.DTO.Mensagens;
using SAPB1.WebForm.App_Code;
using System.Web.Services;
using SAPB1.DTO.GrupoItem;
using SAPB1.BLL.GrupoItem;
using SAPB1.DTO.Concessionario;
using SAPB1.BLL.Concessionario;

namespace SAPB1.WebForms.Foton
{
    public partial class DashboardGerencial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    GrupoItemBLL grupoItem = new GrupoItemBLL();
                    IList<GrupoItemDTO> listGrupoItens = grupoItem.ObterTodos();

                    if(listGrupoItens.Count > 0)
                    {
                        ddlGrupoProduto.Items.Clear();
                        ddlGrupoProduto.AppendDataBoundItems = true;
                        ddlGrupoProduto.Items.Add(new ListItem("Selecione", "-1"));
                        ddlGrupoProduto.DataSource = listGrupoItens;
                        ddlGrupoProduto.DataValueField = "ItmsGrpCod";
                        ddlGrupoProduto.DataTextField = "ItmsGrpNam";
                        ddlGrupoProduto.DataBind();
                    }

                    ConcessionarioBLL concessionarioBLL = new ConcessionarioBLL();
                    IList<ConcessionarioDTO> listConcessionario = concessionarioBLL.ObterConcessionarioPorGrupoCliente("122");

                    if(listConcessionario.Count > 0)
                    {
                        ddlParceiroNegocio.Items.Clear();
                        ddlParceiroNegocio.AppendDataBoundItems = true;
                        ddlParceiroNegocio.Items.Add(new ListItem("Selecione", "-1"));
                        ddlParceiroNegocio.DataSource = listConcessionario;
                        ddlParceiroNegocio.DataValueField = "CardCode";
                        ddlParceiroNegocio.DataTextField = "CardName";
                        ddlParceiroNegocio.DataBind();
                    }

                    DateTime dataFinal = DateTime.Now;
                    DateTime dataInicial = DateTime.Now.AddMonths(-12);

                    FaturamentoBLL faturamentoBLL = new FaturamentoBLL();

                    ltlFaturamento.Text = faturamentoBLL.RetornarGraficoFaturmentoMes(dataInicial, dataFinal);
                    ltrlFaturamentoCliente.Text = faturamentoBLL.ObterGraficoFaturamentoPorCliente(dataInicial, dataFinal);

                    FinanceiroBLL financeiroBLL = new FinanceiroBLL();

                    FinanceiroDTO financeiroRecebimentoDTO = financeiroBLL.RetornarTotais(dataFinal);
                    lblValorAberto.Text = financeiroRecebimentoDTO.ValorTotalAberto.ToString("c");
                    lblValorVencimento.Text = financeiroRecebimentoDTO.ValorTotalVencimento.ToString("c");
                    lblValorTotal.Text = financeiroRecebimentoDTO.ValorTotal.ToString("c");

                    ltrRecebimentos.Text = financeiroBLL.RetornarGaraficoRecebimentoAbertoPorMes(dataInicial, dataFinal);
                    gdvRecebimentosAbertoCliente.DataSource = financeiroBLL.ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(dataInicial, dataFinal);
                    gdvRecebimentosAbertoCliente.DataBind();

                    ltrRecebimentosPagos.Text = financeiroBLL.ObterGraficoRecebimentoPagoPorMes(dataInicial, dataFinal);
                    gdvRecebimentosPagos.DataSource = financeiroBLL.ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(dataInicial, dataFinal);
                    gdvRecebimentosPagos.DataBind();
                }
                catch (Exception er)
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = er.Message;
                    mensagemDTO.Tipo = MensagemType.Erro;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                }
            }
        }

        protected void btnFiltroFaturamento_Click(object sender, EventArgs e)
        {
            string grupoProduto = ddlGrupoProduto.SelectedItem.Text;
            string concesssionario = ddlParceiroNegocio.SelectedValue;

            if(grupoProduto.Equals("-1") && concesssionario.Equals("-1"))
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = "Digite o valor pesquisado em Parceiro de negócio ou Grupo de Itens";
                mensagemDTO.Tipo = MensagemType.Aviso;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                return;
            }

            //DateTime dataFinal = DateTime.Now;
            //DateTime dataInicial = DateTime.Now.AddMonths(-12);

            DateTime dataFinal = DateTime.MinValue;
            DateTime dataInicial = DateTime.MinValue;

            if (!DateTime.TryParse(txtDataInicial.Text, out dataInicial))
                dataInicial = DateTime.Now.AddMonths(-12);

            if(!DateTime.TryParse(txtDataFinal.Text, out dataFinal))
                dataFinal = DateTime.Now;

            FaturamentoBLL faturamentoBLL = new FaturamentoBLL();
            ltlFaturamento.Text = faturamentoBLL.RetonarGraficoBuscaFaturamentoPorCliente(dataInicial, dataFinal, concesssionario, grupoProduto);
        }

        protected void btnListarTudo_Click(object sender, EventArgs e)
        {
            DateTime dataFinal = DateTime.Now;
            DateTime dataInicial = DateTime.Now.AddMonths(-12);

            FaturamentoBLL faturamentoBLL = new FaturamentoBLL();

            ltlFaturamento.Text = faturamentoBLL.RetornarGraficoFaturmentoMes(dataInicial, dataFinal);
        }

        protected void gdvRecebimentosAbertoCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvRecebimentosAbertoCliente.PageIndex = e.NewPageIndex;

            DateTime dataFinal = DateTime.Now;
            DateTime dataInicial = DateTime.Now.AddMonths(-12);

            FinanceiroBLL financeiroBLL = new FinanceiroBLL();
            gdvRecebimentosAbertoCliente.DataSource = financeiroBLL.ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(dataInicial, dataFinal);
            gdvRecebimentosAbertoCliente.DataBind();
            gdvRecebimentosAbertoCliente.Focus();
        }

        protected void gdvRecebimentosPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvRecebimentosPagos.PageIndex = e.NewPageIndex;

            DateTime dataFinal = DateTime.Now;
            DateTime dataInicial = DateTime.Now.AddMonths(-12);

            FinanceiroBLL financeiroBLL = new FinanceiroBLL();
            gdvRecebimentosPagos.DataSource = financeiroBLL.ObterRecebimentoEmAbertoPorParceiroNegocioPorMes(dataInicial, dataFinal);
            gdvRecebimentosPagos.DataBind();
            gdvRecebimentosPagos.Focus();
        }
    }
}