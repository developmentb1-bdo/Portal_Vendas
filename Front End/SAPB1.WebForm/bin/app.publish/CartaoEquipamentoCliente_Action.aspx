<%@ Page Title="Cartão de Equipamento do Cliente - Dados | Foton" Language="C#" MasterPageFile="~/SapB1Master.Master" AutoEventWireup="true" CodeBehind="CartaoEquipamentoCliente_Action.aspx.cs" Inherits="SAPB1.Web.Modulos.Servico.CartaoEquipamentoCliente_Action" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="multiView" runat="server">
        <asp:View ID="view" runat="server">
            <!--Seção do cabeçalho.-->
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Dados
                    </h3>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body" style="display: block">
                    <!--Dados do equipamento-->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>N° de série do fabricante</label>
                                <asp:TextBox 
                                    ID="txtNumSeriFabri" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Status</label>
                                <asp:DropDownList 
                                    ID="cmbStatus" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>N° de série</label>
                                <asp:TextBox 
                                    ID="txtNumSerie" 
                                    runat="server" 
                                    ClientIDMode="Static" 
                                    CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>N° de série anterior</label>
                                <asp:TextBox ID="txtNumSerieAnt" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>N° do item</label>
                                <asp:DropDownList ID="ddlNumItem" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Novo N° de Série</label>
                                <asp:TextBox ID="txtNovoNumeroSerie" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Descrição do item</label>
                                <asp:TextBox ID="txtDescrItem" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--Dados do parceiro de negócio-->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Código do Cliente</label>
                                <asp:DropDownList ID="ddlCodigoCliente" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Técnico</label>
                                <asp:DropDownList ID="ddlTecnico" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Nome do Cliente</label>
                                <asp:TextBox ID="txtNomeCliente" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Território</label>
                                <asp:DropDownList ID="ddlTerritorio" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Pessoa de Contato</label>
                                <asp:DropDownList ID="ddlPessoaContato" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Número de Telefone</label>
                                <asp:TextBox ID="txtNumeroTelefone" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--Parte das Tabs-->
                    <div class="row">
                        <div class="col-md-12">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li class="active"><a href="#tab_1" data-toggle="tab">Endereço</a></li>
                                    <li><a href="#tab_2" data-toggle="tab">Dados de Venda</a></li>
                                </ul>
                                <div class="tab-content">
                                    <!--Tab de endereco-->
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Tipo de Logradouro</label>
                                                    <asp:TextBox runat="server" ID="txtTipoLogradouro" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Rua</label>
                                                    <asp:TextBox runat="server" ID="txtRua" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Rua Nº</label>
                                                    <asp:TextBox runat="server" ID="txtNumeroRua" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Complemento</label>
                                                    <asp:TextBox runat="server" ID="txtComplemento" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>CEP</label>
                                                    <asp:TextBox runat="server" ID="txtCep" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Bairro</label>
                                                    <asp:TextBox runat="server" ID="txtBairro" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Cidade</label>
                                                    <asp:TextBox runat="server" ID="txtCidade" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Estado</label>
                                                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Município</label>
                                                    <asp:DropDownList runat="server" ID="ddlMunicipio" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>País</label>
                                                    <asp:DropDownList runat="server" ID="ddlPais" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Local</label>
                                                    <asp:TextBox runat="server" ID="txtLocal" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Tab de vendas-->
                                    <div class="tab-pane" id="tab_2">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Código</label>
                                                    <asp:DropDownList runat="server" ID="ddlCodigoClienteVenda" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Entrega</label>
                                                    <asp:DropDownList runat="server" ID="ddlEntrega" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Nome</label>
                                                    <asp:TextBox runat="server" ID="txtNomeClienteVenda" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Nota Fiscal</label>
                                                    <asp:DropDownList runat="server" ID="ddlNotaFiscal" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>