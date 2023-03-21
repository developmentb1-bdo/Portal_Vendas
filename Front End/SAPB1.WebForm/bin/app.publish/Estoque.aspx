<%@ Page Title="" 
    Language="C#" 
    MasterPageFile="~/SapB1Master.Master" 
    AutoEventWireup="true" 
    CodeBehind="Estoque.aspx.cs" 
    Inherits="SAPB1.WebForm.Estoque" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfItemId" runat="server" />
    <asp:HiddenField ID="lblListIds" runat="server" />
    <asp:HiddenField ID="hiddenDepo" runat="server" />
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
                            MaxLength="100">
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
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Depósito</label>
                        <asp:TextBox ID="txtDeposito" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        <asp:DropDownList ID="cmbDeposito" runat="server" Visible="false"></asp:DropDownList>
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
                            CssClass="btn btn-primary" 
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
                            <%--<Columns>
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
                            </Columns>--%>
                        <Columns>
                                <asp:TemplateField HeaderText="Cód. Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("ItemCode").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome do Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("ItemName").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cód. Depósito">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("WhsCode").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comprimento(mm)">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToInt32(Eval("Comprimento").ToString()).ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Peças">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToInt32(Eval("TotalPecas").ToString()).ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estoque Disponivel">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("EstoqueDisponivel").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estoque Reservado">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("EstoqueReservado").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Peso Unitário">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("PesoUnitario").ToString()).ToString("n6") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:TemplateField HeaderText="Preço Mínimo">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("PrecoMinimo").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:TemplateField HeaderText="Preço Máximo">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("PrecoMaximo").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:TemplateField HeaderText="Lote">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("Lote").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:TemplateField HeaderText="Grupo Item">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Eval("GrupoItem").ToString() %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
								<asp:TemplateField HeaderText="Entrada Prevista(kg)">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            ID="lblCodigoItemGrid"
                                            Text='<%# Convert.ToDouble(Eval("EntradaPrevista").ToString()).ToString("n6") %>'>
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
    <script src="plugins/jQueryUI/jquery-ui.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <link href="Css/StyleUiAutoComplete.css" rel="stylesheet" />

    <script type="text/javascript">
        <%--$(document).ready(function () {
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
        });--%>
    </script>

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
                    var listaPreco = "";

                    $.ajax({
                        url: "Estoque.aspx/RetornarDadosItem", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);

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

        $(function () {

            availableTags = [];
            matrizIdValorItens = [];
            matrizIdValorDeposito = [];
            _availableTags = [];

            var dadosItens = $("#<%= lblListIds.ClientID %>").val();

            var dados = dadosItens.split('|');
            var matriz;

            for (var i = 0; i < dados.length; i++) {
                matriz = dados[i].split('#');

                availableTags.push(matriz[0]);
                matrizIdValorItens.push({ label: matriz[1], value: matriz[0] });
            }

            var _dadosItens = $("#<%= hiddenDepo.ClientID %>").val();
            var _dados = _dadosItens.split('|');
            var _matriz;

            for (var i = 0; i < _dados.length; i++) {
                _matriz = _dados[i].split('#');

                _availableTags.push(_matriz[0]);
                matrizIdValorDeposito.push({ label: _matriz[1], value: _matriz[0] });
            }

            $("#<%= txtCodigoItem.ClientID %>").autocomplete({
                source: availableTags,
                select: function (event, ui) {
                    event.preventDefault();
                    $("#<%= txtCodigoItem.ClientID %>").val(ui.item.label.toString());
                    $("#<%= hfItemId.ClientID %>").val(ui.item.value.toString());

                    var codigo = ui.item.value.toString();
                    var listaPreco = "";

                    $.ajax({
                        url: "Estoque.aspx/RetornarDadosItem", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);

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
                    var listaPreco = "";

                    $.ajax({
                        url: "Estoque.aspx/RetornarDadosItem", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {

                                var preco = parseFloat(retorno.d[0].Price).toFixed(2);

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


            $("#<%= txtDeposito.ClientID %>").autocomplete({
                source: matrizIdValorDeposito,
                select: function (event, ui) {
                    event.preventDefault();

                    $("#<%= hiddenDepo.ClientID %>").val(ui.item.value.toString());
                    $("#<%= txtDeposito.ClientID %>").val(ui.item.label.toString());

                    var codigo = "0";
                    var listaPreco = "0";

                    $.ajax({
                        url: "Estoque.aspx/RetornarDadosDeposito", //URL da página com o WebMethod 
                        data: "{codItem:'" + codigo + "',tabelaPreco:'" + listaPreco + "'}", //Enviar os parâmetros
                        type: "POST", //Tipo do envio (POST ou GET)
                        dataType: "json", //Tipo retorno dos dados
                        contentType: "application/json; charset=utf-8", //Conteúdo do retorno (header)

                        //Função de sucesso do retorno dos dados feita pelo ajax
                        success: function (retorno) {
                            if (retorno.d.length > 0) {
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
