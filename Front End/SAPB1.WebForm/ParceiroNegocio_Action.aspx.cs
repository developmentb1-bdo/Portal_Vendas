/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.WebForm.App_Code;
using SAPB1.BLL.Services.Cep;
using SAPB1.BLL.Administracao.Configuracao;

namespace SAPB1.WebForm
{
    public partial class ParceiroNegocio_Action : System.Web.UI.Page
    {
        string cardCode = string.Empty;
        string msg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["cardCode"] != null)
                    cardCode = Request.QueryString["cardCode"].ToString();

                if (!IsPostBack)
                {
                    txtCpfCnpj.Attributes.Add("placeholder", "Digite um CPF ou CNPJ");
                    txtRazaoSocial.Attributes.Add("placeholder", "Digite a Razão Social");

                    txtLimiteCredito.Attributes.Add("onkeypress", "return somenteNumeroDecimal(this,event);");

                    if(pnlAviso.Visible)
                    {
                        pnlAviso.Visible = false;
                    }

                    if (string.IsNullOrEmpty(cardCode))
                    {
                        ddlAtivoPn.SelectedValue = "1";

                        txtIdContatoEndereco.Text = "Endereço Cobrança";
                        txtIdContatoEndereco.ReadOnly = true;

                        txtIdContatoEnderecoDest.Text = "Endereço Destinatário";
                        txtIdContatoEnderecoDest.ReadOnly = true;

                        PreencherCombos();

                        checkPagamentoUnico.Checked = true;
                    }
                    else
                        CarregarDados();
                }
            }
            catch (Exception erro)
            {
                Mensagem(erro.Message, MensagemType.Erro);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Salvar();
            }
            catch(Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Tipo = MensagemType.Erro;
                mensagemDTO.Mensagem = er.Message;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

        private void PreencherCombos()
        {
            Combo.Vendedor(ddlVendedor, "0", new DTO.Funcionario.Vendedor.VendedorDTO());
            Combo.Moeda(ddlMoeda, "R$");
            Combo.Estado(ddlEstado, "0", new DTO.Estado.EstadoDTO() { Pais = new DTO.Administracao.Configuracao.PaisDTO() { Code = "BR", Name = "BR" } });
            Combo.Estado(ddlEstadoDest, "0", new DTO.Estado.EstadoDTO() { Pais = new DTO.Administracao.Configuracao.PaisDTO() { Code = "BR", Name = "BR" } });
            Combo.CondicaoPagamento(cmbCondicaoPgto, new DTO.CondicaoPagamento.CondicaoPagamentoDTO() { });

            DTO.FormasPagamento.FormaPagamentoDTO formaPagamentoDTO = new DTO.FormasPagamento.FormaPagamentoDTO();
            formaPagamentoDTO.Active = "Y";
            formaPagamentoDTO.Type = "I";

            Combo.FormaPagamento(ddlFormaPagamento, formaPagamentoDTO);
            Combo.Pais(ddlPais, "1058");
            Combo.Pais(ddlPaisDest, "1058");

            Combo.SetorIndustrial(cmbSetorIndustrial, "0");
            Combo.Representante(ddlRepresentante, "0");
        }

        private void CarregarDados()
         {
            try
            {
                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                parceiroNegocioDTO = parceiroNegocioBLL.Selecionar(cardCode);

                txtRazaoSocial.Text = parceiroNegocioDTO.CardName;
                txtNomeEstranFantasia.Text = parceiroNegocioDTO.CardFName;
                ddlTipo.SelectedValue = parceiroNegocioDTO.CardType.Substring(0, 1).ToUpper();
                Combo.Grupo(ddlGrupo, parceiroNegocioDTO.GroupCode.ToString(), GroupType.None);
                txtRua.Text = parceiroNegocioDTO.MailAddres;
                Combo.Moeda(ddlMoeda, parceiroNegocioDTO.Currency);
                txtDdd.Text = parceiroNegocioDTO.Phone2;
                txtTelefone.Text = parceiroNegocioDTO.Phone1;

                if(parceiroNegocioDTO.Cellular.Length == 11)
                {
                    txtDddCelular.Text = parceiroNegocioDTO.Cellular.Substring(0, 2);
                    txtCelular.Text = parceiroNegocioDTO.Cellular.Substring(2, 9);
                }
                else
                {
                    txtCelular.Text = parceiroNegocioDTO.Cellular;
                }

                txtEmail.Text = parceiroNegocioDTO.E_Mail;
                txtFax.Text = parceiroNegocioDTO.Fax;
                txtLimiteCredito.Text = parceiroNegocioDTO.CreditLine.ToString("c").Replace("R$", "").Replace(" ", "").Trim();
                txtObservacoes.Text = parceiroNegocioDTO.Free_Text;
                txtWebSite.Text = parceiroNegocioDTO.IntrntSite;
                Combo.CondicaoPagamento(cmbCondicaoPgto, new DTO.CondicaoPagamento.CondicaoPagamentoDTO() { });
                cmbCondicaoPgto.SelectedValue = parceiroNegocioDTO.GroupNum.ToString();
                checkPagamentoUnico.Checked = ((parceiroNegocioDTO.SinglePaym == 'Y') ? true : false);

                ddlAtivoPn.SelectedValue = "1";

                // Seção de Endereço.
                IList<EnderecoDTO> listEnderecoDTO = new List<EnderecoDTO>();
                listEnderecoDTO = parceiroNegocioDTO.ListEndereco.OrderBy(tipo => tipo.AdresType).ToList<EnderecoDTO>();

                txtIdContatoEndereco.ReadOnly = true;
                txtIdContatoEnderecoDest.ReadOnly = true;

                // 2 = Cobrança E Destinatário.
                if (listEnderecoDTO.Count == 2)
                {
                    PaisBLL paisBLL = new PaisBLL();

                    txtIdContatoEndereco.Text = listEnderecoDTO[0].Address;
                   
                    txtTipoLogradouro.Text = listEnderecoDTO[0].AddrType;
                    txtRua.Text = listEnderecoDTO[0].Street;
                    txtNumeroRua.Text = listEnderecoDTO[0].StreetNo;
                    txtComplemento.Text = listEnderecoDTO[0].Building;
                    txtCep.Text = listEnderecoDTO[0].ZipCode;
                    txtCidade.Text = listEnderecoDTO[0].City;
                    txtBairro.Text = listEnderecoDTO[0].Block;
                    Combo.Estado(ddlEstado, "0", new DTO.Estado.EstadoDTO() { Pais = new DTO.Administracao.Configuracao.PaisDTO() { Code = "BR", Name = "BR" } });
                    ddlEstado.SelectedValue = listEnderecoDTO[0].State;
                    Combo.Municipio(ddlMunicipio, "0", new DTO.Municipio.MunicipioDTO() { Estado = new DTO.Estado.EstadoDTO() { Code = ddlEstado.SelectedValue } });
                    ddlMunicipio.SelectedValue = listEnderecoDTO[0].County;

                    Combo.Pais(ddlPais, "0");
                    IList<DTO.Administracao.Configuracao.PaisDTO> listPais = paisBLL.BuscarPorSigla(listEnderecoDTO[0].Country);

                    if (listPais.Count > 0)
                        ddlPais.SelectedValue = listPais[0].CntCodNum;
                    else
                        ddlPais.SelectedValue = "1058";

                    txtIdContatoEnderecoDest.Text = listEnderecoDTO[1].Address;
                    
                    txtTipoLogradouroDest.Text = listEnderecoDTO[1].AddrType;
                    txtRuaDest.Text = listEnderecoDTO[1].Street;
                    txtNumeroRuaDest.Text = listEnderecoDTO[1].StreetNo;
                    txtComplementoDest.Text = listEnderecoDTO[1].Building;
                    txtCepDest.Text = listEnderecoDTO[1].ZipCode;
                    txtCidadeDest.Text = listEnderecoDTO[1].City;
                    txtBairroDest.Text = listEnderecoDTO[1].Block;
                    Combo.Estado(ddlEstadoDest, "0", new DTO.Estado.EstadoDTO() { Pais = new DTO.Administracao.Configuracao.PaisDTO() { Code = "BR", Name = "BR" } });
                    ddlEstadoDest.SelectedValue = listEnderecoDTO[1].State;
                    Combo.Municipio(ddlMunicipioDest, "0", new DTO.Municipio.MunicipioDTO() { Estado = new DTO.Estado.EstadoDTO() { Code = ddlEstadoDest.SelectedValue } });
                    ddlMunicipioDest.SelectedValue = listEnderecoDTO[1].County;

                    Combo.Pais(ddlPaisDest, "0");
                    listPais = paisBLL.BuscarPorSigla(listEnderecoDTO[1].Country);

                    if (listPais.Count > 0)
                        ddlPaisDest.SelectedValue = listPais[0].CntCodNum;
                    else
                        ddlPaisDest.SelectedValue = "1058";
                }
                // Fim da Seção de Endereço.

                // Seção de Contato.
                if (parceiroNegocioDTO.ListContato.Count > 0)
                {
                    txtIdContato.Text = parceiroNegocioDTO.ListContato[0].Name;
                    txtEmailContato.Text = parceiroNegocioDTO.ListContato[0].E_MailL;
                    txtTelefoneContato.Text = parceiroNegocioDTO.ListContato[0].Tel1;
                    txtObservacoesContato.Text = parceiroNegocioDTO.ListContato[0].Notes1;
                }

                // Seção de Contabilidade.
                if (parceiroNegocioDTO.ListIdentificacaoFiscal.Count > 0)
                {
                    for (int i = 0; i < parceiroNegocioDTO.ListIdentificacaoFiscal.Count; i++)
                    {
                        if (parceiroNegocioDTO.ListIdentificacaoFiscal[i].TaxId0 != "")
                        {
                            txtCpfCnpj.Text = parceiroNegocioDTO.ListIdentificacaoFiscal[i].TaxId0;
                            txtIe.Text = parceiroNegocioDTO.ListIdentificacaoFiscal[i].TaxId1;

                            if (parceiroNegocioDTO.ListIdentificacaoFiscal[i].TaxId1.ToUpper().Equals("ISENTO"))
                            {
                                check0.Checked = true;
                                check1.Checked = true;
                                check2.Checked = true;
                                txtIe.ReadOnly = true;
                                txtIncricaoEstadualEnderecoDest.ReadOnly = true;
                                txtInscricaoEstadualEndereco.ReadOnly = true;
                            }

                            txtIm.Text = parceiroNegocioDTO.ListIdentificacaoFiscal[i].TaxId2;
                        }
                    }
                }

                txtCpfCnpjDest.Text = txtCpfCnpj.Text;
                txtIncricaoEstadualEnderecoDest.Text = txtIe.Text;

                txtIndicardorIeDest.Text = txtIm.Text;
                txtCpfCnpjEntrega.Text = txtCpfCnpj.Text;

                txtInscricaoEstadualEndereco.Text = txtIe.Text;
                txtIndicadorIe.Text = txtIm.Text;

                Combo.SetorIndustrial(cmbSetorIndustrial, parceiroNegocioDTO.IndustryC);
                Combo.Representante(ddlRepresentante, parceiroNegocioDTO.AgentCode);

                DTO.FormasPagamento.FormaPagamentoDTO formaPagamentoDTO = new DTO.FormasPagamento.FormaPagamentoDTO();
                formaPagamentoDTO.Active = "Y";
                Combo.FormaPagamento(ddlFormaPagamento, formaPagamentoDTO);

                ddlFormaPagamento.SelectedValue = parceiroNegocioDTO.PymCode;

                Combo.Vendedor(ddlVendedor, parceiroNegocioDTO.SlpCode.ToString(), new DTO.Funcionario.Vendedor.VendedorDTO());
            }
            catch (Exception erro)
            {
                Mensagem(erro.Message, MensagemType.Erro);
            }
        }

        private void Salvar()
        {
            try
            {
                if(!hfErros.Value.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Mensagem = hfErros.Value.Replace("-", "<br>"); ;
                    mensagemDTO.Tipo = MensagemType.Aviso;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);

                    return;
                }

                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();
                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();

                parceiroNegocioDTO.CardCode = cardCode;

                parceiroNegocioDTO.CardName = txtRazaoSocial.Text.Trim();
                parceiroNegocioDTO.CardFName = txtNomeEstranFantasia.Text;
                parceiroNegocioDTO.CardType = GetCardType(ddlTipo.SelectedValue);
                parceiroNegocioDTO.GroupCode = Convert.ToInt32(ddlGrupo.SelectedValue);
                parceiroNegocioDTO.MailAddres = txtRua.Text;
                parceiroNegocioDTO.Phone1 = txtTelefone.Text; //(txtDdd.Text.Trim() + txtTelefone.Text.Trim());
                parceiroNegocioDTO.Phone2 = txtDdd.Text;
                parceiroNegocioDTO.Currency = ddlMoeda.SelectedValue;
                parceiroNegocioDTO.Country = "BR"; //ddlPais.SelectedValue;
                parceiroNegocioDTO.E_Mail = txtEmail.Text.Trim();
                parceiroNegocioDTO.Fax = txtFax.Text.Trim();
                parceiroNegocioDTO.Cellular = txtDddCelular.Text + txtCelular.Text;
                parceiroNegocioDTO.Series = ((ddlTipo.SelectedValue == "C" || ddlTipo.SelectedValue == "L") ? 62 : 63);
                parceiroNegocioDTO.CreditLine = (!string.IsNullOrEmpty(txtLimiteCredito.Text) ? Convert.ToDecimal(txtLimiteCredito.Text) : decimal.Zero);
                parceiroNegocioDTO.Free_Text = txtObservacoes.Text;
                parceiroNegocioDTO.IntrntSite = txtWebSite.Text;
                parceiroNegocioDTO.GroupNum = Convert.ToInt32(cmbCondicaoPgto.SelectedValue);
                parceiroNegocioDTO.SinglePaym = ((checkPagamentoUnico.Checked) ? 'Y' : 'N');

                // Seção de Endereço.
                IList<EnderecoDTO> listEnderecoDTO = new List<EnderecoDTO>();

                EnderecoDTO enderecoDTO = new EnderecoDTO();
                enderecoDTO.CardCode = cardCode;
                enderecoDTO.Address = txtIdContatoEndereco.Text;
                enderecoDTO.AddrType = txtTipoLogradouro.Text;
                enderecoDTO.Street = txtRua.Text;
                enderecoDTO.StreetNo = txtNumeroRua.Text;
                enderecoDTO.Building = txtComplemento.Text;
                enderecoDTO.ZipCode = txtCep.Text;
                enderecoDTO.City = txtCidade.Text;
                enderecoDTO.Block = txtBairro.Text;
                enderecoDTO.State = ((ddlEstado.SelectedValue != "0") ? ddlEstado.SelectedValue : "");
                enderecoDTO.County = ((ddlMunicipio.SelectedValue != "0") ? ddlMunicipio.SelectedValue : "");
                enderecoDTO.Country = "BR"; //ddlPais.SelectedValue;
                enderecoDTO.AdresType = 'B';

                listEnderecoDTO.Add(enderecoDTO);

                enderecoDTO = new EnderecoDTO();
                enderecoDTO.CardCode = cardCode;
                enderecoDTO.Address = txtIdContatoEnderecoDest.Text;
                enderecoDTO.AddrType = txtTipoLogradouroDest.Text;
                enderecoDTO.Street = txtRuaDest.Text;
                enderecoDTO.StreetNo = txtNumeroRuaDest.Text;
                enderecoDTO.Building = txtComplementoDest.Text;
                enderecoDTO.ZipCode = txtCepDest.Text;
                enderecoDTO.City = txtCidadeDest.Text;
                enderecoDTO.Block = txtBairroDest.Text;
                enderecoDTO.State = ((ddlEstadoDest.SelectedValue != "0") ? ddlEstadoDest.SelectedValue : "");
                enderecoDTO.County = ((ddlMunicipioDest.SelectedValue != "0") ? ddlMunicipioDest.SelectedValue : "");
                enderecoDTO.Country = "BR"; //ddlPaisDest.SelectedValue;
                enderecoDTO.AdresType = 'S';

                listEnderecoDTO.Add(enderecoDTO);
                parceiroNegocioDTO.ListEndereco = listEnderecoDTO;
                // Fim da Seção de Endereço.

                // Seção de Contato.
                ContatoDTO contatoDTO = new ContatoDTO();
                contatoDTO.Name = ((!string.IsNullOrEmpty(txtIdContato.Text)) ? txtIdContato.Text : "tNO");
                contatoDTO.E_MailL = ((!string.IsNullOrEmpty(txtEmailContato.Text)) ? txtEmailContato.Text : "tNO");
                contatoDTO.Tel1 = ((!string.IsNullOrEmpty(txtDddContato.Text) && !string.IsNullOrEmpty(txtTelefoneContato.Text)) ? txtDddContato.Text + txtTelefoneContato.Text : "tNO");
                contatoDTO.Notes1 = ((!string.IsNullOrEmpty(txtObservacoesContato.Text)) ? txtObservacoesContato.Text : "tNO");

                IList<ContatoDTO> listContatoDTO = new List<ContatoDTO>();
                listContatoDTO.Add(contatoDTO);
                parceiroNegocioDTO.ListContato = listContatoDTO;
                // Fim da Seção de Contato.

                // Seção de Contabilidade.
                IdentificacaoFiscalDTO identificacaoFiscalDTO = new IdentificacaoFiscalDTO();
                identificacaoFiscalDTO.TaxId0 = ((!string.IsNullOrEmpty(txtCpfCnpj.Text)) ? txtCpfCnpj.Text : "tNO");
                identificacaoFiscalDTO.TaxId1 = ((!string.IsNullOrEmpty(txtIe.Text)) ? txtIe.Text : "tNO");
                identificacaoFiscalDTO.TaxId2 = ((!string.IsNullOrEmpty(txtIm.Text)) ? txtIm.Text : "tNO");

                IList<IdentificacaoFiscalDTO> listIdentificacaoDTO = new List<IdentificacaoFiscalDTO>();
                listIdentificacaoDTO.Add(identificacaoFiscalDTO);
                parceiroNegocioDTO.ListIdentificacaoFiscal = listIdentificacaoDTO;

                parceiroNegocioDTO.U_CNPJ = ((!string.IsNullOrEmpty(txtCpfCnpj.Text)) ? txtCpfCnpj.Text : "tNO");

                // Fim da Seção de Contabilidade.

                if (cardCode.Trim() == string.Empty)
                {
                    if (parceiroNegocioBLL.Inserir(parceiroNegocioDTO))
                    {
                        //Mensagem("Ação realizada com sucesso!", MensagemType.Aviso);
                        Response.Redirect("ParceiroNegocio.aspx");
                    }
                    else
                    {
                        msg = ((!string.IsNullOrEmpty(parceiroNegocioBLL.ErrorMessege)) ? "<br />" + parceiroNegocioBLL.ErrorMessege : "");
                        Mensagem("Ação não realizada! Tente novamente ou entre em contato com o suporte." + msg, MensagemType.Erro);
                    }
                }
                else
                {
                    if (parceiroNegocioBLL.Editar(parceiroNegocioDTO))
                    {
                        //Mensagem("Ação realizada com sucesso!", MensagemType.Aviso);
                        Response.Redirect("ParceiroNegocio.aspx");
                    }
                    else
                    {
                        msg = ((!string.IsNullOrEmpty(parceiroNegocioBLL.ErrorMessege)) ? "<br />" + parceiroNegocioBLL.ErrorMessege : "");
                        Mensagem("Ação não realizada! Tente novamente ou entre em contato com o suporte." + msg, MensagemType.Erro);
                    }
                }
            }
            catch (Exception erro)
            {
                Mensagem(erro.Message, MensagemType.Erro);
            }
        }

        private void Mensagem(string mensagem, MensagemType mensagemType)
        {
            MensagemDTO mensagemDTO = new MensagemDTO();
            mensagemDTO.Tipo = mensagemType;
            mensagemDTO.Mensagem = mensagem;

            Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
        }

        private string GetCardType(string valor)
        {
            string s = string.Empty;

            switch (valor)
            {
                case "C":
                    s = "cCustomer";
                    break;
                case "L":
                    s = "cLid";
                    break;
                case "S":
                    s = "cSupplier";
                    break;
            }
            return s;
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTipo.SelectedValue)
            {
                case "C":
                    Combo.Grupo(ddlGrupo, "0", GroupType.Client);
                    break;
                case "L":
                    Combo.Grupo(ddlGrupo, "0", GroupType.Client);
                    break;
                case "S":
                    Combo.Grupo(ddlGrupo, "0", GroupType.Supplier);
                    break;
                default:
                    Combo.Grupo(ddlGrupo, "0", GroupType.None);
                    break;
            }

            //Combo.Vendedor(ddlVendedor, "0", new DTO.Funcionario.Vendedor.VendedorDTO());
            //Combo.Moeda(ddlMoeda, "0");
            //Combo.Estado(ddlEstado, "0", new DTO.Estado.EstadoDTO() { Pais = new DTO.Administracao.Configuracao.PaisDTO() { Code = "BR", Name = "BR" } });
            //Combo.Estado(ddlEstadoDest, "0", new DTO.Estado.EstadoDTO() { Pais = new DTO.Administracao.Configuracao.PaisDTO() { Code = "BR", Name = "BR" } });
            //Combo.CondicaoPagamento(cmbCondicaoPgto, new DTO.CondicaoPagamento.CondicaoPagamentoDTO() { });
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            Combo.Municipio(ddlMunicipio, "0", new DTO.Municipio.MunicipioDTO() { Estado = new DTO.Estado.EstadoDTO() { Code = ddlEstado.SelectedValue } });

            //ddlMunicipio.Focus();
        }

        protected void ddlEstadoDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Combo.Municipio(ddlMunicipioDest, "0", new DTO.Municipio.MunicipioDTO() { Estado = new DTO.Estado.EstadoDTO() { Code = ddlEstadoDest.SelectedValue } });

            ddlMunicipioDest.Focus();
        }

        protected void Isento(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox checkBox = (CheckBox)sender;

                if (checkBox.ID == "check0")
                {
                    txtInscricaoEstadualEndereco.Text = ((checkBox.Checked) ? "Isento" : "");
                    txtInscricaoEstadualEndereco.ReadOnly = ((checkBox.Checked ? true : false));

                    //check0.Focus();
                }

                if (checkBox.ID == "check1")
                {
                    txtIncricaoEstadualEnderecoDest.Text = ((checkBox.Checked) ? "Isento" : "");
                    txtIncricaoEstadualEnderecoDest.ReadOnly = ((checkBox.Checked ? true : false));

                    //check1.Focus();
                }

                if (checkBox.ID == "check2")
                {
                    txtIe.Text = ((checkBox.Checked) ? "Isento" : "");
                    txtIe.ReadOnly = ((checkBox.Checked ? true : false));

                    //check2.Focus();
                }
            }
        }

        protected void txtCep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!txtCep.Text.Equals(""))
                {
                    CepBLL cepBLL = new CepBLL();

                    EnderecoDTO dadosEndereco = cepBLL.RetornarDadosEnderecoPorCep(txtCep.Text);

                    if (dadosEndereco != null)
                    {
                        txtTipoLogradouro.Text = dadosEndereco.AddrType;
                        txtRua.Text = dadosEndereco.Address;
                        ddlEstado.SelectedValue = dadosEndereco.State;

                        DTO.Municipio.MunicipioDTO munDTO = new DTO.Municipio.MunicipioDTO();
                        munDTO.Estado = new DTO.Estado.EstadoDTO();
                        munDTO.Estado.Code = dadosEndereco.State;

                        Combo.Municipio(ddlMunicipio, "0", munDTO);
                        ddlMunicipio.SelectedValue = dadosEndereco.CardCode;

                        txtBairro.Text = dadosEndereco.County;
                        txtCidade.Text = dadosEndereco.City;

                        //txtCep.Focus();
                    }
                }

                //txtCep.Focus();
            }
            catch
            {

            }
        }

        protected void txtCepDest_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!txtCepDest.Text.Equals(""))
                {
                    CepBLL cepBLL = new CepBLL();

                    EnderecoDTO dadosEndereco = cepBLL.RetornarDadosEnderecoPorCep(txtCepDest.Text);

                    if (dadosEndereco != null)
                    {
                        txtTipoLogradouroDest.Text = dadosEndereco.AddrType;
                        txtRuaDest.Text = dadosEndereco.Address;
                        ddlEstadoDest.SelectedValue = dadosEndereco.State;

                        DTO.Municipio.MunicipioDTO munDTO = new DTO.Municipio.MunicipioDTO();
                        munDTO.Estado = new DTO.Estado.EstadoDTO();
                        munDTO.Estado.Code = dadosEndereco.State;

                        Combo.Municipio(ddlMunicipioDest, "0", munDTO);


                        ddlMunicipioDest.SelectedValue = dadosEndereco.CardCode;

                        txtBairroDest.Text = dadosEndereco.County;
                        txtCidadeDest.Text = dadosEndereco.City;

                        //txtCepDest.Focus();
                    }
                }

                //txtCepDest.Focus();
            }
            catch
            {

            }
        }

        protected void chbCopiaEnderecoEntrega_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCopiaEnderecoEntrega.Checked)
            {
                txtTipoLogradouroDest.Text = txtTipoLogradouro.Text;
                txtRuaDest.Text = txtRua.Text;
                txtNumeroRuaDest.Text = txtNumeroRua.Text;
                txtComplementoDest.Text = txtComplemento.Text;
                txtCepDest.Text = txtCep.Text;
                txtCidadeDest.Text = txtCidade.Text;
                txtBairroDest.Text = txtBairro.Text;
                ddlEstadoDest.SelectedValue = ddlEstado.SelectedValue;

                DTO.Municipio.MunicipioDTO munDTO = new DTO.Municipio.MunicipioDTO();
                munDTO.Estado = new DTO.Estado.EstadoDTO();
                munDTO.Estado.Code = ddlEstado.SelectedValue;

                Combo.Municipio(ddlMunicipioDest, "0", munDTO);
                ddlMunicipioDest.Text = ddlMunicipio.SelectedValue;
                ddlPaisDest.Text = ddlPais.SelectedValue;

                txtCodigoParticipanteDest.Text = txtCodigoParticipante.Text;
                txtIncricaoEstadualEnderecoDest.Text = txtInscricaoEstadualEndereco.Text;
                txtIe.Text = txtInscricaoEstadualEndereco.Text;

                txtCpfCnpjDest.Text = txtCpfCnpjEntrega.Text;
                txtCpfCnpj.Text = txtCpfCnpjEntrega.Text;

                txtIndicardorIeDest.Text = txtIndicadorIe.Text;
                txtIm.Text = txtIndicadorIe.Text;

                if(check0.Checked)
                {
                    check1.Checked = true;
                    txtIncricaoEstadualEnderecoDest.ReadOnly = true;
                }
            }
            else
            {
                txtTipoLogradouroDest.Text = "";
                txtRuaDest.Text = "";
                txtNumeroRuaDest.Text = "";
                txtComplementoDest.Text = "";
                txtCepDest.Text = "";
                txtCidadeDest.Text = "";
                txtBairroDest.Text = "";
                ddlEstadoDest.SelectedValue = "0";
                ddlMunicipioDest.Text = "0";
                ddlPaisDest.Text = "";
            }

            //chbCopiaEnderecoEntrega.Focus();
        }
    }
}