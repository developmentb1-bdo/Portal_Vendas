using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.ItensTabelaPreco;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.TabelaPreco;
using SAPB1.DTO.Item;
using SAPB1.DTO.Mensagens;
using SAPB1.WebForm.App_Code;

namespace SAPB1.WebForms.Foton
{
    public partial class TabelaPreco_Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                pnlAviso.Visible = false;

                if (!IsPostBack)
                {
                    hfNumTabelaPreco.Value = Session["CardCode"].ToString();

                    ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
                    grdProdutos.DataSource = itensTabelaPrecoBLL.ListarComVariosPrecos(hfNumTabelaPreco.Value);
                    grdProdutos.DataBind();
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

        protected void grdProdutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdProdutos.PageIndex = e.NewPageIndex;

                TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();

                ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();

                if (!txtCodigoItem.Text.Equals("") || !txtNomeItem.Text.Equals(""))
                {
                    ItemDTO itemDTO = new ItemDTO();
                    itemDTO.ItemCode = txtCodigoItem.Text;
                    itemDTO.ItemName = txtNomeItem.Text;
                    itensTabelaPrecoDTO.Item = itemDTO;

                    ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
                    grdProdutos.DataSource = itensTabelaPrecoBLL.BuscarItensDeMaisDeUmaTabelapreco(hfNumTabelaPreco.Value, itensTabelaPrecoDTO);
                    grdProdutos.DataBind();
                }
                else
                {
                    ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
                    grdProdutos.DataSource = itensTabelaPrecoBLL.ListarComVariosPrecos(hfNumTabelaPreco.Value);
                    grdProdutos.DataBind();
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

        private void PopularGrid(ref ItensTabelaPrecoDTO itensTabelaPrecoDTO)
        {
            ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
            grdProdutos.DataSource = itensTabelaPrecoBLL.Listar(itensTabelaPrecoDTO);
            grdProdutos.DataBind();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ItemDTO itemDTO = new ItemDTO();
                itemDTO.ItemCode = txtCodigoItem.Text;
                itemDTO.ItemName = txtNomeItem.Text;

                ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                itensTabelaPrecoDTO.Item = itemDTO;

                ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
                grdProdutos.DataSource = itensTabelaPrecoBLL.BuscarItensDeMaisDeUmaTabelapreco(hfNumTabelaPreco.Value, itensTabelaPrecoDTO);
                grdProdutos.DataBind();
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnListarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
                grdProdutos.DataSource = itensTabelaPrecoBLL.ListarComVariosPrecos(hfNumTabelaPreco.Value);
                grdProdutos.DataBind();

                txtCodigoItem.Text = string.Empty;
                txtNomeItem.Text = string.Empty;
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