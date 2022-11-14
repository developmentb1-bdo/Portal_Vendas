using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Concessionario;
using SAPB1.DTO.Concessionario;

namespace SAPB1.WebForms.Foton
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlAviso.Visible = false;
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            ConcessionarioBLL concessionarioBLL = new ConcessionarioBLL();
            ConcessionarioDTO concessionarioDTO = concessionarioBLL.RetornarDadosConcessionarioPorLogin(txtEmail.Text, txtSenha.Text);

            if (string.IsNullOrEmpty(concessionarioDTO.CardCode))
            {
                lblAviso.Text = "Login incorreto. Verifique se o usuário e senha foi digitado corretamente.";
                pnlAviso.CssClass = "alert alert-info alert-dismissible";
                pnlAviso.Visible = true;
            }
            else
            {
                Session.Add("CardCode", concessionarioDTO.CardCode);
                Session.Add("ListNum", concessionarioDTO.ListNum);
                Session.Add("CardName", concessionarioDTO.CardName);

                Response.Redirect("PedidoPeca.aspx");
            }
        }
    }
}