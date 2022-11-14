<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/FotonMaster.Master" 
    AutoEventWireup="true" 
    CodeBehind="DocumentoTecnico_Action.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.DocumentoTecnico_Action" 
%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <asp:Literal
                    runat="server"
                    ID="ltrCatalogos">
                </asp:Literal>
            </div>
        </div>
    </section>
</asp:Content>
