using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.DTO.PedidoVenda;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.BLL.PedidoVenda;
using SAPB1.DTO.Mensagens;
using SAPB1.WebForm.App_Code;
using SAPB1.DTO.Funcionario;
using SAPB1.BLL.Funcionario;

namespace SAPB1.WebForm
{
    public partial class PedidoVenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ColaboradorBLL colaboradorBLL = new ColaboradorBLL();
                    ColaboradorDTO colaborador = colaboradorBLL.SelecionarColaboradorPorId(Convert.ToInt32(Session["EmpId"]));

                    if (colaborador.EmpId == 0)
                        return;

                    if (colaborador.Position == 3)
                    {
                        CarregarDadosGrid(null);
                        hfEmprId.Value = "0";
                    }
                    else if (colaborador.Position == 4)
                    {
                        PedidoVendaDTO pedidoVenda = new PedidoVendaDTO();
                        pedidoVenda.Vendedor = new DTO.Funcionario.Vendedor.VendedorDTO();
                        pedidoVenda.Vendedor.SlpCode = colaborador.SalesPrson;

                        hfEmprId.Value = pedidoVenda.Vendedor.SlpCode.ToString();

                        CarregarDadosGrid(pedidoVenda);
                    }
                }
            }
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("PedidoVenda_Action.aspx");
        }

        protected void grdPedidoVenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                

                if (hfEmprId.Value.Equals("") || hfEmprId.Value.Equals("0"))
                {
                    if(txtCnpj.Text.Equals("") && txtDtLancamentoFinal.Text.Equals("") && txtDtLancamentoInicial.Text.Equals("") && txtNumeroPedido.Text.Equals("") && txtRazaoSocial.Text.Equals(""))
                        CarregarDadosGrid(null);
                    else
                        Buscar();
                }
                else
                {
                    PedidoVendaDTO pedidoVenda = new PedidoVendaDTO();
                    pedidoVenda.Vendedor = new DTO.Funcionario.Vendedor.VendedorDTO();
                    pedidoVenda.Vendedor.SlpCode = Convert.ToInt32(hfEmprId.Value);

                    if (txtCnpj.Text.Equals("") && txtDtLancamentoFinal.Text.Equals("") && txtDtLancamentoInicial.Text.Equals("") && txtNumeroPedido.Text.Equals("") && txtRazaoSocial.Text.Equals(""))
                        CarregarDadosGrid(pedidoVenda);
                    else
                        Buscar();
                }

                grdPedidoVenda.PageIndex = e.NewPageIndex;
                grdPedidoVenda.DataBind();
            }
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        private void CarregarDadosGrid(PedidoVendaDTO pedidoVendaDTO)
        {
            PedidoVendaBLL pedidoVendaBLL = new PedidoVendaBLL();

            if (Session["U_AcessoPortal"] != null)
            {
                if (Session["U_AcessoPortal"].ToString() == "02")
                {
                    pedidoVendaDTO = new PedidoVendaDTO();
                    pedidoVendaDTO.OwnerCode = Session["EmpId"].ToString();
                }
            }

            grdPedidoVenda.DataSource = pedidoVendaBLL.Listar(pedidoVendaDTO);
            grdPedidoVenda.DataBind();
        }

        protected void btnListarTudo_Click(object sender, EventArgs e)
        {
            try
            {
                grdPedidoVenda.PageIndex = 0;

                txtRazaoSocial.Text = "";
                txtCnpj.Text = "";
                txtDtLancamentoFinal.Text = "";
                txtDtLancamentoInicial.Text = "";
                txtNumeroPedido.Text = "";
                

                if (hfEmprId.Value.Equals("") || hfEmprId.Value.Equals("0"))
                    CarregarDadosGrid(null);
                else
                {
                    PedidoVendaDTO pedidoVenda = new PedidoVendaDTO();
                    pedidoVenda.Vendedor = new DTO.Funcionario.Vendedor.VendedorDTO();
                    pedidoVenda.Vendedor.SlpCode = Convert.ToInt32(hfEmprId.Value);

                    CarregarDadosGrid(pedidoVenda);
                }
            }
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            try
            {
                if (hfMensagemErro.Value.Equals(""))
                {
                    PedidoVendaDTO pedidoVendaDTO = new PedidoVendaDTO();

                    if (!txtNumeroPedido.Text.Equals(""))
                    {
                        pedidoVendaDTO.DocNum = Convert.ToInt32(txtNumeroPedido.Text);
                    }

                    DateTime dataInicial = DateTime.MinValue;
                    DateTime dataFinal = DateTime.MinValue;

                    DateTime.TryParse(txtDtLancamentoInicial.Text, out dataInicial);
                    DateTime.TryParse(txtDtLancamentoFinal.Text, out dataFinal);

                    pedidoVendaDTO.DocDate = dataInicial;
                    pedidoVendaDTO.DocDueDate = dataFinal;
                    pedidoVendaDTO.CardName = txtRazaoSocial.Text;
                    pedidoVendaDTO.U_CNPJ = txtCnpj.Text;

                    if (hfEmprId.Value.Equals(""))
                    {
                        pedidoVendaDTO.Vendedor = new DTO.Funcionario.Vendedor.VendedorDTO();
                        pedidoVendaDTO.Vendedor.SlpCode = Convert.ToInt32(hfEmprId.Value);
                    }

                    if (Session["U_AcessoPortal"].ToString() == "02")
                        pedidoVendaDTO.OwnerCode = Session["EmpId"].ToString();

                    PedidoVendaBLL pedidoVendaBLL = new PedidoVendaBLL();
                    grdPedidoVenda.DataSource = pedidoVendaBLL.BuscarPedidoVenda(pedidoVendaDTO);
                    grdPedidoVenda.DataBind();
                }
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = hfMensagemErro.Value;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                }
            }
            catch(Exception er)
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

            Response.Redirect("PedidoVenda_Action.aspx?id=" + linkBtnGrid.CommandArgument);
        }

        protected void grdPedidoVenda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "RedirecionarPaginaGridView(" + e.Row.Cells[0].Text + ");");
                e.Row.Attributes.Add("style", "cursor:pointer");
            }
        }

        protected string RetornarNomeStatus(string docStatus, string cancelado)
        {
            if (cancelado.Equals("Y"))
            {
                return "Cancelado";
            }
            else
            {
                if (docStatus.Equals("O"))
                {
                    return "Abrir";
                }
                else
                {
                    return "Fechado";
                }
            }
        }
    }
}