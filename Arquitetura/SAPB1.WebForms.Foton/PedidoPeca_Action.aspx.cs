using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.CondicaoPagamento;
using SAPB1.DTO.FormasPagamento;
using SAPB1.WebForm.App_Code;
using SAPB1.DTO.TiposEnvio;
using SAPB1.DTO.Utilizacao;
using SAPB1.DTO.Funcionario.Vendedor;
using SAPB1.DTO.Empresa.Filial;
using SAPB1.BLL.Empresa.Filial;
using SAPB1.DTO.PedidoPeca;
using SAPB1.BLL.PedidoPeca;
using SAPB1.BLL.Estado;
using SAPB1.DTO.Estado;
using SAPB1.BLL.Municipio;
using SAPB1.DTO.Municipio;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.BLL.Empregado;
using SAPB1.DTO.Empregado;
using SAPB1.DTO.Projeto;
using SAPB1.BLL.Projeto;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.BLL.Utilizacao.Cfop;
using SAPB1.DTO.Item;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.BLL.Item;
using SAPB1.BLL.ItensTabelaPreco;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.TabelaPreco;
using System.Text;
using SAPB1.DTO.Email;
using SAPB1.BLL.Email;
using SAPB1.DTO.Concessionario;
using SAPB1.BLL.Concessionario;
using SAPB1.DTO.NotaFiscal;
using SAPB1.BLL.NotaFiscal;
using SAPB1.BLL.Estoque;
using SAPB1.DTO.Estoque;
using SAPB1.BLL.Servicos;
using SAPB1.DTO.Servico;

namespace SAPB1.WebForms.Foton
{
    public partial class PedidoPeca_Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                pnlAviso.Visible = false;

