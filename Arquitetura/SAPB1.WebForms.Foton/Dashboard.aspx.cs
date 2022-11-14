using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.BLL.Estoque;
using SAPB1.BLL.PedidoVenda;
using SAPB1.BLL.Compras;
using SAPB1.WebForm.App_Code;

namespace SAPB1.WebForms.Foton
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

                    ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                    parceiroNegocioDTO.CardType = "C";

                    lblQuantidadeClientes.Text = "Clientes: " + parceiroNegocioBLL.RetornarQtdParceiroNegocio(parceiroNegocioDTO).ToString();

                    EstoqueBLL estoqueBLL = new EstoqueBLL();
                    lblEstoqueValor.Text = estoqueBLL.RetornarTotalValorEstoque().ToString("c");

                    PedidoVendaBLL pedidoVendaBLL = new PedidoVendaBLL();
                    lblVendasNoMes.Text = pedidoVendaBLL.RetornarValorTotalPorMes(DateTime.Now, DateTime.Now).ToString("c");

                    CompraBLL compraBLL = new CompraBLL();
                    lblCompraMes.Text = compraBLL.RetornarValorCompras(DateTime.Now, DateTime.Now).ToString("c");
                }
            }
            catch (Exception er)
            {
                DTO.Mensagens.MensagemDTO mensagemDTO = new DTO.Mensagens.MensagemDTO();
                mensagemDTO.Mensagem = er.Message;
                mensagemDTO.Tipo = DTO.Mensagens.MensagemType.Erro;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }
    }
}