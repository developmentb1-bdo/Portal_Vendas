<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Estoque.aspx.cs" Inherits="SAPB1.WebForms.Dagan.Estoque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField
        runat="server"
        ID="hfMensagemErros" />
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
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Filtros de Pesquisa
            </h3>
            <div class="box-tools pull-right">
               <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
           </div>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Cód. Item</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCodigoItem"
                            CssClass="form-control"
                            MaxLength="10">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Nome do Item</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNomeItem"
                            CssClass="form-control"
                            MaxLength="100">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-footer clearfix" style="text-align:center;">
            <asp:Button
                runat="server"
                ID="btnPesquisar"
                Text="Pesquisar" 
                CssClass="btn btn-warning" 
                OnClick="btnPesquisar_Click"/>
        </div>
     </div>
     <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Relação de Produtos no Estoque
            </h3>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Button
                            runat="server"
                            ID="btnListarTudo"
                            Text="Carregar Tudo" 
                            CssClass="btn btn-warning" 
                            OnClick="btnListarTudo_Click"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView
                        runat="server"
                        ID="grdEstoque"
                        CssClass="table table-bordered table-striped dataTable"
                        role="grid"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        AllowPaging="true"
                        PageSize="25"
                        OnPageIndexChanging="grdEstoque_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Cód. Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("Item.ItemCode").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome do Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("Item.ItemName").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cód. Depósito">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("Deposito.WhsCode").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome do depósito">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("Deposito.WhsName").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Em Estoque">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("OnHand").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Confirmado">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("IsCommited").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pedido">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("OnOrder").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Disponível">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("Disponivel").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                            <EmptyDataTemplate>
                                Nenhum item a ser relacionado
                            </EmptyDataTemplate>
                    </asp:GridView>            
                </div>
            </div>
        </div>
    </div>

    <!--CSS da paginação do GridView!-->
    <link href="Css/Estilos.css" rel="stylesheet" />
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnPesquisar.ClientID%>").click(function () {
                var erros = "";
                var codigoItem = $("#<%=txtCodigoItem.ClientID%>").val();
                var nomeItem = $("#<%=txtNomeItem.ClientID%>").val();

                if(codigoItem == "" && nomeItem == "")
                {
                    erros += "Digite um valor no mínimo em um dos parâmetros do filtro";
                }

                $("#<%=hfMensagemErros.ClientID%>").val(erros);
            });
        });
    </script>
</asp:Content>
