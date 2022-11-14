<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="LoginCliente.aspx.cs" 
    Inherits="SAPB1.WebForm.LoginCliente" 
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>PORTAL DE VENDAS</title>

        <!--Define o charset da página-->
        <meta charset="utf-8"/>

        <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
        <meta name="description" content=""/>

        <!--Tag que define a resposividade da página-->
        <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1"/>

        <!--Scripts CSS-->
        <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css"/>
        <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css"/>
        <link href="dist/css/AdminLTE.min.css" rel="stylesheet" />
        <link href="plugins/iCheck/square/blue.css" rel="stylesheet" />
    </head>
    <body class="hold-transition login-page" style="background-color:#dd4b39;">
        <div class="login-box">
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
                        ID="ds" 
                        Text="fddfsf">
                    </asp:Label>
            </asp:Panel>
            <div class="login-box-body">
                <div class="login-logo">
                    <img src="Imagens/logo-tempo.png" width="150" height="85"/>
                </div>
                <p class="login-box-msg">Faça o login para iniciar sua sessão</p>
                <form id="form1" runat="server">
                    <div class="form-group has-feedback">
                        <asp:TextBox
                            runat="server"
                            ID="txtCpfCnpj"
                            ClientIDMode="Static"
                            CssClass="form-control"
                            MaxLength="18">
                        </asp:TextBox>
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:TextBox
                            runat="server"
                            ID="txtSenha"
                            ClientIDMode="Static"
                            TextMode="Password"
                            CssClass="form-control"
                            MaxLength="50">
                        </asp:TextBox>
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    </div>
                    <div class="row">
                        <div class="col-xs-8">
                        </div>
                        <div class="col-xs-4">
                          <asp:Button
                              runat="server"
                              ID="btnEntrar"
                              Text="Entrar" 
                              CssClass="btn btn-warning btn-block btn-flat" 
                              OnClick="btnEntrar_Click"/>
                        </div>
                    </div>
                </form>
            </div>

            <img src="Imagens/imgProdutos.png" width="360" height="250"/>
        </div>

        <!--Scripts-->
        <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>
       
    </body>
</html>
