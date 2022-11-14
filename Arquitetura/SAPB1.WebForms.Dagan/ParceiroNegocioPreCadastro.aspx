<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ParceiroNegocioPreCadastro.aspx.cs" Inherits="SAPB1.WebForms.Dagan.ParceiroNegocioPreCadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField 
        runat="server" 
        ID="hfErros" />
    <asp:Panel 
        runat="server" 
        ID="pnlAviso" 
        CssClass="alert alert-danger"
        Visible="false">
            <h4>
                <i class="icon fa fa-info"></i> 
                Alerta
            </h4>
            <asp:Label 
                runat="server" 
                ID="lblAvisos">
            </asp:Label>
    </asp:Panel>
    <asp:HiddenField
        runat="server"
        ID="hfVendedor" />
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Pré Cadastro Cliente
            </h3>
            <div class="box-tools pull-right">
                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <div class="box-body" style="display: block;">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label>Razão Social</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtRazaoSocial"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                        <div class="form-group">
                        <label>ID de Contato</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtIdContato"
                            CssClass="form-control"
                            MaxLength="50">
                        </asp:TextBox>
                        </div>
                    <div class="form-group">
                        <label>E-mail</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtEmailContato"
                            CssClass="form-control"
                            MaxLength="100">
                        </asp:TextBox>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>DDD</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtDddContato"
                                    CssClass="form-control"
                                    MaxLength="2">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group">
                                <label>Telefone</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtTelefoneContato"
                                    CssClass="form-control"
                                    MaxLength="9">
                                </asp:TextBox>
                                </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Observação:</label>
                                <asp:TextBox
                                    runat="server"
                                    ID="txtObservacaoContato"
                                    TextMode="MultiLine"
                                    Rows="5"
                                    CssClass="form-control"
                                    MaxLength="256">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtCpfCnpj">ID Fiscal CNPJ</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtCpfCnpj"
                            CssClass="form-control">
                        </asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtCpfCnpj">ID Fiscal IE</label>
                        <asp:TextBox
                            runat="server"
                            ID="txtIe"
                            CssClass="form-control">
                        </asp:TextBox>
                        <asp:CheckBox ID="check2" runat="server" TextAlign="Right" Text="Isento" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center;">
            <div class="form-group">
                <asp:Button
                    runat="server"
                    ID="btnSalvar"
                    Text="Salvar" 
                    class="btn btn-warning"
                    OnClick="btnSalvar_Click"/>
            </div>
        </div>
    </div>
    
    <!-- Modal Loader-->
    <div class="modal fade" id="myModalLoader" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabelLoader" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabelLoader">Executando a operação. Aguarde...</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="text-align:center;">
                            <img src="Imagens/5.gif" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                                
                </div>
            </div>
        </div>
    </div>

    <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script type="text/javascript">
        $("<%= btnSalvar.ClientID %>").click(function () {
            $('#myModalLoader').modal('show');
        });
    </script>
</asp:Content>
