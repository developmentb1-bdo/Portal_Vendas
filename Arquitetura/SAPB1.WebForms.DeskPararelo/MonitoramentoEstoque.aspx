<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitoramentoEstoque.aspx.cs" Inherits="SAPB1.WebForms.DeskPararelo.MonitoramentoEstoque" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Monitoramento do Estoque</title>

        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

        <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
        <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
        <link href="dist/css/AdminLTE.min.css" rel="stylesheet" />
        <link href="dist/css/skins/_all-skins.min.css" rel="stylesheet" />
        <script src="plugins/jQuery/jQuery-2.1.4.min.js"></script>

        <script type="text/javascript">
            function Atualizar() {
                window.location.reload();
            }
        </script>
    </head>
    <body class="hold-transition login-page" onload="setTimeout('Atualizar()', 60000)">
        <form id="form1" runat="server">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">Dados do Estoque</h3>
                </div>
                <div class="box-body">
                    <div class="table-responsive">
                        <div class="dataTables_wrapper form-inline dt-bootstrap">
                            <asp:Literal
                                runat="server"
                                ID="ltlTabelaEstoque">
                            </asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </form>

        
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <link href="plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" />
        <script src="plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>

        <style>
            .table-striped>tbody>tr:nth-child(odd)>td, 
            .table-striped>tbody>tr:nth-child(odd)>th {
                background-color: #9e9e9e;
            }
        </style>

        <script type="text/javascript">
            $(document).ready(function () {
                $('#tblTab').DataTable({
                    "language": {
                        "lengthMenu": "Mostrando _MENU_ registros por página",
                        "zeroRecords": "Nenhum registro para Mostar",
                        "info": "Mostrando Página _PAGE_ de _PAGES_",
                        "infoEmpty": "Nenhum registro para Mostrar",
                        "infoFiltered": "(Filtrando de _MAX_ total de registros)",
                        "search": "Pesquisar",
                        "paginate": {
                            "first": "Primeiro",
                            "last": "Último",
                            "next": "Próximo",
                            "previous": "Anterior"
                        }
                    },
                    responsive: true
                });
            });
        </script>
    </body>
</html>
