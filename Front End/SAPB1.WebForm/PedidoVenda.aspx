<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/SapB1Master.Master" 
    AutoEventWireup="true" 
    CodeBehind="PedidoVenda.aspx.cs" 
    Inherits="SAPB1.WebForm.PedidoVenda" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField
        runat="server"
        ID="hfMensagemErro" />
    <asp:HiddenField
        runat="server"
        ID="hfEmprId" />
    <asp:Panel 
        runat="server" 
        ID="pnlAviso" 
        CssClass="alert alert-danger alert-dismissible"
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
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Número:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNumeroPedido"
                            CssClass="form-control"
                            MaxLength="15">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Data de Lançamento Inicial:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDtLancamentoInicial"
                            CssClass="form-control"
                            MaxLength="10">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="txtRazaoSocial">Data de Lançamento Final:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDtLancamentoFinal"
                            CssClass="form-control"
                            MaxLength="10">
                        </asp:TextBox>
                    </div>
                </div>
           </div>
           <div class="row">
               <div class="col-md-8">
                   <div class="form-group">
                        <label for="txtRazaoSocial">Razão Social:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtRazaoSocial"
                            CssClass="form-control"
                            MaxLength="10">
                        </asp:TextBox>
                    </div>
               </div>
               <div class="col-md-4">
                   <div class="form-group">
                        <label for="txtRazaoSocial">CPNJ:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCnpj"
                            CssClass="form-control"
                            MaxLength="20">
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
                OnClick="btnPesquisar_Click" />
            <asp:Button
                runat="server"
                ID="btnListarTudo"
                Text="Carregar Tudo" 
                CssClass="btn btn-warning" 
                OnClick="btnListarTudo_Click"/>
        </div>
     </div>
     <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Relação de Pedido de Venda
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
                            OnClick="btnIncluir_Click"/>
                    </div>
                </div>
            </div>
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
                    DataKeyNames="DocNum, DocEntry"
                    OnPageIndexChanging="grdPedidoVenda_PageIndexChanging"
                    OnRowDataBound="grdPedidoVenda_RowDataBound">
                        <Columns>
                            <asp:BoundField
                                DataField="DocNum"
                                HeaderText="Número" />
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblStatusGrid"
                                        Text='<%# 
                                                RetornarNomeStatus(Eval("DocStatus").ToString(), Eval("Canceled").ToString()) 
                                                %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parceiro de Negócio">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblParceiroNegocioGrid"
                                        Text='<%#Eval("CardName").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CNPJ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblCnpjGrid"
                                        Text='<%#Eval("U_CNPJ").ToString() %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data de Lançamento">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblDataLancamentoGrid"
                                        Text='<%# Convert.ToDateTime(Eval("DocDate").ToString()).ToString("dd/MM/yyyy") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data de Entrega" Visible="false">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblDataEntregaGrid"
                                        Text='<%# Convert.ToDateTime(Eval("DocDueDate").ToString()).ToString("dd/MM/yyyy") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total a Pagar">
                                <ItemTemplate>
                                    <asp:Label
                                        runat="server"
                                        ID="lblTotalPedido"
                                        Text='<%# Convert.ToDouble(Eval("DocTotalSy")).ToString("n6") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <a href="PedidoVenda_Action.aspx?id=<%# Eval("DocEntry") %>">
                                    <i class="fa fa-search fa-lg" aria-hidden="true" style="color: #dd4b39"></i>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Detalhes do Pedido" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton
                                        runat="server"
                                        ID="lkbDetalhesPedidoGrid"
                                        OnClick="lkbDetalhesPedidoGrid_Click"
                                        CommandArgument='<%#
                                                            Eval("DocNum").ToString() 
                                                            %>'>
                                            <i class="fa fa-search fa-lg" aria-hidden="true" style="color:#dd4b39"></i>
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

    <!--CSS da paginação do GridView!-->
    <link href="Css/Estilos.css" rel="stylesheet" />
    <!--Scripts!-->
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.date.extensions.js"></script>

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
                var dataInicial = $("<%= txtDtLancamentoInicial.ClientID %>").val();
                var dataFinal = $("<%= txtDtLancamentoFinal.ClientID%>").val();
                var razao = $("<%= txtRazaoSocial.ClientID %>").val();
                var cnpj = $("<%= txtCnpj.ClientID %>").val();

                if (numeroPedido == "" && dataInicial == "" && dataFinal == "" && razao == "" && cnpj=="") {
                    erros += "Digite um valor no mínimo em um parâmetro de pesquisa.";
                }

                $("#<%=hfMensagemErro.ClientID%>").val(erros);
                    
            });

            //Máscaras
            $("#<%=txtDtLancamentoInicial.ClientID%>").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
            $("#<%=txtDtLancamentoFinal.ClientID%>").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
        });

        function RedirecionarPaginaGridView(valor) {
            window.location = "PedidoVenda_Action.aspx?id=" + valor;
        }
    </script>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtNumeroPedido.ClientID%>").datepicker({
                format: "dd/mm/yyyy",
                language: "pt-BR",
                autoclose:true
            });
       });
    </script>--%>
</asp:Content>
