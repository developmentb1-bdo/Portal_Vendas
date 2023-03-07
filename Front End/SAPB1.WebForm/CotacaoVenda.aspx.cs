/*
 * @author Victor Oliveira.
 */

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.PedidoVenda;
using SAPB1.DTO.PedidoVenda;

namespace SAPB1.WebForm
{
    public partial class CotacaoVenda : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    Carregar(sender, e);
        }

        protected void Carregar(object sender, EventArgs e)
        {
            CotacaoBLL cotacaoBLL = new CotacaoBLL();
            gridCotacao.DataSource = cotacaoBLL.Listar();
            gridCotacao.DataBind();
        }

        protected void gridCotacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            

            if (true)
                Carregar(sender, e);

            gridCotacao.PageIndex = e.NewPageIndex;
            gridCotacao.DataBind();
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CotacaoVenda_Action.aspx");
        }

        protected void Selecionar(object sender, EventArgs e)
        {

            if (txtCodigo.Text.Equals("") && txtCnpj.Text.Equals("") && txtRazao.Text.Equals(""))
            {
                lblMensagem.Text = "Digite o um valor em pelo menos um parâmetro.";
                pnlAviso.Visible = true;
            }

            CotacaoDTO cotacao = new CotacaoDTO();
            cotacao.DocEntry = Convert.ToInt32(txtCodigo.Text);
            cotacao.U_CNPJ = txtCnpj.Text;
            cotacao.CardName = txtRazao.Text;

            CotacaoBLL cotacaoBLL = new CotacaoBLL();
            gridCotacao.DataSource = cotacaoBLL.Selecionar(cotacao.DocEntry);
            gridCotacao.DataBind();
        }
    }
}