<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="SolicitacaoGarantia.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.SolicitacaoGarantia" 
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
     <asp:HiddenField
         runat="server"
         ID="hfIdConcessionario" />
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
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Número da SG</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtNumeroSg"
                            CssClass="form-control"
                            MaxLength="20">
                        </asp:TextBox>   
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Período Inicial:</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataInicial"
                            CssClass="form-control"
                            MaxLength="20">
                        </asp:TextBox>   
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label>Perído Final</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtDataFinal"
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
                CssClass="btn btn-primary"
                OnClick="btnPesquisar_Click" />
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
                Relação de Solicitação de Garantia
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
                        ID="gridCartaoEquipamento"
                        CssClass="table table-bordered table-striped dataTable"
                        role="grid"
                        AutoGenerateColumns="false"
                        GridLines="None"
                        AllowPaging="true"
                        PageSize="25"
                        OnPageIndexChanging="gridCartaoEquipamento_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Número SG">
                                    <ItemTemplate>
                                        <asp:Label 
                                            ID="lblNumeroSg" 
                                            runat="server" 
                                            Text='<%# Eval("callID") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label 
                                            ID="lblStatusSolicitacao" 
                                            runat="server" 
                                            Text='<%# RetornarStatus(Eval("U_Status").ToString()) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data de Abertura">
                                    <ItemTemplate>
                                        <asp:Label 
                                            ID="lblNumeroSerieFabricante" 
                                            runat="server" 
                                            Text='<%# Convert.ToDateTime(Eval("createDate").ToString()).ToString("dd/MM/yyyy") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data de Fechamento">
                                    <ItemTemplate>
                                        <asp:Label 
                                            ID="lblNumeroSerie" 
                                            runat="server" 
                                            Text='<%# RetornarDataFechamento(Eval("closeDate").ToString()) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detalhes da SG">
                                    <ItemTemplate>
                                        <asp:LinkButton
                                            runat="server"
                                            ID="lkbDetalhesChamadoGrid"
                                            OnClick="lkbDetalhesChamadoGrid_Click"
                                            CommandArgument='<%# Eval("callID").ToString() %>'>
                                                <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" /> 
                            <EmptyDataTemplate>
                                Não há Solicitação de Garantia para ser exibido.
                            </EmptyDataTemplate>
                    </asp:GridView>      
                </div>
            </div>
        </div>
    </div>

    <link href="Css/Estilos.css" rel="stylesheet" />
</asp:Content>
