/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using SAPB1.WebForm.App_Code;
using SAPB1.BLL.Empresa.Filial;
using SAPB1.BLL.Funcionario;
using SAPB1.BLL.Item;
using SAPB1.BLL.ItensTabelaPreco;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.BLL.PedidoVenda;
using SAPB1.DTO.CondicaoPagamento;
using SAPB1.DTO.Empresa.Filial;
using SAPB1.DTO.Funcionario;
using SAPB1.DTO.Item;
using SAPB1.DTO.ItensTabelaPreco;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.DTO.PedidoVenda;
using SAPB1.DTO.TabelaPreco;
using SAPB1.DTO.TiposEnvio;
using SAPB1.DTO.Utilizacao;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace SAPB1.WebForm
{
    public partial class CotacaoVenda_Action : Page
    {
        int docEntry = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnImprimir.Visible = false;

            if (Request.QueryString["docEntry"] != null)
            {
                docEntry = Convert.ToInt32(Request.QueryString["docEntry"].ToString());
                hfNumeroPedido.Value = Request.QueryString["docEntry"].ToString();
            }
                

            if (!IsPostBack)
            {
                if (docEntry > 0)
                {
                    pnlAviso.Attributes.Add("style", "display:none");
                    pnlAvisoItem.Attributes.Add("style", "display:none");
                    Carregar();
                }
                    
                else
                {
                    txtDataLancamento.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    Combo.CondicaoPagamento(ddlCondicoesPagamento, new CondicaoPagamentoDTO() { });
                    Combo.Filial(cmbFilial, "0");
                    Combo.Transportadora(cmbTransportadora, "-1", new ParceiroNegocioDTO() { });
                    Combo.Utilizacao(ddlUtilizacaoItem, new UtilizacaoDTO() { });
                    Combo.TiposEnvio(cmbTipoFrete, new TipoEnvioDTO() { });

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
                }
            }
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

        void Carregar()
        {
            try
            {
                CotacaoBLL cotacaoBLL = new CotacaoBLL();
                IList<CotacaoDTO> cotacaoDTO = cotacaoBLL.Selecionar(docEntry);

                btnSalvar.Visible = false;
                btnInserirItem.Visible = false;
                btnImprimir.Visible = true;

                txtCodigoPn.Text = cotacaoDTO[0].CardCode.Trim();
                txtParceiroNegocio.Text = cotacaoDTO[0].CardName.Trim();
                txtDataLancamento.Text = ((cotacaoDTO[0].DocDate > DateTime.MinValue) ? cotacaoDTO[0].DocDate.ToString("dd/MM/yyyy") : "");
                txtDataDocumento.Text = ((cotacaoDTO[0].DocDueDate > DateTime.MinValue) ? cotacaoDTO[0].DocDueDate.ToString("dd/MM/yyyy") : "");
                txtObservacoes.Text = cotacaoDTO[0].Comments.ToString().Trim();
                Combo.CondicaoPagamento(ddlCondicoesPagamento, new CondicaoPagamentoDTO() { GroupNum = Convert.ToInt32(cotacaoDTO[0].GroupNum) });

                //



                #region Dados do cabeçalho

                txtParceiroNegocio.Text = cotacaoDTO[0].CardName;
                hfParceiroNegocio.Value = cotacaoDTO[0].CardCode;
                //hfListaPreco.Value = cotacaoDTO.ListNum.ToString();
                txtCnpj.Text = cotacaoDTO[0].U_CNPJ;

                txtCodigoPn.Text = cotacaoDTO[0].CardCode;
                //txtMoeda.Text = cotacaoDTO.DocCur;
                txtNumero.Text = cotacaoDTO[0].DocNum.ToString();
                txtStatus.Text = RetornarStatus(cotacaoDTO[0].DocStatus.ToString(), cotacaoDTO[0].CANCELED.ToString());
                txtDataLancamento.Text = cotacaoDTO[0].DocDate.ToString("dd/MM/yyyy");
                txtDataEntrega.Text = cotacaoDTO[0].DocDueDate.ToString("dd/MM/yyyy");
                txtDataDocumento.Text = cotacaoDTO[0].TaxDate.ToString("dd/MM/yyyy");
                //txtNumeroPedido.Text = cotacaoDTO.NumAtCard;
                //ddlTitular.SelectedValue = cotacaoDTO.OwnerCode;
                //txtDespesaAdicional.Text = cotacaoDTO.RetornarValorDespesaFrete(Convert.ToInt64(cotacaoDTO.DocEntry)).ToString("n6");
                //txtDespesaAdicional.Text = cotacaoDTO.ValorFreteCab.ToString("n2");
                //txtImposto.Text = cotacaoDTO.VatSum.ToString("n6");

                //if (pedidoVendaDTO.TemFrete != "")
                //    cmbTemFrete.SelectedValue = cotacaoDTO.TemFrete;

                //txtPercentualFrete.Text = cotacaoDTO.PercentualFrete.ToString("n2");

                /* Início dados filial */
                FilialDTO filialDTO = new FilialDTO();
                filialDTO.Disabled = "N";
                //Combo.Filial(ddlFilial, cotacaoDTO.Filial.BPLId.ToString(), filialDTO);

                //txtCnpjFilial.Text = cotacaoDTO.Filial.TaxIdNum;
                /*Fim dados filial*/

                //string codigoTransp = cotacaoDTO.RetornarCodigoTransportadora(Convert.ToInt64(cotacaoDTO.DocEntry));

                //if (!codigoTransp.Equals("") && !codigoTransp.Equals("0"))
                //{
                //    cmbTransportadora.SelectedValue = codigoTransp;

                //    ParceiroNegocioDTO transDTO = parceiroNegocioBLL.Selecionar(codigoTransp);

                //    if (!string.IsNullOrEmpty(transDTO.CardCode))
                //        txtCnpjTransp.Text = transDTO.U_CNPJ;
                //}

                //cmbTipoFrete.SelectedValue = cotacaoDTO.TipoEnvio.TrnspCode.ToString();

                #endregion

                PedidoVendaDTO pedidoVendaEnderecoLogistica = new PedidoVendaDTO();
                pedidoVendaEnderecoLogistica.DocNum = cotacaoDTO[0].DocNum;

                SAPB1.BLL.PedidoVenda.EnderecoBLL enderecoBLL = new SAPB1.BLL.PedidoVenda.EnderecoBLL();
                SAPB1.DTO.PedidoVenda.EnderecoDTO enderecoDTO = enderecoBLL.RetornarEndereco(pedidoVendaEnderecoLogistica);

                //MunicipioDTO municipioDTO = new MunicipioDTO();
                //municipioDTO.Estado = new EstadoDTO();
                //municipioDTO.Estado.Code = enderecoDTO.State;

                //hfListaPreco.Value = parceiroNegocioDTO.ListNum.ToString();

                //ddlCondicoesPagamento.SelectedValue = cotacaoDTO.GroupNum;
                //ddlFormaPagamento.SelectedValue = cotacaoDTO.PeyMethod;

                //txtObservacoes.Text = cotacaoDTO.Comments;

                #region Dados do rodapé
                //txtTotalPagar.Text = cotacaoDTO.DocTotal.ToString("n6");

                ////Vendedor
                //VendedorDTO vendedorDTO = new VendedorDTO();
                //vendedorDTO.Locked = "N";
                //vendedorDTO.Active = "Y";
                //Combo.Vendedor(ddlVendedor, cotacaoDTO.Vendedor.SlpCode.ToString(), vendedorDTO);
                #endregion

                CotacaoItemDTO CotacaoItemDTO = new CotacaoItemDTO();
                CotacaoItemDTO.DocEntry = cotacaoDTO[0].DocEntry;

                ItemCotacaoBLL itemCotacaoBLL = new ItemCotacaoBLL();
                IList<CotacaoItemDTO> itens = itemCotacaoBLL.Listar(CotacaoItemDTO);

                gdvItens.DataSource = itens;
                gdvItens.DataBind();

                if (itens.Count > 0)
                    txtTotalProdutos.Text = itens.Sum(t => t.LineTotal).ToString("n6");
                else
                    txtTotalProdutos.Text = "0,000000";

                btnSalvar.Visible = false;
                btnInserirItem.Visible = false;
                btnImprimir.Visible = true;

                //gdvItens.Columns[14].Visible = false;

                txtDataEntrega.Enabled = false;
                txtParceiroNegocio.Enabled = false;
                txtCodigoItem.Enabled = false;
                txtNomeItem.Enabled = false;
                txtQtdItem.Enabled = false;
                txtDesconto.Enabled = false;
                //cmbTransportadora.Enabled = false;
                cmbTipoFrete.Enabled = false;
                txtObservacoes.Enabled = false;
                ddlCondicoesPagamento.Enabled = false;
                txtCnpj.Enabled = false;
                cmbTemFrete.Enabled = false;
                txtPercentualFrete.Enabled = false;

            }
            catch (Exception erro)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = erro.Message;
                mensagemDTO.Tipo = MensagemType.Erro;

                hfErrosRegras.Value = "1";

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string diretorio = HttpRuntime.AppDomainAppPath;
            string diretorioPedido = $@"{HttpRuntime.AppDomainAppPath}\PDFs\Cotacao_{docEntry.ToString()}.pdf";
            string nome = $@"PDFs\Cotacao_{docEntry.ToString()}.pdf";
            bool Cria = CriaPDFe(diretorioPedido, diretorio, docEntry.ToString());
            if (Cria)
                Response.Redirect(nome);

        }

        public bool CriaPDFe(string AttachPath, string Diretorio, string DEntryNF)
        {
            bool retorno = false;

            //ConnectionSQLSAP conn = new ConnectionSQLSAP();


            try
            {
                ReportDocument cryRpt = new ReportDocument();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                Tables CrTables;

                string locale = "Cotacao";
                string sNomeArquivo = null;
                sNomeArquivo = $@"C:\Portal Dev\SAPB1";


                /* Mudar a FLAG para Processando */
                //LOGTXT.LogTxt("Processando o Invoice: " + DNum, 3);

                //CARREGA RPT
                sNomeArquivo += @"\RPT\" + locale + ".rpt"; //Pegando o RPT da pasta do serviço com nome PC.
                cryRpt.Load(sNomeArquivo); //Load no RPT

                SqlConnection conexao = new SqlConnection(/*Criptografia.Decriptar(*/ConfigurationManager.ConnectionStrings["SqlServerConexao"].ToString()/*, "UE9846MB")*/);
                string Base = conexao.Database.ToString();

                //Cria conexão com o RPT
                try
                {
                    crConnectionInfo.ServerName = "SRV-SAP"; //Propriedade BD
                    crConnectionInfo.DatabaseName = Base; //Propriedade_BASE
                    crConnectionInfo.UserID = "sa"; //Propriedade_Usuario_Banco
                    crConnectionInfo.Password = "b1admin"; //Propriedade_Senha_Banco
                }
                catch (Exception ex)
                {
                    //conn.ListaSQL(@"UPDATE OPCH SET U_StatusComprovante = '3',U_LogComprovante = '" + ex.Message.ToString() + "' WHERE DocEntry = " + DEntry, empresa);
                    //LOGTXT.LogTxt("Erro ao criar conexão com Crystal PDF. Motivo: " + ex.Message.ToString(), 2);
                }
                CrTables = cryRpt.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }


                //Seta os Parametros do RPT
                cryRpt.SetParameterValue(0, DEntryNF);

                //Salvar o RPT


                try
                {
                    CreateDirectoryIfNotExists(Diretorio);
                    DeleteFileIfExists(AttachPath);

                    //Achei na NET
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    try
                    {

                        CrDiskFileDestinationOptions.DiskFileName = AttachPath;

                    }
                    catch (Exception ex)
                    {
                        //LOGTXT.LogTxt("Erro ao setar Path. Motivo: " + ex.Message.ToString(), 2);
                    }

                    try
                    {
                        CrExportOptions = cryRpt.ExportOptions;
                        {
                            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                            CrExportOptions.FormatOptions = CrFormatTypeOptions;
                        }
                    }
                    catch (Exception ex)
                    {
                        //LOGTXT.LogTxt("Erro ao setar Export Options. Motivo: " + ex.Message.ToString(), 2);
                    }

                    try
                    {
                        cryRpt.Export();
                        retorno = true;
                    }
                    catch (Exception ex)
                    {
                        //LOGTXT.LogTxt("Erro ao Exportar. Motivo: " + ex.Message.ToString() + " ServerName: " + crConnectionInfo.ServerName + " DataBaseName: " + crConnectionInfo.DatabaseName + " UserId: " + crConnectionInfo.UserID + " Password: " + crConnectionInfo.Password + " ", 2);
                        retorno = false;
                    }

                    try
                    {
                        if (cryRpt != null)
                        {
                            cryRpt.Close();
                            cryRpt.Dispose();
                        }
                    }
                    catch (Exception ex1)
                    {
                        //LOGTXT.LogTxt($@"Erro ao fechar RPT: {ex1.Message}", 2);
                    }


                }
                catch (Exception ex)
                {
                    //conn.ListaSQL(@"UPDATE OPCH SET U_StatusComprovante = '3',U_LogComprovante = '" + ex.Message.ToString() + "' WHERE DocEntry = " + DEntry, empresa);
                    //LOGTXT.LogTxt("Erro ao exportar PDF. Motivo: " + ex.Message.ToString(), 2);
                    retorno = false;
                }


            }
            catch (Exception ex)
            {
                //conn.ListaSQL(@"UPDATE OPCH SET U_StatusComprovante = '3',U_LogComprovante = '" + ex.Message.ToString().Replace("'", "") + "' WHERE DocEntry = " + DEntry, empresa);
                //LOGTXT.LogTxt("Erro ao carregar o arquivo RPT. Motivo: " + ex.Message.ToString(), 2);
            }



            return retorno;
        }

        public void DeleteFileIfExists(string AttachPath)
        {
            try
            {
                if (System.IO.File.Exists(AttachPath))
                    System.IO.File.Delete(AttachPath);
            }
            catch (Exception e)
            {

            }


        }
        public void CreateDirectoryIfNotExists(string Path)
        {
            if (!System.IO.Directory.Exists(Path))
                System.IO.Directory.CreateDirectory(Path);
        }

        void Salvar()
        {
            try
            {
                CotacaoBLL cotacaoBLL = new CotacaoBLL();
                CotacaoDTO cotacaoDTO = new CotacaoDTO();
                cotacaoDTO.CardCode = txtCodigoPn.Text.ToUpper().Trim();
                cotacaoDTO.CardName = txtParceiroNegocio.Text.ToUpper().Trim();
                cotacaoDTO.Address = "";
                cotacaoDTO.BPLId = Convert.ToInt32(cmbFilial.SelectedValue);
                cotacaoDTO.DocDate = ((!string.IsNullOrEmpty(txtDataLancamento.Text)) ? Convert.ToDateTime(txtDataLancamento.Text) : DateTime.Now);
                cotacaoDTO.DocDueDate = ((!string.IsNullOrEmpty(txtDataEntrega.Text)) ? Convert.ToDateTime(txtDataEntrega.Text) : DateTime.MinValue);
                cotacaoDTO.TaxDate = DateTime.Now;
                cotacaoDTO.Itens = new List<CotacaoItemDTO>();
                cotacaoDTO.DocTotal = ((!string.IsNullOrEmpty(txtTotalPagar.Text)) ? Convert.ToDecimal(txtTotalPagar.Text.Replace(".", ",")) : 0m);
                cotacaoDTO.Carrier = cmbTransportadora.SelectedValue;
                cotacaoDTO.TrnspCode = Convert.ToInt32(cmbTipoFrete.SelectedValue);
                cotacaoDTO.PaymentGroupCode = ddlCondicoesPagamento.SelectedValue;
                cotacaoDTO.U_S7_CobrarFrete = cmbTemFrete.SelectedValue;
                cotacaoDTO.U_S7_TaxaFrete = ((!string.IsNullOrEmpty(txtPercentualFrete.Text)) ? Convert.ToDecimal(txtPercentualFrete.Text) : 0M);
                cotacaoDTO.U_S7_ValorFrete = ((!string.IsNullOrEmpty(txtDespesaAdicional.Text)) ? Convert.ToDecimal(txtDespesaAdicional.Text) : 0M);
                cotacaoDTO.Comments = txtObservacoes.Text.ToUpper().Trim();
                string[] s = hfDadosItens.Value.Split('#');

                for (int i = 0; i < s.Length; i++)
                {
                    CotacaoItemDTO item = new CotacaoItemDTO();
                    item.ItemCode = s[i].Split('|')[1];
                    item.Price = Convert.ToDouble(s[i].Split('|')[3].Replace(".", ","));
                    item.Quantity = Convert.ToDouble(s[i].Split('|')[2].Replace(".", ","));
                    item.DiscPrcnt = Convert.ToDouble(s[i].Split('|')[4].Replace(".", ","));
                    item.LineTotal = ((item.Price * item.Quantity) - item.DiscPrcnt);
                    item.Usage = Convert.ToInt32(s[i].Split('|')[7]);
                    item.U_Peso = Convert.ToDouble(s[i].Split('|')[8].Replace(".", ","));
                    item.unitMsr = s[i].Split('|')[9];
                    item.Comprimento = Convert.ToDouble(s[i].Split('|')[10].Replace(".", ","));
                    item.QtdBarra = Convert.ToDouble(s[i].Split('|')[11].Replace(".", ","));
                    cotacaoDTO.Itens.Add(item);
                }
                
                if (cotacaoBLL.EditarInserir(cotacaoDTO))
                    Response.Redirect("CotacaoVenda.aspx");
                else
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = cotacaoBLL.Resultado;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    hfErrosRegras.Value = "1";
                }
            }
            catch (Exception erro)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = erro.Message;
                mensagemDTO.Tipo = MensagemType.Aviso;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                hfErrosRegras.Value = "1";
            }
        }

        #region

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

        #endregion

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }
    }
}