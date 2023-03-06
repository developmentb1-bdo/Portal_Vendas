<%@ Page Title="" Language="C#" MasterPageFile="~/SapB1Master.Master" AutoEventWireup="true" CodeBehind="CotacaoVenda.aspx.cs" Inherits="SAPB1.WebForm.CotacaoVenda" %>

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
                OnClick="Carregar" />
            <asp:Button
                runat="server"
                ID="btnListarTudo"
                Text="Listar Tudo"
                CssClass="btn btn-warning"
                OnClick="Carregar" />
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
                            CssClass="btn btn-primary"
                            OnClick="btnIncluir_Click" />
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="gridCotacao"
                    CssClass="table table-bordered table-striped dataTable"
                    role="grid"
                    AutoGenerateColumns="false"
                    GridLines="None"
                    AllowPaging="true"
                    PageSize="25"
                    OnPageIndexChanging="gridCotacao_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Número">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    ID="lblCodigo"
                                    Text='<%# Eval("DocNum") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Razão Social">
                            <ItemTemplate>
                                <asp:Label ID="lblRazaoSocial" runat="server" Text='<%# Eval("CardName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancelado">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    ID="lblStatus"
                                    Text='<%# ((Eval("CANCELED").ToString().Equals("Y")) ? "Sim" : "Não") %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data Lançamento">
                            <ItemTemplate>
                                <asp:Label ID="lblData" runat="server" Text='<%# Convert.ToDateTime(Eval("DocDate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <a href="CotacaoVenda_Action.aspx?docEntry=<%# Eval("DocEntry") %>">
                                    <i class="fa fa-search fa-lg" aria-hidden="true" style="color: #dd4b39"></i>
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