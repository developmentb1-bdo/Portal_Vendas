using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Estoque;
using SAPB1.BLL.Item;
using SAPB1.BLL.ItensTabelaPreco;
using SAPB1.DTO.Deposito;
using SAPB1.DTO.Estoque;
using SAPB1.DTO.Item;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.TabelaPreco;
using SAPB1.WebForm.App_Code;

namespace SAPB1.WebForm
{
    public partial class Estoque : System.Web.UI.Page
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

                    PopularGridView(null);
                    CarregarCombos();

                    if (ViewState["CodigosItens"] == null)
                        CriarViewStateCodigoItem();

                    if (ViewState["Depositos"] == null)
                        CriarViewStateDeposito();

                    PopularHiddenFieldDadosItens();
                    PopularHiddenFieldDadosDeposito();
                }
                else
                {
                    pnlAviso.Visible = false;
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

        void CarregarCombos()
        {
            Combo.Estoque(cmbDeposito, "0");
        }

        protected void grdEstoque_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                /*grdEstoque.PageIndex = e.NewPageIndex;

                ItemDTO itemDTO = new ItemDTO();
                itemDTO.ItemName = txtNomeItem.Text;
                itemDTO.ItemCode = txtCodigoItem.Text;

                EstoqueDTO estoqueDTO = new EstoqueDTO();
                estoqueDTO.Item = itemDTO;*/

                ItemDTO itemDTO = new ItemDTO();
                itemDTO.ItemName = txtNomeItem.Text;
                itemDTO.ItemCode = txtCodigoItem.Text;

                EstoqueDTO estoqueDTO = new EstoqueDTO();
                estoqueDTO.Item = itemDTO;

                if (!string.IsNullOrEmpty(txtDeposito.Text))
                {
                    DepositoDTO depositoDTO = new DepositoDTO();
                    //depositoDTO.WhsCode = cmbDeposito.SelectedValue;
                    //depositoDTO.WhsName = txtDeposito.Text.Trim();
                    depositoDTO.WhsCode = txtDeposito.Text.Trim();

                    estoqueDTO.Deposito = depositoDTO;

                    if (string.IsNullOrEmpty(txtNomeItem.Text) && string.IsNullOrEmpty(txtCodigoItem.Text))
                        estoqueDTO.Item = null;
                }

                PopularGridView(estoqueDTO);

                grdEstoque.PageIndex = e.NewPageIndex;
                grdEstoque.DataBind();
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
            grdEstoque.DataSource = estoqueBLL.Listar(estoqueDTO,true);
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

                    if (!string.IsNullOrEmpty(txtDeposito.Text))
                    {
                        DepositoDTO depositoDTO = new DepositoDTO();
                        //depositoDTO.WhsCode = cmbDeposito.SelectedValue;
                        //depositoDTO.WhsName = txtDeposito.Text.Trim();
                        depositoDTO.WhsCode = txtDeposito.Text.Trim();

                        estoqueDTO.Deposito = depositoDTO;

                        if (string.IsNullOrEmpty(txtNomeItem.Text) && string.IsNullOrEmpty(txtCodigoItem.Text))
                            estoqueDTO.Item = null;
                    }

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
            catch(Exception er)
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
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        [WebMethod]
        public static object RetornarDadosItem(string codItem, string tabelaPreco)
        {
            ItemDTO itemDTO = new ItemDTO();
            itemDTO.SellItem = "Y";
            itemDTO.ItemCode = codItem;

            ItemBLL itemVendaBLL = new ItemBLL();
            IList<ItemDTO> listItem = itemVendaBLL.BuscarItemPorId(itemDTO);

            if (listItem.Count > 0)
            {
                ItensTabelaPrecoDTO itensTabelaPrecoDTO = new ItensTabelaPrecoDTO();
                itensTabelaPrecoDTO.Item = new ItemDTO();
                itensTabelaPrecoDTO.Item.ItemCode = listItem[0].ItemCode;
                itensTabelaPrecoDTO.TabelaPreco = new TabelaPrecoDTO();
                itensTabelaPrecoDTO.TabelaPreco.ListNum = Convert.ToInt32((tabelaPreco.Equals("") ? "0" : tabelaPreco));

                ItensTabelaPrecoBLL itensTabelaPrecoBLL = new ItensTabelaPrecoBLL();
                IList<ItensTabelaPrecoDTO> listaItens = itensTabelaPrecoBLL.Listar(itensTabelaPrecoDTO);

                return listaItens;
            }

            return new List<ItensTabelaPrecoDTO>();
        }

        [WebMethod]
        public static object RetornarDadosDeposito(string codItem, string tabelaPreco)
        {
            DepositoBLL depositoBLL = new DepositoBLL();
            IList<DepositoDTO> listDepositoDTO = depositoBLL.Listar();

            if (listDepositoDTO.Count > 0)
                return listDepositoDTO;

            return new List<DepositoDTO>();
        }

        private void PopularHiddenFieldDadosItens()
        {
            IList<ItemDTO> listItens = (IList<ItemDTO>)ViewState["CodigosItens"];

            StringBuilder stb = new StringBuilder();

            for (int i = 0; i < listItens.Count; i++)
            {
                stb.Append(listItens[i].ItemCode + "#" + listItens[i].ItemName);

                if (i < (listItens.Count - 1))
                    stb.Append("|");
            }

            lblListIds.Value = stb.ToString();
        }

        private void PopularHiddenFieldDadosDeposito()
        {
            IList<DepositoDTO> listDepositoDTO = (IList<DepositoDTO>)ViewState["Depositos"];

            StringBuilder stb = new StringBuilder();

            for (int i = 0; i < listDepositoDTO.Count; i++)
            {
                stb.Append(listDepositoDTO[i].WhsCode + "#" + listDepositoDTO[i].WhsName);

                if (i < (listDepositoDTO.Count - 1))
                    stb.Append("|");
            }

            hiddenDepo.Value = stb.ToString();
        }

        private void CriarViewStateCodigoItem()
        {
            if (ViewState["CodigosItens"] == null)
            {
                ItemBLL itemBLL = new ItemBLL();

                ItemDTO itemDTO = new ItemDTO();
                itemDTO.SellItem = "Y";
                itemDTO.validFor = "Y";

                List<string> listCategorias = new List<string>();
                listCategorias.Add("104");

                ViewState["CodigosItens"] = itemBLL.Listar(itemDTO);
            }
        }

        private void CriarViewStateDeposito()
        {
            if (ViewState["Depositos"] == null)
            {
                DepositoBLL depositoBLL = new DepositoBLL();
                
                ViewState["Depositos"] = depositoBLL.Listar();
            }
        }
    }
}