using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.DTO.EstruturaItem;
using SAPB1.BLL.EstruturaItem;

namespace SAPB1.WebForms.Abt
{
    public partial class ListaMateriais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string caminho = Server.MapPath("~/ArquivosExportacao");
            string nomeArquivo = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".txt";
            string caminhoCompleto = caminho + "//" + nomeArquivo;

            EstruturaItemBLL estruturaItemBLL = new EstruturaItemBLL();
            string retorno = estruturaItemBLL.GerarListaMateriasTxt(caminhoCompleto);

            if (retorno.Equals(""))
            {
                try
                {
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=ListaMateriais.txt");
                    Response.WriteFile(caminhoCompleto);
                    Response.End();
                }
                catch
                {

                }
            }
            else
            {
                pnlAviso.Visible = true;
                lblAviso.Text = retorno;
            }
        }
    }
}