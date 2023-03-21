<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/SapB1Master.Master" 
    AutoEventWireup="true" 
    CodeBehind="ParceiroNegocio_Action.aspx.cs" 
    Inherits="SAPB1.WebForm.ParceiroNegocio_Action" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField 
        runat="server" 
        ID="hfCamposDirecionarPostBack" />
    <asp:HiddenField 
        runat="server" 
        ID="hfErros" />
    <asp:Panel 
        runat="server" 
        ID="pnlAviso" 
        CssClass="alert alert-info alert-dismissible"
        Visible="false">
            <button 
                type="button" 
                class="close" 
                data-dismiss="alert" 
                aria-hidden="true">×
            </button>
            <h4>
                <i class="icon fa fa-info"></i> 
                Alerta
            </h4>
            <asp:Label 
                runat="server" 
                ID="lblAvisos">
            </asp:Label>
    </asp:Panel>
    <asp:ScriptManager runat="server" ID="smr"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="upPn">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        Geral
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block;">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                               <label for="txtRazaoSocial">Tipo</label>
                                <asp:DropDownList
                                    runat="server"
                                    ID="ddlTipo"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"
                                    AutoPostBack="true">
                                        <asp:ListItem Text="Selecione um Tipo" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Cliente" Value="C"></asp:ListItem>
                                        <asp:ListItem Text="Lead" Value="L"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Razão Social</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtRazaoSocial"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Nome Estrangeiro/Fantasia</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtNomeEstranFantasia"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Grupo</label>
                                <asp:DropDownList
                                    runat="server"
                                    ID="ddlGrupo"
                                    CssClass="form-control">
                                        <asp:ListItem Text="Selecione um Grupo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Moeda</label>
                                <asp:DropDownList
                                    runat="server"
                                    ID="ddlMoeda"
                                    CssClass="form-control">
                                        <asp:ListItem Text="Selecione uma Moeda" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Fax</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtFax"
                                   CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">WebSite</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtWebSite"
                                   CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">E-mail</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtEmail"
                                   CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">DDD</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtDdd"
                                           CssClass="form-control"
                                           MaxLength="2">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-10">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Telefone</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtTelefone"
                                           CssClass="form-control"
                                           MaxLength="8">
                                        </asp:TextBox>
                                     </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">DDD</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtDddCelular"
                                           CssClass="form-control"
                                           MaxLength="2">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-10">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Celular</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtCelular"
                                           CssClass="form-control"
                                           MaxLength="9">
                                        </asp:TextBox>
                                     </div>
                                </div>
                            </div>
                            <div id="divSetorIndus" class="form-group">
                              <label for="txtRazaoSocial">Setor Industrial</label>
                               <asp:DropDownList
                                   runat="server"
                                   ID="cmbSetorIndustrial"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um Setor Industrial" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                              <label for="txtRazaoSocial">Vendedor/Representante</label>
                               <asp:DropDownList
                                   runat="server"
                                   ID="ddlVendedor"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um Vendedor" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <asp:Panel runat="server" ID="pnlRepresentante" CssClass="form-group" Visible="false">
                               <label for="txtRazaoSocial">Representante</label>
                               <asp:DropDownList
                                   runat="server"
                                   ID="ddlRepresentante"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um Representante" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </asp:Panel>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Status</label>
                               <asp:DropDownList
                                   runat="server"
                                   ID="ddlAtivoPn"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Ativo" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="Inativo" Value="2"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Observações</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtObservacoes"
                                   CssClass="form-control"
                                   Rows="3"
                                   TextMode="MultiLine">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
               </div>
            </div>
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        Contato
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block;">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                               <label for="txtCpfCnpj">ID de Contato</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtIdContato"
                                   CssClass="form-control">
                                </asp:TextBox>
                             </div>
                            <div class="form-group">
                               <label for="txtCpfCnpj">E-mail</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtEmailContato"
                                   CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">DDD</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtDddContato"
                                           CssClass="form-control"
                                           MaxLength="2">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-10">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Telefone</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtTelefoneContato"
                                           CssClass="form-control"
                                           MaxLength="9">
                                        </asp:TextBox>
                                     </div>
                                </div>
                            </div>
                            <div class="form-group">
                               <label for="txtRazaoSocial">Observações</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtObservacoesContato"
                                   CssClass="form-control"
                                   Rows="3"
                                   TextMode="MultiLine">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Endereço de Cobrança.-->
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        Endereço de Cobrança
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block;">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtRazaoSocial">ID do Endereço de Cobrança</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtIdContatoEndereco"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Tipo de Logradouro</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtTipoLogradouro"
                                           CssClass="form-control">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Rua</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtRua"
                                           CssClass="form-control">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Rua Nº</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtNumeroRua"
                                           CssClass="form-control">
                                        </asp:TextBox>
                                     </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Complemento</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtComplemento"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">CEP</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCep"
                                    CssClass="form-control"
                                    OnTextChanged="txtCep_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Cidade</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCidade"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Bairro</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtBairro"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Estado</label>
                                <asp:DropDownList
                                   runat="server"
                                   ID="ddlEstado"
                                   CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                       <asp:ListItem Text="Selecione um Estado" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Município</label>
                                <asp:DropDownList
                                   runat="server"
                                   ID="ddlMunicipio"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um Município" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">País</label>
                                <asp:DropDownList
                                   runat="server"
                                   ID="ddlPais"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um País" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                             <asp:Panel runat="server" ID="pnlCodigoParticipante" CssClass="form-group" Visible="false">
                                <label for="txtRazaoSocial">Código do Participante</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCodigoParticipante"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </asp:Panel>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Inscrição Estadual</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtInscricaoEstadualEndereco"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:CheckBox ID="check0" runat="server" TextAlign="Right" Text="Isento" AutoPostBack="true" OnCheckedChanged="Isento" />
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">CPF/CNPJ - Entrega</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCpfCnpjEntrega"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <asp:Panel runat="server" ID="pnlIndicadorIe" CssClass="form-group" Visible="false">
                                <label for="txtRazaoSocial">Indicador de IE</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtIndicadorIe"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <!--Destinatário.-->
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        Endereço de Destinatário
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block;">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:CheckBox 
                                runat="server" 
                                ID="chbCopiaEnderecoEntrega" 
                                Text="Copia Endereço Cobrança"
                                OnCheckedChanged="chbCopiaEnderecoEntrega_CheckedChanged"
                                AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtRazaoSocial">ID do Endereço de Destintário</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtIdContatoEnderecoDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Tipo de Logradouro</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtTipoLogradouroDest"
                                           CssClass="form-control">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Rua</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtRuaDest"
                                           CssClass="form-control">
                                        </asp:TextBox>
                                     </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                       <label for="txtRazaoSocial">Rua Nº</label>
                                       <asp:TextBox
                                           runat="server"
                                           ID="txtNumeroRuaDest"
                                           CssClass="form-control">
                                        </asp:TextBox>
                                     </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Complemento</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtComplementoDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">CEP</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCepDest"
                                    CssClass="form-control"
                                    OnTextChanged="txtCepDest_TextChanged"
                                    AutoPostBack="true">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Cidade</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCidadeDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Bairro</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtBairroDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Estado</label>
                                <asp:DropDownList
                                   runat="server"
                                   ID="ddlEstadoDest"
                                   CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlEstadoDest_SelectedIndexChanged">
                                       <asp:ListItem Text="Selecione um Estado" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Município</label>
                                <asp:DropDownList
                                   runat="server"
                                   ID="ddlMunicipioDest"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um Município" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">País</label>
                                <asp:DropDownList
                                   runat="server"
                                   ID="ddlPaisDest"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione um País" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                             <asp:Panel runat="server" ID="pnlCodigoParticipanteEntr" CssClass="form-group" Visible="false">
                                <label for="txtRazaoSocial">Código do Participante</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCodigoParticipanteDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </asp:Panel>
                            <div class="form-group">
                                <label for="txtRazaoSocial">Inscrição Estadual</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtIncricaoEstadualEnderecoDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                                <asp:CheckBox ID="check1" runat="server" TextAlign="Right" Text="Isento" AutoPostBack="true" OnCheckedChanged="Isento" />
                            </div>
                            <div class="form-group">
                                <label for="txtRazaoSocial">CPF/CNPJ - Entrega</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCpfCnpjDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                            <asp:Panel runat="server" ID="pnlIndiIeen" class="form-group" Visible="false">
                                <label for="txtRazaoSocial">Indicador de IE</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtIndicardorIeDest"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-warning">
                <div class="box-header with-border">
                    <h3 class="box-title">
                       Condições de Pagamento
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block;">
                    <div class="row">
                        <div class="col-md-12">
                             <div class="form-group">
                              <label for="txtRazaoSocial">Condições de Pagamento</label>
                               <asp:DropDownList
                                   runat="server"
                                   ID="cmbCondicaoPgto"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione uma Condição de Pagamento" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Cliente" Value="CP1"></asp:ListItem>
                                       <asp:ListItem Text="Fornecedor" Value="CP2"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <div class="form-group">
                              <label for="txtRazaoSocial">Limite de Crédito</label>
                              <asp:TextBox
                                   runat="server"
                                   ID="txtLimiteCredito"
                                   CssClass="form-control"
                                   MaxLength="50">
                               </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">
                       Execução de Pagamento
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block;">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                              <label for="txtRazaoSocial">Forma de Pagamento</label>
                               <asp:DropDownList
                                   runat="server"
                                   ID="ddlFormaPagamento"
                                   CssClass="form-control">
                                       <asp:ListItem Text="Selecione uma Forma de Pagamento" Value="0"></asp:ListItem>
                               </asp:DropDownList>
                            </div>
                            <asp:Panel runat="server" ID="pnlPagamentoUnico" CssClass="form-group" Visible="false">
                              <label for="txtRazaoSocial">Pagamento Único</label>
                              <asp:CheckBox ID="checkPagamentoUnico" runat="server" Checked="true" />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-default">
               <div class="box-header with-border">
                   <h3 class="box-title">
                        Contabilidade
                   </h3>
                   <div class="box-tools pull-right">
                       <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                   </div>
               </div>
               <div class="box-body" style="display: block;">
                   <div class="row">
                       <div class="col-md-12">
                           <div class="form-group">
                               <label for="txtCpfCnpj">ID Fiscal CNPJ</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtCpfCnpj"
                                   CssClass="form-control">
                               </asp:TextBox>
                           </div>
                           <div class="form-group">
                               <label for="txtCpfCnpj">ID Fiscal IE</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtIe"
                                   CssClass="form-control">
                               </asp:TextBox>
                               <asp:CheckBox ID="check2" runat="server" TextAlign="Right" Text="Isento" AutoPostBack="true" OnCheckedChanged="Isento" />
                           </div>
                           <asp:Panel runat="server" ID="pnlIdIm" CssClass="form-group" Visible="false">
                               <label for="txtCpfCnpj">ID Fiscal IM</label>
                               <asp:TextBox
                                   runat="server"
                                   ID="txtIm"
                                   CssClass="form-control">
                               </asp:TextBox>
                           </asp:Panel>
                      </div>
                   </div>
               </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-12" style="text-align:center;">
            <asp:Button
                runat="server"
                ID="btnSalvar"
                Text="Salvar" 
                class="btn btn-warning"
                OnClientClick="ValidarCampos();"
                OnClick="btnSalvar_Click"
                Visible="false"/>
        </div>
    </div>
    
    <script type="text/javascript">
        $("#<%=txtCpfCnpjEntrega.ClientID%>").blur(function () {

            alert($("#<%=txtCpfCnpjEntrega.ClientID%>").val());

            $("#<%=txtCpfCnpj.ClientID%>").val($("#<%=txtCpfCnpjEntrega.ClientID%>").val());
            $("#<%=txtCpfCnpjDest.ClientID%>").val($("#<%=txtCpfCnpjEntrega.ClientID%>").val());
        });

        $("#<%=txtInscricaoEstadualEndereco.ClientID%>").blur(function () {
            $("#<%=txtIe.ClientID%>").val($("#<%=txtInscricaoEstadualEndereco.ClientID%>").val());
            $("#<%=txtIncricaoEstadualEnderecoDest.ClientID%>").val($("#<%=txtInscricaoEstadualEndereco.ClientID%>").val());
        });

        $("#<%=txtIndicadorIe.ClientID%>").blur(function () {
            $("#<%=txtIm.ClientID%>").val($("#<%=txtIndicadorIe.ClientID%>").val());
            $("#<%=txtIndicardorIeDest.ClientID%>").val($("#<%=txtIndicadorIe.ClientID%>").val());
        });

        $("#<%=txtLimiteCredito.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .(190)
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtDdd.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtTelefone.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtDddCelular.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtCelular.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        function somenteNumeroDecimal(objTextBox, e) {
            var sep = 0;
            var key = '';
            var i = j = 0;
            var len = len2 = 0;
            var strCheck = '0123456789';
            var aux = aux2 = '';
            if (e.which) {
                var whichCode = e.which;
            } else {
                var whichCode = e.keyCode;
            }
            if ((whichCode == 13) || (whichCode == 0) || (whichCode == 8)) return true;
            key = String.fromCharCode(whichCode); // Valor para o código da Chave
            if (strCheck.indexOf(key) == -1) return false; // Chave inválida
            len = objTextBox.value.length;
            for (i = 0; i < len; i++)
                if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != ",")) break;
            aux = '';
            for (; i < len; i++)
                if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
            aux += key;
            len = aux.length;
            if (len == 0) objTextBox.value = '';
            if (len == 1) objTextBox.value = '0' + "," + '0' + aux;
            if (len == 2) objTextBox.value = '0' + "," + aux;
            if (len > 2 && len < 13) {
                aux2 = '';
                for (j = 0, i = len - 3; i >= 0; i--) {
                    if (j == 3) {
                        aux2 += ".";
                        j = 0;
                    }
                    aux2 += aux.charAt(i);
                    j++;
                }
                objTextBox.value = '';
                len2 = aux2.length;
                for (i = len2 - 1; i >= 0; i--)
                    objTextBox.value += aux2.charAt(i);
                objTextBox.value += "," + aux.substr(len - 2, len);
            }
            return false;
        }

        function ValidarCampos() {
            //Variável aonde irá guardar os erros
            var erros = "";

            //Tipo de parceiro
            var tipoParceiro = document.getElementById('<%= ddlTipo.ClientID %>').value;

            
            var razao = document.getElementById('<%= txtRazaoSocial.ClientID %>').value;
            var grupo = document.getElementById('<%= ddlGrupo.ClientID %>').value;
            var rua = document.getElementById('<%= txtRua.ClientID %>').value;
            var telefone = document.getElementById('<%= txtTelefone.ClientID %>').value;
            var ddd = document.getElementById('<%= txtDdd.ClientID %>').value;
            var moeda = document.getElementById('<%= ddlMoeda.ClientID %>').value;
            var fax = document.getElementById('<%= txtFax.ClientID %>').value;
            var email = document.getElementById('<%= txtEmail.ClientID %>').value;
            var dddCel = document.getElementById('<%= txtDddCelular.ClientID %>').value;
            var celular = document.getElementById('<%= txtCelular.ClientID %>').value;
            var tipo = document.getElementById('<%= ddlTipo.ClientID %>').value;
            var site = document.getElementById('<%= txtWebSite.ClientID %>').value;
            var condPgto = document.getElementById('<%= cmbCondicaoPgto.ClientID %>').value;
            var tipoLog = document.getElementById('<%= txtTipoLogradouro.ClientID %>').value;
            var numeroRua = document.getElementById('<%= txtNumeroRua.ClientID %>').value;
            var cep = document.getElementById('<%= txtCep.ClientID %>').value;
            var cidade = document.getElementById('<%= txtCidade.ClientID %>').value;
            var bairro = document.getElementById('<%= txtBairro.ClientID %>').value;
            var estado = document.getElementById('<%= ddlEstado.ClientID %>').value;
            var municipio = document.getElementById('<%= ddlMunicipio.ClientID %>').value;
            var vendedor = document.getElementById('<%= ddlVendedor.ClientID %>').value;

            var tipoLogDest = document.getElementById('<%= txtTipoLogradouroDest.ClientID %>').value;
            var ruaDest = document.getElementById('<%= txtRuaDest.ClientID %>').value;
            var numeroDest = document.getElementById('<%= txtNumeroRuaDest.ClientID %>').value;
            var cepDest = document.getElementById('<%= txtCepDest.ClientID %>').value;
            var cidadeDest = document.getElementById('<%= txtCidadeDest.ClientID %>').value;
            var bairroDest = document.getElementById('<%= txtBairroDest.ClientID %>').value;
            var estadoDest = document.getElementById('<%= ddlEstadoDest.ClientID %>').value;
            var municipioDest = document.getElementById('<%= ddlMunicipioDest.ClientID %>').value;
            var idContatoDest = document.getElementById('<%= txtIdContato.ClientID %>').value;
            var emailContato = document.getElementById('<%= txtEmailContato.ClientID %>').value;
            var telefoneContato = document.getElementById('<%= txtTelefoneContato.ClientID %>').value;
            var dddContato = document.getElementById('<%= txtDddContato.ClientID %>').value;

            var cnpj = document.getElementById('<%= txtCpfCnpj.ClientID %>').value;
            var ie = document.getElementById('<%= txtIe.ClientID %>').value;
            var im = document.getElementById('<%= txtIm.ClientID %>').value;

            if (tipoParceiro == "C") {
                if (razao == "") {
                    erros += "Razão Social é um campo obrigatório.-";
                }
                if (grupo == "0") {
                    erros += "Grupo é um campo obrigatório.-";
                }
                if (rua == "") {
                    erros += "Rua é um campo obrigatório.-";
                }
                if (telefone == "") {
                    erros += "Telefone é um campo obrigatório.-";
                }
                if (ddd == "") {
                    erros += "DDD é um campo obrigatório.-";
                }
                if (moeda == "") {
                    erros += "Moeda é um campo obrigatório.-";
                }
                if (fax == "") {
                    erros += "Fax é um campo obrigatório.-";
                }
                if (email == "") {
                    erros += "E-mail é um campo obrigatório.-";
                }
                if (dddCel == "") {
                    erros += "DDD Celular é um campo obrigatório.-";
                }
                if (celular == "") {
                    erros += "Celular é um campo obrigatório.-";
                }
                if (tipo == "0") {
                    erros += "Tipo é um campo obrigatório.-";
                }
                if (site == "") {
                    erros += "Web Site é um campo obrigatório.-";
                }
                if (condPgto == "0") {
                    erros += "Condição de Pagamento é um campo obrigatório.-";
                }
                if (tipoLog == "") {
                    erros += "Tipo Logradouro é um campo obrigatório.-";
                }
                if (numeroRua == "") {
                    erros += "Número da rua é um campo obrigatório.-";
                }
                if (cep == "") {
                    erros += "Cep é um campo obrigatório.-";
                }
                if (cidade == "") {
                    erros += "Cidade é um campo obrigatório.-";
                }
                if (bairro == "") {
                    erros += "Bairro é um campo obrigatório.-";
                }
                if (estado == "0") {
                    erros += "Estado é um campo obrigatório.-";
                }
                if (municipio == "") {
                    erros += "Município é um campo obrigatório.-";
                }

                if (tipoLogDest == "") {
                    erros += "Tipo Logradouro de destino é um campo obrigatório.-";
                }
                if (ruaDest == "") {
                    erros += "Rua de destino é um campo obrigatório.-";
                }
                if (numeroDest == "") {
                    erros += "Número de destino é um campo obrigatório.-";
                }
                if (cepDest == "") {
                    erros += "Cep de destino é um campo obrigatório.-";
                }
                if (cidadeDest == "") {
                    erros += "Cidade de destino é um campo obrigatório.-";
                }
                if (bairroDest == "") {
                    erros += "Bairro de destino é um campo obrigatório.-";
                }
                if (estadoDest == "") {
                    erros += "Estado de destino é um campo obrigatório.-";
                }
                if (municipioDest == "") {
                    erros += "Município de destino é um campo obrigatório.-";
                }
                if (idContatoDest == "") {
                    erros += "Id do contato é um campo obrigatório.-";
                }
                if (emailContato == "") {
                    erros += "E-mail do contato é um campo obrigatório.-";
                }
                if (telefoneContato == "") {
                    erros += "Telefone do contato é um campo obrigatório.-";
                }
                if (dddContato == "") {
                    erros += "DDD do contato é um campo obrigatório.-";
                }

                if (cnpj == "") {
                    erros += "CNPJ é um campo obrigatório. Parte da contabilidade.-";
                }
                if (ie == "") {
                    erros += "Inscrição estadual é um campo obrigatório. Parte da contabilidade.-";
                }
                if (im == "") {
                    erros += "Inscrição Municipal é um campo obrigatório. Parte da contabilidade.-";
                }
            }
            else if (tipoParceiro == "L") {
                if (razao == "") {
                    erros += "Razão Social é um campo obrigatório.-";
                }

                if (tipo == "0") {
                    erros += "Tipo é um campo obrigatório.-";
                }

                if (fax == "") {
                    erros += "Fax é um campo obrigatório.-";
                }

                if (site == "") {
                    erros += "Web Site é um campo obrigatório.-";
                }

                if (email == "") {
                    erros += "E-mail é um campo obrigatório.-";
                }

                if (emailContato == "") {
                    erros += "E-mail do contato é um campo obrigatório.-";
                }

                if (telefone == "") {
                    erros += "Telefone é um campo obrigatório.-";
                }
                if (ddd == "") {
                    erros += "DDD é um campo obrigatório.-";
                }
                if (idContatoDest == "") {
                    erros += "Id do contato é um campo obrigatório.-";
                }
                if (emailContato == "") {
                    erros += "E-mail do contato é um campo obrigatório.-";
                }
                if (telefoneContato == "") {
                    erros += "Telefone do contato é um campo obrigatório.-";
                }
                if (dddContato == "") {
                    erros += "DDD do contato é um campo obrigatório.-";
                }
                if (vendedor == "0") {
                    erros += "Vendedor é um campo obrigatório.-";
                }
            }
           
            document.getElementById('<%= hfErros.ClientID %>').value = erros;

        }
    </script>
    <script type="text/javascript">
        $("#divSetorIndus").hide();
    </script>
</asp:Content>
