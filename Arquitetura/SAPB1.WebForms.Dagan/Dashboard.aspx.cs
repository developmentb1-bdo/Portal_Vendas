using SAPB1.BLL.Compras;
using SAPB1.BLL.Estoque;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.BLL.PedidoVenda;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.WebForms.Dagan.App_Code;
using System;

namespace SAPB1.WebForms.Dagan
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!IsPostBack)
            //    {
            //        ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

            //        ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
            //        parceiroNegocioDTO.CardType = "C";

            //        lblQuantidadeClientes.Text = "Clientes: " + parceiroNegocioBLL.RetornarQtdParceiroNegocio(parceiroNegocioDTO).ToString();

            //        EstoqueBLL estoqueBLL = new EstoqueBLL();
            //        lblEstoqueValor.Text = estoqueBLL.RetornarTotalValorEstoque().ToString("c");

            //        PedidoVendaBLL pedidoVendaBLL = new PedidoVendaBLL();
            //        lblVendasNoMes.Text = pedidoVendaBLL.RetornarValorTotalPorMes(DateTime.Now, DateTime.Now).ToString("c");

            //        CompraBLL compraBLL = new CompraBLL();
            //        lblCompraMes.Text = compraBLL.RetornarValorCompras(DateTime.Now, DateTime.Now).ToString("c");
            //    }
            //}
            //catch (Exception er)
            //{
            //    DTO.Mensagens.MensagemDTO mensagemDTO = new DTO.Mensagens.MensagemDTO();
            //    mensagemDTO.Mensagem = er.Message;
            //    mensagemDTO.Tipo = DTO.Mensagens.MensagemType.Erro;

            //    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            //}
        }
    }
}