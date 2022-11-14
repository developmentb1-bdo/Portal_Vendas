<%@ Page Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="SolicitacaoGarantia_Action.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.SolicitacaoGarantia_Action" 
%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="multiView" runat="server">
        <asp:View ID="view" runat="server">
            <asp:HiddenField
                runat="server"
                ID="hfIdConcessionario" />
            <asp:HiddenField
                runat="server"
                ID="hfListaCodigosItem" />
            <asp:HiddenField
                runat="server"
                ID="hfListaCodigoNome" />
            <asp:HiddenField
                runat="server"
                ID="hfCodChamado" />
            <asp:HiddenField 
                runat="server"
                ID="hfListaPrecoGarantia" />
            <asp:HiddenField
                runat="server"
                ID="hfPrecoItem" />
            <asp:HiddenField
                runat="server"
                ID="hfTotalItem" />
            <asp:HiddenField
                runat="server"
                ID="hfNomeItem" />

            <asp:HiddenField
                runat="server"
                ID="hfCodTpr" />
            <asp:HiddenField
                runat="server"
                ID="hfCodAnexo" />
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
                        Aviso
                    </h4>
                    <asp:Label 
                        runat="server" 
                        ID="lblAvisos">
                    </asp:Label>
            </asp:Panel>
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Solicitação de Garantia
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Chassis:</label>
                                <asp:TextBox 
                                    ID="txtChassis" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Data da Venda:</label>
                                <asp:TextBox 
                                    ID="txtDataVenda" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Modelo:</label>
                                <asp:TextBox 
                                    ID="txtModeloChassi" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>KM Atual:</label>
                                <asp:TextBox 
                                    ID="txtKmAtual" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    onkeypress="return isNumberKey(event)">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Nome do Cliente:</label>
                                <asp:TextBox 
                                    ID="txtNomeCliente" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Data da Falha:</label>
                                <asp:TextBox 
                                    ID="txtDataAbeturaFalha" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Placa:</label>
                                <asp:TextBox 
                                    ID="txtPlaca" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    MaxLength="8">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>KM Falha:</label>
                                <asp:TextBox 
                                    ID="txtKmFalha" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    onkeypress="return isNumberKey(event)">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Número do Motor:</label>
                                <asp:TextBox 
                                    ID="txtNumeroMotor" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Modelo Motor:</label>
                                <asp:TextBox 
                                    ID="txtModeloMotor" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Tipo de Garantia:</label>
                                <asp:DropDownList 
                                    ID="ddlTipoGarantia" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlTipoGarantia_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Opções:</label>
                                <asp:DropDownList 
                                    ID="ddlOpcaoTipoGarantia" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlOpcaoTipoGarantia_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Data do Envio:</label>
                                <asp:TextBox 
                                    ID="txtDataEnvio" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Ordem de Seriço do Serviço da Concessionária:</label>
                                <asp:TextBox 
                                    ID="txtOrdemServico" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Concessionária:</label>
                                <asp:TextBox 
                                    ID="txtConcessinario" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>CNPJ:</label>
                                <asp:TextBox 
                                    ID="txtCnpj" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Nome do Responsável:</label>
                                <asp:TextBox 
                                    ID="txtNomeResponsavel" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Função:</label>
                                <asp:TextBox 
                                    ID="txtFuncao" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Descrição da Falha:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtDescricaoFalha"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    style="resize:none;">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Causa da Falha:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCausaFalha"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    style="resize:none;">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Correção da Falha:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCorrecaoFalha"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    style="resize:none;">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Observações Gerais:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtObservacoesGerais"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    style="resize:none;">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Seção do cabeçalho.-->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Dados
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li class="active"><a href="#tab_2" data-toggle="tab">Relação de Peças Substituídas</a></li>
                                    <li><a href="#tab_3" data-toggle="tab">Relação de Serviços (TPR)</a></li>
                                    <li><a href="#tab_4" data-toggle="tab">Anexos</a></li>
                                </ul>
                                <div class="tab-content">
                                    <!--Relação de Peças Substituídas-->
                                    <div class="tab-pane active" id="tab_2">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Código da Peça:</label>
                                                    <asp:TextBox 
                                                        ID="txtCodigoPeca" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    <label>Nome da Peça:</label>
                                                    <asp:TextBox 
                                                        ID="txtItem" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Quantidade:</label>
                                                    <asp:TextBox 
                                                        ID="txtQtd" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control"
                                                        onkeypress="return isNumberKey(event)"
                                                        onkeydown="Formata(this,10,event,2)">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Valor Unitário:</label>
                                                    <asp:TextBox 
                                                        ID="txtValorUnitario" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Total:</label>
                                                    <asp:TextBox
                                                        runat="server"
                                                        ID="txtTotal"
                                                        ClientIDMode="Static"
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="form-group">
                                                    <asp:Button
                                                        runat="server"
                                                        ID="btnIncluiritem"
                                                        CssClass="btn btn-primary"
                                                        Text="Incluir"
                                                        OnClick="btnIncluiritem_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView
                                                    runat="server"
                                                    ID="grdItens"
                                                    AutoGenerateColumns="false"
                                                    CssClass="table table-bordered table-striped dataTable">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Linha">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblLinhaGrid"
                                                                        Text='<%# Eval("U_LineNum").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Código">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblCodigoGrid"
                                                                        Text='<%# Eval("U_ItemAlt").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblCodigoGrid"
                                                                        Text='<%# Eval("U_dscription").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantidade">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblCodigoGrid"
                                                                        Text='<%# Eval("U_Quantity").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Preço">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblPrecoGrid"
                                                                        Text='<%# Eval("U_Price").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblPrecoGrid"
                                                                        Text='<%# Convert.ToDecimal(Eval("Total").ToString()).ToString("c") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Relação de Relação de Serviços (TPR)-->
                                    <div class="tab-pane" id="tab_3">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Código TPR:</label>
                                                    <asp:TextBox 
                                                        ID="txtCodigoTpr" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-9">
                                                <div class="form-group">
                                                    <label>Descrição TPR:</label>
                                                    <asp:TextBox 
                                                        ID="txtDescricaoTpr" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Quantidade:</label>
                                                    <asp:TextBox 
                                                        ID="txtQtdTpr" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Valor:</label>
                                                    <asp:TextBox 
                                                        ID="txtValorTpr" 
                                                        runat="server" 
                                                        ClientIDMode="Static" 
                                                        CssClass="form-control">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="form-group">
                                                    <asp:Button
                                                        runat="server"
                                                        ID="btnInserirTpr"
                                                        CssClass="btn btn-primary"
                                                        Text="Inserir"
                                                        OnClick="btnInserirTpr_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView
                                                    runat="server"
                                                    ID="gdvTpr"
                                                    AutoGenerateColumns="false"
                                                    CssClass="table table-bordered table-striped dataTable">
                                                        <Columns>
                                                            <asp:BoundField DataField="U_CodTpr" HeaderText="Código TPR" />
                                                            <asp:BoundField DataField="U_ItmMan" HeaderText="Descrição" />
                                                            <asp:TemplateField HeaderText="Quantidade">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblQtdGrid"
                                                                        Text='<%# Eval("U_Qtd").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Valor Total">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblTotalGrid"
                                                                        Text='<%# Convert.ToDecimal(Eval("U_Total").ToString()).ToString("c") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                     <!--Relação de Anexos-->
                                    <div class="tab-pane" id="tab_4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Tipos de Anexo:</label>
                                                        <asp:DropDownList
                                                            runat="server"
                                                            ID="ddlTipoAnexo"
                                                            CssClass="form-control">
                                                                <asp:ListItem Text="Foto do Manual do Caminhão" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Foto do Painel com KM" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Foto da Plaqueta da Cabine/Chassi" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Foto da Peça Defeituosa" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="Foto do Checklist de Entrega" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="Foto Geral do Caminhão" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="Nota Fiscal de Venda" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="Reembolso Nota Fiscal da Peça" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="Reembolso Nota Fiscal de Serviço" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="Outros" Value="10"></asp:ListItem>
                                                        </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Anexo:</label>
                                                    <asp:FileUpload
                                                        runat="server"
                                                        ID="fuFotosFalha"
                                                        CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group" style="text-align:center;">
                                                    <asp:Button
                                                        runat="server"
                                                        ID="btnInserirAnexo"
                                                        CssClass="btn btn-primary"
                                                        Text="Inserir"
                                                        OnClick="btnInserirAnexo_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView
                                                    runat="server"
                                                    ID="gdvChamadoAnexo"
                                                    AutoGenerateColumns="false"
                                                    CssClass="table table-bordered table-striped dataTable">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Linha">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblLinhaGrid"
                                                                        Text='<%# Eval("Line").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nome do Arquivo">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblNomeArquivoGrid"
                                                                        Text='<%# Eval("NomeArquivo").ToString() %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Data de Criação">
                                                                <ItemTemplate>
                                                                    <asp:Label
                                                                        runat="server"
                                                                        ID="lblNomeArquivoGrid"
                                                                        Text='<%# Convert.ToDateTime(Eval("Date").ToString()).ToString("dd/MM/yyyy") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Visualizar o Anexo">
                                                                <ItemTemplate>
                                                                    <a href="ArquivosExportacao/<%# Eval("NomeArquivo").ToString() + "."+Eval("Extensao").ToString() %>" target="_blank">
                                                                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                             </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align:center;">
                            <div class="form-group">
                                <asp:Button
                                    runat="server"
                                    ID="btnSalvar"
                                    Text="Salvar"
                                    CssClass="btn btn-primary"
                                    OnClick="btnSalvar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <link href="Css/StyleAutoComplete.css" rel="stylesheet" />
            <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
            <script src="Javascript/Validations.js"></script>
            <script src="plugins/jQueryUI/jquery-ui.min.js"></script>
            <script src="plugins/datepicker/bootstrap-datepicker.js"></script>
            <link href="plugins/datepicker/datepicker3.css" rel="stylesheet" />

            <script type="text/javascript">
                $(function () {
                    var dadosItens = $("#<%= hfListaCodigosItem.ClientID %>").val();

                    var dados = dadosItens.split(',');

                    $("#<%= txtCodigoPeca.ClientID %>").autocomplete({
                        source: dados,
                        select: function (event, ui) {
                            event.preventDefault();

                            var codigo = ui.item.value.toString();

                            $("#<%= txtCodigoPeca.ClientID %>").val(codigo);

                            var listaPreco = $("#<%= hfListaPrecoGarantia.ClientID %>").val();

                            $.ajax({
                               url: "SolicitacaoGarantia_Action.aspx/RetornarDadosItemPorCodigo", //URL da página com o WebMethod 
                                data: "{codigo:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                                type: "POST", //Tipo do envio (POST ou GET)
                                dataType: "json", //Tipo retorno dos dados
                                contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                                //Função de sucesso do retorno dos dados feita pelo ajax
                                success: function (retorno) {
                                    console.log(retorno.d);

                                    if (retorno.d.length > 0) {

                                        var valorDropTipoGarantia = $("#<%= ddlOpcaoTipoGarantia.ClientID %>").find('option:selected').val();
                                        var valorGarantia = $("#<%= ddlTipoGarantia.ClientID %>").find('option:selected').val();

                                        if (valorGarantia != "4" && valorDropTipoGarantia != "14" && valorDropTipoGarantia != "15" && valorDropTipoGarantia != "16") {
                                            var preco = parseFloat(retorno.d[0].Price).toFixed(2);

                                            $("#<%= txtValorUnitario.ClientID%>").val(preco.toString().replace(".", ","));
                                        }

                                        var nome = retorno.d[0].Item.ItemName;

                                        $("#<%= txtItem.ClientID %>").val(nome);

                                        CalcularValoresItem();

                                    }
                                },
                                //Função de erro do retorno dos dados feita pelo ajax
                                error: function (req, status, error) {
                                    alert(error);
                                }
                            })
                        }
                    });
                });

                $(function () {
                    var dadosTpr = $("#<%= hfCodTpr.ClientID %>").val();

                    var dados = dadosTpr.split(',');

                    $("#<%= txtCodigoTpr.ClientID %>").autocomplete({
                        source: dados,
                        select: function (event, ui) {
                            event.preventDefault();

                            var codigo = ui.item.value.toString();

                            $("#txtCodigoTpr").val(codigo);

                            $.ajax({
                                url: "SolicitacaoGarantia_Action.aspx/RetornarDadosTrpPorCodigo", //URL da página com o WebMethod 
                                data: "{codigo:'" + codigo + "'}", //Enviar os parâmetros
                                type: "POST", //Tipo do envio (POST ou GET)
                                dataType: "json", //Tipo retorno dos dados
                                contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                                //Função de sucesso do retorno dos dados feita pelo ajax
                                success: function (retorno) {
                                    $("#<%= txtDescricaoTpr.ClientID %>").val(retorno.d.U_ItmMan);

                                    var valorDropTipoGarantia = $("#<%= ddlOpcaoTipoGarantia.ClientID %>").find('option:selected').val();
                                    var valorGarantia = $("#<%= ddlTipoGarantia.ClientID %>").find('option:selected').val();

                                    if (valorGarantia != "4" && valorDropTipoGarantia != "14" && valorDropTipoGarantia != "15" && valorDropTipoGarantia != "16") {
                                        var qtdValorHora = retorno.d.U_AumLev;

                                        $("#<%= txtQtdTpr.ClientID %>").val(qtdValorHora);

                                        var total = parseFloat(qtdValorHora.replace(',', '.')) * 100;

                                        $("#<%= txtValorTpr.ClientID %>").val(total.toString());
                                    }
                                },
                                //Função de erro do retorno dos dados feita pelo ajax
                                error: function (req, status, error) {
                                   
                                }
                            });
                        }
                    });
                });

                function CalcularValoresItem() {
                    var qtdDigitado = $("#txtQtd").val();
                    var precoUnitarioDigitado = $("#txtValorUnitario").val();

                    var qtd = 0;
                    var precoUnitario = 0;
                    var total = 0;

                    if (qtdDigitado != "") {
                        qtd = parseFloat(qtdDigitado.replace(',', '.'));
                    }

                    if (precoUnitarioDigitado != "") {
                        precoUnitario = parseFloat(precoUnitarioDigitado.replace(',', '.'));
                    }

                    total = precoUnitario * qtd;

                    $("#txtTotal").val(total.toString());
                }
                
                $("#txtQtd").blur(function () {
                    CalcularValoresItem();
                });

                $("#txtChassis").blur(function () {
                    var valorChassi = $("#txtChassis").val();

                    $.ajax({
                        url: "SolicitacaoGarantia_Action.aspx/RetornarDadosPeloChassi", //URL da página com o WebMethod 
                        data: "{chassi:'" + valorChassi + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            $("#txtNomeCliente").val(retorno.d.U_Cliente);
                            $("#txtNumeroMotor").val(retorno.d.U_Motor);
                            $("#txtModeloChassi").val(retorno.d.U_Modelo);
                            $("#txtDataVenda").val(retorno.d.U_DataVenda);
                            $("#txtModeloMotor").val(retorno.d.U_ModeloMotor);
                        },
                        //Função de erro do retorno dos dados feita pelo ajax
                        error: function (req, status, error) {
                            
                        }
                    });
                });

                $("#<%= ddlOpcaoTipoGarantia.ClientID %>").change(function () {
                    var texto = $("#<%= ddlOpcaoTipoGarantia.ClientID %>").find('option:selected').text();

                    var tipoGarantia = $("#<%= ddlTipoGarantia.ClientID %>").val();

                    if (tipoGarantia == "4" || tipoGarantia == "5") {
                        $("#<%= txtDescricaoFalha.ClientID %>").val(texto);
                        $("#<%= txtCausaFalha.ClientID %>").val(texto);
                        $("#<%= txtCorrecaoFalha.ClientID %>").val(texto);
                    }
                });

                (function ($) {
                    $.fn.datepicker.dates['pt-BR'] = {
                        days: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo"],
                        daysShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom"],
                        daysMin: ["Do", "Se", "Te", "Qu", "Qu", "Se", "Sa", "Do"],
                        months: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                        monthsShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                        today: "Hoje",
                        clear: "Limpar"
                    };
                }(jQuery));

                $("#<%= txtDataAbeturaFalha.ClientID %>").datepicker({
                    format: "dd/mm/yyyy",
                    language: "pt-BR",
                    autoclose:true
                });

                function ConsistePlaca(Tecla) {
                    var valorPlaca = document.getElementById('<%= txtPlaca.ClientID %>');

                    if (valorPlaca.value.length <= 2) {
                        if (Tecla > 96 && Tecla < 123) {
                            event.returnValue = true;
                        }
                        else {
                            event.returnValue = false;
                        }
                    }
                    else if (valorPlaca.value.length == 3) {
                        valorPlaca.value += "-";
                        event.returnValue = false;
                    }
                        //Se a quantidade de valores for maior que 3 ele verifica se é numero 
                        //se nao for retorna nulo
                    else if (Tecla > 47 && Tecla < 58) {
                        event.returnValue = true;
                    }
                    else {
                        event.returnValue = false;
                    }
                }
            </script>
        </asp:View>
    </asp:MultiView>
</asp:Content>
