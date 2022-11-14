using SAPB1.BLL.Estoque;
using SAPB1.DTO.Estoque;
using SAPB1.DTO.Item;
using SAPB1.DTO.Mensagens;
using SAPB1.WebForms.Dagan.App_Code;
using System;
using System.Web.UI.WebControls;

namespace SAPB1.WebForms.Dagan
{
    public partial class Estoque : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!IsPostBack)
            //    {
            //        if (pnlAviso.Visible)
            //        {
            //            pnlAviso.Visible = false;
            //        }

            //        PopularGridView(null);
            //    }
            //    else
            //    {
            //        pnlAviso.Visible = false;
            //    }
            //}
            //catch (Exception er)
            //{
            //    MensagemDTO mensagemDTO = new MensagemDTO();
            //    mensagemDTO.Mensagem = er.Message;
            //    mensagemDTO.Tipo = MensagemType.Erro;

            //    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            //}
        }

        protected void grdEstoque_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdEstoque.PageIndex = e.NewPageIndex;

                PopularGridView(null);
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        private void PopularGridView(EstoqueDTO estoqueDTO)
        {
            EstoqueBLL estoqueBLL = new EstoqueBLL();
            grdEstoque.DataSource = estoqueBLL.Listar(estoqueDTO);
            grdEstoque.DataBind();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfMensagemErros.Value.Equals(""))
                {
                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemName = txtNomeItem.Text;
                    itemDTO.ItemCode = txtCodigoItem.Text;

                    EstoqueDTO estoqueDTO = new EstoqueDTO();
                    estoqueDTO.Item = itemDTO;

                    PopularGridView(estoqueDTO);
                }
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = hfMensagemErros.Value;
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
                PopularGridView(null);

                grdEstoque.PageIndex = 0;
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