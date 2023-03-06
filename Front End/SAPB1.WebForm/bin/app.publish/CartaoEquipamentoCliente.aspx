<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/SapB1Master.Master" 
    AutoEventWireup="true" 
    CodeBehind="CartaoEquipamentoCliente.aspx.cs" 
    Inherits="SAPB1.WebForm.CartaoEquipamentoCliente" 
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
                Alerta
            </h4>
            <asp:Label 
                runat="server" 
                ID="ds" 
                Text="fddfsf">
            </asp:Label>
     </asp:Panel>
     <div class="box box-primary">
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
                <div class="col-md-12">
                    
                </div>
            </div>
        </div>
        <div class="box-footer clearfix" style="text-align:center;">
            <asp:Button
                runat="server"
                ID="btnPesquisar"
                Text="Pesquisar" 
                CssClass="btn btn-primary" />
            <asp:Button
                runat="server"
                ID="btnCarregarTudo"
                Text="Carregar Tudo" 
                CssClass="btn btn-primary" 
                OnClick="btnCarregarTudo_Click"/>
        </div>
     </div>
     <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                Relação de Cartão de Equipamento Cliente
            </h3>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                 <div class="table-responsive">
                      <asp:GridView
                        runat="server"
                        ID="gridCartaoEquipamento"
                        CssClass="table table-bordered table-striped dataTable"
                        role="grid"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        AllowPaging="true"
                        PageSize="25"
                        OnPageIndexChanging="gridCartaoEquipamento_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField ShowHeader="false">
                                    <ItemTemplate>
                                        <a href="CartaoEquipamentoCliente_Action.aspx?id=<%# Eval("insID") %>">Visualizar</a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº de Série Fabricante">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumeroSerieFabricante" runat="server" Text='<%# Eval("manufSN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nº de Série">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumeroSerie" runat="server" Text='<%# Eval("internalSN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Parceiro de Negócio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRazaoSocial" runat="server" Text='<%# Eval("custmrName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemGrid" runat="server" Text='<%# Eval("itemName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
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
        <div class="box-footer clearfix" style="text-align:center;">
            <asp:Button
                runat="server"
                ID="btnIncluir"
                Text="Incluir" 
                CssClass="btn btn-primary" 
                OnClick="btnIncluir_Click"
                Visible="false"/>
        </div>
    </div>

    <link href="Css/Estilos.css" rel="stylesheet" />
</asp:Content>
