using SAPB1.BLL.Usuario;
using System;
using System.Configuration;

namespace SAPB1.WebForms.Dagan
{
    public partial class Ajuda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string usuarioPortal = ConfigurationManager.AppSettings["UserPortal"].ToString();

                UsuarioBLL usuarioBLL = new UsuarioBLL();
                hfVideo.Value = usuarioBLL.RetornarCodigoVideoYoutubeDoUsuarioPortal(usuarioPortal);
            }
        }
    }
}