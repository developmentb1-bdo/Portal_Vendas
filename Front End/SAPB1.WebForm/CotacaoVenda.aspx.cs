/*
 * @author Victor Oliveira.
 */

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.PedidoVenda;

namespace SAPB1.WebForm
{
    public partial class CotacaoVenda : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Carregar();
        }

        void Carregar()
        {
            CotacaoBLL cotacaoBLL = new CotacaoBLL();

            gridCotacao.DataSource = cotacaoBLL.Listar();
            gridCotacao.DataBind();
        }

        protected void gridCotacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            

            if (true)
                Carregar();

            gridCotacao.PageIndex = e.NewPageIndex;
            gridCotacao.DataBind();
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CotacaoVenda_Action.aspx");
        }
    }
}