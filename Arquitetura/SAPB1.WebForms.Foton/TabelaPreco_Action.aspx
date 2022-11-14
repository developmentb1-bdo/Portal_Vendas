<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="TabelaPreco_Action.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.TabelaPreco_Action" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField
        runat="server"
        ID="hfMensagemErro" />
     <asp:HiddenField
        runat="server"
        ID="hfNumTabelaPreco" />
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
                ID="lblAvisos" 
                Text="fddfsf">
            </asp:Label>
    </asp:Panel>
    <div class="box box-default">
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
                            MaxLength="30">
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
                CssClass="btn btn-primary" 
                OnClick="btnPesquisar_Click"/>
        </div>
    </div>
    <div class="box box-default">
        <div class="box-header with-border">
            <h3 class="box-title">
                Relação de Produtos
            </h3>
        </div>
        <div class="box-body" style="display: block;padding-bottom:10px;">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Button
                            runat="server"
                            ID="btnListarTodos"
                            Text="Carregar Tudo" 
                            class="btn btn-primary"
                            OnClick="btnListarTodos_Click"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView
                        runat="server"
                        ID="grdProdutos"
                        CssClass="table table-bordered table-striped dataTable"
                        role="grid"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        PageSize="25"
                        AllowPaging="true"
                        OnPageIndexChanging="grdProdutos_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Cód. Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("CodigoItem").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome do Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblNomeGrid"
                                            Text='<%# Eval("NomeItem").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NCM">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoNcmGrid"
                                            Text='<%# Eval("NcmCode").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Preço Reposição" >
                                   <ItemTemplate>
                                       <asp:Label
                                           runat="server"
                                           ID="lblPrecoPadraoGrid"
                                           Text='<%# Convert.ToDouble(Eval("PrecoReposicao").ToString()).ToString("c") %>'>
                                       </asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Right" />
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Preço Garantia">
                                   <ItemTemplate>
                                       <asp:Label
                                           runat="server"
                                           ID="lblPrecoGarantiaGrid"
                                           Text='<%# Convert.ToDouble(Eval("PrecoGarantia").ToString()).ToString("c") %>'>
                                       </asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Right" />
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Preço Sugerido">
                                   <ItemTemplate>
                                       <asp:Label
                                           runat="server"
                                           ID="lblPrecoSugeridoGrid"
                                           Text='<%# Convert.ToDouble(Eval("PrecoSugerido").ToString()).ToString("c") %>'>
                                       </asp:Label>
                                   </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Right" />
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
                var codItem = $("#<%=txtCodigoItem.ClientID%>").val();
                var nomeItem = $("#<%=txtNomeItem.ClientID%>").val();
                var erros = "";

                if (codItem == "" && nomeItem == "") {
                    erros += "Digite um valor no mínimo em um parâmetro";
                }

                $("#<%=hfMensagemErro.ClientID%>").val(erros);
            });
        });
    </script>
</asp:Content>
