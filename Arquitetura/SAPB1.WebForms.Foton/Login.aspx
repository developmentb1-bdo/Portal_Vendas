<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="Login.aspx.cs" 
    Inherits="SAPB1.WebForms.Foton.Login" 
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Portal Foton | Login</title>

        <!--Define o charset da página-->
        <meta charset="utf-8">

        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="description" content="">

        <!--Tag que define a resposividade da página-->
        <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1">

        <!--Scripts CSS-->
        <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
        <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
        <link href="dist/css/AdminLTE.min.css" rel="stylesheet" />
        <link href="plugins/iCheck/square/blue.css" rel="stylesheet" />
        <link href="Css/login.css" rel="stylesheet" />
    </head>
    <body class="hold-transition login-page">
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
                        ID="lblAviso" 
                        Text="fddfsf">
                    </asp:Label>
            </asp:Panel>
            <div class="login-logo">
                
            </div>
            <div class="login-box-body">
                <div class="login-logo">
                    <img src="Imagens/logoFoton.png" />
                </div>
                <p class="login-box-msg">Faça o login para iniciar sua sessão</p>
                <form id="form1" runat="server">
                    <div class="form-group has-feedback">
                        <asp:TextBox
                            runat="server"
                            ID="txtEmail"
                            ClientIDMode="Static"
                            CssClass="form-control">
                        </asp:TextBox>
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <asp:TextBox
                            runat="server"
                            ID="txtSenha"
                            ClientIDMode="Static"
                            TextMode="Password"
                            CssClass="form-control">
                        </asp:TextBox>
                        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    </div>
                    <div class="row">
                        <div class="col-xs-12" style="text-align:right">
                          <asp:Button
                              runat="server"
                              ID="btnEntrar"
                              Text="Entrar" 
                              CssClass="btn btn-primary btn-flat" 
                              OnClick="btnEntrar_Click"/>
                        </div>
                    </div>
                </form>
            </div>
        </div>

        <!--Scripts-->
        <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>
        <script src="bootstrap/js/bootstrap.min.js"></script>

    </body>
</html>
