using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Usuario;
using System.Configuration;

namespace SAPB1.WebForm
{
    public partial class VideoAjuda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string usuarioPortal = ConfigurationManager.AppSettings["UserPortal"].ToString();

                UsuarioBLL usuarioBLL = new UsuarioBLL();
                hfVideo.Value = usuarioBLL.RetornarCodigoVideoYoutubeDoUsuarioPortal(usuarioPortal);
            }
        }
    }
}