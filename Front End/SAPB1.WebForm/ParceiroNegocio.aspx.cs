/*
 * @author Victor Oliveira.
 */ 

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.DTO.Funcionario;
using SAPB1.DTO.Funcionario.Vendedor;
using SAPB1.BLL.Funcionario;
using SAPB1.BLL.Funcionario.Vendedor;

namespace SAPB1.WebForm
{
    public partial class ParceiroNegocio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (pnlAviso.Visible)
                pnlAviso.Visible = false;

            if(!IsPostBack)
            {
                if (pnlAviso.Visible)
                    pnlAviso.Visible = false;


                ColaboradorBLL colaboradorBLL = new ColaboradorBLL();
                ColaboradorDTO colaborador = colaboradorBLL.SelecionarColaboradorPorId(Convert.ToInt32(Session["EmpId"]));

                if (colaborador.EmpId == 0)
                    return;

                //if (colaborador.Position == 3 || colaborador.SalesPrson <= 0)
                //{
                    CarregarDados();
                    hfVend.Value = "0";
                //}
                //else if (colaborador.Position == 4)
                //{
                    //hfVend.Value = colaborador.SalesPrson.ToString();

                    //ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                    //parceiroNegocioDTO.CardCode = txtCodigo.Text;
                    //parceiroNegocioDTO.CardName = txtRazao.Text;
                    //parceiroNegocioDTO.CardType = ddlTipo.SelectedValue;
                    //parceiroNegocioDTO.U_CNPJ = txtCnpj.Text;
                    //parceiroNegocioDTO.CardType = "C-L";
                    //parceiroNegocioDTO.SlpCode = Convert.ToInt32(hfVend.Value);

                    //ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
                    //gridParceiroNegocio.DataSource = parceiroNegocioBLL.Buscar(parceiroNegocioDTO);
                    //gridParceiroNegocio.DataBind();
                //}
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParceiroNegocio_Action.aspx");
        }

        protected void gridParceiroNegocio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridParceiroNegocio.PageIndex = e.NewPageIndex;

            if (txtCodigo.Text.Equals("") && txtCnpj.Text.Equals("") && txtRazao.Text.Equals("") && ddlTipo.SelectedValue.Equals("") && (hfVend.Value.Equals("0") || hfVend.Value.Equals("-1")))
            {
                CarregarDados();
            }
            else
            {
                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                parceiroNegocioDTO.CardCode = txtCodigo.Text;
                parceiroNegocioDTO.CardName = txtRazao.Text;
                parceiroNegocioDTO.CardType = ddlTipo.SelectedValue;
                parceiroNegocioDTO.U_CNPJ = txtCnpj.Text;
                parceiroNegocioDTO.CardType = "C-L";
                //parceiroNegocioDTO.SlpCode = Convert.ToInt32(hfVend.Value);

                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
                gridParceiroNegocio.DataSource = parceiroNegocioBLL.Buscar(parceiroNegocioDTO);
                gridParceiroNegocio.DataBind();
            }

            gridParceiroNegocio.DataBind();
        }

        private void CarregarDados()
        {
            try
            {
                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

                gridParceiroNegocio.DataSource = parceiroNegocioBLL.Listar();
                gridParceiroNegocio.DataBind();
            }
            catch (Exception erro)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Tipo = MensagemType.Erro;
                mensagemDTO.Mensagem = erro.Message;

                //Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected string RetonarTipoParceiroNegocio(string cardType)
        {
            switch(cardType)
            {
                case "C":
                    return "Cliente";
                case "L":
                    return "Lead";
                case "S":
                    return "Fornecedor";
                default:
                    return "";
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Equals("") && txtCnpj.Text.Equals("") && txtRazao.Text.Equals("") && ddlTipo.SelectedValue.Equals(""))
            {
                lblMensagem.Text = "Digite o um valor em pelo menos um parâmetro.";
                pnlAviso.Visible = true;
            }
            else
            {
                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                parceiroNegocioDTO.CardCode = txtCodigo.Text;
                parceiroNegocioDTO.CardName = txtRazao.Text;
                parceiroNegocioDTO.CardType = ddlTipo.SelectedValue;
                parceiroNegocioDTO.U_CNPJ = txtCnpj.Text;
                parceiroNegocioDTO.CardType = "C-L";
                //parceiroNegocioDTO.SlpCode = Convert.ToInt32(hfVend.Value);

                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
                gridParceiroNegocio.DataSource = parceiroNegocioBLL.Buscar(parceiroNegocioDTO);
                gridParceiroNegocio.DataBind();
            }
        }

        protected void btnListarTudo_Click(object sender, EventArgs e)
        {
            gridParceiroNegocio.PageIndex = 0;

            ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

            if (hfVend.Value.Equals("0") || hfVend.Value.Equals("-1"))
                gridParceiroNegocio.DataSource = parceiroNegocioBLL.Listar();
            else
            {
                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                parceiroNegocioDTO.CardCode = txtCodigo.Text;
                parceiroNegocioDTO.CardName = txtRazao.Text;
                parceiroNegocioDTO.CardType = ddlTipo.SelectedValue;
                parceiroNegocioDTO.U_CNPJ = txtCnpj.Text;
                parceiroNegocioDTO.CardType = "C-L";
                //parceiroNegocioDTO.SlpCode = Convert.ToInt32(hfVend.Value);

                gridParceiroNegocio.DataSource = parceiroNegocioBLL.Buscar(parceiroNegocioDTO);
            }

            gridParceiroNegocio.DataBind();

            txtCodigo.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtRazao.Text = string.Empty;
            ddlTipo.SelectedValue = "";
        }

        protected string ObterNomeVendedor(int slpCode)
        {
            VendedorBLL vendedorBLL = new VendedorBLL();
            IList<VendedorDTO> listVendedorDTO = vendedorBLL.Listar(new VendedorDTO() { SlpCode = slpCode });

            return ((listVendedorDTO.Count > 0) ? listVendedorDTO[0].SlpName : "");
        }
    }
}