                if (!IsPostBack)
                {
                    ListarModelosVeiculos();

                    ListarAnoModelo();

                    ListarEntreEixos();

                    txtPrecoUnitario.Attributes.Add("readOnly", "readOnly");
                    txtValorTotal.Attributes.Add("readOnly", "readOnly");

                    txtModeloVeiculo.Attributes.Add("readOnly", "readOnly");
                    txtAnoModelo.Attributes.Add("readOnly", "readOnly");
                    txtEntreEixos.Attributes.Add("readOnly", "readOnly");

                    hfListapreco.Value = Session["ListNum"].ToString();

                    ItensTabelaPrecoBLL itensTabelaPreco = new ItensTabelaPrecoBLL();
                    IList<ItensTabelaPrecoDTO> listItensTabela = itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(hfListapreco.Value);

                    if (listItensTabela.Count > 0)
                    {
                        StringBuilder codigos = new StringBuilder();
                        StringBuilder codigosNomes = new StringBuilder();

                        for (int i = 0; i < listItensTabela.Count; i++)
                        {
                            codigos.Append(listItensTabela[i].CodigoItem);
                            codigosNomes.Append(listItensTabela[i].CodigoItem + "," + listItensTabela[i].NomeItem);

                            if (i < (listItensTabela.Count - 1))
                            {
                                codigos.Append(",");
                                codigosNomes.Append("|");
                            }
                        }

                        hfListaCodigosItem.Value = codigos.ToString();
                        hfListaCodigoNome.Value = codigosNomes.ToString();
                    }

                    ConcessionarioBLL concessionarioBLL = new ConcessionarioBLL();
                    ConcessionarioDTO concessionarioDTO = concessionarioBLL.ObterConcessionarioPorId(Session["CardCode"].ToString());

                    txtConcessionario.Text = concessionarioDTO.CardName;
                    txtCidadeUf.Text = concessionarioDTO.City + " - " + concessionarioDTO.State;

                    hfIdConcessionario.Value = Session["CardCode"].ToString();

                    string valorQueryString = Request.QueryString["id"];

                    if (valorQueryString != null)
                    {
                        hfNumeroPedido.Value = valorQueryString;

                        CarregarDadosPedidoPeça();
                    }
                    else
                        txtDataLancamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    if (ViewState["ItensGrid"] != null)
                    {
                        gdvItens.DataSource = (IList<ItemPecaDTO>)ViewState["ItensGrid"];
                        gdvItens.DataBind();
                    }
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message + "-" + er.StackTrace;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        #region WebMetohods
        [System.Web.Services.WebMethod]
        public static object RetornarDadosItemPorId(string itemCode, string tabelaPreco)
        {
            ItemDTO itemDTO = new ItemDTO();
            itemDTO.SellItem = "Y";
            itemDTO.ItemCode = itemCode;

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

        [System.Web.Services.WebMethod]
        public static object RetornarDadosPeloChassi(string chassi)
        {
            ChassiAntigoBLL chassiAntigoBLL = new ChassiAntigoBLL();

            ChassiAntigoDTO chassiDTO = chassiAntigoBLL.ObterDadosPeloChassi(chassi);

            return chassiDTO;
        }

        #endregion

        public WsFotonRamo.OBJ17TypeOBJECTRow RetornarDocumento()
        {
            WsFotonRamo.OBJ17TypeOBJECTRow documento = new WsFotonRamo.OBJ17TypeOBJECTRow();

            if (!hfNumeroPedido.Value.Equals(""))
                documento.DocEntry = hfNumeroPedido.Value;

            documento.CardCode = hfIdConcessionario.Value;
            documento.DocDate = ((!string.IsNullOrEmpty(txtDataLancamento.Text)) ? Convert.ToDateTime(txtDataLancamento.Text).ToString("dd/MM/yyyy") : DateTime.MinValue.ToString("dd/MM/yyyy"));
            documento.DocDueDate = ((!string.IsNullOrEmpty(txtDataLancamento.Text)) ? Convert.ToDateTime(txtDataLancamento.Text).ToString("dd/MM/yyyy") : DateTime.MinValue.ToString("dd/MM/yyyy"));
            documento.BPL_IDAssignedToInvoice = "4";
            documento.Comments = "Pedido enviado pelo portal pelo concessionário " + txtConcessionario.Text;
            documento.U_UND_PARADA = (ddlTipoPedidoConcessionario.SelectedValue.Equals("1") ? "N" : "S");
            documento.Pick = "Y";
            documento.PaymentGroupCode = "31";

            if (ddlTipoPedidoConcessionario.SelectedValue.Equals("2"))
            {
                documento.U_NomeCliente = txtCliente.Text;
                documento.U_ObsAdc = txtObservacoes.Text;
                documento.U_QtdDiasParado = Convert.ToDouble((txtqtdDiasParado.Text.Equals("") ? "0" : txtqtdDiasParado.Text));
                documento.U_QtdDiasParadoSpecified = true;
                documento.U_TstRealizado = txtTestesRealizados.Text;
                documento.U_FalhasApresent = txtFalhasApresentadas.Text;
                documento.U_KmAtual = Convert.ToDouble((txtKm.Text.Equals("") ? "0" : txtKm.Text));
                documento.U_KmAtualSpecified = true;
                documento.U_Chassi = txtChassi.Text;
                documento.U_ModVei = txtModeloVeiculo.Text;
                documento.U_AnoModelo = txtAnoModelo.Text;
                documento.U_EntreEixos = txtEntreEixos.Text;
            }

            if (hfNumeroPedido.Value.Trim().Equals(""))
                documento.U_ST_CONCESS = "W";
            else
                documento.U_ST_CONCESS = hfStatusConcessionario.Value;

            return documento;
        }

        #region Email
        public string RetornarConteudoEmail(string pedido)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("<h3>Pedido de Venda " + pedido + "</h3>");
            stb.Append("<table>");
            stb.Append("<tr>");
            stb.Append("<td>Chassi: " + txtChassi.Text + "</td>");
            stb.Append("</tr>");
            stb.Append("<tr>");
            stb.Append("<td>Modelo: " + txtModeloVeiculo.Text + "</td>");
            stb.Append("</tr>");
            stb.Append("<tr>");
            stb.Append("<td>Ano: " + txtAnoModelo.Text + "</td>");
            stb.Append("</tr>");
            stb.Append("</table>");
            stb.Append("<br/>");
            stb.Append("<br/>");
            stb.Append("<br/>");

            if (ViewState["ItensGrid"] != null)
            {
                IList<ItemPecaDTO> itempedidoPeca = (IList<ItemPecaDTO>)ViewState["ItensGrid"];

                stb.Append("<table>");
                stb.Append("<tr>");
                stb.Append("<th>Código do Item</th>");
                stb.Append("<th>Nome do Item</th>");
                stb.Append("<th>Preço</th>");
                stb.Append("<th>Quantidade</th>");
                stb.Append("<th>Total</th>");
                stb.Append("</tr>");

                foreach (ItemPecaDTO item in itempedidoPeca)
                {
                    stb.Append("<tr>");
                    stb.Append("<td>" + item.ItemCode + "</td>");
                    stb.Append("<td>" + item.ItemName + "</td>");
                    stb.Append("<td>" + item.Price.ToString("c") + "</td>");
                    stb.Append("<td>" + item.Quantity + "</td>");
                    stb.Append("<td>" + item.LineTotal.ToString("c") + "</td>");
                    stb.Append("</tr>");
                }

                stb.Append("<table>");
            }

            return stb.ToString();
        }

        public void EnviarEmail(string mensagem)
        {
            EmailDTO emailDTO = new EmailDTO();
            emailDTO.Titulo = "Aviso de UP - Concessionário: " + txtConcessionario.Text;

            emailDTO.Copia = new List<string>();
            emailDTO.Copia.Add("vinicius@stch.com.br");
            emailDTO.Copia.Add("leandro.gedanken@fotonmotors.com.br");
            emailDTO.Copia.Add("wagner.galhego@fotonmotors.com.br");
            emailDTO.Copia.Add("marcio.cardoso@fotonmotors.com.br");

            emailDTO.Remetente = "naoresponda@redefotonmotors.com.br";
            emailDTO.Destinatario = new List<string>();
            emailDTO.Destinatario.Add("hony.filho@fotonmotors.com.br");
            emailDTO.Mensagem = RetornarConteudoEmail(mensagem);
            emailDTO.Smtp = "mail.redefotonmotors.com.br";
            emailDTO.Porta = 587;
            emailDTO.IsSsl = false;
            emailDTO.Usuario = "naoresponda@redefotonmotors.com.br";
            emailDTO.Senha = "FO##2016";

            //emailDTO.Mensagem = "Pedido de venda " + mensagem;

            emailDTO.IsHtml = true;

            EmailBLL emailBLL = new EmailBLL();
            emailBLL.EnviarEmail(emailDTO);
        }
        #endregion

        #region DroDowns
        protected void ddlModelosVeiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListarAnoModelo();
                ListarEntreEixos();

                btnSalvar.Focus();
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void ddlAnoModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListarEntreEixos();

                btnSalvar.Focus();
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void ddlTipoPedidoConcessionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoPedidoConcessionario.SelectedValue.Equals("1"))
            {
                pnlUnidadeParada.Visible = false;

                MostrarOcultarColunasGridItens(true);

                pnlInfoModelo.Visible = true;
            }
            else
            {
                pnlUnidadeParada.Visible = true;

                MostrarOcultarColunasGridItens(false);

                pnlInfoModelo.Visible = false;
            }
        }
        #endregion

        #region Botões
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ItensTabelaPrecoBLL itensTabelaPreco = new ItensTabelaPrecoBLL();
                IList<ItensTabelaPrecoDTO> listItensTabelaPrecoDealer = itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco("1");

                if (ddlTipoPedidoConcessionario.SelectedValue.Equals("2"))
                {
                    string errosHidden = hfErrosUnidadeParada.Value;

                    if (!errosHidden.Equals(""))
                    {
                        errosHidden = errosHidden.Replace("|", "<li>");

                        MensagemDTO mensagemDTO = new MensagemDTO();
                        mensagemDTO.Mensagem = errosHidden;
                        mensagemDTO.Tipo = MensagemType.Aviso;

                        Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                        return;
                    }

                    string retornoErros = ValidarCamposUnidadeParada();

                    if (!retornoErros.Equals(""))
                    {
                        MensagemDTO mensagemDTO = new MensagemDTO();
                        mensagemDTO.Mensagem = errosHidden;
                        mensagemDTO.Tipo = MensagemType.Aviso;

                        Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                    }

                    hfErrosUnidadeParada.Value = "";
                }

                IList<ItemPecaDTO> lista = new List<ItemPecaDTO>();
                if (ViewState["ItensGrid"] != null)
                    lista = (IList<ItemPecaDTO>)ViewState["ItensGrid"];

                if (lista.Count == 0)
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "<li>Insira no mínimo 1 item.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                WsFotonRamo.ipostep_vP0010000104in_WCSX_comsapb1ivplatformruntime_INB_WS_CALL_SYNC_XPT_INB_WS_CALL_SYNC_XPTipo_proc_Service ws = new WsFotonRamo.ipostep_vP0010000104in_WCSX_comsapb1ivplatformruntime_INB_WS_CALL_SYNC_XPT_INB_WS_CALL_SYNC_XPTipo_proc_Service();
                ws.Url = System.Configuration.ConfigurationManager.AppSettings["SAPUrl"].ToString();
                ws.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["User"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Password"].ToString());

                WsFotonRamo.OBJ17Type pv = new WsFotonRamo.OBJ17Type();
                List<WsFotonRamo.OBJ17TypeOBJECTRow> pedidoVendaDados = new List<WsFotonRamo.OBJ17TypeOBJECTRow>();
                List<WsFotonRamo.OBJ17TypeOBJECTRow1> pedidovendaItem = new List<WsFotonRamo.OBJ17TypeOBJECTRow1>();
                List<WsFotonRamo.OBJ17TypeOBJECTRow2> taxas = new List<WsFotonRamo.OBJ17TypeOBJECTRow2>();

                taxas.Add(new WsFotonRamo.OBJ17TypeOBJECTRow2()
                {
                    Incoterms = "0"
                });

                pedidoVendaDados.Add(RetornarDocumento());

                EstoqueBLL estoqueBLL = new EstoqueBLL();

                if (lista.Count > 0)
                {
                    foreach (ItemPecaDTO peca in lista)
                    {
                        //estoqueBLL.RetornarDadosEstoqueProduto(peca.ItemCode);

                        WsFotonRamo.OBJ17TypeOBJECTRow1 item = new WsFotonRamo.OBJ17TypeOBJECTRow1();
                        item.LineNum = peca.LineNum.ToString();

                        if (string.IsNullOrEmpty(peca.CodigoGenerico))
                        {
                            item.ItemCode = peca.ItemCode;

                            var precoDealer = listItensTabelaPrecoDealer.Where(p => p.CodigoItem == peca.ItemCode);

                            foreach (var preco in precoDealer)
                            {
                                item.UnitPrice = preco.Price;
                            }

                            item.UnitPriceSpecified = true;

                            item.ItemDescription = peca.ItemName;
                            item.ItemDetails = peca.ItemName;
                        }
                        else
                        {
                            item.ItemCode = peca.CodigoGenerico;
                            item.UnitPrice = peca.Price;
                            item.UnitPriceSpecified = true;

                            item.ItemDescription = peca.ItemCode + "-" + peca.ItemName.Replace("-", " ");
                            item.ItemDetails = peca.ItemCode + "-" + peca.ItemName.Replace("-", " "); ;
                        }

                        item.Quantity = peca.Quantity;
                        item.QuantitySpecified = true;

                        item.WarehouseCode = "DPR002";
                        item.Usage = "16";
                        item.CostingCode = "801002";
                        item.LineTotal = peca.LineTotal;
                        item.U_Modelo = peca.Modelo;
                        item.U_AnoModel = peca.AnoModelo;
                        item.U_EntreEix = peca.EntreEixos;

                        pedidovendaItem.Add(item);
                    }
                }

                pv.OBJECT = new WsFotonRamo.OBJ17TypeOBJECT();
                pv.OBJECT.Documents = pedidoVendaDados.ToArray();
                pv.OBJECT.Document_Lines = pedidovendaItem.ToArray();
                pv.OBJECT.TaxExtension = taxas.ToArray();

                WsFotonRamo.LOG17Type resposta = ws.RSDWSFOBJ17(pv);

                if (resposta.Result.Contains("Sucessful") || resposta.Result.Contains("success"))
                {
                    if (ddlTipoPedidoConcessionario.SelectedValue == "2")
                    {
                        if (txtNumeroPedido.Text.Trim().Equals(""))
                            EnviarEmail(resposta.Message);
                        else
                            EnviarEmail(txtNumeroPedido.Text);

                        Response.Redirect("PedidoPeca.aspx");
                    }
                    else
                        Response.Redirect("PedidoPeca.aspx");
                }
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = resposta.Message + "." + "Status:" + resposta.Status;
                    mensagemDTO.Tipo = MensagemType.Erro;

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

        protected void btnInserirItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!hfErrosItem.Value.Equals(""))
                {
                    string erros = hfErrosItem.Value;

                    erros = erros.Replace("|", "<li>");

                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = erros;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                string retornoValidacao = ValidarCamposItem();

                if (!retornoValidacao.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = retornoValidacao;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                double totalItem = Convert.ToDouble((hfTotalItem.Value.Trim().Equals("") ? "0" : hfTotalItem.Value.Trim().Replace(".", ",")));

                ItemPecaDTO itemPecaDTO = new ItemPecaDTO();

                if (hfItemId.Value.Trim().Equals(""))
                {
                    itemPecaDTO.CodigoGenerico = "001";
                    itemPecaDTO.ItemCode = txtPartNumber.Text;
                }
                else
                {
                    itemPecaDTO.ItemCode = txtPartNumber.Text;
                }

                itemPecaDTO.ItemName = txtDescricao.Text;
                itemPecaDTO.Dscription = itemPecaDTO.ItemName;
                itemPecaDTO.Quantity = Convert.ToDouble((txtQtdItem.Text.Equals("") ? "1" : txtQtdItem.Text));
                itemPecaDTO.Price = Convert.ToDouble((hfItemId.Value.Trim().Equals("") ? "0" : hfPrecoItem.Value.Replace(".", ",").Trim()));
                itemPecaDTO.LineTotal = totalItem;
                itemPecaDTO.Modelo = ddlModelosVeiculos.SelectedValue;
                itemPecaDTO.AnoModelo = ddlAnoModelo.SelectedValue;
                itemPecaDTO.EntreEixos = ddlEntreEixos.SelectedValue;

                IList<ItemPecaDTO> listaItens = new List<ItemPecaDTO>();

                if (ViewState["ItensGrid"] != null)
                {
                    listaItens = (IList<ItemPecaDTO>)ViewState["ItensGrid"];
                    itemPecaDTO.LineNum = listaItens.Count + 1;

                    listaItens.Add(itemPecaDTO);
                }
                else
                {
                    itemPecaDTO.LineNum = 1;
                    listaItens.Add(itemPecaDTO);
                }

                ViewState["ItensGrid"] = listaItens;

                gdvItens.DataSource = listaItens;
                gdvItens.DataBind();

                lblValorTotal.Text = "Valor Total: " + listaItens.Sum(t => t.LineTotal).ToString("c");

                txtPrecoUnitario.Text = hfPrecoItem.Value;
                txtValorTotal.Text = hfTotalItem.Value;

                hfItemId.Value = "";
                hfPrecoItem.Value = "";
                hfTotalItem.Value = "";

                txtPrecoUnitario.Text = "0";
                txtValorTotal.Text = "0";
                txtPartNumber.Text = string.Empty;
                txtDescricao.Text = string.Empty;

                hfErrosItem.Value = "";

                btnInserirItem.Focus();
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }
        #endregion

        #region Rotinas Diversas
        private void ListarModelosVeiculos()
        {
            ModeloVeiculoBLL modeloVeiculo = new ModeloVeiculoBLL();
            IList<ModeloVeiculoDTO> listModelos = modeloVeiculo.ListarTodosModelos();

            ddlModelosVeiculos.Items.Clear();
            ddlModelosVeiculos.Items.Add(new ListItem { Value = "0", Text = "Selecione" });
            ddlModelosVeiculos.DataSource = listModelos;
            ddlModelosVeiculos.DataValueField = "Modelo";
            ddlModelosVeiculos.DataTextField = "Modelo";
            ddlModelosVeiculos.DataBind();
        }

        private void ListarAnoModelo()
        {
            ModeloVeiculoBLL modeloVeiculo = new ModeloVeiculoBLL();
            IList<ModeloVeiculoDTO> listModelos = modeloVeiculo.ListarANoModeloPorModelo(ddlModelosVeiculos.SelectedValue);

            ddlAnoModelo.Items.Clear();
            ddlAnoModelo.Items.Add(new ListItem { Value = "0", Text = "Selecione" });
            ddlAnoModelo.DataSource = listModelos;
            ddlAnoModelo.DataValueField = "AnoModelo";
            ddlAnoModelo.DataTextField = "AnoModelo";
            ddlAnoModelo.DataBind();
        }

        private void ListarEntreEixos()
        {
            ModeloVeiculoBLL modeloVeiculo = new ModeloVeiculoBLL();
            IList<ModeloVeiculoDTO> listModelos = modeloVeiculo.ListarEntreEixosPorAnoModelo(ddlModelosVeiculos.SelectedValue, ddlAnoModelo.SelectedValue);

            ddlEntreEixos.Items.Clear();
            ddlEntreEixos.Items.Add(new ListItem { Value = "0", Text = "Selecione" });
            ddlEntreEixos.DataSource = listModelos;
            ddlEntreEixos.DataValueField = "EntreEixos";
            ddlEntreEixos.DataTextField = "EntreEixos";
            ddlEntreEixos.DataBind();
        }

        private void MostrarOcultarColunasGridItens(bool visivel)
        {
            gdvItens.Columns[0].Visible = visivel;
            gdvItens.Columns[1].Visible = visivel;
            gdvItens.Columns[2].Visible = visivel;
        }

        private string ValidarCamposItem()
        {
            string errosItem = string.Empty;

            if (txtPartNumber.Text.Trim().Equals(""))
                errosItem += "<li>Código do Item é um campo obrigatório.";

            if (txtDescricao.Text.Equals(""))
                errosItem += "<li>Descrição do Item é um campo obrigatório.";

            if (txtQtdItem.Text.Trim().Equals(""))
                errosItem += "<li>Quantidade do Item é um campo obrigatório.";

            return errosItem;
        }

        private void CarregarDadosPedidoPeça()
        {
            PedidoPecaDTO pedidoPecaDTO = new PedidoPecaDTO();
            pedidoPecaDTO.DocNum = Convert.ToInt32(hfNumeroPedido.Value);

            PedidoPecaBLL pedidoPecaBLL = new PedidoPecaBLL();
            pedidoPecaDTO = pedidoPecaBLL.Listar(pedidoPecaDTO)[0];

            txtNumeroPedido.Text = pedidoPecaDTO.DocNum.ToString();
            txtDataLancamento.Text = pedidoPecaDTO.DocDate.ToString("dd/MM/yyyy");

            ddlTipoPedidoConcessionario.SelectedValue = (pedidoPecaDTO.U_UND_PARADA.Equals("2") || pedidoPecaDTO.U_UND_PARADA.Equals("S") ? "2" : "1");

            NotaFiscalItemBLL notafiscalItem = new NotaFiscalItemBLL();
            IList<NotaFiscalItemDTO> listNotas = notafiscalItem.ObterNotasFiscaisPorPedidoVenda(pedidoPecaDTO.DocNum.ToString());

            if (listNotas.Count > 0)
            {
                string notasFiscaisEmitidas = string.Empty;

                for (int i = 0; i < listNotas.Count; i++)
                {
                    notasFiscaisEmitidas += listNotas[i].Serial;

                    if (i < (listNotas.Count - 1))
                        notasFiscaisEmitidas += ", ";
                }

                txtNotasFiscalEmitidas.Text = notasFiscaisEmitidas;
            }

            if (pedidoPecaDTO.U_UND_PARADA.Equals("S"))
            {
                MostrarOcultarColunasGridItens(false);

                pnlUnidadeParada.Visible = true;

                txtCliente.Text = pedidoPecaDTO.U_NomeCliente;
                txtFalhasApresentadas.Text = pedidoPecaDTO.U_FalhasApresent;
                txtKm.Text = pedidoPecaDTO.U_KmAtual.ToString("n2");
                txtObservacoes.Text = pedidoPecaDTO.U_ObsAdc;
                txtTestesRealizados.Text = pedidoPecaDTO.U_TstRealizado;
                txtqtdDiasParado.Text = pedidoPecaDTO.U_QtdDiasParado.ToString("n2");
                txtEntreEixos.Text = pedidoPecaDTO.U_EntreEixos;
                txtChassi.Text = pedidoPecaDTO.U_Chassi;
                txtModeloVeiculo.Text = pedidoPecaDTO.U_ModVei;
                txtAnoModelo.Text = pedidoPecaDTO.U_AnoModelo;
            }

            if (!string.IsNullOrEmpty(pedidoPecaDTO.U_ST_CONCESS))
            {
                hfStatusConcessionario.Value = pedidoPecaDTO.U_ST_CONCESS;

                switch (pedidoPecaDTO.U_ST_CONCESS)
                {
                    case "W":
                        txtStatus.Text = "EM ANÁLISE";
                        break;
                    case "A":
                        txtStatus.Text = "EM PROCESSAMENTO";
                        break;
                    case "P":
                        txtStatus.Text = "ATENDIDO PARCIAL";
                        break;
                    case "F":
                        txtStatus.Text = "ATENDIDO TOTAL";
                        break;
                    case "C":
                        txtStatus.Text = "CANCELADO";
                        break;
                    case "B":
                        txtStatus.Text = "FINANCEIRO";
                        break;
                }
            }
            else
            {
                switch (pedidoPecaDTO.DocStatus)
                {
                    case "O":
                        txtStatus.Text = "ABERTO";
                        break;
                    case "C":
                        txtStatus.Text = "FECHADO";
                        break;
                }
            }

            //lblValorTotal.Text = "Valor Total: " + pedidoPecaDTO.DocTotalSy.ToString("c");

            if (pedidoPecaDTO.DocStatus.Equals("C") || !pedidoPecaDTO.U_ST_CONCESS.Equals("W"))
            {
                ddlTipoPedidoConcessionario.Enabled = false;

                txtPartNumber.ReadOnly = true;
                txtDescricao.ReadOnly = true;
                txtQtdItem.ReadOnly = true;
                txtValorTotal.ReadOnly = true;
                txtPrecoUnitario.ReadOnly = true;

                txtCliente.ReadOnly = true;
                txtChassi.ReadOnly = true;
                txtKm.ReadOnly = true;
                txtqtdDiasParado.ReadOnly = true;
                txtModeloVeiculo.ReadOnly = true;
                txtAnoModelo.ReadOnly = true;
                txtEntreEixos.ReadOnly = true;
                txtFalhasApresentadas.ReadOnly = true;
                txtTestesRealizados.ReadOnly = true;
                txtObservacoes.ReadOnly = true;

                ddlAnoModelo.Enabled = false;
                ddlEntreEixos.Enabled = false;
                ddlModelosVeiculos.Enabled = false;

                btnInserirItem.Enabled = false;
                btnInserirItem.Visible = false;

                btnSalvar.Enabled = false;
                btnSalvar.Visible = false;

                btnCancelar.Enabled = false;
                btnCancelar.Visible = false;

                gdvItens.Columns[8].Visible = false;
            }

            ItemPecaDTO itemPecaDTO = new ItemPecaDTO();
            itemPecaDTO.DocEntry = pedidoPecaDTO.DocNum;

            ItemPecaBLL itemPecaBLL = new ItemPecaBLL();
            IList<ItemPecaDTO> listaPecas = itemPecaBLL.Listar(itemPecaDTO);

            ItensTabelaPrecoBLL itensTabelaPreco = new ItensTabelaPrecoBLL();
            IList<ItensTabelaPrecoDTO> listItensTabelaPrecoComImposto = itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(hfListapreco.Value);

            double qtd = 0;
            double preco = 0;

            if (listaPecas.Count > 0)
            {
                foreach (ItemPecaDTO peca in listaPecas)
                {
                    if (peca.ItemCode.Equals("001"))
                    {
                        peca.CodigoGenerico = peca.ItemCode;

                        string[] matriz = peca.Dscription.Split('-');

                        if (matriz.Length >= 2)
                        {
                            peca.ItemCode = matriz[0];
                            peca.ItemName = matriz[1];
                        }
                    }
                    else
                    {
                        var precoComImposto = listItensTabelaPrecoComImposto.Where(p => p.CodigoItem == peca.ItemCode);

                        foreach (var pecaImposto in precoComImposto)
                        {
                            preco = pecaImposto.Price;
                        }

                        peca.Price = preco;

                        qtd = peca.Quantity;
                        peca.LineTotal = preco * qtd;

                        preco = 0;
                    }
                }
            }

            lblValorTotal.Text = "Valor Total: " + listaPecas.Sum(t => t.LineTotal).ToString("c");

            gdvItens.DataSource = listaPecas;
            gdvItens.DataBind();

            ViewState["ItensGrid"] = listaPecas;
        }

        private string ValidarCamposUnidadeParada()
        {
            string erros = string.Empty;

            if (txtCliente.Text.Trim().Equals(""))
                erros += "<li>Nome do Cliente é um campo obrigatório.";

            if (txtChassi.Text.Trim().Equals(""))
                erros += "<li>Chassi é um campo obrigatório.";

            if (txtKm.Text.Trim().Equals(""))
                erros += "<li>Km Atual é um campo obrigatório.";

            if (txtqtdDiasParado.Text.Trim().Equals(""))
                erros += "<li>Quantidade Dias Parado é um campo obrigatório.";

            if (txtFalhasApresentadas.Text.Trim().Equals(""))
                erros += "<li>Falhas Apresentadas é um campo obrigatório.";

            if (txtTestesRealizados.Text.Trim().Equals(""))
                erros += "<li>Testes Realizados é um campo obrigatório.";

            return erros;
        }
        #endregion

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PedidoPeca_Action.aspx");
        }

        protected void lkbDetalhesPedidoGrid_Click(object sender, EventArgs e)
        {
            LinkButton lknButtonGrid = (LinkButton)sender;

            int linha = Convert.ToInt32(lknButtonGrid.CommandArgument);

            IList<ItemPecaDTO> listPecas = new List<ItemPecaDTO>();

            if (ViewState["ItensGrid"] != null)
                listPecas = (IList<ItemPecaDTO>)ViewState["ItensGrid"];

            if (listPecas.Count > 0)
            {
                listPecas = listPecas.Where(l => l.LineNum != linha).ToList();
                gdvItens.DataSource = listPecas;
                gdvItens.DataBind();

                ViewState["ItensGrid"] = listPecas;
            }

            if (listPecas != null)
                lblValorTotal.Text = "Valor Total: " + listPecas.Sum(t => t.LineTotal).ToString("c");

            lblValorTotal.Focus();
        }
    }
}