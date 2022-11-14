using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.DeskPack;

namespace SAPB1.WebForms.DeskPararelo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlAviso.Visible = false;
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            LoginDeskBLL loginBLL = new LoginDeskBLL();
            int retorno = loginBLL.AutenticarUsuarioPeloSap(txtEmail.Text, txtSenha.Text);

            if (retorno > 0)
            {
                Session["CodUsu"] = retorno.ToString();

                Response.Redirect("MonitoramentoEstoque.aspx");
            }
            else
            {
                lblMensagem.Text = loginBLL.Erros;
                pnlAviso.Visible = true;
                pnlAviso.CssClass = "alert alert-danger alert-dismissible";
            }
        }
    }
}