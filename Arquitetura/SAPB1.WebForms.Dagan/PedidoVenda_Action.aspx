<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="PedidoVenda_Action.aspx.cs" Inherits="SAPB1.WebForms.Dagan.PedidoVenda_Action" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hfEmpId" />
    <asp:Panel 
        runat="server" 
        ID="pnlAviso" 
        CssClass="alert alert-danger">
            <h4>
                <i class="icon fa fa-info"></i> 
                Aviso
            </h4>
            <asp:Label 
                runat="server" 
                ID="lblAvisos">
            </asp:Label>
    </asp:Panel>
    <asp:HiddenField
        runat="server"
        ID="hfNumeroPedido" />
    <asp:HiddenField
        runat="server"
        ID="hfErros" />
    <asp:HiddenField
        runat="server"
        ID="hfListaPreco" />
    <asp:HiddenField
        runat="server"
        ID="hfUtilizacao" />
    <asp:HiddenField
        runat="server"
        ID="lblListIds" />
    <asp:HiddenField
        runat="server"
        ID="hfItemId" />
    <asp:HiddenField
        runat="server"
        ID="hfParceiroNegocio" />
    <asp:HiddenField
        runat="server"
        ID="hfListPn" />
    <asp:HiddenField
        runat="server"
        ID="hfDadosItens" />
    <asp:HiddenField
        runat="server"
        ID="hfCondPagto" />
    <asp:HiddenField
        runat="server"
        ID="hfClickBotao" />
    <asp:HiddenField
        runat="server"
        ID="hfErrosRegras" />
    <asp:HiddenField
        runat="server"
        ID="hfListaParceiroNegocioId" />
    <!--Cabeçalho.-->
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">Dados do Cliente
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label>Cliente:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtParceiroNegocio"
                            CssClass="form-control"
                            MaxLength="200">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2" style="display:none">
                    <div class="form-group">
                        <label>Tipo:</label>
                        <asp:DropDownList 
                            ID="ddlTipoNumeroPedido" 
                            runat="server"
                            CssClass="form-control"
                            Enabled="false">
                                <asp:ListItem Value="1" Text="Primário" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Manual"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Número do Pedido:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNumero"
                            CssClass="form-control"
                            Enabled="false">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <div class="col-md-4">
                        <label>Código do PN:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCodigoPn"
                            CssClass="form-control"
                            MaxLength="20">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label>CPNJ:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCnpj"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Status:</label>
                            <asp:TextBox
                                runat="server"
                                ID="txtStatus"
                                CssClass="form-control"
                                ReadOnly="true">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8" style="display:none;">
                    <div class="form-group">
                        <label>Pessoa de Contato:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="ddlPessoaContato"
                            CssClass="form-control"
                            Enabled="false">
                                <asp:ListItem Text="-" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Data de Emissão:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataLancamento"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Data de Entrega:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataEntrega"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8" style="display:none;">
                    <div class="form-group">
                        <label>Número do Pedido:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNumeroPedido"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4" style="display:none;">
                    <div class="form-group">
                        <label>Tipo da Moeda:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="ddlTipoMoeda"
                            Enabled="false"
                            CssClass="form-control">
                                <asp:ListItem Text="Moeda corrente" Value=""></asp:ListItem>
                                <asp:ListItem Text="Moeda do sistema" Value=""></asp:ListItem>
                                <asp:ListItem Text="Moeda do PN" Value="" Selected="True"></asp:ListItem>	
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" style="display:none;">
                    <div class="form-group">
                        <label>Moeda:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtMoeda"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4" style="display:none;">
                    <div class="form-group">
                        <label>Data do Documento:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataDocumento"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8" style="display:none;">
                    <div class="form-group">
                        <label>Filial:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="ddlFilial"
                            CssClass="form-control"
                            Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4" style="display:none;">
                    <div class="form-group">
                        <label>CNPJ:</label>
                        <asp:TextBox
                            ID="txtCnpjFilial"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Condições de pagamento:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="ddlCondicoesPagamento"
                            CssClass="form-control">
                                <asp:ListItem Text="-" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Conteúdo
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <asp:Panel 
                runat="server" 
                ID="pnlAvisoItem" 
                CssClass="alert alert-danger">
                    <h4>
                        <i class="icon fa fa-info"></i> 
                        Erros
                    </h4>
                    <asp:Label 
                        runat="server" 
                        ID="lblAvisosItem">
                    </asp:Label>
            </asp:Panel>
            <asp:Panel 
                runat="server" 
                ID="pnlInclusaoItens">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Código do Item:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtCodigoItem"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="form-group">
                                <label>Item:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtNomeItem"
                                    CssClass="form-control"
                                    MaxLength="150">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Quantidade:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtQtdItem"
                                    CssClass="form-control"
                                    onkeypress="return isNumberKey(event)"
                                    MaxLength="50">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Preço Lista:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtPrecoUnitario"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Preço Venda:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtPrecoVenda"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>% de Desconto:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtDesconto"
                                    CssClass="form-control"
                                    onkeypress="return keypressed(event);"
                                    MaxLength="50"
                                    ReadOnly="true">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label>Total do Item:</label>
                            <asp:TextBox
                                runat="server"
                                ID="txtTotal"
                                CssClass="form-control">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="display:none">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Utilização:</label>
                                <asp:DropDownList
                                    runat="server"
                                    ID="ddlUtilizacaoItem"
                                    Enabled="false"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button
                                    runat="server"
                                    ID="btnInserirItem"
                                    Text="Inserir Item"
                                    CssClass="btn btn-warning"
                                    OnClick="btnInserirItem_Click" />
                            </div>
                        </div>
                    </div>
            </asp:Panel>
            <div class="table-responsive">
                <table id="tblInsert" class="table table-bordered table-striped dataTable">
                    <tbody>
                        <tr>
                            <th>
                                Linha
                            </th>
                            <th>
                                Cód. Item
                            </th>
                            <th>
                                Quantidade Fornecida
                            </th>
                            <th>
                                Nº de Embalagens
                            </th>
                            <th>
                                Preço Unitário
                            </th>
                            <th>
                                % do Desconto
                            </th>
                            <th>
                                CFOP
                            </th>
                            <th>
                                CST para ICMS
                            </th>
                            <th>
                                Total (MC)
                            </th>
                            <th>
                                #
                            </th>
                        </tr>
                    </tbody>
                </table>
                <asp:GridView
                    runat="server"
                    ID="gdvItens"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered table-striped dataTable">
                        <Columns>
                            <asp:TemplateField HeaderText="Linha">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblILinhaGrid"
                                        Text='<%#Eval("LineNum").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cód. Item">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblItemCodeGrid"
                                        Text='<%#Eval("ItemCode").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantidade">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblQtdGrid"
                                        Text='<%# Eval("Quantity").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantidade Fornecida">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblDeliveryGrid"
                                        Text='<%#Eval("DelivrdQty").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código da UM" Visible="false">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblUomCodeGrid"
                                        Text='<%#Eval("UomCode").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nº de Embalagens">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblIPackGrid"
                                        Text='<%#Eval("PackQty").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Preço Unitário">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblPriceGrid"
                                        Text='<%# "R$ " +  Convert.ToDouble(Eval("Price").ToString()).ToString("n6") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% do Desconto">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblDescontoGrid"
                                        Text='<%# Convert.ToDouble(Eval("DiscPrcnt").ToString()).ToString("n6") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Utilização" Visible="false">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblUsageGrid"
                                        Text='<%#Eval("Usage").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código de Imposto" Visible="false">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblTaxCodeGrid"
                                        Text='<%# Eval("TaxCode").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CFOP">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblCfopGrid"
                                        Text='<%#Eval("CFOPCode").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CST para ICMS">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblCstGrid"
                                        Text='<%#Eval("CSTCode").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total(MC)">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblTotalGrid"
                                        Text='<%# Convert.ToDouble(Eval("LineTotal").ToString()).ToString("c") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Permitir Documento de Suprimento" Visible="false">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblLinePoPrssGrid"
                                        Text='<%# (Eval("LinePoPrss").ToString().Equals("S")?"Sim":"Não") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="#">
                                <ItemTemplate>
                                    <asp:LinkButton
                                        runat="server"
                                        ID="hfExclusao"
                                        CommandArgument='<%# Eval("LineNum").ToString() %>'
                                        OnClick="hfExclusao_Click">
                                             <i class="fa fa-times fa-lg" aria-hidden="true" style="color:red"></i></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="box box-warning" style="display:none;">
        <div class="box-header with-border">
            <h3 class="box-title">
                Financeiro
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <div class="row">
                <div class="col-md-6" style="display:none;">
                    <div class="form-group">
                        <label>Forma de pagamento:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="ddlFormaPagamento"
                            CssClass="form-control">
                                <asp:ListItem Text="-" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Dados da Transportadora
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Cobrar Frete do Cliente:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="cmbTemFrete"
                            CssClass="form-control">
                                <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                                <asp:ListItem Value="N" Selected="True" Text="Não"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Percentual do Frete:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtPercentualFrete"
                            CssClass="form-control"
                            MaxLength="50">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Tipo do Frete:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="cmbTipoFrete"
                            CssClass="form-control">
                                <asp:ListItem Value="-1" Text="Selecione"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="form-group">
                        <label>Transportadora:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="cmbTransportadora"
                            CssClass="form-control">
                                <asp:ListItem Value="-1" Text="Selecione"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>CPF/CNPJ:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCnpjTransp"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Informações Gerais
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <div class="row" style="display:none;">
                <div class="col-md-6">
                    <label>Vendedor:</label>
                    <div class="form-group">
                        <asp:DropDownList
                            runat="server"
                            ID="ddlVendedor"
                            CssClass="form-control"
                            Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>Antes do Desconto:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtAntesDesconto"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row" style="display:none;">
                <div class="col-md-6">
                    <label>Titular:</label>
                    <div class="form-group">
                        <asp:DropDownList
                            runat="server"
                            ID="ddlTitular"
                            Enabled="false"
                            CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Desconto %:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtPercentualDesconto"
                            ReadOnly="true"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Valor:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtValorDesconto"
                            ReadOnly="true"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="display:none;">
                    <label>Uso Principal:</label>
                    <div class="form-group">
                        <asp:DropDownList
                            runat="server"
                            ID="ddlUtilizacao"
                            CssClass="form-control"
                            Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="display:none;">
                    <asp:CheckBox
                        runat="server"
                        ID="chkArredondamento"/>
                    <label>Arredondamento:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtValorArredondamento"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label>Total dos Produtos:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtTotalProdutos"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Frete:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtDespesaAdicional"
                            CssClass="form-control"
                            onkeydown="Formata(this,8,event,2)"
                            onkeypress="return isNumberKey(event)"
                            MaxLength="50">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <label>Imposto:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtImposto"
                            CssClass="form-control"
                            ReadOnly="true"
                            onkeydown="Formata(this,8,event,2)"
                            onkeypress="return isNumberKey(event)">
                        </asp:TextBox>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <label>Total do Pedido:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtTotalPagar"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <label>Observações:</label>
                    <div class="form-group">
                        <asp:TextBox
                            runat="server"
                            ID="txtObservacoes"
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
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <div class="form-group">
                <asp:Button
                    runat="server"
                    ID="btnSalvar"
                    Text="Salvar" 
                    CssClass="btn btn-warning"
                    OnClick="btnSalvar_Click"/>
            </div>
        </div>
    </div>
    <!-- Modal Loader-->
    <div class="modal fade" id="myModalLoader" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabelLoader" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabelLoader">Executando a operação. Aguarde...</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="text-align:center;">
                            <img src="Imagens/5.gif" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                                
                </div>
            </div>
        </div>
    </div>

    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="plugins/jQueryUI/jquery-ui.min.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <link href="Css/StyleUiAutoComplete.css" rel="stylesheet" />

    <script src="Javascript/Validations.js"></script>

    <script type="text/javascript">
        $(function () {

            var availableTags4 = [];

            var dadosItens = $("#<%= hfListaParceiroNegocioId.ClientID %>").val();

            var matrizAux = dadosItens.split(',');
            var matriz;

            for (var i = 0; i < matrizAux.length; i++) {

                availableTags4.push({ label: matrizAux[i], value: matrizAux[i] });
            }

            $("#<%= txtCodigoPn.ClientID %>").autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(availableTags4, function (item) {
                        return matcher.test(item.label);
                    }));
                },
                select: function (event, ui) {
                    event.preventDefault();

                    $("#<%= hfParceiroNegocio.ClientID %>").val(ui.item.value.toString());

                    var codigo = ui.item.value.toString();

                    $.ajax({
                        url: "PedidoVenda_Action.aspx/RetornarDadosParceiroNegocio", //URL da página com o WebMethod 
                        data: "{cardCode:'" + codigo + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            $("#<%= txtCodigoPn.ClientID %>").val(retorno.d.CardCode);
                            $("#<%= txtCnpj.ClientID %>").val(retorno.d.U_CNPJ);
                            $("#<%= hfListaPreco.ClientID %>").val(retorno.d.ListNum);
                            $("#<%= txtParceiroNegocio.ClientID %>").val(retorno.d.CardName);

                            if (retorno.d.MainUsage != "" && retorno.d.MainUsage != "0") {
                                $("#<%= ddlUtilizacao.ClientID %>").val(retorno.d.MainUsage.toString());
                                $("#<%= ddlUtilizacaoItem.ClientID %>").val(retorno.d.MainUsage.toString());
                                $("#<%= hfUtilizacao.ClientID %>").val(retorno.d.MainUsage.toString());
                            }

                            $("#<%= ddlCondicoesPagamento.ClientID %>").val(retorno.d.GroupNum.toString());

                            if (retorno.d.SlpCode != "0" && retorno.d.SlpCode != "-1") {
                                $("#<%= ddlVendedor.ClientID %>").val(retorno.d.SlpCode.toString());
                            }
                        },
                        //Função de erro do retorno dos dados feita pelo ajax
                        error: function (req, status, error) {
                            //alert(error);
                        }
                    });
                }
            });
        });
    </script>

    <script type="text/javascript">
        $("#<%= txtPrecoUnitario.ClientID %>").prop("readonly", true);
        $("#<%= txtDesconto.ClientID %>").prop("readonly", true);
        $("#<%= txtTotal.ClientID %>").prop("readonly", true);
        $("#<%= txtImposto.ClientID %>").prop("readonly", true);
        $("#<%= txtTotalPagar.ClientID %>").prop("readonly", true);
        $("#<%= txtTotalProdutos.ClientID %>").prop("readonly", true);
        $("#<%= txtCnpjTransp.ClientID %>").prop("readonly", true);
        $("#<%= txtDespesaAdicional.ClientID %>").prop("readonly", true);

        //Máscaras
        $("#<%= txtDataLancamento.ClientID %>").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
        $("#<%= txtDataEntrega.ClientID %>").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
        $("#<%= txtDataDocumento.ClientID %>").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });

        $(document).ready(function () {
            var consulta = $("#<%= hfNumeroPedido.ClientID %>").val();

            if (consulta != "") {
                $("#tblInsert").hide();
            }

            var errosRegras = $("#<%= hfErrosRegras.ClientID %>").val();

            if (errosRegras != "") {
                $("#<%= pnlAviso.ClientID %>").show();

                $("#<%= hfErrosRegras.ClientID %>").val("");
            }

            var valorPostBack = $("#<%= hfClickBotao.ClientID %>").val();

            if (valorPostBack == "1") {
 
                var hiddenValores = $("#<%= hfDadosItens.ClientID%>").val();

                if (hiddenValores != "") {
                    var tabelaPostBack = $("#tblInsert tbody");

                    var matriz = hiddenValores.split('#');

                    var htmlPostBack = "";

                    for (var i = 0; i < matriz.length; i++) {
                        if (matriz[i] != "") {
                            var dadosItemInd = matriz[i].split('|');

                            if (dadosItemInd.length == 7) {
                                htmlPostBack = "<tr>";
                                htmlPostBack += "<td>" + dadosItemInd[0] + "</td>";
                                htmlPostBack += "<td>" + dadosItemInd[1] + "</td>";
                                htmlPostBack += "<td>" + dadosItemInd[2] + "</td>";
                                htmlPostBack += "<td>0</td>";
                                htmlPostBack += "<td>" + dadosItemInd[3] + "</td>";
                                htmlPostBack += "<td>" + dadosItemInd[4] + "</td>";
                                htmlPostBack += "<td>0</td>";
                                htmlPostBack += "<td>0</td>";
                                htmlPostBack += "<td>" + dadosItemInd[6] + "</td>";
                                htmlPostBack += "<td style='cursor:pointer;' onclick='ExcluirItem(" + dadosItemInd[0] + ");'><i class=\"fa fa-times fa-lg\" aria-hidden=\"true\" style=\"color:red\"></i></td>";
                                htmlPostBack += "</tr>";

                                tabelaPostBack.append(htmlPostBack);
                            }
                        }

                        htmlPostBack = "";
                    }
                }

                $("#<%= hfClickBotao.ClientID %>").val("");
            }

            //DropDownList de filial
            $("#<%=ddlFilial.ClientID%>").change(function () {
                var codigoFilial = $("#<%=ddlFilial.ClientID%>").val();
                var valorHabilitado = "N";
                $.ajax({
                    url: "PedidoVenda_Action.aspx/RetornarDadosEmpresa", //URL da página com o WebMethod 
                    data: "{codFilial:'" + codigoFilial + "',habilitado:'" + valorHabilitado + "'}", //Enviar os parâmetros
                    type: "POST", //Tipo do envio (POST ou GET)
                    dataType: "json", //Tipo retorno dos dados
                    contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                    //Função de sucesso do retorno dos dados feita pelo ajax
                    success: function (retorno) {
                        $("#<%=txtCnpjFilial.ClientID%>").val(retorno.d[0].TaxIdNum);
                    },
                    //Função de erro do retorno dos dados feita pelo ajax
                    error: function (req, status, error) {
                       
                    }
                });
            });

            $("#<%=btnInserirItem.ClientID%>").click(function () {
                var codItem = $("#<%= hfItemId.ClientID %>").val();
                var qtdItem = $("#<%= txtQtdItem.ClientID %>").val();
                var precoUnitario = $("#<%= txtPrecoVenda.ClientID %>").val();

                var erros = "";
                var valor = 0;

                if (qtdItem == "") {
                    erros += "<li>Quantidade é um campo obrigatório.";
                }
                else {
                    valor = parseFloat(qtdItem);

                    if (valor < 1) {
                        erros += "<li>Quantidade precisa ser maior que 0.";
                    }
                }

                if (precoUnitario == "") {
                    erros += "<li>Preço Unitário é um campo obrigatório e precisa ser maior que 0.";
                }
                else {
                    valor = parseFloat(precoUnitario);

                    if (valor < 1) {
                        erros += "<li>Preço de Venda é um campo obrigatório e precisa ser maior que 0.";
                    }
                }
                
                var valorUtilizacao = $("#<%= hfUtilizacao.ClientID %>").val();

                if (valorUtilizacao == "" || valorUtilizacao == "0") {
                    erros += "<li>Cliente sem utilização. Por favor, verificar com a equipe de vendas interna.";
                }

                if (erros == "") {

                    $("#<%=pnlAvisoItem.ClientID %>").hide();

                    InserirItem();
                }
                else {
                    $("#<%= lblAvisosItem.ClientID %>").html(erros);

                    $("#<%=pnlAvisoItem.ClientID %>").show();
                }

                return false;
            });

            $("#<%= btnSalvar.ClientID %>").click(function () {
                var erros = "";

                var valorHiddenItens = $("#<%= hfDadosItens.ClientID %>").val();

                if (valorHiddenItens == "") {
                    erros+="<li>Precisa ter no mínimo um item."
                }

                var valorTipoFrete = $("#<%= cmbTipoFrete.ClientID %>").val();
                var transportadora = $("#<%= cmbTransportadora.ClientID %>").val();

                if (valorTipoFrete != "2" && valorTipoFrete != "-1" && transportadora == "0") {
                    erros += "<li>Selecione uma transportadora.";
                }

                if (erros != "") {
                    $("#<%= lblAvisos.ClientID %>").html(erros);
                    $("#<%= pnlAviso.ClientID %>").show();
                    $("#tituloPanel").focus();

                    return false;
                }
                else {
                    $("#<%= pnlAviso.ClientID %>").hide();
                    $("#<%= hfClickBotao.ClientID %>").val("1");

                    $('#myModalLoader').modal('show');
                }
            });
        });
    </script>
    
    <!-- Função autocomplete Item-->
    <script type="text/javascript">
        function keypressed(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 44) {
                return true;
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                else if (charCode === 8)
                    return true;
            }

            return true;
        }

        $(function () {
            
            availableTags = [];

            var dadosItens = $("#<%= lblListIds.ClientID %>").val();

            var dados = dadosItens.split('|');
            var matriz;

            for (var i = 0; i < dados.length; i++) {
                matriz = dados[i].split('#');

                availableTags.push(matriz[0]);
            }

            $("#<%= txtCodigoItem.ClientID %>").autocomplete({
                source: availableTags,
                select: function (event, ui) {
                    event.preventDefault();
                    $("#<%= txtCodigoItem.ClientID %>").val(ui.item.label.toString());

                    $("#<%= hfItemId.ClientID %>").val(ui.item.value.toString());

                    var codigo = ui.item.value.toString();
                    var listaPreco = $("#<%= hfListaPreco.ClientID %>").val();

                    $.ajax({
                        url: "PedidoVenda_Action.aspx/RetornarDadosItem", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);

                                $("#<%= txtPrecoUnitario.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtPrecoVenda.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtNomeItem.ClientID%>").val(retorno.d[0].Item.ItemName.toString());
                            }
                        },
                        //Função de erro do retorno dos dados feita pelo ajax
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });
        });

        $("#<%= txtQtdItem.ClientID %>").blur(function () {
            CalcularValoresItem();
        });

        $("#<%= txtPrecoVenda.ClientID %>").blur(function () {
            CalcularValoresItem();
        });

        function CalcularValoresItem() {
            var qtdItem = 0;
            var precoUnitario = 0;
            var desconto = 0;
            var total = 0;
            var precoVenda = 0;

            var valorCampoQtdItem = $("#<%= txtQtdItem.ClientID %>").val();
            var valorCampoPrecoUnitario = $("#<%= txtPrecoUnitario.ClientID %>").val();
            var valorCampoPrecoVenda = $("#<%= txtPrecoVenda.ClientID %>").val();

            if (valorCampoQtdItem != "") {
                valorCampoQtdItem = valorCampoQtdItem.replace('.', '');
                qtdItem = parseFloat(valorCampoQtdItem.replace(',', '.'));
            }

            if (valorCampoPrecoUnitario != "") {
                valorCampoPrecoUnitario = valorCampoPrecoUnitario.replace('.', '');
                valorCampoPrecoUnitario = valorCampoPrecoUnitario.replace(',', '.');

                precoUnitario = parseFloat(valorCampoPrecoUnitario);
            }

            if (valorCampoPrecoVenda != "") {
                valorCampoPrecoVenda = valorCampoPrecoVenda.replace('.', '');
                valorCampoPrecoVenda = valorCampoPrecoVenda.replace(',', '.');

                precoVenda = parseFloat(valorCampoPrecoVenda);
            }

            if (precoUnitario > 0 && precoVenda > 0 && precoVenda <= precoUnitario) {
                var valorDesconto = (precoUnitario - precoVenda);

                desconto = (valorDesconto * 100) / precoUnitario;
            }

            if (qtdItem > 0) {
                if (desconto > 0 && precoUnitario > 0) {
                    
                    total = qtdItem * precoVenda;

                    $("#<%= txtTotal.ClientID %>").val(total.toFixed(2).toString());
                    $("#<%= txtDesconto.ClientID %>").val(desconto.toFixed(2).toString());
                }
                else {
                    total = qtdItem * precoVenda;
                    $("#<%= txtTotal.ClientID %>").val(total.toFixed(2).toString());
                    $("#<%= txtDesconto.ClientID %>").val("0.00");
                }
            }
            else {
                $("#<%= txtTotal.ClientID %>").val("0.00");
                $("#<%= txtDesconto.ClientID %>").val("0.00");
            }
        }

        $(function () {

            availableTags = [];
            matrizIdValorItens = [];

            var dadosItens = $("#<%= lblListIds.ClientID %>").val();

            var dados = dadosItens.split('|');
            var matriz;

            for (var i = 0; i < dados.length; i++) {
                matriz = dados[i].split('#');

                availableTags.push(matriz[0]);
                matrizIdValorItens.push({ label: matriz[1], value: matriz[0] });
            }

            $("#<%= txtCodigoItem.ClientID %>").autocomplete({
                source: availableTags,
                select: function (event, ui) {
                    event.preventDefault();
                    $("#<%= txtCodigoItem.ClientID %>").val(ui.item.label.toString());
                    $("#<%= hfItemId.ClientID %>").val(ui.item.value.toString());

                    var codigo = ui.item.value.toString();
                    var listaPreco = $("#<%= hfListaPreco.ClientID %>").val();

                    $.ajax({
                        url: "PedidoVenda_Action.aspx/RetornarDadosItem", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);

                                $("#<%= txtPrecoUnitario.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtPrecoVenda.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtNomeItem.ClientID%>").val(retorno.d[0].Item.ItemName.toString());
                            }
                        },
                        //Função de erro do retorno dos dados feita pelo ajax
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });

            $("#<%= txtNomeItem.ClientID %>").autocomplete({
                source: matrizIdValorItens,
                select: function (event, ui) {
                    event.preventDefault();

                    $("#<%= hfItemId.ClientID %>").val(ui.item.value.toString());
                    $("#<%= txtNomeItem.ClientID %>").val(ui.item.label.toString());

                    var codigo = ui.item.value.toString();
                    var listaPreco = $("#<%= hfListaPreco.ClientID %>").val();

                    $.ajax({
                        url: "PedidoVenda_Action.aspx/RetornarDadosItem", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);

                                $("#<%= txtPrecoUnitario.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtPrecoVenda.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtCodigoItem.ClientID%>").val(retorno.d[0].Item.ItemCode.toString());
                            }
                        },
                        //Função de erro do retorno dos dados feita pelo ajax
                        error: function (req, status, error) {
                            alert(error);
                        }
                    });
                }
            });

            $("#<%= txtDesconto.ClientID %>").blur(function () {
                CalcularValoresItem();
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {

            availableTags = [];

            var dadosItens = $("#<%= hfListPn.ClientID %>").val();

            var dados = dadosItens.split('|');
            var matriz;

            for (var i = 0; i < dados.length; i++) {
                matriz = dados[i].split(',');

                availableTags.push({ label: matriz[1], value: matriz[0] });
            }

            $("#<%= txtParceiroNegocio.ClientID %>").autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(availableTags, function (item) {
                        return matcher.test(item.label);
                    }));
                },
                select: function (event, ui) {
                    event.preventDefault();
                    
                    $("#<%= txtParceiroNegocio.ClientID %>").val(ui.item.label.toString());
                    $("#<%= hfParceiroNegocio.ClientID %>").val(ui.item.value.toString());

                    var codigo = ui.item.value.toString();

                    $.ajax({
                        url: "PedidoVenda_Action.aspx/RetornarDadosParceiroNegocio", //URL da página com o WebMethod 
                        data: "{cardCode:'" + codigo + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            $("#<%= txtCodigoPn.ClientID %>").val(retorno.d.CardCode);
                            $("#<%= txtCnpj.ClientID %>").val(retorno.d.U_CNPJ);
                            $("#<%= hfListaPreco.ClientID %>").val(retorno.d.ListNum);

                            if (retorno.d.MainUsage != "" && retorno.d.MainUsage != "0") {
                                $("#<%= ddlUtilizacao.ClientID %>").val(retorno.d.MainUsage.toString());
                                $("#<%= ddlUtilizacaoItem.ClientID %>").val(retorno.d.MainUsage.toString());
                                $("#<%= hfUtilizacao.ClientID %>").val(retorno.d.MainUsage.toString());
                            }

                            $("#<%= ddlCondicoesPagamento.ClientID %>").val(retorno.d.GroupNum.toString());

                            if (retorno.d.SlpCode != "0" && retorno.d.SlpCode != "-1") {
                                $("#<%= ddlVendedor.ClientID %>").val(retorno.d.SlpCode.toString());
                            }
                        },
                        //Função de erro do retorno dos dados feita pelo ajax
                        error: function (req, status, error) {
                            //alert(error);
                        }
                    });
                }
            });
        });

        $("#<%= cmbTransportadora.ClientID %>").change(function () {
            var valor = $("#<%= cmbTransportadora.ClientID %>").val();

            if (valor != "-1" && valor != "0") {
                $.ajax({
                    url: "PedidoVenda_Action.aspx/RetornarDadosParceiroNegocio", //URL da página com o WebMethod 
                    data: "{cardCode:'" + valor + "'}", //Enviar os parâmetros
                    type: "POST", //Tipo do envio (POST ou GET)
                    dataType: "json", //Tipo retorno dos dados
                    contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                    //Função de sucesso do retorno dos dados feita pelo ajax
                    success: function (retorno) {
                        $("#<%= txtCnpjTransp.ClientID %>").val(retorno.d.U_CNPJ);
                    },
                    //Função de erro do retorno dos dados feita pelo ajax
                    error: function (req, status, error) {
                        //alert(error);
                    }
                });
            }
            else {
                $("#<%= txtCnpjTransp.ClientID %>").val("");
            }
        });

        $("#<%= cmbTipoFrete.ClientID %>").change(function () {
            var valorTipoFrete = $("#<%= cmbTipoFrete.ClientID %>").val();

            if (valorTipoFrete == "2" || valorTipoFrete == "-1") {
                $("#<%= txtDespesaAdicional.ClientID %>").val("0,00");
                $("#<%= cmbTransportadora.ClientID %>").val("0");
                $("#<%= cmbTransportadora.ClientID %>").prop("disabled", true);
                $("#<%= txtCnpjTransp.ClientID %>").val("");
                $("#<%= txtPercentualFrete.ClientID %>").prop("readonly", true);
                $("#<%= txtPercentualFrete.ClientID %>").val("0,00");

                $("#<%= cmbTemFrete.ClientID %>").val("N");

                var valorProdutos = $("#<%= txtTotalProdutos.ClientID %>").val();
                $("#<%= txtTotalPagar.ClientID %>").val(valorProdutos);
            }
            else {
                $("#<%= txtPercentualFrete.ClientID %>").prop("readonly", false);
                $("#<%= cmbTransportadora.ClientID %>").prop("disabled", false);
                $("#<%= cmbTemFrete.ClientID %>").val("S");
            }
        });

        //INSERIR ITEM NA TABELA
        function InserirItem() {
            var tabela = $("#tblInsert tbody");

            var codigoItem = $("#<%= txtCodigoItem.ClientID %>").val();
            var precoVenda = $("#<%= txtPrecoVenda.ClientID %>").val();
            var qtd = $("#<%= txtQtdItem.ClientID %>").val();
            var desconto = $("#<%= txtDesconto.ClientID %>").val();
            var total = $("#<%= txtTotal.ClientID %>").val();

            var linhaTb = 0;

            $("#tblInsert tbody").find("tr").each(function () {
                linhaTb += 1;
            });

            var html = "<tr>";
            html += "<td>" + linhaTb.toString() + "</td>";
            html += "<td>" + codigoItem + "</td>";
            html += "<td>" + qtd + "</td>";
            html += "<td>0</td>";
            html += "<td>" + precoVenda + "</td>";

            if (desconto == "") {
                html += "<td>0</td>";
            }
            else {
                desconto = desconto.replace('.', ',');

                html += "<td>" + desconto + "</td>";
            }

            html += "<td>0</td>";
            html += "<td>0</td>";
            html += "<td>" + total + "</td>";
            html += "<td style='cursor:pointer;' onclick='ExcluirItem(" + linhaTb.toString() + ");'><i class=\"fa fa-times fa-lg\" aria-hidden=\"true\" style=\"color:red\"></i></td>";
            html += "</tr>";

            tabela.append(html);

            var dados = $("#<%= hfDadosItens.ClientID %>").val();
            var matrizDados = dados.split('#');

            var novoDado = linhaTb.toString() + "|" + codigoItem + "|" + qtd + "|" + precoVenda.toString() + "|" + desconto.toString() + "|0|" + total.toString();

            var matrizNova = [];

            if (matrizDados.length > 0) {
                for (var i = 0; i < matrizDados.length; i++) {
                    if (matrizDados[i] != "") {
                        matrizNova.push(matrizDados[i]);
                    }
                }
            }

            matrizNova.push(novoDado);

            var dadosHf = "";

            for (var i = 0; i < matrizNova.length; i++) {
                dadosHf += matrizNova[i];

                if (i < (matrizNova.length - 1)) {
                    dadosHf += "#";
                }
            }

            $("#<%= hfDadosItens.ClientID %>").val(dadosHf);

            var totalPedido = 0;
           
            var valorStringTotalPedido = $("#<%= txtTotalPagar.ClientID %>").val();

            if (valorStringTotalPedido != "") {
                totalPedido = parseFloat(valorStringTotalPedido);
            }

            var totalItem = parseFloat(total);

            totalPedido = totalPedido + totalItem;

            $("#<%= txtTotalPagar.ClientID %>").val(totalPedido.toFixed(2).toString());
            $("#<%= txtTotalProdutos.ClientID %>").val(totalPedido.toFixed(2).toString());

            $("#<%= txtNomeItem.ClientID %>").val("");
            $("#<%= txtCodigoItem.ClientID %>").val("");
            $("#<%= txtPrecoVenda.ClientID %>").val("");
            $("#<%= txtQtdItem.ClientID %>").val("");
            $("#<%= txtDesconto.ClientID %>").val("");
            $("#<%= txtTotal.ClientID %>").val("0.00");
            $("#<%= txtPrecoUnitario.ClientID %>").val("");
        }

        //EXCLUIR LINHA TABELA
        function ExcluirItem(linha) {
            var dadosHf = $("#<%= hfDadosItens.ClientID %>").val().split('#');
            var linhaConvertida = parseInt(linha) - 1;
            var tamanhoMatriz = dadosHf.length - 1;
            var novaMatriz = [];

            var primeiraLinha = true;

            $("#tblInsert tbody").find("tr").each(function () {
                if (primeiraLinha == true) {
                    primeiraLinha = false;
                }
                else {
                    $(this).remove();
                }
            });

            var valorTotalItemExcluir = 0;

            for (var i = 0; i < dadosHf.length; i++) {
                if (dadosHf[i] != "") {
                    if (i == linhaConvertida) {
                        var matrizExclusao = dadosHf[i].split('|');

                        if (matrizExclusao[6] != "") {
                            valorTotalItemExcluir = parseFloat(matrizExclusao[6]);
                        }
                        continue;
                    }
                    else {
                        novaMatriz.push(dadosHf[i]);
                    }
                }
            }

            var linhaTb = 1;
            var novosDadosHf = "";
            for (var i = 0; i < novaMatriz.length; i++) {
                if (novaMatriz[i] != "") {
                    var dadosLinha = novaMatriz[i].split('|');

                    var html = "<tr>";
                    html += "<td>" + linhaTb.toString() + "</td>";
                    html += "<td>" + dadosLinha[1] + "</td>";
                    html += "<td>" + dadosLinha[2] + "</td>";
                    html += "<td>0</td>";
                    html += "<td>" + dadosLinha[3] + "</td>";
                    html += "<td>" + dadosLinha[4] + "</td>";
                    html += "<td>0</td>";
                    html += "<td>0</td>";
                    html += "<td>" + dadosLinha[6] + "</td>";
                    html += "<td style='cursor:pointer;' onclick='ExcluirItem(" + linhaTb.toString() + ");'><i class=\"fa fa-times fa-lg\" aria-hidden=\"true\" style=\"color:red\"></i></td>";
                    html += "</tr>";

                    var tabela = $("#tblInsert tbody").append(html);

                    linhaTb += 1;

                    novosDadosHf += novaMatriz[i];
                }

                if (i < (novaMatriz.length - 1) && novaMatriz[i] != "") {
                    novosDadosHf += "#";
                }
            }

            $("#<%= hfDadosItens.ClientID %>").val(novosDadosHf);

            var valorTotal = 0;
            var valorStringTotal = $("#<%= txtTotalPagar.ClientID %>").val();

            if (valorStringTotal != "") {
                valorTotal = parseFloat(valorStringTotal);
            }

            if (valorTotal > 0 && valorTotalItemExcluir > 0) {
                valorTotal = valorTotal - valorTotalItemExcluir;
            }

            $("#<%= txtTotalPagar.ClientID %>").val(valorTotal.toFixed(2).toString());
            $("#<%= txtTotalProdutos.ClientID %>").val(valorTotal.toFixed(2).toString());
        }

        $("#<%= ddlCondicoesPagamento.ClientID %>").change(function () {
            $("#<%=hfCondPagto.ClientID %>").val($("#<%= ddlCondicoesPagamento.ClientID %>").val());
        });

        $("#<%= cmbTemFrete.ClientID %>").change(function () {
            var valor = $("#<%= cmbTemFrete.ClientID %>").val();

            if (valor == "S") {
                $("#<%= cmbTipoFrete.ClientID %>").prop("disabled", false);

                if ($("#<%= cmbTipoFrete.ClientID %>").val() != 2) {
                    $("#<%= txtPercentualFrete.ClientID %>").prop("readonly", false);
                    $("#<%= cmbTransportadora.ClientID %>").prop("disabled", false);
                }
                else {
                    $("#<%= txtPercentualFrete.ClientID %>").prop("readonly", true);
                    $("#<%= cmbTransportadora.ClientID %>").prop("disabled", true);
                }
            }
            else {
                $("#<%= cmbTipoFrete.ClientID %>").val("2");
                $("#<%= cmbTipoFrete.ClientID %>").prop("disabled", true);
                $("#<%= txtPercentualFrete.ClientID %>").val("0,00");
                $("#<%= txtPercentualFrete.ClientID %>").prop("readonly", true);
                $("#<%= txtDespesaAdicional.ClientID %>").val("0,00");
                $("#<%= cmbTransportadora.ClientID %>").prop("disabled", true);
            }
        });

        $("#<%= txtPercentualFrete.ClientID %>").blur(function () {
            var valor = $("#<%= txtPercentualFrete.ClientID %>").val();
            var valorProdutos = $("#<%= txtTotalProdutos.ClientID %>").val().replace(',', '.');

            if (valor != "" && valorProdutos != "") {
                valor = valor.replace('.', '');
                valor = valor.replace(',', '.');

                var valorConvertido = parseFloat(valor);

                
                var valorConvertidoProduto = parseFloat(valorProdutos);

                var total = parseFloat((valorConvertidoProduto * (valorConvertido / 100)).toFixed(2));

                $("#<%= txtDespesaAdicional.ClientID %>").val(total.toFixed(2).toString().replace(".", ","));

                var totalPedido = valorConvertidoProduto + total;

                $("#<%= txtTotalPagar.ClientID %>").val(totalPedido.toString().replace(".", ","))
            }
            else {
                $("#<%= txtPercentualFrete.ClientID %>").val("0,00");
                $("#<%= txtDespesaAdicional.ClientID %>").val("0,00");
                $("#<%= cmbTipoFrete.ClientID %>").val("2");

                var valorProdutos = $("#<%= txtTotalProdutos.ClientID %>").val().replace(',', '.');

                $("#<%= txtTotalPagar.ClientID %>").val(valorProdutos);
            }
        });
    </script>
</asp:Content>
