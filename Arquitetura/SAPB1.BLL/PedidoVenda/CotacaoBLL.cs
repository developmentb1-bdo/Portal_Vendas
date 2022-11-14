/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SAPB1.BLL.Item;
using SAPB1.BLL.SAP.Web.Services.WsIntegra;
using SAPB1.DALFactory.PedidoVenda;
using SAPB1.DTO.PedidoVenda;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.BLL.PedidoVenda
{
    public class CotacaoBLL
    {
        public CotacaoBLL() { }

        public string Resultado { get; private set; }

        public bool EditarInserir(CotacaoDTO cotacaoDTO)
        {
            try
            {
                WsIntegraSoapClient wsIntegra = new WsIntegraSoapClient();
                Message message = wsIntegra.AddSalesQuotation("1", Xml(cotacaoDTO));

                Resultado = ((message.Error != null) ? message.Error.ErrMsg : "");

                return string.IsNullOrEmpty(Resultado);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public IList<CotacaoDTO> Listar()
        {
            try
            {
                ICotacao cotacaoDAL = CotacaoFactory.CotacaoDAL();

                return cotacaoDAL.Listar();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public CotacaoDTO Selecionar(int docEntry)
        {
            try
            {
                ICotacao cotacaoDAL = CotacaoFactory.CotacaoDAL();

                return cotacaoDAL.Selecionar(docEntry);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        string Xml(CotacaoDTO cotacaoDTO)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<BOM>");
            xml.Append("<BO>");
            xml.Append("<AdmInfo>");
            xml.Append("<Object>23</Object>");
            xml.Append("<Version>2</Version>");
            xml.Append("</AdmInfo>");
            xml.Append("<Documents>");
            xml.Append("<row>");
            xml.Append("<DocEntry>" + cotacaoDTO.DocEntry.ToString() + "</DocEntry>");
            xml.Append("<DocNum>" + cotacaoDTO.DocNum.ToString() + "</DocNum>");
            xml.Append("<DocType>dDocument_Items</DocType>");
            xml.Append("<HandWritten>tNO</HandWritten>");
            xml.Append("<Printed>psNo</Printed>");
            xml.Append("<DocDate>" + cotacaoDTO.DocDate.ToString("yyyyMMdd") + "</DocDate>");
            xml.Append("<CardCode>" + cotacaoDTO.CardCode.Trim() + "</CardCode>");
            xml.Append("<CardName>" + cotacaoDTO.CardName.ToUpper().Trim() + "</CardName>");
            xml.Append("<Address>" + cotacaoDTO.Address.ToUpper().Trim() + "</Address>");
            xml.Append("<BPL_IDAssignedToInvoice>" + cotacaoDTO.BPLId.ToString() + "</BPL_IDAssignedToInvoice>");
            xml.Append("<TransportationCode>" + cotacaoDTO.TrnspCode.ToString() + "</TransportationCode>");
            xml.Append("<PaymentGroupCode>" + cotacaoDTO.PaymentGroupCode + "</PaymentGroupCode>");
            //xml.Append("<DocTotal>" + cotacaoDTO.DocTotal.ToString("0.000000", CultureInfo.InvariantCulture) + "</DocTotal>"); //SAP Business One utiliza como padrão seis casas decimais para tipo [decimal/C#] e [numeric/SQL].
            xml.Append("<DocCurrency>R$</DocCurrency>");
            xml.Append("<U_S7_CobrarFrete>" + cotacaoDTO.U_S7_CobrarFrete + "</U_S7_CobrarFrete>");
            xml.Append("<U_S7_TaxaFrete>" + cotacaoDTO.U_S7_TaxaFrete.ToString("0.000000", CultureInfo.InvariantCulture) + "</U_S7_TaxaFrete>");
            xml.Append("<U_S7_ValorFrete>" + cotacaoDTO.U_S7_ValorFrete.ToString("0.000000", CultureInfo.InvariantCulture) + "</U_S7_ValorFrete>");
            xml.Append("<DocRate>1.000000</DocRate>");
            xml.Append("<Reference1>1</Reference1>");
            xml.Append("<Comments>" + cotacaoDTO.Comments.ToString() + "</Comments>");
            xml.Append("</row>");
            xml.Append("</Documents>");
            xml.Append("<Document_Lines>");

            for (int i = 0; i < cotacaoDTO.Itens.Count; i++)
            {
                xml.Append("<row>");
                xml.Append("<LineNum>" + i.ToString() + "</LineNum>");
                xml.Append("<ItemCode>" + cotacaoDTO.Itens[i].ItemCode.ToUpper().Trim() + "</ItemCode>");
                //xml.Append("<ItemDescription>" + cotacaoDTO.Itens[i].Dscription.ToUpper().Trim() + "</ItemDescription>");
                xml.Append("<Quantity>" + cotacaoDTO.Itens[i].Quantity.ToString("0.000000", CultureInfo.InvariantCulture) + "</Quantity>");
                xml.Append("<U_Metros>" + cotacaoDTO.Itens[i].Quantity.ToString("0.000000", CultureInfo.InvariantCulture) + "</U_Metros>");
                xml.Append("<UnitPrice>" + cotacaoDTO.Itens[i].Price.ToString("0.000000", CultureInfo.InvariantCulture) + "</UnitPrice>");
                /*xml.Append("<DiscountPercent>" + cotacaoDTO.Itens[i].DiscPrcnt.ToString("0.000000", CultureInfo.InvariantCulture) + "</DiscountPercent>");*/
                xml.Append("<U_Peso>" + cotacaoDTO.Itens[i].U_Peso.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_Peso>");
                xml.Append("<U_Comprimento2>" + cotacaoDTO.Itens[i].Comprimento.ToString("0.##").Replace(".", "").Replace(",", ".") + "</U_Comprimento2>");
                xml.Append("<U_Pecas>" + cotacaoDTO.Itens[i].QtdBarra.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_Pecas>");
                xml.Append("<MeasureUnit>" + cotacaoDTO.Itens[i].unitMsr + "</MeasureUnit>");
                xml.Append("<LineTotal>" + (cotacaoDTO.Itens[i].Price*(cotacaoDTO.Itens[i].U_Peso == 0 ? cotacaoDTO.Itens[i].Quantity : cotacaoDTO.Itens[i].U_Peso)).ToString("0.000000", CultureInfo.InvariantCulture) + "</LineTotal>");
                xml.Append("<Currency>R$</Currency>");
                xml.Append("<Rate>0.000000</Rate>");
                /*xml.Append("<DiscountPercent>0.000000</DiscountPercent>");*/
                xml.Append("<Usage>" + cotacaoDTO.Itens[i].Usage.ToString() + "</Usage>");
                xml.Append("<WarehouseCode>" + new ItemBLL().Listar(new DTO.Item.ItemDTO() { SellItem = "Y" }).Where(x => x.ItemCode == cotacaoDTO.Itens[i].ItemCode).ToList()[0].DfltWH + "</WarehouseCode>");
                xml.Append("</row>");
            }

            xml.Append("</Document_Lines>");

            if (!string.IsNullOrEmpty(cotacaoDTO.Carrier))
            {
                xml.Append("<TaxExtension>");
                xml.Append("<row>");
                xml.Append("<Carrier>" + cotacaoDTO.Carrier.Trim() + "</Carrier>");
                xml.Append("</row>");
                xml.Append("</TaxExtension>");
            }

            xml.Append("</BO>");
            xml.Append("</BOM>");

            return xml.ToString();
        }
    }
}