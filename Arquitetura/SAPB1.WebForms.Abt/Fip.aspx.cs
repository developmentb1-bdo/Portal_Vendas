using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Producao;

namespace SAPB1.WebForms.Abt
{
    public partial class Fip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string caminho = Server.MapPath("~/ArquivosExportacao");
            string nomeArquivo = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".txt";
            string caminhoCompleto = caminho + "//" + nomeArquivo;

            ProducaoBLL producaoBLL = new ProducaoBLL();
            string retorno = producaoBLL.GerarTxtArquivoFip(caminhoCompleto);

            if (retorno.Equals(""))
            {
                try
                {
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=Fip.txt");
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