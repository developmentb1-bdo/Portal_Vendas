using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAPB1.WebForms.Foton
{
    public partial class FotonMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CardCode"] == null || Session["ListNum"] == null)
                Response.Redirect("Login.aspx");

            if (Session["CardName"] != null)
            {
                lblUsuario.Text = Session["CardName"].ToString();

                lblNomeBarra.Text = Session["CardName"].ToString();
            }
        }
    }
}