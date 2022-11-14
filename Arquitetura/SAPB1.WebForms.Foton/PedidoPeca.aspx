<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="PedidoPeca.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.PedidoPeca" 
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
        ID="hfIdConcessionario" />
    <asp:HiddenField
        runat="server"
        ID="hfListaPreco" />
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
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Número:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNumeroPedido"
                            CssClass="form-control"
                            MaxLength="30"
                            onkeypress="return isNumberKey(event)">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Período Inicial:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataInicial"
                            CssClass="form-control"
                            MaxLength="10"
                            onkeypress="return isNumberKey(event)"
                            onkeyup="formataData(this, retornaKeyCode(event));">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Período Final:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataFinal"
                            CssClass="form-control"
                            MaxLength="10"
                            onkeypress="return isNumberKey(event)"
                            onkeyup="formataData(this, retornaKeyCode(event));">
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
                OnClick="btnPesquisar_Click" />
            <asp:Button
                runat="server"
                ID="btnListarTudo"
                Text="Carregar Tudo" 
                CssClass="btn btn-primary" 
                OnClick="btnListarTudo_Click"/>
        </div>
     </div>
     <div class="box box-default">
        <div class="box-header with-border">
            <h3 class="box-title">
                Relação de Pedido de Peça
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
                            OnClick="btnIncluir_Click"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView
                        runat="server"
                        ID="grdPedidoVenda"
                        CssClass="table table-bordered table-striped dataTable"
                        role="grid"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        AllowPaging="true"
                        PageSize="25"
                        OnPageIndexChanging="grdPedidoVenda_PageIndexChanging">
                            <Columns>
                                <asp:BoundField
                                    DataField="DocNum"
                                    HeaderText="Número" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblStatusGrid"
                                            Text='<%# RetornarStatus(Eval("DocStatus").ToString(), Eval("U_ST_CONCESS").ToString()) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data do Pedido">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblDataLancamentoGrid"
                                            Text='<%# Convert.ToDateTime(Eval("DocDate").ToString()).ToString("dd/MM/yyyy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total a Pagar">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblTotalPedido"
                                            Text='<%# Convert.ToDouble(Eval("DocTotalSy")).ToString("c") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detalhes do Pedido">
                                    <ItemTemplate>
                                        <asp:LinkButton
                                            runat="server"
                                            ID="lkbDetalhesPedidoGrid"
                                            OnClick="lkbDetalhesPedidoGrid_Click"
                                            CommandArgument='<%# Eval("DocNum").ToString() %>'>
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" /> 
                            <EmptyDataTemplate>
                                Nenhum Pedido de Venda a ser relacionado
                            </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>


    <!--CSS da paginação do GridView!-->
    <link href="Css/Estilos.css" rel="stylesheet" />
    <!--Scripts!-->
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="Javascript/Validations.js"></script>
    

    <script type="text/javascript">
        $(document).ready(function () {
            //Evento de OnKeyPress
            $("#<%=txtNumeroPedido.ClientID%>").keypress(function (e) {
                //Retorna caso a tecla pressionada não seja número
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });

            $("#<%=btnPesquisar.ClientID%>").click(function () {
                var erros = "";

                var numeroPedido = $("#<%=txtNumeroPedido.ClientID%>").val();
                var dataInicial = $("#<%= txtDataInicial.ClientID %>").val();
                var dataFinal = $("#<%= txtDataFinal.ClientID %>").val();

                if (numeroPedido == "" && dataInicial == "" && dataFinal == "") {
                    erros += "Digite um valor no mínimo em um parâmetro de pesquisa.";
                }

                if (dataInicial != "" || dataFinal != "") {

                }

                $("#<%=hfMensagemErro.ClientID%>").val(erros);
                    
            });
        });
    </script>
</asp:Content>
