<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="Dashboard.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.Dashboard" 
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
    <link href="plugins/morris/morris.css" rel="stylesheet" />

    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="plugins/morris/morris.min.js"></script>
    <script src="plugins/fastclick/fastclick.min.js"></script>
    
    <div class="row">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-yellow">
                    <i class="ion ion-ios-people-outline"></i>
                </span>
                <div class="info-box-content">
                    <span class="info-box-text">Parceiro de Negócio</span>
                    <asp:Label 
                        runat="server" 
                        ID="lblQuantidadeClientes" 
                        Text="150" 
                        CssClass="info-box-number">
                    </asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-green">
                    <i class="ion-social-usd-outline"></i>
                </span>
                <div class="info-box-content">
                    <span class="info-box-text">Vendas no Mês</span>
                    <asp:Label 
                        runat="server" 
                        ID="lblVendasNoMes" 
                        Text="0" 
                        CssClass="info-box-number">
                    </asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-blue">
                    <i class="ion ion-ios-pricetag-outline"></i>
                </span>
                <div class="info-box-content">
                    <span class="info-box-text">Estoque Valor</span>
                    <asp:Label 
                        runat="server" 
                        ID="lblEstoqueValor" 
                        Text="0" 
                        CssClass="info-box-number">
                    </asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="info-box">
                <span class="info-box-icon bg-red">
                    <i class="ion-ios-calculator-outline"></i>
                </span>
                <div class="info-box-content">
                    <span class="info-box-text">Compras no Mês</span>
                    <asp:Label 
                        runat="server" 
                        ID="lblCompraMes" 
                        Text="0" 
                        CssClass="info-box-number">
                    </asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
