using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.DeskPararelo.Estoque;
using SAPB1.DTO.Estoque;

namespace SAPB1.WebForms.DeskPararelo
{
    public partial class MonitoramentoEstoque : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["CodUsu"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }

                    EstoqueDadosBLL estoqueDadosBLL = new EstoqueDadosBLL();
                    ltlTabelaEstoque.Text = estoqueDadosBLL.RetonarTabelaPopuladaHtml();
                }
            }
            catch
            {

            }
        }
    }
}