using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.DTO.TabelaPreco;
using SAPB1.BLL.TabelaPreco;
using SAPB1.WebForm.App_Code;
using SAPB1.DTO.Mensagens;

namespace SAPB1.WebForms.Foton
{
    public partial class TabelaPreco : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    hfTabelaPrecoConcessionario.Value = Session["ListNum"].ToString();
                    PopularGrid(Convert.ToInt32(hfTabelaPrecoConcessionario.Value));

                    if (pnlAviso.Visible)
                    {
                        pnlAviso.Visible = false;
                    }
                }
                else
                {
                    pnlAviso.Visible = false;
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void lkbPrecosGrid_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton linkButtonProduto = (LinkButton)sender;
                Response.Redirect("TabelaPreco_Action.aspx?id=" + linkButtonProduto.CommandArgument);
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void grdTabelaPreco_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                PopularGrid(Convert.ToInt32(hfTabelaPrecoConcessionario.Value));
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        private void PopularGrid(int codTabela)
        {
            TabelaPrecoBLL tabelaBLL = new TabelaPrecoBLL();
            grdTabelaPreco.DataSource = tabelaBLL.ListarTabelaPrecoConcessionario(codTabela);
            grdTabelaPreco.DataBind();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfMensagemErro.Value.Equals(""))
                {
                    TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                    tabelaPrecoDTO.ListName = txtNomeTabelaPreco.Text;
                    tabelaPrecoDTO.ListNum = Convert.ToInt32((txtCodTabelaPreco.Text.Equals("") ? "0" : txtCodTabelaPreco.Text));
                    //PopularGrid(tabelaPrecoDTO);
                }
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = hfMensagemErro.Value;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnListarTudo_Click(object sender, EventArgs e)
        {
            try
            {
                PopularGrid(Convert.ToInt32(hfTabelaPrecoConcessionario.Value));
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }
    }
}