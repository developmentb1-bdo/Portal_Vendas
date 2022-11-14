<%@ Page 
    Title="Dashboard - Gerencial" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="DashboardGerencial.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.DashboardGerencial" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField
        runat="server"
        ID="hfIdConcessionario" />
    <asp:HiddenField
        runat="server"
        ID="hfGrupoProduto" />

    <link href="plugins/morris/morris.css" rel="stylesheet" />
    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="plugins/morris/morris.min.js"></script>
    <script src="plugins/fastclick/fastclick.min.js"></script>
    <link href="Css/StyleAutoComplete.css" rel="stylesheet" />
    <script src="plugins/jQueryUI/jquery-ui.min.js"></script>

    <section class="content">
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
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-red">
                        <i class="ion-social-usd-outline"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text">Recebimentos Em Aberto</span>
                        <asp:Label
                            runat="server"
                            ID="lblValorAberto"
                            Font-Bold="true">
                        </asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-red">
                        <i class="ion-social-usd-outline"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text">Recebimentos Em Atraso</span>
                        <asp:Label
                            runat="server"
                            ID="lblValorVencimento"
                            Font-Bold="true">
                        </asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-red">
                        <i class="ion-social-usd-outline"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text">Total Geral Recebimentos</span>
                        <asp:Label
                            runat="server"
                            ID="lblValorTotal"
                            Font-Bold="true">
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-green">
                        <i class="ion-social-usd-outline"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text">Total de Recebimentos no Mês</span>
                        <asp:Label
                            runat="server"
                            ID="lblTotalRecebimentosMes"
                            Font-Bold="true">
                        </asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <div class="info-box">
                    <span class="info-box-icon bg-green">
                        <i class="ion-social-usd-outline"></i>
                    </span>
                    <div class="info-box-content">
                        <span class="info-box-text">Total de Recebimentos no Ano</span>
                        <asp:Label
                            runat="server"
                            ID="lblRecebimentosAno"
                            Font-Bold="true">
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <!--Gráfico de faturamento-->
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Faturamento</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Parceiro de Negócio:</label>
                                    <asp:DropDownList
                                        runat="server"
                                        ID="ddlParceiroNegocio"
                                        CssClass="form-control">
                                            <asp:ListItem Text="Selecione" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Grupo de Itens:</label>
                                    <asp:DropDownList
                                        runat="server"
                                        ID="ddlGrupoProduto"
                                        CssClass="form-control">
                                            <asp:ListItem Text="Selecione" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Data Inicial:</label>
                                    <asp:TextBox
                                        runat="server"
                                        ID="txtDataInicial"
                                        CssClass="form-control"
                                        data-date-format="mm/dd/yyyy">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Data Final:</label>
                                    <asp:TextBox
                                        runat="server"
                                        ID="txtDataFinal"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="text-align:center;">
                                    <asp:Button
                                        runat="server"
                                        ID="btnFiltroFaturamento"
                                        Text="Filtrar"
                                        CssClass="btn btn-primary" 
                                        OnClick="btnFiltroFaturamento_Click"/>
                                    <asp:Button
                                        runat="server"
                                        ID="btnListarTudo"
                                        Text="Listar Tudo"
                                        CssClass="btn btn-primary" 
                                        OnClick="btnListarTudo_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer chart-responsive">
                         <div class="chart" id="divFaturamento" style="height: 300px;">
                             <asp:Literal
                                 runat="server"
                                 ID="ltlFaturamento">
                             </asp:Literal>
                         </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <!--Gráfico de faturamento por parceiro de negócio-->
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Faturamento Por Parceiro de Negócio</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body chart-responsive">
                         <div class="chart" id="divFaturamentoCliente" style="height: 300px;">
                             <asp:Literal
                                 runat="server"
                                 ID="ltrlFaturamentoCliente">
                             </asp:Literal>
                         </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <!--Gráfico de recebimentos Em Aberto-->
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Recebimentos Em Aberto</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body chart-responsive">
                         <div class="chart" id="divRecebimento" style="height: 300px;">
                             <asp:Literal
                                 runat="server"
                                 ID="ltrRecebimentos">
                             </asp:Literal>
                         </div>
                    </div>
                    <div class="box-footer ">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Parceiro de Negócio:</label>
                                    <asp:DropDownList
                                        runat="server"
                                        ID="txtPnRecebimentoAberto"
                                        CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Data Inicial:</label>
                                    <asp:TextBox
                                        runat="server"
                                        ID="txtDataInicialRecebAberto"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Data Final:</label>
                                    <asp:TextBox
                                        runat="server"
                                        ID="txtDataFinalRecebAberto"
                                        CssClass="form-control">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView
                                        runat="server"
                                        ID="gdvRecebimentosAbertoCliente"
                                        AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-striped"
                                        AllowPaging="true"
                                        PageSize="25"
                                        OnPageIndexChanging="gdvRecebimentosAbertoCliente_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField HeaderText="Codigo PN" DataField="CodigoPn" />
                                                <asp:BoundField HeaderText="Razão Social" DataField="Nome" />
                                                <asp:BoundField HeaderText="Mês/Ano" DataField="Data" />
                                                <asp:TemplateField HeaderText="Valor">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            ID="lblValorGrid"
                                                            Text='<%# Convert.ToDecimal(Eval("ValorTotal").ToString()).ToString("c") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" /> 
                                            <EmptyDataTemplate>
                                                Nenhum cliente para ser relacionado
                                            </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <!--Gráfico de recebimentos pagos-->
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Recebimentos Pagos</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body chart-responsive">
                         <div class="chart" id="divRecebimentoPagos" style="height: 300px;">
                             <asp:Literal
                                 runat="server"
                                 ID="ltrRecebimentosPagos">
                             </asp:Literal>
                         </div>
                    </div>
                    <div class="box-footer ">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView
                                    runat="server"
                                    ID="gdvRecebimentosPagos"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-striped"
                                    AllowPaging="true"
                                    PageSize="25"
                                    OnPageIndexChanging="gdvRecebimentosPagos_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField HeaderText="Codigo PN" DataField="CodigoPn" />
                                            <asp:BoundField HeaderText="Razão Social" DataField="Nome" />
                                            <asp:BoundField HeaderText="Mês/Ano" DataField="Data" />
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        ID="lblValorGrid"
                                                        Text='<%# Convert.ToDecimal(Eval("ValorTotal").ToString()).ToString("c") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" /> 
                                        <EmptyDataTemplate>
                                            Nenhum cliente para ser relacionado
                                        </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <link href="Css/Estilos.css" rel="stylesheet" />
    <script src="plugins/datepicker/bootstrap-datepicker.js"></script>
    <link href="plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="Javascript/Validations.js"></script>

    <script type="text/javascript">
        $("#<%= txtDataInicial.ClientID %>").datepicker({
            format: 'dd/mm/yyyy'
        });

        $("#<%= txtDataFinal.ClientID %>").datepicker({
            format: 'dd/mm/yyyy'
        });

        $("#<%= txtDataInicialRecebAberto.ClientID%>").datepicker({
            format: 'dd/mm/yyyy'
        });

        $("#<%= txtDataFinalRecebAberto.ClientID %>").datepicker({
            format: 'dd/mm/yyyy'
        });
    </script>
</asp:Content>
