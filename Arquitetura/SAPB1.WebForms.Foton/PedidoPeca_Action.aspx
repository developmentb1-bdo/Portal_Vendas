<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="PedidoPeca_Action.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.PedidoPeca_Action" 
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
        ID="hfNumeroPedido" />
    <asp:HiddenField
        runat="server"
        ID="hfListaCodigosItem" />
    <asp:HiddenField
        runat="server"
        ID="hfListaCodigoNome" />
    <asp:HiddenField
        runat="server"
        ID="hfItemId" />
    <asp:HiddenField
        runat="server"
        ID="hfIdConcessionario" />
    <asp:HiddenField
        runat="server"
        ID="hfListapreco" />
    <asp:HiddenField
        runat="server"
        ID="hfStatusConcessionario" />
    <asp:HiddenField
        runat="server"
        ID="hfPrecoItem" />
    <asp:HiddenField
        runat="server"
        ID="hfTotalItem" />
    <asp:HiddenField 
        runat="server"
        ID="hfErrosUnidadeParada" />
    <asp:HiddenField
        runat="server"
        ID="hfErrosItem" />
    <!--Cabeçalho.-->
    <div class="box box-default">
        <div class="box-header with-border">
            <h3 class="box-title">Cabeçalho
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <div class="row">
                <div class="col-md-8">
                    <div class="form-group">
                        <label>Concessionário:</label>
                        <asp:TextBox 
                            ID="txtConcessionario" 
                            runat="server"
                            CssClass="form-control"
                            Enabled="false">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Cidade (UF):</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCidadeUf"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Tipo do Pedido:</label>
                        <asp:DropDownList
                            runat="server"
                            ID="ddlTipoPedidoConcessionario"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlTipoPedidoConcessionario_SelectedIndexChanged">
                                <asp:ListItem Text="Pedido Normal" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Pedido de Unidade Parada" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Número do Pedido:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNumeroPedido"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Data do Pedido:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataLancamento"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
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
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Notas Fiscais Emitidas para o pedido:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNotasFiscalEmitidas"
                            CssClass="form-control"
                            ReadOnly="true">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel runat="server" ID="pnlUnidadeParada" CssClass="box box-default" Visible="false">
        <div class="box-header with-border">
            <h3 class="box-title">
                Dados da Unidade Parada
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Nome do Cliente:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCliente"
                            CssClass="form-control"
                            MaxLength="25">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Chassi</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtChassi"
                            CssClass="form-control"
                            MaxLength="25">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>KM Atual:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtKm"
                            CssClass="form-control"
                            MaxLength="25"
                            onkeydown="Formata(this,10,event,2)">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Quantidade de Dias Parado:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtqtdDiasParado"
                            CssClass="form-control"
                            MaxLength="25"
                            onkeydown="Formata(this,10,event,2)">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Modelo do Veículo:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtModeloVeiculo"
                            CssClass="form-control">                            
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Ano/Modelo:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtAnoModelo"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Entre - Eixos:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtEntreEixos"
                            CssClass="form-control">                              
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Falhas Apresentadas:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtFalhasApresentadas"
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
                        <label>Testes Realizados:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtTestesRealizados"
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
                    <label>Observações Adicionais:</label>
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
    </asp:Panel>
    <div class="box box-default">
        <div class="box-header with-border">
            <h3 class="box-title">
                Relação de Peças
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block">
            <asp:Panel runat="server" ID="pnlInfoModelo" CssClass="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Modelo:</label>
                        <asp:DropDownList 
                            runat="server"
                            ID="ddlModelosVeiculos"
                            CssClass="form-control"
                            AppendDataBoundItems="true"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlModelosVeiculos_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Ano/Modelo:</label>
                        <asp:DropDownList 
                            runat="server"
                            ID="ddlAnoModelo"
                            CssClass="form-control"
                            AppendDataBoundItems="true"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAnoModelo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Entre-Eixos:</label>
                        <asp:DropDownList 
                            runat="server"
                            ID="ddlEntreEixos"
                            CssClass="form-control"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Código da Peça:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtPartNumber"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="form-group">
                        <label>Descrição:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDescricao"
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
                            runat="server"
                            ID="txtQtdItem"
                            CssClass="form-control"
                            onkeydown="Formata(this,10,event,2)"
                            Text="1">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Preço Unitário:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtPrecoUnitario"
                            CssClass="form-control"
                            onkeydown="Formata(this,10,event,2)">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Valor Total:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtValorTotal"
                            CssClass="form-control"
                            onkeydown="Formata(this,10,event,2)">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align:center;">
                    <div class="form-group">
                        <asp:Button
                            runat="server"
                            ID="btnInserirItem"
                            Text="Inserir Item"
                            CssClass="btn btn-primary"
                            OnClick="btnInserirItem_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="box-footer clearfix">
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView
                            runat="server"
                            ID="gdvItens"
                            AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped dataTable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Modelo De Veículo">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblModeloVeiculoGrid"
                                                Text='<%# Eval("Modelo").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ano/Modelo">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblAnoModeloGrid"
                                                Text='<%# Eval("AnoModelo").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entre - Eixos">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblEntreEixos"
                                                Text='<%# Eval("EntreEixos").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código da Peça">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblItemCodeGrid"
                                                Text='<%#Eval("ItemCode").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrição da Peça">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblItemDescricao"
                                                Text='<%#Eval("Dscription").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtde">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblQtdGrid"
                                                Text='<%#Eval("Quantity").ToString() %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Preço Unitário">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblPriceGrid"
                                                Text='<%# Convert.ToDouble(Eval("Price").ToString()).ToString("c") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor Total">
                                        <ItemTemplate>
                                            <asp:Label
                                                runat="server"
                                                ID="lblTotalGrid"
                                                Text='<%# Convert.ToDouble(Eval("LineTotal").ToString()).ToString("c") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Excluir Item">
                                    <ItemTemplate>
                                        <asp:LinkButton
                                            runat="server"
                                            ID="lkbDetalhesPedidoGrid"
                                            OnClientClick="return confirm('Deseja excluir esse item?');"
                                            OnClick="lkbDetalhesPedidoGrid_Click"
                                            ToolTip="Excluir Item"
                                            CommandArgument='<%# Eval("LineNum").ToString() %>'>
                                                <span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align:right;">
                    <div class="form-group">
                        <asp:Label
                            runat="server"
                            Font-Bold="true"
                            ID="lblValorTotal">
                        </asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <asp:Button
                runat="server"
                ID="btnSalvar"
                Text="Salvar" 
                CssClass="btn btn-primary"
                OnClick="btnSalvar_Click"/>

             <asp:Button
                runat="server"
                ID="btnCancelar"
                Text="Cancelar" 
                CssClass="btn btn-danger"
                OnClientClick="return confirm('Deseja cancelar o pedido? Todos os dados serão perdidos);"
                OnClick="btnCancelar_Click"/>
        </div>
    </div>

    <link href="Css/StyleAutoComplete.css" rel="stylesheet" />
    <script src="plugins/input-mask/jquery.inputmask.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="Javascript/Validations.js"></script>
    <script src="plugins/jQueryUI/jquery-ui.min.js"></script>
    <link href="Css/StyleAutoCompleteScroll.css" rel="stylesheet" />

    <script type="text/javascript">
        $("#<%=txtQtdItem.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtPrecoUnitario.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtKm.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtqtdDiasParado.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%=txtValorTotal.ClientID%>").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#<%= txtQtdItem.ClientID %>").blur(function () {
            CalcularValoresItem();
        });

        $("#<%= txtPrecoUnitario.ClientID %>").blur(function () {
            CalcularValoresItem();
        });

        $("#<%= txtChassi.ClientID %>").blur(function () {
            var valorChassi = $("#<%= txtChassi.ClientID %>").val();

            $.ajax({
                url: "PedidoPeca_Action.aspx/RetornarDadosPeloChassi", //URL da página com o WebMethod 
                data: "{chassi:'" + valorChassi + "'}", //Enviar os parâmetros
                type: "POST", //Tipo do envio (POST ou GET)
                dataType: "json", //Tipo retorno dos dados
                contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                //Função de sucesso do retorno dos dados feita pelo ajax
                success: function (retorno) {
                    $("#<%= txtModeloVeiculo.ClientID %>").val(retorno.d.U_Modelo);
                    $("#<%= txtAnoModelo.ClientID%>").val(retorno.d.U_Ano);
                    $("#<%= txtEntreEixos.ClientID %>").val(retorno.d.U_EntreEixos);
                },
                //Função de erro do retorno dos dados feita pelo ajax
                error: function (req, status, error) {
                    alert(error);
                }
            });
        });

        function CalcularValoresItem() {
            var qtdDigitado = $("#<%= txtQtdItem.ClientID %>").val();
            var precoUnitarioDigitado = $("#<%= txtPrecoUnitario.ClientID %>").val();

            var qtd = 0;
            var precoUnitario = 0;
            var total = 0;

            if (qtdDigitado != "") {
                qtd = parseFloat(qtdDigitado.replace(',', '.'));
            }

            if (precoUnitarioDigitado != "") {
                precoUnitario = parseFloat(precoUnitarioDigitado.replace(',','.'));
            }

            total = precoUnitario * qtd;

            $("#<%= txtValorTotal.ClientID %>").val(total.toFixed(2).toString());
            $("#<%= hfPrecoItem.ClientID %>").val(precoUnitario.toString());
            $("#<%= hfTotalItem.ClientID %>").val(total.toFixed(2).toString());
        }

        $(function () {
            var dadosItens = $("#<%= hfListaCodigosItem.ClientID %>").val();

            var dados = dadosItens.split(',');
            
            $("#<%= txtPartNumber.ClientID %>").autocomplete({
                source: dados,
                select: function (event, ui) {
                    event.preventDefault();

                    var codigo = ui.item.value.toString();
                    $("#<%= hfItemId.ClientID %>").val(codigo);
                    var listaPreco = $("#<%= hfListapreco.ClientID %>").val();

                    $("#<%= txtPartNumber.ClientID %>").val(codigo);

                    $.ajax({
                        url: "PedidoPeca_Action.aspx/RetornarDadosItemPorId", //URL da página com o WebMethod 
                        data: "{itemCode:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);
                                var nome = retorno.d[0].Item.ItemName;

                                $("#<%= txtDescricao.ClientID %>").val(nome);
                                $("#<%= txtPrecoUnitario.ClientID%>").val(preco.toString().replace(".", ","));

                                CalcularValoresItem();
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

        $("#<%= btnSalvar.ClientID %>").click(function () {
            ValidarCamposCabecalho();
        });

        function ValidarCamposCabecalho() {
            var tipoPedido = $("#<%= ddlTipoPedidoConcessionario.ClientID %>").val();
            
            if (tipoPedido == "2") {
                var erros = "";

                var nomeCliente = $("#<%= txtCliente.ClientID %>").val();
                var chassi = $("#<%= txtChassi.ClientID %>").val();
                var kmAtual = $("#<%= txtKm.ClientID %>").val();
                var diasParado = $("#<%= txtqtdDiasParado.ClientID %>").val();
                var falhas = $("#<%= txtFalhasApresentadas.ClientID %>").val();
                var testes = $("#<%= txtTestesRealizados.ClientID %>").val();

                if (nomeCliente == "") {
                    erros += "|Nome do Cliente é um Campo obrigatório.";
                }

                if (chassi == "") {
                    erros += "|Chassi é um campo obrigatório.";
                }

                if (kmAtual == "") {
                    erros += "|Km Atual é um campo obrigatório";
                }

                if (diasParado == "") {
                    erros += "|Quantidade Dias Parado é um campo obrigatório.";
                }

                if (falhas == "") {
                    erros += "|Falhas Apresentadas é um campo obrigatório.";
                }

                if (testes == "") {
                    erros += "|Testes Realizados é um campo obrigatório.";
                }

                $("#<%= hfErrosUnidadeParada.ClientID %>").val(erros);
            }
        }

        $("#<%= btnInserirItem.ClientID %>").click(function () {
            var erros = "";
            
            var codigoItem = $("#<%= txtPartNumber.ClientID %>").val();
            var descricaoItem = $("#<%= txtDescricao.ClientID %>").val();
            var quantidade = $("#<%= txtQtdItem.ClientID %>").val();

            if (codigoItem == "") {
                erros += "|Código do Item é um campo obrigatório.";
            }

            if (descricaoItem == "") {
                erros += "|Descrição do Item é um campo obrigatório.";
            }

            if (quantidade == "") {
                erros += "|Quantidade do Item é um campo obrigatório.";
            }

            $("#<%= hfErrosItem.ClientID %>").val(erros);

            return true;
        });

        $(function () {

            availableTags = [];

            var dadosItens = $("#<%= hfListaCodigoNome.ClientID %>").val();
            var dados = dadosItens.split('|');

            var matriz;

            for (var i = 0; i < dados.length; i++) {
                matriz = dados[i].split(',');

                availableTags.push({ label: matriz[1], value: matriz[0] });
            }

            $("#<%= txtDescricao.ClientID %>").autocomplete({
                source: availableTags,
                select: function (event, ui) {
                    event.preventDefault();

                    var codigo = ui.item.value.toString();

                    $("#<%= hfItemId.ClientID %>").val(codigo);

                    $("#<%= txtDescricao.ClientID %>").val(ui.item.label.toString());

                    var listaPreco = $("#<%= hfListapreco.ClientID %>").val();

                    $.ajax({
                        url: "PedidoPeca_Action.aspx/RetornarDadosItemPorId", //URL da página com o WebMethod 
                        data: "{itemCode:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            console.log(retorno.d);

                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);
                                var nome = retorno.d[0].Item.ItemCode;

                                $("#<%= txtPrecoUnitario.ClientID%>").val(preco.toString().replace(".", ","));
                                $("#<%= txtPartNumber.ClientID %>").val(nome);

                                CalcularValoresItem();
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
    </script>
</asp:Content>
