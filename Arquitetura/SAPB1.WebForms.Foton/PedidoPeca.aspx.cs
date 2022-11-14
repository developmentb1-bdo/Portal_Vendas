using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.DTO.PedidoPeca;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.BLL.PedidoPeca;
using SAPB1.DTO.Mensagens;
using SAPB1.WebForm.App_Code;

namespace SAPB1.WebForms.Foton
{
    public partial class PedidoPeca : System.Web.UI.Page
    {
        private readonly PedidoPecaBLL _pedidoPeca;

        public PedidoPeca()
        {
            _pedidoPeca = new PedidoPecaBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (pnlAviso.Visible)
                    pnlAviso.Visible = false;

                if (!IsPostBack)
                {
                    hfIdConcessionario.Value = Session["CardCode"].ToString();
                    hfListaPreco.Value = Session["ListNum"].ToString();

                    CarregarDadosGrid(null);
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message + "-" + er.StackTrace;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("PedidoPeca_Action.aspx");
        }

        protected void grdPedidoVenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPedidoVenda.PageIndex = e.NewPageIndex;

                if (txtNumeroPedido.Text.Trim().Equals("") && txtDataInicial.Text.Trim().Equals("") && txtDataFinal.Text.Trim().Equals(""))
                    CarregarDadosGrid(null);
                else
                {
                    PedidoPecaDTO pedidoPecaDTO = new PedidoPecaDTO();
                    pedidoPecaDTO.CardCode = hfIdConcessionario.Value;

                    if (!txtNumeroPedido.Text.Equals(""))
                        pedidoPecaDTO.DocNum = Convert.ToInt32(txtNumeroPedido.Text);

                    DateTime dataInicial = DateTime.MinValue;
                    DateTime dataFinal = DateTime.MinValue;

                    if (!txtDataInicial.Text.Equals(""))
                    {
                        if (DateTime.TryParse(txtDataInicial.Text, out dataInicial))
                            pedidoPecaDTO.DocDate = dataInicial;
                    }

                    if (!txtDataFinal.Text.Equals(""))
                    {
                        if (DateTime.TryParse(txtDataFinal.Text, out dataFinal))
                            pedidoPecaDTO.DocDueDate = dataFinal;
                    }

                    CarregarDadosGrid(pedidoPecaDTO);
                }

            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        private void CarregarDadosGrid(PedidoPecaDTO pedidoPecaDTO)
        {
            if (pedidoPecaDTO == null)
                grdPedidoVenda.DataSource = _pedidoPeca.ListarPedidosPorIdConcessionario(hfIdConcessionario.Value, hfListaPreco.Value);
            else
                grdPedidoVenda.DataSource = _pedidoPeca.BuscarPedidoPorConcessionario(pedidoPecaDTO, hfListaPreco.Value);

            grdPedidoVenda.DataBind();
        }

        protected void btnListarTudo_Click(object sender, EventArgs e)
        {
            try
            {
                CarregarDadosGrid(null);

                txtNumeroPedido.Text = string.Empty;
                txtDataInicial.Text = string.Empty;
                txtDataFinal.Text = string.Empty;
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfMensagemErro.Value.Equals(""))
                {
                    if (txtNumeroPedido.Text.Trim().Equals("") && txtDataInicial.Text.Trim().Equals("") && txtDataFinal.Text.Trim().Equals(""))
                    {
                        MensagemDTO mensagemDTO = new MensagemDTO();
                        mensagemDTO.Mensagem = "Digite um valor no mínimo em um parâmetro";
                        mensagemDTO.Tipo = MensagemType.Aviso;

                        Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                    }
                    else
                    {
                        PedidoPecaDTO pedidoPecaDTO = new PedidoPecaDTO();
                        pedidoPecaDTO.CardCode = hfIdConcessionario.Value;

                        if (!txtNumeroPedido.Text.Equals(""))
                            pedidoPecaDTO.DocNum = Convert.ToInt32(txtNumeroPedido.Text);

                        DateTime dataInicial = DateTime.MinValue;
                        DateTime dataFinal = DateTime.MinValue;

                        if(!txtDataInicial.Text.Equals(""))
                        {
                            if (DateTime.TryParse(txtDataInicial.Text, out dataInicial))
                                pedidoPecaDTO.DocDate = dataInicial;
                        }
                        
                        if(!txtDataFinal.Text.Equals(""))
                        {
                            if (DateTime.TryParse(txtDataFinal.Text, out dataFinal))
                                pedidoPecaDTO.DocDueDate = dataFinal;
                        }

                        CarregarDadosGrid(pedidoPecaDTO);
                    }
                }
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = hfMensagemErro.Value;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void lkbDetalhesPedidoGrid_Click(object sender, EventArgs e)
        {
            LinkButton linkBtnGrid = (LinkButton)sender;

            Response.Redirect("PedidoPeca_Action.aspx?id=" + linkBtnGrid.CommandArgument);
        }

        protected string RetornarStatus(string status, string statusConcessionario)
        {
            if(!string.IsNullOrEmpty(statusConcessionario))
            {
                switch (statusConcessionario)
                {
                    case "W":
                        return "EM ANÁLISE";
                    case "A":
                        return "EM PROCESSAMENTO";
                    case "P":
                        return "ATENDIDO PARCIAL";
                    case "F":
                        return "ATENDIDO TOTAL";
                    case "C":
                        return "CANCELADO";
                    case "B":
                        return "FINANCEIRO";
                    default:
                        return "INDEFINIDO";
                }
            }
            else
            {
                switch (status)
                {
                    case "O":
                        return "ABERTO";
                    case "C":
                        return "FECHADO";
                    default:
                        return "INDEFINIDO";
                }
            }
        }
    }
}