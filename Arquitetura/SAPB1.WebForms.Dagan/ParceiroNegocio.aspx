<%@ Page 
    Title=""
    Language="C#"
    MasterPageFile="~/SiteMaster.Master"
    AutoEventWireup="true"
    CodeBehind="ParceiroNegocio.aspx.cs"
    Inherits="SAPB1.WebForms.Dagan.ParceiroNegocio" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel
        runat="server"
        ID="pnlAviso"
        CssClass="alert alert-info alert-dismissible"
        Visible="false">
        <button
            type="button"
            class="close"
            data-dismiss="alert"
            aria-hidden="true">
            ×
        </button>
        <h4>
            <i class="icon fa fa-info"></i>
            Alerta
        </h4>
        <asp:Label
            runat="server"
            ID="lblMensagem"
            Text="fddfsf">
        </asp:Label>
    </asp:Panel>
    <asp:HiddenField runat="server" ID="hfVend" />
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">Filtros de Pesquisa
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Código:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCodigo"
                            CssClass="form-control"
                            MaxLength="50">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Razão Social:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtRazao"
                            CssClass="form-control"
                            MaxLength="50">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>CNPJ:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCnpj"
                            CssClass="form-control"
                            MaxLength="50"
                            onkeypress="return isNumberKey(event)">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <label>Tipo de Parceiro de Negócio:</label>
                    <asp:DropDownList
                        runat="server"
                        ID="ddlTipo"
                        CssClass="form-control">
                        <asp:ListItem Text="Tipo de Parceiro de Negócio" Value=""></asp:ListItem>
                        <asp:ListItem Text="Cliente" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Lead" Value="L"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="box-footer clearfix" style="text-align: center;">
            <asp:Button
                runat="server"
                ID="btnPesquisar"
                Text="Pesquisar"
                CssClass="btn btn-warning"
                OnClick="btnPesquisar_Click" />
            <asp:Button
                runat="server"
                ID="btnListarTudo"
                Text="Listar Tudo"
                CssClass="btn btn-warning"
                OnClick="btnListarTudo_Click" />
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">Relação de Parceiros de Negócios
            </h3>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Button
                            runat="server"
                            ID="btnIncluir"
                            Text="Incluir"
                            CssClass="btn btn-warning"
                            OnClick="btnIncluir_Click" />
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="gridParceiroNegocio"
                    CssClass="table table-bordered table-striped dataTable"
                    role="grid"
                    AutoGenerateColumns="false"
                    GridLines="None"
                    AllowPaging="true"
                    PageSize="25"
                    OnPageIndexChanging="gridParceiroNegocio_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    ID="lblCodigo"
                                    Text='<%# Eval("CardCode") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Razão Social">
                            <ItemTemplate>
                                <asp:Label ID="lblRazaoSocial" runat="server" Text='<%# Eval("CardName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo de PN">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    ID="lblTipoParceiro"
                                    Text='<%# RetonarTipoParceiroNegocio(Eval("CardType").ToString()) %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-Mail">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("E_Mail") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <a href="ParceiroNegocio_Action.aspx?cardCode=<%# Eval("CardCode") %>">
                                    <i class="fa fa-search fa-lg" aria-hidden="true" style="color: #f39c12"></i>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                    <EmptyDataTemplate>
                        Não há parceiro de negócio a ser exibido.
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

    <link href="Css/Estilos.css" rel="stylesheet" />
    <script src="Javascript/Validations.js"></script>
</asp:Content>
