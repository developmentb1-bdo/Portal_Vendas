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

namespace SAPB1.WebForm
{
    public partial class TabelaPreco_Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if(pnlAviso.Visible)
                    {
                        pnlAviso.Visible = false;
                    }

                    string valorNumTabelaPreco = string.Empty;

                    if (hfNumTabelaPreco.Value == "")
                    {
                        valorNumTabelaPreco = Cache["tabPrecoId"].ToString();
                    }
                    else
                    {
                        valorNumTabelaPreco = hfNumTabelaPreco.Value;
                    }

                    if (!string.IsNullOrEmpty(valorNumTabelaPreco))
                    {
                        hfNumTabelaPreco.Value = valorNumTabelaPreco;

                        TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                        tabelaPrecoDTO.ListNum = Convert.ToInt32(valorNumTabelaPreco);

                        ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                        itensTabelaPrecoDTO.TabelaPreco = tabelaPrecoDTO;

                        PopularGrid(ref itensTabelaPrecoDTO);
                    }
                }
                else
                {
                    if(pnlAviso.Visible)
                    {
                        pnlAviso.Visible = false;
                    }
                }
            }
            catch(Exception er)
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
                tabelaPrecoDTO.ListNum = Convert.ToInt32(hfNumTabelaPreco.Value);

                ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                itensTabelaPrecoDTO.TabelaPreco = tabelaPrecoDTO;

                PopularGrid(ref itensTabelaPrecoDTO);
            }
            catch(Exception er)
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
                TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                tabelaPrecoDTO.ListNum = Convert.ToInt32(hfNumTabelaPreco.Value);

                ItemDTO itemDTO = new ItemDTO();
                itemDTO.ItemCode = txtCodigoItem.Text;
                itemDTO.ItemName = txtNomeItem.Text;

                ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                itensTabelaPrecoDTO.Item = itemDTO;
                itensTabelaPrecoDTO.TabelaPreco = tabelaPrecoDTO;

                PopularGrid(ref itensTabelaPrecoDTO);
            }
            catch(Exception er)
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
                TabelaPrecoDTO tabelaPrecoDTO = new TabelaPrecoDTO();
                tabelaPrecoDTO.ListNum = Convert.ToInt32(hfNumTabelaPreco.Value);

                ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                itensTabelaPrecoDTO.TabelaPreco = tabelaPrecoDTO;

                PopularGrid(ref itensTabelaPrecoDTO);

                grdProdutos.PageIndex = 0;
            }
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }
    }
}