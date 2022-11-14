<%@ Page 
    Title="" 
    Language="C#"
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="TabelaPreco.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.TabelaPreco" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField
        runat="server"
        ID="hfMensagemErro" />
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
    <asp:HiddenField
        runat="server"
        ID="hfTabelaPrecoConcessionario" />
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
                        <label for="txtRazaoSocial">Cód. Tabela de Preço</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCodTabelaPreco"
                            CssClass="form-control"
                            MaxLength="30">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Nome Tabela de Preço</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNomeTabelaPreco"
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
                Relação de Tabela de Preço
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
                            CssClass="btn btn-primary" 
                            OnClick="btnListarTudo_Click"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView
                        runat="server"
                        ID="grdTabelaPreco"
                        CssClass="table table-bordered table-striped dataTable"
                        role="grid"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        AllowPaging="true"
                        PageSize="25"
                        OnPageIndexChanging="grdTabelaPreco_PageIndexChanging">
                            <Columns>
                                <asp:BoundField
                                    DataField="ListNum"
                                    HeaderText="Cód. Tabela" />
                                <asp:BoundField
                                    DataField="ListName"
                                    HeaderText="Nome Tabela" />
                                <asp:BoundField
                                    DataField="GroupCode"
                                    HeaderText="Cód. Grupo" />
                                <asp:TemplateField HeaderText="Relação de Produtos">
                                    <ItemTemplate>
                                        <asp:LinkButton
                                            runat="server"
                                            ID="lkbPrecosGrid"
                                            OnClick="lkbPrecosGrid_Click"
                                            CommandArgument='<%#
                                                                Eval("ListNum").ToString() 
                                                             %>'>
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                            <EmptyDataTemplate>
                                Nenhuma tabela de preço a ser relacionada
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
            $("#<%=txtCodTabelaPreco.ClientID%>").keypress(function (e) {
                //Retorna caso a tecla pressionada não seja número
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $("#<%=btnPesquisar.ClientID%>").click(function () {
                var codTabelaPreco = $("#<%=txtCodTabelaPreco.ClientID%>").val();
                var nomeTabelaPreco = $("#<%=txtNomeTabelaPreco.ClientID%>").val();
                var erros = "";

                if (codTabelaPreco == "" && nomeTabelaPreco == "") {
                    erros += "Digite um valor no mínimo em um dos parâmetros";
                }

                $("#<%=hfMensagemErro.ClientID%>").val(erros);
            });
        });
    </script>
</asp:Content>
