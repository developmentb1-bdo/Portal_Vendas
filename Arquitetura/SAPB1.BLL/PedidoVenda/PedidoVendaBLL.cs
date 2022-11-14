using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.PedidoVenda;
using SAPB1.DTO.PedidoVenda;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.PedidoVenda;
using SAPB1.BLL.SAP.Web.Services.WsIntegra;

namespace SAPB1.BLL.PedidoVenda
{
    public class PedidoVendaBLL
    {
        IPedidoVenda pedidoVendaDAL = PedidoVendaFactory.PedidoVendaDAL();

        public IList<PedidoVendaDTO> Listar(PedidoVendaDTO pedidoVendaDTO)
        {
            return pedidoVendaDAL.Listar(pedidoVendaDTO);
        }

        public double RetornarValorTotalPorMes(DateTime dataInicial, DateTime dataFinal)
        {
            return pedidoVendaDAL.RetornarValorTotalPorMes(dataInicial, dataFinal);
        }

        public string InseriPedidoVenda(PedidoVendaDTO pedidoVendaDTO, IList<ItemVendaDTO> listItens)
        {
            Message messege = new Message();
            WsIntegraSoapClient wsIntegra = new WsIntegraSoapClient();

            PedidoVendaXml pedidoVendaXml = new PedidoVendaXml();

            messege = wsIntegra.AddSalesOrder("1", pedidoVendaXml.MontarXmlPedidoVenda(pedidoVendaDTO, listItens));
     
            if(messege.Error ==null)
                return "";
            else
            {
                return "Ocorreu um erro ao inserir o pedido de venda. " + messege.Error.ErrCode.ToString() + " - " + messege.Error.ErrMsg;
            }
        }

        public IList<PedidoVendaDTO> BuscarPedidoVenda(PedidoVendaDTO pedidoVendaDTO)
        {
            return pedidoVendaDAL.BuscarPedidoVenda(pedidoVendaDTO);
        }

        public string RetornarCodigoTransportadora(long docNum)
        {
            return pedidoVendaDAL.RetornarCodigoTransportadora(docNum);
        }

        public double RetornarValorDespesaFrete(long docNum)
        {
            return pedidoVendaDAL.RetornarValorDespesaFrete(docNum);
        }
    }
}
