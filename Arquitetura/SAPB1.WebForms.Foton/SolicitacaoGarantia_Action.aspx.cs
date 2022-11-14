using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.WebForm.App_Code;
using SAPB1.DTO.Item;
using SAPB1.DTO.Servico;
using SAPB1.BLL.Servicos;
using SAPB1.DTO.Estado;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.DTO.Municipio;
using SAPB1.DTO.Empregado;
using SAPB1.DTO.Concessionario;
using SAPB1.BLL.Concessionario;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.BLL.Item;
using SAPB1.BLL.ItensTabelaPreco;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.TabelaPreco;
using System.Text;
using SAPB1.DTO.Mensagens;
using System.Web.Services;
using SAPB1.DTO.Anexo;
using SAPB1.BLL.Anexo;

namespace SAPB1.WebForms.Foton
{
    public partial class SolicitacaoGarantia_Action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = 0;

            if (!IsPostBack)
            {
                if (ViewState["ItensGrid"] != null)
                    ViewState["ItensGrid"] = null;

                CarregarTipoGarantia("");

                if (Session["CardCode"] != null)
                    hfIdConcessionario.Value = Session["CardCode"].ToString();
                else
                    hfIdConcessionario.Value = string.Empty;

                txtDataVenda.Attributes.Add("readOnly", "readOnly");
                txtModeloChassi.Attributes.Add("readOnly", "readOnly");
                txtModeloMotor.Attributes.Add("readOnly", "readOnly");
                txtNomeCliente.Attributes.Add("readOnly", "readOnly");
                txtNumeroMotor.Attributes.Add("readOnly", "readOnly");

                txtItem.Attributes.Add("readOnly", "readOnly");
                txtValorUnitario.Attributes.Add("readOnly", "readOnly");
                txtTotal.Attributes.Add("readOnly", "readOnly");

                txtDescricaoTpr.Attributes.Add("readOnly", "readOnly");
                txtQtdTpr.Attributes.Add("readOnly", "readOnly");
                txtValorTpr.Attributes.Add("readOnly", "readOnly");

                txtPlaca.Attributes.Add("onkeypress", "ConsistePlaca(event.keyCode);");

                txtDataAbeturaFalha.Attributes.Add("onkeypress", "return isNumberKey(event)");
                txtDataAbeturaFalha.Attributes.Add("onkeyup", "formataData(this, retornaKeyCode(event));");

                ConcessionarioBLL concessionarioBLL = new ConcessionarioBLL();
                ConcessionarioDTO concessionarioDTO = concessionarioBLL.ObterConcessionarioPorId(hfIdConcessionario.Value);

                if (!string.IsNullOrEmpty(concessionarioDTO.U_TabGarant))
                    hfListaPrecoGarantia.Value = Session["ListNum"].ToString();
                else
                    hfListaPrecoGarantia.Value = concessionarioDTO.U_TabGarant;

                txtConcessinario.Text = concessionarioDTO.CardName;
                txtCnpj.Text = concessionarioDTO.U_Tsystem;

                hfListaPrecoGarantia.Value = concessionarioDTO.U_TabGarant;

                ItensTabelaPrecoBLL itensTabelaPreco = new ItensTabelaPrecoBLL();
                IList<ItensTabelaPrecoDTO> listItensTabela = itensTabelaPreco.ListarItensComPrecoMaiorQueZeroPorIdTabelaPreco(hfListaPrecoGarantia.Value);

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

                TprBLL tprBLL = new TprBLL();
                List<TprDTO> listTpr = tprBLL.ObterTodos();

                if (listTpr.Count > 0)
                {
                    string codigosTpr = string.Empty;

                    for (int i = 0; i < listTpr.Count; i++)
                    {
                        codigosTpr += listTpr[i].U_Codigo;

                        if (i < (listTpr.Count - 1))
                            codigosTpr += ",";
                    }

                    hfCodTpr.Value = codigosTpr;
                }

                if (Request.QueryString["id"] != null)
                    CarregarDados();
                else
                {
                    txtDataEnvio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    CarregarOpcoesTipoGarantia("");
                }
            }
            else
            {
                CarregarItensChamadoGrid();
            }
        }

        private void CarregarDados()
        {
            int callId = Convert.ToInt32(Request.QueryString["id"]);
            string customer = hfIdConcessionario.Value;

            ChamadoServicoBLL chamadoServicoBLL = new ChamadoServicoBLL();
            ChamadoServicoDTO chamadoServicoDTO = chamadoServicoBLL.ListarChamadoPorIdPorCustomer(callId, customer);

            if (chamadoServicoDTO.callID > 0)
            {
                hfCodAnexo.Value = chamadoServicoDTO.AtcEntry;

                txtDataEnvio.Text = chamadoServicoDTO.createDate.ToString("dd/MM/yyyy");
                txtChassis.Text = chamadoServicoDTO.U_Chassi;
                txtDataVenda.Text = (chamadoServicoDTO.U_DtVenda == DateTime.MinValue ? string.Empty : chamadoServicoDTO.U_DtVenda.ToString("dd/MM/yyyy"));
                txtModeloChassi.Text = chamadoServicoDTO.U_Modelo;
                txtKmAtual.Text = chamadoServicoDTO.U_KmAt.ToString("n");
                txtNomeCliente.Text = chamadoServicoDTO.U_NomCli;
                txtDataAbeturaFalha.Text = (chamadoServicoDTO.U_DtAbertFal == DateTime.MinValue ? string.Empty : chamadoServicoDTO.U_DtAbertFal.ToString("dd/MM/yyyy"));
                txtPlaca.Text = chamadoServicoDTO.U_Placa;
                txtKmFalha.Text = chamadoServicoDTO.U_KmFal.ToString("n");
                txtNumeroMotor.Text = chamadoServicoDTO.U_NumMoto;
                txtModeloMotor.Text = chamadoServicoDTO.U_ModelMoto;
                txtOrdemServico.Text = chamadoServicoDTO.U_OrdemServ;
                txtNomeResponsavel.Text = chamadoServicoDTO.U_NomResp;
                txtFuncao.Text = chamadoServicoDTO.U_Funcao;
                txtDescricaoFalha.Text = chamadoServicoDTO.U_DescFal;
                txtCausaFalha.Text = chamadoServicoDTO.U_CausaFal;
                txtCorrecaoFalha.Text = chamadoServicoDTO.U_CorrecaoFal;
                txtObservacoesGerais.Text = chamadoServicoDTO.U_ObsGerais;

                string[] dadosTipoGarantia = chamadoServicoDTO.U_TpGarant.Split('-');
                string[] dadosOpcaoGarantia = chamadoServicoDTO.U_SubTipoGarant.Split('-');

                ddlTipoGarantia.SelectedValue = dadosTipoGarantia[0];
                CarregarOpcoesTipoGarantia(dadosOpcaoGarantia[0]);

                hfCodChamado.Value = chamadoServicoDTO.callID.ToString();

                CarregarItensChamadoGrid();

                CarregarItensChamadoTpr();

                CarregarAnexos();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtDataVenda.Text.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "Chassi inválido. Selecione um chassi válido.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                if (txtDataAbeturaFalha.Text.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "Data da falha é um campo obrigatório.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                if(txtKmAtual.Text.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "KM atual é um campo obrigatório.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                if (txtKmFalha.Text.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "KM da falha é um campo obrigatório.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                decimal kmAtual = (txtKmAtual.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtKmAtual.Text));
                decimal kmFalha = (txtKmFalha.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtKmFalha.Text));

                if (kmFalha > kmAtual)
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "Km da falha não pode ser maior que o Km atual.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                DateTime dataFalha = Convert.ToDateTime(txtDataAbeturaFalha.Text);
                DateTime dataAtual = (Request.QueryString["id"] == null ? DateTime.Now : Convert.ToDateTime(txtDataEnvio.Text));

                if (dataFalha > dataAtual)
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "Data da falha não pode ser maior do data de envio";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                WsFotonRamo.ipostep_vP0010000104in_WCSX_comsapb1ivplatformruntime_INB_WS_CALL_SYNC_XPT_INB_WS_CALL_SYNC_XPTipo_proc_Service ws = new WsFotonRamo.ipostep_vP0010000104in_WCSX_comsapb1ivplatformruntime_INB_WS_CALL_SYNC_XPT_INB_WS_CALL_SYNC_XPTipo_proc_Service();
                ws.Url = System.Configuration.ConfigurationManager.AppSettings["SAPUrl"].ToString();
                ws.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["User"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Password"].ToString());

                WsFotonRamo.OBJ191TypeOBJECTRow ServiceCalls = new WsFotonRamo.OBJ191TypeOBJECTRow();

                if (hfCodAnexo.Value.Equals(""))
                {
                    if (ViewState["ListaAnexos"] != null)
                    {
                        IList<AnexoDTO> listAnexos = (IList<AnexoDTO>)ViewState["ListaAnexos"];

                        if (listAnexos.Count > 0)
                        {
                            WsFotonRamo.OBJ221Type Anexo = new WsFotonRamo.OBJ221Type();

                            List<WsFotonRamo.OBJ221TypeOBJECTRow> Anexos_rows = new List<WsFotonRamo.OBJ221TypeOBJECTRow>();

                            foreach (AnexoDTO anexoDoc in listAnexos)
                            {
                                WsFotonRamo.OBJ221TypeOBJECTRow Anexos = new WsFotonRamo.OBJ221TypeOBJECTRow();
                                Anexos.SourcePath = anexoDoc.Caminho;
                                Anexos.FileName = anexoDoc.NomeArquivo;
                                Anexos.FileExtension = anexoDoc.Extensao;

                                Anexos_rows.Add(Anexos);
                            }

                            Anexo.OBJECT = new WsFotonRamo.OBJ221TypeOBJECT();

                            Anexo.OBJECT.Attachments2_Lines = Anexos_rows.ToArray();

                            WsFotonRamo.LOG221Type RetornoAnexo = ws.RSDWSFOBJ221(Anexo);

                            if (RetornoAnexo.Status.Equals("S"))
                            {
                                ServiceCalls.AttachmentEntry = Convert.ToInt64(RetornoAnexo.Message);
                                ServiceCalls.AttachmentEntrySpecified = true;
                            }
                        }
                    }
                }

                WsFotonRamo.OBJ191Type service = new WsFotonRamo.OBJ191Type();
                List<WsFotonRamo.OBJ191TypeOBJECTRow> chamadoServico = new List<WsFotonRamo.OBJ191TypeOBJECTRow>();

                ServiceCalls.CustomerCode = hfIdConcessionario.Value;

                ServiceCalls.U_NomeSolic = txtNomeResponsavel.Text;
                ServiceCalls.U_DataFalha = Convert.ToDateTime(txtDataAbeturaFalha.Text).ToString("yyyyMMdd");

                ConcessionarioBLL concessionarioBLL = new ConcessionarioBLL();
                ConcessionarioDTO concessionarioDTO = concessionarioBLL.ObterConcessionarioPorId(hfIdConcessionario.Value);

                ServiceCalls.U_NomeConc = concessionarioDTO.CardName;
                ServiceCalls.U_CodPNConc = hfIdConcessionario.Value;
                ServiceCalls.U_NomeCliF = txtNomeCliente.Text;

                ServiceCalls.U_KM = Convert.ToInt64(txtKmAtual.Text);
                ServiceCalls.U_KMSpecified = true;

                ServiceCalls.Subject = "Chamado feito pelo Portal referente ao concessionário " + txtConcessinario.Text;
                ServiceCalls.CreationDate = ((!string.IsNullOrEmpty(txtDataEnvio.Text)) ? Convert.ToDateTime(txtDataEnvio.Text).ToString("yyyyMMdd") : DateTime.MinValue.ToString("yyyyMMdd"));
                ServiceCalls.U_CausaFal = txtCausaFalha.Text;
                ServiceCalls.U_Chassi = txtChassis.Text;
                ServiceCalls.U_CorrecaoFal = txtCorrecaoFalha.Text;
                ServiceCalls.U_DescFal = txtDescricaoFalha.Text;
                ServiceCalls.U_DtAbertFal = ((!string.IsNullOrEmpty(txtDataAbeturaFalha.Text)) ? Convert.ToDateTime(txtDataAbeturaFalha.Text).ToString("yyyyMMdd") : DateTime.MinValue.ToString("yyyyMMdd"));
                ServiceCalls.U_DtVenda = ((!string.IsNullOrEmpty(txtDataVenda.Text)) ? Convert.ToDateTime(txtDataVenda.Text).ToString("yyyyMMdd") : DateTime.MinValue.ToString("yyyyMMdd"));
                ServiceCalls.U_Funcao = txtFuncao.Text;
                ServiceCalls.U_KmAt = ((!string.IsNullOrEmpty(txtKmAtual.Text)) ? Convert.ToDouble(txtKmAtual.Text) : 0d);
                ServiceCalls.U_KmAtSpecified = true;
                ServiceCalls.U_KmFal = ((!string.IsNullOrEmpty(txtKmFalha.Text)) ? Convert.ToDouble(txtKmFalha.Text) : 0d);
                ServiceCalls.U_KmFalSpecified = true;
                ServiceCalls.U_ModelMoto = txtModeloMotor.Text;
                ServiceCalls.U_Modelo = txtModeloChassi.Text;
                ServiceCalls.U_NomCli = txtNomeCliente.Text;
                ServiceCalls.U_NomResp = txtNomeResponsavel.Text;
                ServiceCalls.U_NumMoto = txtNumeroMotor.Text;
                ServiceCalls.U_ObsGerais = txtObservacoesGerais.Text;
                ServiceCalls.U_OrdemServ = txtOrdemServico.Text;
                ServiceCalls.U_Placa = txtPlaca.Text;
                ServiceCalls.U_TpGarant = ddlTipoGarantia.SelectedValue + "-" + ddlTipoGarantia.SelectedItem.Text;
                ServiceCalls.U_SubTipoGarant = ddlOpcaoTipoGarantia.SelectedValue + "-" + ddlOpcaoTipoGarantia.SelectedItem.Text;

                chamadoServico.Add(ServiceCalls);

                service.OBJECT = new WsFotonRamo.OBJ191TypeOBJECT();
                service.OBJECT.ServiceCalls = chamadoServico.ToArray();

                if (!hfCodChamado.Value.Equals(""))
                    service.OBJECT.ServiceCallID = Convert.ToInt64(hfCodChamado.Value);

                service.OBJECT.ServiceCallIDSpecified = true;

                int codigoChamado = 0;

                WsFotonRamo.LOG191Type Retorno = ws.RSDWSFOBJ191(service);

                if (Retorno.Status.Equals("S"))
                {
                    IList<ItemChamadoServicoDTO> listItensChamado = new List<ItemChamadoServicoDTO>();

                    if (ViewState["ItensGrid"] != null)
                        listItensChamado = (IList<ItemChamadoServicoDTO>)ViewState["ItensGrid"];

                    if (listItensChamado.Count > 0)
                    {
                        int linha = 0;

                        if (!hfCodChamado.Value.Equals(""))
                            codigoChamado = Convert.ToInt32(hfCodChamado.Value);
                        else
                        {
                            codigoChamado = Convert.ToInt32(Retorno.Message);
                        }

                        List<WsFotonRamo.ITMCALLTypeRow> ItensChamado_rows = new List<WsFotonRamo.ITMCALLTypeRow>();

                        foreach (ItemChamadoServicoDTO item in listItensChamado)
                        {
                            WsFotonRamo.ITMCALLTypeRow ItensChamado = new WsFotonRamo.ITMCALLTypeRow();

                            ItensChamado.U_CallID = codigoChamado;
                            ItensChamado.U_LineNum = linha;
                            ItensChamado.U_ItemAlt = item.U_ItemAlt;
                            ItensChamado.U_dscription = item.U_dscription;
                            ItensChamado.U_Quantity = Convert.ToDouble(item.U_Quantity);
                            ItensChamado.U_QuantitySpecified = true;
                            ItensChamado.U_Price = Convert.ToDouble(item.U_Price);
                            ItensChamado.U_PriceSpecified = true;

                            ItensChamado_rows.Add(ItensChamado);

                            linha += 1;
                        }

                        WsFotonRamo.LOGITMCALLType RetornoItens = ws.RSDWSFITMCALL(ItensChamado_rows.ToArray());

                    }

                    InserirAtualizarTpr(codigoChamado);

                    Response.Redirect("SolicitacaoGarantia.aspx");
                }
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "Status: " + Retorno.Status + "-Message:" + Retorno.Message + "Result: " + Retorno.Result;
                    mensagemDTO.Tipo = MensagemType.Erro;

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

        private string InserirAtualizarTpr(int codChamado)
        {
            if (hfCodChamado.Value.Equals(""))
            {
                if (ViewState["ItensChamadoTpr"] != null)
                {
                    IList<ChamadoTprDTO> listTpr = (IList<ChamadoTprDTO>)ViewState["ItensChamadoTpr"];

                    ChamadoTprBLL chamadoTprBLL = new ChamadoTprBLL();

                    foreach (ChamadoTprDTO tpr in listTpr)
                    {
                        tpr.U_CallId = codChamado;
                        chamadoTprBLL.InserirChamadoTpr(tpr);
                    }

                    return "";
                }
            }

            return "";
        }

        private void CarregarItensChamadoGrid()
        {
            if (ViewState["ItensGrid"] != null)
            {
                grdItens.DataSource = (IList<ItemChamadoServicoDTO>)ViewState["ItensGrid"];
                grdItens.DataBind();
            }
            else
            {
                if (!hfCodChamado.Value.Equals(""))
                {
                    ItemChamadoServicoBLL itemChamadoBLL = new ItemChamadoServicoBLL();

                    IList<ItemChamadoServicoDTO> list = itemChamadoBLL.ListarPorIdChamado(Convert.ToInt32((hfCodChamado.Value.Equals("") ? "0" : hfCodChamado.Value)));
                    grdItens.DataSource = list;
                    grdItens.DataBind();

                    ViewState["ItensGrid"] = list;
                }
            }
        }

        private void CarregarItensChamadoTpr()
        {
            if (!hfCodChamado.Value.Equals(""))
            {
                ChamadoTprBLL chamadoServicoBLL = new ChamadoTprBLL();
                IList<ChamadoTprDTO> list = chamadoServicoBLL.ObterTprPorChamado((Convert.ToInt32(hfCodChamado.Value.Equals("") ? 0 : Convert.ToInt32(hfCodChamado.Value))));

                if (list.Count > 0)
                {
                    gdvTpr.DataSource = list;
                    gdvTpr.DataBind();

                    ViewState["ItensChamadoTpr"] = list;
                }
            }
            else
            {
                if (ViewState["ItensChamadoTpr"] != null)
                {
                    gdvTpr.DataSource = (IList<ChamadoTprDTO>)ViewState["ItensChamadoTpr"];
                    gdvTpr.DataBind();
                }
            }
        }

        private void CarregarAnexos()
        {
            if (!hfCodAnexo.Value.Equals(""))
            {
                AnexoBLL anexoBLL = new AnexoBLL();
                gdvChamadoAnexo.DataSource = anexoBLL.ListarTodosAnexosPorAbsEntry(hfCodAnexo.Value);
                gdvChamadoAnexo.DataBind();
            }
            else
            {
                IList<AnexoDTO> lista = new List<AnexoDTO>();

                if (ViewState["ListaAnexos"] != null)
                {
                    gdvChamadoAnexo.DataSource = (IList<AnexoDTO>)ViewState["ListaAnexos"];
                    gdvChamadoAnexo.DataBind();
                }
            }
        }

        protected void btnIncluiritem_Click(object sender, EventArgs e)
        {
            IList<ItemChamadoServicoDTO> listItens = new List<ItemChamadoServicoDTO>();

            if (ViewState["ItensGrid"] != null)
                listItens = (IList<ItemChamadoServicoDTO>)ViewState["ItensGrid"];

            ItemChamadoServicoDTO itemChamado = new ItemChamadoServicoDTO();

            if (listItens.Count > 0)
                itemChamado.U_LineNum = listItens.Count + 1;
            else
                itemChamado.U_LineNum = 0;

            itemChamado.U_ItemAlt = txtCodigoPeca.Text;
            itemChamado.U_dscription = txtItem.Text;
            itemChamado.U_Price = (txtValorUnitario.Text.Equals("") ? 0 : Convert.ToDecimal(txtValorUnitario.Text));
            itemChamado.U_Quantity = (txtQtd.Text.Equals("") ? 0 : Convert.ToDecimal(txtQtd.Text));
            itemChamado.Total = (txtTotal.Text.Equals("") ? 0 : Convert.ToDecimal(txtTotal.Text.Replace(".", ",")));

            listItens.Add(itemChamado);

            ViewState["ItensGrid"] = listItens;

            CarregarItensChamadoGrid();
        }

        //BOTÃO PARA INSERIR O TPR
        protected void btnInserirTpr_Click(object sender, EventArgs e)
        {
            IList<ChamadoTprDTO> listChamadoTpr = new List<ChamadoTprDTO>();
            if (ViewState["ItensChamadoTpr"] != null)
                listChamadoTpr = (IList<ChamadoTprDTO>)ViewState["ItensChamadoTpr"];

            ChamadoTprDTO chamadoTpr = new ChamadoTprDTO();
            chamadoTpr.U_CallId = (hfCodChamado.Value.Equals("") ? 0 : Convert.ToInt32(hfCodChamado.Value));
            chamadoTpr.U_CodTpr = txtCodigoTpr.Text;
            chamadoTpr.U_ItmMan = txtDescricaoTpr.Text;
            chamadoTpr.U_Qtd = (txtQtdTpr.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtQtdTpr.Text.Replace(".", ",")));
            chamadoTpr.U_Total = (txtValorTpr.Text.Trim().Equals("") ? 0 : Convert.ToDecimal(txtValorTpr.Text.Replace(".", ",")));

            if (!hfCodChamado.Value.Equals(""))
            {
                chamadoTpr.U_CallId = Convert.ToInt32(hfCodChamado.Value);

                ChamadoTprBLL chamadoTprBLL = new ChamadoTprBLL();

                if (!chamadoTprBLL.InserirChamadoTpr(chamadoTpr))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = "Não foi possível inserir o TPR.";
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                }
                else
                {
                    txtDescricaoTpr.Text = "";
                    txtCodigoTpr.Text = "";
                    txtQtdTpr.Text = "";

                    if (!ddlTipoGarantia.SelectedValue.Equals("4") && !ddlOpcaoTipoGarantia.SelectedValue.Equals("14") && !ddlOpcaoTipoGarantia.SelectedValue.Equals("15") && !ddlOpcaoTipoGarantia.SelectedValue.Equals("16"))
                        txtValorTpr.Text = "";

                    btnInserirTpr.Focus();
                }
            }
            else
            {
                listChamadoTpr.Add(chamadoTpr);
                ViewState["ItensChamadoTpr"] = listChamadoTpr;

                txtDescricaoTpr.Text = "";
                txtCodigoTpr.Text = "";
                txtQtdTpr.Text = "";
                txtValorTpr.Text = "";

                btnInserirTpr.Focus();
            }

            CarregarItensChamadoTpr();
        }

        [WebMethod]
        public static object RetornarDadosPeloChassi(string chassi)
        {
            ChassiAntigoBLL chassiAntigoBLL = new ChassiAntigoBLL();

            ChassiAntigoDTO chassiDTO = chassiAntigoBLL.ObterDadosPeloChassi(chassi);

            return chassiDTO;
        }

        [WebMethod]
        public static object RetornarDadosTrpPorCodigo(string codigo)
        {
            TprBLL tprBLL = new TprBLL();

            TprDTO tprDTO = tprBLL.ObterDadosPorCodigo(codigo);

            return tprDTO;
        }

        [WebMethod]
        public static object RetornarDadosItemPorCodigo(string codigo, string tabelaPreco)
        {
            ItemDTO itemDTO = new ItemDTO();
            itemDTO.SellItem = "Y";
            itemDTO.ItemCode = codigo;

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

        private void CarregarTipoGarantia(string valor)
        {
            TipoGarantiaBLL tipoGarantiaBLL = new TipoGarantiaBLL();

            ddlTipoGarantia.Items.Clear();
            ddlTipoGarantia.AppendDataBoundItems = true;
            ddlTipoGarantia.DataSource = tipoGarantiaBLL.ObterTipoGarantiaAtivas();
            ddlTipoGarantia.DataValueField = "Code";
            ddlTipoGarantia.DataTextField = "U_NomeTpGarant";
            ddlTipoGarantia.DataBind();

            if (!string.IsNullOrEmpty(valor))
                ddlTipoGarantia.SelectedValue = valor;
        }

        private void CarregarOpcoesTipoGarantia(string valor)
        {
            OpcaoTipoGarantiaBLL opcaoTipoGarantiaBLL = new OpcaoTipoGarantiaBLL();

            ddlOpcaoTipoGarantia.Items.Clear();
            ddlOpcaoTipoGarantia.DataSource = opcaoTipoGarantiaBLL.ObterOpcoesTipoGaratiaPorGarantia(Convert.ToInt32(ddlTipoGarantia.SelectedValue));
            ddlOpcaoTipoGarantia.DataValueField = "Code";
            ddlOpcaoTipoGarantia.DataTextField = "U_NomeOpTpGarant";
            ddlOpcaoTipoGarantia.DataBind();

            if (!string.IsNullOrEmpty(valor))
                ddlOpcaoTipoGarantia.SelectedValue = valor;
        }

        protected void ddlTipoGarantia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarOpcoesTipoGarantia("");

            if (ddlTipoGarantia.SelectedValue.Equals("4") || ddlTipoGarantia.SelectedValue.Equals("5"))
            {
                if (ddlOpcaoTipoGarantia.Items.Count > 0)
                {
                    txtDescricaoFalha.Text = ddlOpcaoTipoGarantia.SelectedItem.Text;
                    txtCorrecaoFalha.Text = ddlOpcaoTipoGarantia.SelectedItem.Text;
                    txtCausaFalha.Text = ddlOpcaoTipoGarantia.SelectedItem.Text;
                }

                if (ddlTipoGarantia.SelectedValue.Equals("4"))
                {
                    txtValorUnitario.Text = "160,00";
                    txtValorTpr.Text = "160,00";
                }

                if (ddlOpcaoTipoGarantia.SelectedValue.Equals("14"))
                {
                    txtValorUnitario.Text = "200,00";
                    txtValorTpr.Text = "200,00";
                }
            }
            
            txtDescricaoFalha.Focus();
        }

        private string RemoverAcentuacao(string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != System.Globalization.UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        //BOTÃO PARA INSERIR O ANEXO
        protected void btnInserirAnexo_Click(object sender, EventArgs e)
        {
            try
            {
                string caminho = "";
                string extensao = "";
                string nomeArquivo = "";

                if (fuFotosFalha.HasFile)
                {
                    nomeArquivo = fuFotosFalha.PostedFile.FileName;

                    string nomeArqDrop = RemoverAcentuacao(ddlTipoAnexo.SelectedItem.Text.Replace(" ", "").Replace("/", ""));

                    caminho = Server.MapPath("~/ArquivosExportacao/");
                    extensao = nomeArquivo.Substring(nomeArquivo.Length - 4).ToLower();

                    nomeArquivo = nomeArqDrop + DateTime.Now.ToString("ddMMyyyyHHmmss");

                    string caminhoCompleto = caminho + nomeArquivo + extensao;

                    fuFotosFalha.SaveAs(caminho + nomeArquivo + extensao);
                }

                if (!hfCodAnexo.Value.Equals(""))
                {
                    WsFotonRamo.ipostep_vP0010000104in_WCSX_comsapb1ivplatformruntime_INB_WS_CALL_SYNC_XPT_INB_WS_CALL_SYNC_XPTipo_proc_Service ws = new WsFotonRamo.ipostep_vP0010000104in_WCSX_comsapb1ivplatformruntime_INB_WS_CALL_SYNC_XPT_INB_WS_CALL_SYNC_XPTipo_proc_Service();
                    ws.Url = "http://FOTONWEB.sapcloud.local:8080/B1iXcellerator/exec/soap/vP.0010000106.in_WCSX/com.sap.b1i.vplatform.runtime/INB_WS_CALL_SYNC_XPT/INB_WS_CALL_SYNC_XPT.ipo/proc";
                    ws.Credentials = new System.Net.NetworkCredential("foton_int", "Foton@!nt2016");

                    WsFotonRamo.OBJ221Type Anexo = new WsFotonRamo.OBJ221Type();

                    List<WsFotonRamo.OBJ221TypeOBJECTRow> Anexos_rows = new List<WsFotonRamo.OBJ221TypeOBJECTRow>();

                    WsFotonRamo.OBJ221TypeOBJECTRow Anexos;

                    if (!hfCodAnexo.Value.Equals(""))
                    {
                        int linhasGrid = gdvChamadoAnexo.Rows.Count;

                        for (int i = 1; i <= linhasGrid; i++)
                        {
                            Anexos = new WsFotonRamo.OBJ221TypeOBJECTRow();
                            Anexos_rows.Add(Anexos);
                        }
                    }

                    Anexos = new WsFotonRamo.OBJ221TypeOBJECTRow();
                    Anexos.SourcePath = caminho;
                    Anexos.FileName = nomeArquivo;
                    Anexos.FileExtension = extensao.Replace(".", "").Trim();

                    Anexos_rows.Add(Anexos);

                    Anexo.OBJECT = new WsFotonRamo.OBJ221TypeOBJECT();

                    if (!hfCodAnexo.Value.Equals(""))
                    {
                        Anexo.OBJECT.AbsoluteEntry = Convert.ToInt64(hfCodAnexo.Value);
                        Anexo.OBJECT.AbsoluteEntrySpecified = true;
                    }

                    Anexo.OBJECT.Attachments2_Lines = Anexos_rows.ToArray();

                    WsFotonRamo.LOG221Type Retorno = ws.RSDWSFOBJ221(Anexo);

                    if (Retorno.Status.Equals("S"))
                        CarregarAnexos();
                }
                else
                {
                    IList<AnexoDTO> lista = new List<AnexoDTO>();
                    AnexoDTO anexoDTO = new AnexoDTO();
                    anexoDTO.Line = (lista.Count + 1).ToString();
                    anexoDTO.Date = DateTime.Now;
                    anexoDTO.Extensao = extensao.Replace(".", "");
                    anexoDTO.NomeArquivo = nomeArquivo;
                    anexoDTO.Caminho = caminho;

                    if (ViewState["ListaAnexos"] != null)
                    {
                        lista = (IList<AnexoDTO>)ViewState["ListaAnexos"];
                        lista.Add(anexoDTO);

                        ViewState["ListaAnexos"] = lista;
                    }
                    else
                    {
                        lista.Add(anexoDTO);
                        ViewState["ListaAnexos"] = lista;
                    }

                    CarregarAnexos();
                }
            }
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = er.Message + ". " + er.StackTrace;
                mensagemDTO.Tipo = MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void ddlOpcaoTipoGarantia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOpcaoTipoGarantia.SelectedValue.Equals("14"))
            {
                txtValorUnitario.Text = "200,00";
                txtValorTpr.Text = "200,00";
            }
            else if (ddlOpcaoTipoGarantia.SelectedValue.Equals("15"))
            {
                txtValorUnitario.Text = "168,00";
                txtValorTpr.Text = "168,00";
            }
            else if (ddlOpcaoTipoGarantia.SelectedValue.Equals("16"))
            {
                txtValorUnitario.Text = "208,00";
                txtValorTpr.Text = "208,00";
            }
            else
                txtValorUnitario.Text = "";

            txtDescricaoFalha.Focus();
        }
    }
}