using SAPB1.BLL.Empresa.Filial;
using SAPB1.BLL.Funcionario;
using SAPB1.BLL.Item;
using SAPB1.BLL.ItensTabelaPreco;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.BLL.PedidoVenda;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.DTO.CondicaoPagamento;
using SAPB1.DTO.Empregado;
using SAPB1.DTO.Empresa.Filial;
using SAPB1.DTO.Estado;
using SAPB1.DTO.FormasPagamento;
using SAPB1.DTO.Funcionario;
using SAPB1.DTO.Funcionario.Vendedor;
using SAPB1.DTO.Item;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.Municipio;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.DTO.PedidoVenda;
using SAPB1.DTO.Projeto;
using SAPB1.DTO.TabelaPreco;
using SAPB1.DTO.TiposEnvio;
using SAPB1.DTO.Utilizacao;
using SAPB1.WebForms.Dagan.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAPB1.WebForms.Dagan
{
    public partial class PedidoVenda_Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //pnlAviso.Visible = false;
                //pnlAvisoItem.Visible = false;

                if (!IsPostBack)
                {
                    ColaboradorBLL colaboradorBLL = new ColaboradorBLL();
                    ColaboradorDTO colaborador = colaboradorBLL.SelecionarColaboradorPorId(Convert.ToInt32(Session["EmpId"]));

                    pnlAviso.Attributes.Add("style", "display:none");
                    pnlAvisoItem.Attributes.Add("style", "display:none");

                    if (colaborador.EmpId == 0)
                        return;

                    else if (colaborador.Position == 4)
                        hfEmpId.Value = colaborador.SalesPrson.ToString();

                    if (ViewState["CodigosItens"] == null)
                        CriarViewStateCodigoItem();

                    PopularHiddenFieldDadosItens();

                    if (ViewState["CodigosParceiroNegocio"] == null)
                        CriarViewStateParceiroNegocio();

                    PopularHiddenFieldDadosParceiroNegocio();
                    PopularHiddenFieldDadosParceiroNegocioId();

                    #region DropDown Condição de pagamento

                    Combo.CondicaoPagamento(ddlCondicoesPagamento, new CondicaoPagamentoDTO());

                    #endregion

                    #region DropDown Forma de pagamento

                    FormaPagamentoDTO formaPagamentoDTO = new FormaPagamentoDTO();
                    formaPagamentoDTO.Active = "Y";
                    formaPagamentoDTO.Type = "I";

                    Combo.FormaPagamento(ddlFormaPagamento, formaPagamentoDTO);

                    #endregion

                    #region DropDown Tipo de Envio

                    //Combo.TiposEnvio(ddlTipoEnvio, new TipoEnvioDTO());

                    #endregion

                    #region DropDown Utilização

                    UtilizacaoDTO utilizacaoDTO = new UtilizacaoDTO();
                    utilizacaoDTO.Locked = "N";

                    Combo.Utilizacao(ddlUtilizacao, utilizacaoDTO);
                    Combo.Utilizacao(ddlUtilizacaoItem, new UtilizacaoDTO());

                    #endregion

                    #region DropDown Idioma
                    //Combo.Idioma(ddlIdioma, "0");
                    #endregion

                    #region DropDown Vendedor

                    VendedorDTO vendedorDTO = new VendedorDTO();
                    vendedorDTO.Locked = "N";
                    vendedorDTO.Active = "Y";

                    Combo.Vendedor(ddlVendedor, "", vendedorDTO);
                    #endregion

                    #region DropDown Filial

                    FilialDTO filialDTO = new FilialDTO();
                    filialDTO.Disabled = "N";

                    Combo.Filial(ddlFilial, "1", filialDTO);

                    #endregion

                    #region DropDown Estado
                    EstadoDTO estadoDTO = new EstadoDTO();
                    estadoDTO.Pais = new PaisDTO();
                    estadoDTO.Pais.Name = "BR";

                    //Combo.Estado(ddlEstado, "0", estadoDTO);
                    //Combo.Estado(ddlCodigoEstado, "0", estadoDTO);
                    #endregion

                    EmpregadoDTO empregadoDTO = new EmpregadoDTO();
                    empregadoDTO.Active = "Y";

                    Combo.Empregado(ddlTitular, "0", empregadoDTO);

                    ProjetoDTO projetoDTO = new ProjetoDTO();
                    projetoDTO.Active = "Y";

                    //Combo.Projeto(ddlProjeto, "0", projetoDTO);

                    ParceiroNegocioDTO transportadora = new ParceiroNegocioDTO();
                    transportadora.CardType = "S";
                    transportadora.GroupCode = 114;

                    Combo.Transportadora(cmbTransportadora, "-1", transportadora);

                    Combo.TiposEnvio(cmbTipoFrete, new TipoEnvioDTO());

                    if (Request.QueryString["id"] == null)
                    {
                        txtDataDocumento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txtDataLancamento.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        if (!hfEmpId.Value.Equals(""))
                            ddlVendedor.SelectedValue = hfEmpId.Value;

                        txtStatus.Text = "Aberto";

                        cmbTipoFrete.SelectedValue = "2";
                        cmbTransportadora.Attributes.Add("disabled", "true");
                        txtPercentualFrete.Text = "0,00";
                        txtPercentualFrete.Attributes.Add("readonly", "true");
                    }
                    else
                    {
                        hfNumeroPedido.Value = Request.QueryString["id"].ToString();

                        CarregarDados();
                    }
                }
                else
                {
                    if (!hfUtilizacao.Value.Equals(""))
                    {
                        ddlUtilizacao.SelectedValue = hfUtilizacao.Value;
                        ddlUtilizacaoItem.SelectedValue = hfUtilizacao.Value;
                    }

                    if (!hfCondPagto.Value.Equals(""))
                        ddlCondicoesPagamento.SelectedValue = hfCondPagto.Value;
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                hfErrosRegras.Value = "1";

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
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

                ViewState["CodigosItens"] = itemBLL.ListarPorCategoria(itemDTO, listCategorias);
            }
        }

        private void CriarViewStateParceiroNegocio()
        {
            if (ViewState["CodigosParceiroNegocio"] == null)
            {
                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                parceiroNegocioDTO.CardType = "C-L";
                parceiroNegocioDTO.validFor = 'Y';

                if (!hfEmpId.Value.Equals(""))
                    parceiroNegocioDTO.SlpCode = Convert.ToInt32(hfEmpId.Value);

                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
                IList<ParceiroNegocioDTO> listPn = parceiroNegocioBLL.Listar(parceiroNegocioDTO);

                ViewState["CodigosParceiroNegocio"] = listPn;
            }
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

        private void PopularHiddenFieldDadosParceiroNegocio()
        {
            IList<ParceiroNegocioDTO> listPn = (IList<ParceiroNegocioDTO>)ViewState["CodigosParceiroNegocio"];

            StringBuilder stb = new StringBuilder();

            for (int i = 0; i < listPn.Count; i++)
            {
                stb.Append(listPn[i].CardCode + "," + listPn[i].CardName);

                if (i < (listPn.Count - 1))
                    stb.Append("|");
            }

            hfListPn.Value = stb.ToString();
        }

        private void PopularHiddenFieldDadosParceiroNegocioId()
        {
            IList<ParceiroNegocioDTO> listPn = (IList<ParceiroNegocioDTO>)ViewState["CodigosParceiroNegocio"];

            StringBuilder stb = new StringBuilder();

            for (int i = 0; i < listPn.Count; i++)
            {
                stb.Append(listPn[i].CardCode);

                if (i < (listPn.Count - 1))
                    stb.Append(",");
            }

            hfListaParceiroNegocioId.Value = stb.ToString();
        }

        private void CarregarDados()
        {
            PedidoVendaDTO pedidoVendaDTO = new PedidoVendaDTO();
            pedidoVendaDTO.DocNum = Convert.ToInt32(hfNumeroPedido.Value);

            PedidoVendaBLL pedidoVendaBLL = new PedidoVendaBLL();
            pedidoVendaDTO = pedidoVendaBLL.Listar(pedidoVendaDTO)[0];

            #region Dados do cabeçalho

            ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
            ParceiroNegocioDTO parceiroNegocioDTO = parceiroNegocioBLL.Selecionar(pedidoVendaDTO.CardCode);

            txtParceiroNegocio.Text = parceiroNegocioDTO.CardName;
            hfParceiroNegocio.Value = pedidoVendaDTO.CardCode;
            hfListaPreco.Value = parceiroNegocioDTO.ListNum.ToString();
            txtCnpj.Text = parceiroNegocioDTO.U_CNPJ;

            txtCodigoPn.Text = pedidoVendaDTO.CardCode;
            txtMoeda.Text = pedidoVendaDTO.DocCur;
            txtNumero.Text = pedidoVendaDTO.DocNum.ToString();
            txtStatus.Text = RetornarStatus(pedidoVendaDTO.DocStatus, pedidoVendaDTO.Canceled);
            txtDataLancamento.Text = pedidoVendaDTO.DocDate.ToString("dd/MM/yyyy");
            txtDataEntrega.Text = pedidoVendaDTO.DocDueDate.ToString("dd/MM/yyyy");
            txtDataDocumento.Text = pedidoVendaDTO.TaxDate.ToString("dd/MM/yyyy");
            txtNumeroPedido.Text = pedidoVendaDTO.NumAtCard;
            ddlTitular.SelectedValue = pedidoVendaDTO.OwnerCode;
            txtDespesaAdicional.Text = pedidoVendaBLL.RetornarValorDespesaFrete(Convert.ToInt64(pedidoVendaDTO.DocEntry)).ToString("n6");
            txtDespesaAdicional.Text = pedidoVendaDTO.ValorFreteCab.ToString("n2");
            txtImposto.Text = pedidoVendaDTO.VatSum.ToString("n6");

            if (pedidoVendaDTO.TemFrete != "")
                cmbTemFrete.SelectedValue = pedidoVendaDTO.TemFrete;

            txtPercentualFrete.Text = pedidoVendaDTO.PercentualFrete.ToString("n2");

            /* Início dados filial */
            FilialDTO filialDTO = new FilialDTO();
            filialDTO.Disabled = "N";
            Combo.Filial(ddlFilial, pedidoVendaDTO.Filial.BPLId.ToString(), filialDTO);

            txtCnpjFilial.Text = pedidoVendaDTO.Filial.TaxIdNum;
            /*Fim dados filial*/

            string codigoTransp = pedidoVendaBLL.RetornarCodigoTransportadora(Convert.ToInt64(pedidoVendaDTO.DocEntry));

            if (!codigoTransp.Equals("") && !codigoTransp.Equals("0"))
            {
                cmbTransportadora.SelectedValue = codigoTransp;

                ParceiroNegocioDTO transDTO = parceiroNegocioBLL.Selecionar(codigoTransp);

                if (!string.IsNullOrEmpty(transDTO.CardCode))
                    txtCnpjTransp.Text = transDTO.U_CNPJ;
            }

            cmbTipoFrete.SelectedValue = pedidoVendaDTO.TipoEnvio.TrnspCode.ToString();

            #endregion

            PedidoVendaDTO pedidoVendaEnderecoLogistica = new PedidoVendaDTO();
            pedidoVendaEnderecoLogistica.DocNum = pedidoVendaDTO.DocNum;

            SAPB1.BLL.PedidoVenda.EnderecoBLL enderecoBLL = new SAPB1.BLL.PedidoVenda.EnderecoBLL();
            SAPB1.DTO.PedidoVenda.EnderecoDTO enderecoDTO = enderecoBLL.RetornarEndereco(pedidoVendaEnderecoLogistica);

            MunicipioDTO municipioDTO = new MunicipioDTO();
            municipioDTO.Estado = new EstadoDTO();
            municipioDTO.Estado.Code = enderecoDTO.State;

            hfListaPreco.Value = parceiroNegocioDTO.ListNum.ToString();

            ddlCondicoesPagamento.SelectedValue = pedidoVendaDTO.GroupNum;
            ddlFormaPagamento.SelectedValue = pedidoVendaDTO.PeyMethod;

            txtObservacoes.Text = pedidoVendaDTO.Comments;

            #region Dados do rodapé
            txtTotalPagar.Text = pedidoVendaDTO.DocTotal.ToString("n6");

            //Vendedor
            VendedorDTO vendedorDTO = new VendedorDTO();
            vendedorDTO.Locked = "N";
            vendedorDTO.Active = "Y";
            Combo.Vendedor(ddlVendedor, pedidoVendaDTO.Vendedor.SlpCode.ToString(), vendedorDTO);
            #endregion

            ItemVendaDTO itemVendaDTO = new ItemVendaDTO();
            itemVendaDTO.DocEntry = pedidoVendaDTO.DocEntry;

            ItemVendaBLL itemVendaBLL = new ItemVendaBLL();
            IList<ItemVendaDTO> itens = itemVendaBLL.Listar(itemVendaDTO);

            gdvItens.DataSource = itens;
            gdvItens.DataBind();

            if (itens.Count > 0)
                txtTotalProdutos.Text = itens.Sum(t => t.LineTotal).ToString("n6");
            else
                txtTotalProdutos.Text = "0,000000";

            btnSalvar.Visible = false;
            btnInserirItem.Visible = false;

            gdvItens.Columns[14].Visible = false;

            txtDataEntrega.Enabled = false;
            txtParceiroNegocio.Enabled = false;
            txtCodigoItem.Enabled = false;
            txtNomeItem.Enabled = false;
            txtQtdItem.Enabled = false;
            txtDesconto.Enabled = false;
            cmbTransportadora.Enabled = false;
            cmbTipoFrete.Enabled = false;
            txtObservacoes.Enabled = false;
            ddlCondicoesPagamento.Enabled = false;
            txtCnpj.Enabled = false;
            cmbTemFrete.Enabled = false;
            txtPercentualFrete.Enabled = false;
        }

        private string RetornarStatus(string status, string cancelado)
        {
            if (cancelado.Equals("Y"))
            {
                return "Cancelado";
            }
            else
            {
                if (status.Equals("O"))
                {
                    return "Abrir";
                }
                else
                {
                    return "Fechado";
                }
            }
        }

        #region Web Methods
        [WebMethod]
        public static object RetornarDadosEmpresa(string codFilial, string habilitado)
        {
            FilialDTO filialDTO = new FilialDTO();
            filialDTO.Disabled = "N";
            filialDTO.BPLId = Convert.ToInt32(codFilial);

            FilialBLL filialBLL = new FilialBLL();

            return filialBLL.Listar(filialDTO);
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
        public static object RetornarDadosParceiroNegocio(string cardCode)
        {
            ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
            ParceiroNegocioDTO parceiroNegocioDTO = parceiroNegocioBLL.Selecionar(cardCode);

            return parceiroNegocioDTO;
        }

        #endregion

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                PedidoVendaDTO pedidoVendaDTO = new PedidoVendaDTO();
                pedidoVendaDTO.CardCode = hfParceiroNegocio.Value;
                pedidoVendaDTO.DocNum = 0;
                pedidoVendaDTO.Address = "";
                pedidoVendaDTO.Address2 = "";
                pedidoVendaDTO.Canceled = "N";
                pedidoVendaDTO.Confirmed = "N";
                pedidoVendaDTO.DiscPrcnt = 0;
                pedidoVendaDTO.DocDate = Convert.ToDateTime(txtDataLancamento.Text);
                pedidoVendaDTO.DocDueDate = (txtDataEntrega.Text.Equals("") ? DateTime.Now : Convert.ToDateTime(txtDataEntrega.Text));
                pedidoVendaDTO.DocEntry = 0;
                pedidoVendaDTO.TaxDate = Convert.ToDateTime(txtDataLancamento.Text);
                pedidoVendaDTO.PaymentGroupCode = ddlCondicoesPagamento.SelectedValue;

                switch (txtStatus.Text)
                {
                    case "Abrir":
                        pedidoVendaDTO.DocStatus = "O";
                        break;
                    default:
                        pedidoVendaDTO.DocStatus = "C";
                        break;
                }

                pedidoVendaDTO.DocTotalSy = Convert.ToDouble(txtTotalPagar.Text.Equals("") ? "0" : txtTotalPagar.Text);
                pedidoVendaDTO.Filial = new FilialDTO();
                pedidoVendaDTO.Filial.Disabled = "N";
                pedidoVendaDTO.Filial.BPLId = Convert.ToInt32(ddlFilial.SelectedValue);

                pedidoVendaDTO.Canceled = "N";
                pedidoVendaDTO.HandWrtten = "N";

                pedidoVendaDTO.JrnlMemo = "Pedido feito do portal";
                pedidoVendaDTO.LangCode = "-1";

                pedidoVendaDTO.NumAtCard = txtNumeroPedido.Text;

                pedidoVendaDTO.PartSupply = "N";
                pedidoVendaDTO.Confirmed = "N";
                pedidoVendaDTO.Pick = "N";
                pedidoVendaDTO.PoPrss = "N";

                pedidoVendaDTO.Vendedor = new VendedorDTO();
                pedidoVendaDTO.Vendedor.Locked = "N";
                pedidoVendaDTO.Vendedor.Active = "Y";
                pedidoVendaDTO.Vendedor.SlpCode = Convert.ToInt32(ddlVendedor.SelectedValue);

                pedidoVendaDTO.PickRmrk = "";

                SAPB1.DTO.PedidoVenda.EnderecoDTO enderecoDTO = new SAPB1.DTO.PedidoVenda.EnderecoDTO();

                enderecoDTO.Incoterms = "";
                enderecoDTO.QoP = "";
                enderecoDTO.PackDesc = "";
                enderecoDTO.Vehicle = "";
                enderecoDTO.Brand = "";
                enderecoDTO.NoSu = "";
                enderecoDTO.NfRef = "";

                pedidoVendaDTO.TipoEnvio = new TipoEnvioDTO();
                pedidoVendaDTO.TipoEnvio.TrnspCode = Convert.ToInt32(cmbTipoFrete.SelectedValue);
                pedidoVendaDTO.TransportadoraId = cmbTransportadora.SelectedValue;
                pedidoVendaDTO.Comments = txtObservacoes.Text;
                pedidoVendaDTO.DespesasAdicionais = (txtDespesaAdicional.Text.Equals("") ? 0 : Convert.ToDouble(txtDespesaAdicional.Text.Replace(".", "").Replace(".", ",")));
                pedidoVendaDTO.DocTotal = (txtTotalProdutos.Text.Equals("") ? 0 : Convert.ToDouble(txtTotalProdutos.Text.Replace(".", ",")));
                pedidoVendaDTO.DocTotal += pedidoVendaDTO.DespesasAdicionais;

                pedidoVendaDTO.TemFrete = cmbTemFrete.SelectedValue;

                if (cmbTemFrete.SelectedValue.Equals("S") && cmbTipoFrete.SelectedValue.Equals("2"))
                    pedidoVendaDTO.TemFrete = "N";

                pedidoVendaDTO.PercentualFrete = txtPercentualFrete.Text.Equals("") ? 0 : Convert.ToDouble(txtPercentualFrete.Text);

                if (!txtPercentualDesconto.Text.Equals(""))
                    pedidoVendaDTO.DiscPrcnt = Convert.ToDouble(txtPercentualDesconto.Text.Replace(".", "").Replace(".", ","));

                IList<ItemVendaDTO> lista = new List<ItemVendaDTO>();
                string[] dadosItens = hfDadosItens.Value.Split('#');

                if (dadosItens.Length > 0)
                {
                    for (int i = 0; i < dadosItens.Length; i++)
                    {
                        if (dadosItens[i] != "")
                        {
                            string[] dadoItem = dadosItens[i].Split('|');

                            if (dadoItem.Length == 7)
                            {
                                lista.Add(new ItemVendaDTO()
                                {
                                    DiscPrcnt = (dadoItem[4] == "" ? 0 : Convert.ToDouble(dadoItem[4].Replace(".", ","))),
                                    ItemCode = dadoItem[1],
                                    LineNum = ((dadoItem[0] == "" ? 0 : Convert.ToInt32(dadoItem[0]))),
                                    LineTotal = (dadoItem[6] == "" ? 0 : Convert.ToDouble(dadoItem[6].Replace(".", ","))),
                                    Quantity = (dadoItem[2] == "" ? 0 : Convert.ToDouble(dadoItem[2].Replace(".", ","))),
                                    Price = (dadoItem[3] == "" ? 0 : Convert.ToDouble(dadoItem[3].Replace(".", ","))),
                                    Usage = Convert.ToInt32(hfUtilizacao.Value)
                                });
                            }
                        }
                    }
                }

                PedidoVendaBLL pedidoVendaBLL = new PedidoVendaBLL();
                string retorno = pedidoVendaBLL.InseriPedidoVenda(pedidoVendaDTO, lista);

                if (string.IsNullOrEmpty(retorno))
                    Response.Redirect("PedidoVenda.aspx");
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = retorno;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    hfErrosRegras.Value = "1";
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Tipo = MensagemType.Erro;
                mensagemDTO.Mensagem = er.Message;

                hfErrosRegras.Value = "1";

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnInserirItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if(!hfErros.Value.Equals(""))
                //{
                //    MensagemDTO mensagemDTO = new MensagemDTO();
                //    mensagemDTO.Mensagem = hfErros.Value.Replace("-", "<li>");
                //    mensagemDTO.Tipo = MensagemType.Erro;
                //    Mensagens.MostrarMensagem(ref pnlAvisoItem, ref lblAvisosItem, mensagemDTO);

                //    btnInserirItem.Focus();

                //    return;
                //}

                //if(hfUtilizacao.Value.Equals(""))
                //{
                //    MensagemDTO mensagemDTO = new MensagemDTO();
                //    mensagemDTO.Mensagem = "Cliente sem utilização padrão";
                //    mensagemDTO.Tipo = MensagemType.Erro;
                //    Mensagens.MostrarMensagem(ref pnlAvisoItem, ref lblAvisosItem, mensagemDTO);

                //    btnInserirItem.Focus();

                //    return;
                //}

                //IList<ItemVendaDTO> listItemVenda = new List<ItemVendaDTO>();

                //if (ViewState["listaItensGrid"] != null)
                //    listItemVenda = (IList<ItemVendaDTO>)ViewState["listaItensGrid"];

                //ItemVendaDTO itemVendaDTO = new ItemVendaDTO();
                //itemVendaDTO.ItemCode = hfItemId.Value;
                //itemVendaDTO.Quantity = Convert.ToDouble(txtQtdItem.Text.Replace(".", ","));
                //itemVendaDTO.Price = Convert.ToDouble(txtPrecoVenda.Text.Replace(".", ","));
                //itemVendaDTO.Usage = Convert.ToInt32(hfUtilizacao.Value);
                //itemVendaDTO.CSTCode = "";
                //itemVendaDTO.UomCode = "";
                //itemVendaDTO.LineTotal = Convert.ToDouble(txtTotal.Text.Replace(".", ","));
                //itemVendaDTO.LineNum = listItemVenda.Count + 1;
                //itemVendaDTO.LinePoPrss = "N";
                //itemVendaDTO.TaxCode = "0";
                //itemVendaDTO.CFOPCode = "";
                //itemVendaDTO.PackQty = 0;
                //itemVendaDTO.DiscPrcnt = Convert.ToDouble(txtDesconto.Text.Equals("") ? "0" : txtDesconto.Text);
                //listItemVenda.Add(itemVendaDTO);

                //gdvItens.DataSource = listItemVenda;
                //gdvItens.DataBind();

                //ViewState["listaItensGrid"] = listItemVenda;

                //double total = listItemVenda.Sum(t => t.LineTotal);

                //txtTotalProdutos.Text = total.ToString("n2");

                //double frete = txtDespesaAdicional.Text.Equals("") ? 0 : Convert.ToDouble(txtDespesaAdicional.Text);

                //if (frete > 0)
                //    total += frete;

                //txtTotalPagar.Text = total.ToString("n2");

                //txtCodigoItem.Text = string.Empty;
                //txtNomeItem.Text = string.Empty;
                //txtQtdItem.Text = string.Empty;
                //txtDesconto.Text = string.Empty;
                //txtPrecoUnitario.Text = string.Empty;
                //txtTotal.Text = string.Empty;
                //txtPrecoVenda.Text = string.Empty;
                //ddlUtilizacaoItem.SelectedValue = hfUtilizacao.Value;

                //btnInserirItem.Focus();
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAvisoItem, ref lblAvisosItem, mensagemDTO);

                btnInserirItem.Focus();
            }
        }

        protected void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalItem();
        }

        protected void txtPrecoUnitario_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalItem();
        }

        protected void txtQtdItem_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalItem();
        }

        private void CalcularTotalItem()
        {
            decimal total = 0;
            decimal qtd = Convert.ToDecimal((txtQtdItem.Text.Equals("") ? "0" : txtQtdItem.Text));
            decimal precoUnitario = Convert.ToDecimal((txtPrecoUnitario.Text.Equals("") ? "0" : txtPrecoUnitario.Text));
            decimal desconto = Convert.ToDecimal((txtDesconto.Text.Equals("") ? "0" : txtDesconto.Text));

            total = precoUnitario * qtd;

            if (total > 0)
            {
                if (desconto > 0)
                {
                    total = total - (total * (desconto / 100));
                }
            }

            txtTotal.Text = total.ToString("c").Replace("R", "").Replace("$", "").Trim();

            btnInserirItem.Focus();
        }

        protected void txtCodigoPn_TextChanged(object sender, EventArgs e)
        {
            //if(!txtCodigoPn.Text.Equals(""))
            //{
            //    ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
            //    ParceiroNegocioDTO parceiroNegocioDTO = parceiroNegocioBLL.Selecionar(txtCodigoPn.Text);

            //    if(!string.IsNullOrEmpty(parceiroNegocioDTO.CardCode))
            //    {
            //        hfListaPreco.Value = parceiroNegocioDTO.ListNum.ToString();

            //        hfParceiroNegocio.Value = parceiroNegocioDTO.CardCode;

            //        txtParceiroNegocio.Text = parceiroNegocioDTO.CardName;

            //        if (parceiroNegocioDTO.SlpCode > 0)
            //            ddlVendedor.SelectedValue = parceiroNegocioDTO.SlpCode.ToString();

            //        ddlCondicoesPagamento.SelectedValue = parceiroNegocioDTO.GroupNum.ToString();

            //        txtCnpj.Text = parceiroNegocioDTO.U_CNPJ;

            //        if (!string.IsNullOrEmpty(parceiroNegocioDTO.MainUsage))
            //        {
            //            ddlUtilizacao.SelectedValue = parceiroNegocioDTO.MainUsage;
            //            hfUtilizacao.Value = parceiroNegocioDTO.MainUsage;
            //        }
            //    }
            //}
        }

        protected void txtCnpj_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
            //    ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
            //    parceiroNegocioDTO.U_CNPJ = txtCnpj.Text;

            //    IList<ParceiroNegocioDTO> listParceiros = parceiroNegocioBLL.Buscar(parceiroNegocioDTO);

            //    if (listParceiros.Count > 0)
            //    {
            //        txtCodigoPn.Text = listParceiros[0].U_CNPJ;
            //        hfParceiroNegocio.Value = listParceiros[0].CardCode;
            //        txtParceiroNegocio.Text = listParceiros[0].CardName;

            //        if (parceiroNegocioDTO.SlpCode > 0)
            //            ddlVendedor.SelectedValue = listParceiros[0].SlpCode.ToString();

            //        ddlCondicoesPagamento.SelectedValue = listParceiros[0].GroupNum.ToString();

            //        txtCnpj.Text = listParceiros[0].U_CNPJ;

            //        if (!string.IsNullOrEmpty(listParceiros[0].MainUsage))
            //        {
            //            ddlUtilizacao.SelectedValue = listParceiros[0].MainUsage;
            //            hfUtilizacao.Value = listParceiros[0].MainUsage;
            //        }

            //        hfListaPreco.Value = listParceiros[0].ListNum.ToString();
            //    }
            //}
            //catch (Exception er)
            //{
            //    MensagemDTO mensagemDTO = new MensagemDTO();
            //    mensagemDTO.Tipo = MensagemType.Erro;
            //    mensagemDTO.Mensagem = er.Message;

            //    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            //}
        }

        protected void hfExclusao_Click(object sender, EventArgs e)
        {
            LinkButton botao = (LinkButton)sender;

            string conteudo = botao.CommandArgument;

            if (ViewState["listaItensGrid"] != null)
            {
                IList<ItemVendaDTO> lista = (IList<ItemVendaDTO>)ViewState["listaItensGrid"];
                lista.RemoveAt((Convert.ToInt32(conteudo) - 1));

                int linha = 0;

                foreach (var item in lista)
                {
                    linha += 1;

                    item.LineNum = linha;
                }

                gdvItens.DataSource = lista;
                gdvItens.DataBind();

                double total = lista.Sum(t => t.LineTotal);

                txtTotalProdutos.Text = total.ToString("n2");

                double frete = txtDespesaAdicional.Text.Equals("") ? 0 : Convert.ToDouble(txtDespesaAdicional.Text);

                if (frete > 0)
                    total += frete;

                txtTotalPagar.Text = total.ToString("n2");

                gdvItens.Focus();
            }
        }
    }
}