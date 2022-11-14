<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/AbtMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="ListaMateriais.aspx.cs" 
    Inherits="SAPB1.WebForms.Abt.ListaMateriais" 
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
                ID="lblAviso">
            </asp:Label>
     </asp:Panel>
     <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                Exportação Lista de Materiais
            </h3>
            <div class="box-tools pull-right">
               <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
           </div>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group" style="text-align:center;">
                        <asp:Button
                            runat="server"
                            ID="btnExportar"
                            Text="Exportar"
                            CssClass="btn btn-primary" 
                            OnClick="btnExportar_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
