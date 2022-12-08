using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.PedidoVenda;
using SAPB1.BLL.Item;

namespace SAPB1.BLL.PedidoVenda
{
    public class PedidoVendaXml
    {
        public string MontarXmlPedidoVenda(PedidoVendaDTO pedidoVendaDTO, IList<ItemVendaDTO> listItens)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("<BOM>");
            stb.Append("<BO>");
            //-------------------------------------------------------------------Cabeçalho----------------------------------------------------------------------------------
            stb.Append("<AdmInfo>");
            stb.Append("<Object>17</Object>");
            stb.Append("<Version>2</Version>");
            stb.Append("</AdmInfo>");
            stb.Append("<Documents>");
            stb.Append("<row>");
            stb.Append("<DocType>dDocument_Items</DocType>");
            stb.Append("<HandWritten>tNO</HandWritten>");
            stb.Append("<DocDate>" + pedidoVendaDTO.DocDate.ToString("yyyyMMdd") + "</DocDate>");
            stb.Append("<TaxDate>" + pedidoVendaDTO.TaxDate.ToString("yyyyMMdd") + "</TaxDate>");
            stb.Append("<DocDueDate>" + pedidoVendaDTO.DocDueDate.ToString("yyyyMMdd") + "</DocDueDate>");
            stb.Append("<CardCode>" + pedidoVendaDTO.CardCode + "</CardCode>");
            stb.Append("<Comments>" + pedidoVendaDTO.Comments + "</Comments>");
            stb.Append("<JournalMemo>" + pedidoVendaDTO.JrnlMemo + "</JournalMemo>");
            stb.Append("<PaymentGroupCode>" + pedidoVendaDTO.PaymentGroupCode + "</PaymentGroupCode>");
            stb.Append("<SalesPersonCode>" + pedidoVendaDTO.Vendedor.SlpCode + "</SalesPersonCode>");
            stb.Append("<BPL_IDAssignedToInvoice>" + pedidoVendaDTO.BPLId.ToString() + "</BPL_IDAssignedToInvoice>");
            if (!string.IsNullOrEmpty(pedidoVendaDTO.OwnerCode))
                stb.Append("<DocumentsOwner>" + pedidoVendaDTO.OwnerCode + "</DocumentsOwner>");

            if (!string.IsNullOrEmpty(listItens.FirstOrDefault().NumeroPedidoCompra))
                stb.Append("<NumAtCard>" + listItens.FirstOrDefault().NumeroPedidoCompra + "</NumAtCard>");

            if (pedidoVendaDTO.TipoEnvio.TrnspCode > 0)
                stb.Append("<TransportationCode>" + pedidoVendaDTO.TipoEnvio.TrnspCode + "</TransportationCode>");

            stb.Append("<U_S7_CobrarFrete>" + pedidoVendaDTO.TemFrete + "</U_S7_CobrarFrete>");
            stb.Append("<U_S7_TaxaFrete>" + pedidoVendaDTO.PercentualFrete.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_S7_TaxaFrete>");
            stb.Append("<U_S7_ValorFrete>" + pedidoVendaDTO.DespesasAdicionais.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_S7_ValorFrete>");

            stb.Append("<DocObjectCode>17</DocObjectCode>");
            stb.Append("<SequenceCode>27</SequenceCode>");
            stb.Append("<SequenceModel>39</SequenceModel>");
            
            stb.Append("</row>");
            stb.Append("</Documents>");
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------

            //------------------------------------------------------------------------Itens---------------------------------------------------------------------------------

            if (listItens != null)
            {
                if (listItens.Count > 0)
                {
                    stb.Append("<Document_Lines>");

                    foreach (ItemVendaDTO i in listItens)
                    {
                        stb.Append("<row>");
                        stb.Append("<ItemCode>" + i.ItemCode + "</ItemCode>");
                        stb.Append("<ItemDescription>" + i.Dscription + "</ItemDescription>");
                        stb.Append("<Quantity>" + i.Quantity + "</Quantity>");
                        stb.Append("<UnitPrice>" + i.Price.ToString("n6").Replace(".", "").Replace(",", ".") + "</UnitPrice>");
                        stb.Append("<DiscountPercent>" + i.DiscPrcnt.ToString("n6").Replace(".", "").Replace(",", ".") + "</DiscountPercent>");
                        stb.Append("<Usage>" + i.Usage + "</Usage>");
                        stb.Append("<WarehouseCode>" + new ItemBLL().Listar(new DTO.Item.ItemDTO() { SellItem = "Y" }).Where(x => x.ItemCode == i.ItemCode).ToList()[0].DfltWH + "</WarehouseCode>");
                        stb.Append("<U_Comprimento2>" + i.Comprimento.ToString("0.##").Replace(".", "").Replace(",", ".") + "</U_Comprimento2>");
                        stb.Append("<U_Pecas>" + i.QtdBarra.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_Pecas>");
                        stb.Append("<U_Lote>" + i.Lote + "</U_Lote>");
                        stb.Append("<U_Metros>" + i.QtdMetro.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_Metros>");
                        stb.Append("<U_Norma>" + i.Norma + "</U_Norma>");
                        stb.Append("<U_Peso>" + i.Peso.ToString("n6").Replace(".", "").Replace(",", ".") + "</U_Peso>");
                        stb.Append("<LineTotal>" + (i.Price * (i.Peso == 0 ? i.Quantity : i.Peso)).ToString("n6").Replace(".", "").Replace(",", ".") + "</LineTotal>");
                        stb.Append("<MeasureUnit>" + i.UnidadeMedida + "</MeasureUnit>");
                        stb.Append("<U_SKILL_NP>" + i.NumeroPedidoCompra + "</U_SKILL_NP>");
                        stb.Append("<U_SKILL_IP>" + i.ItemPedidoCompra + "</U_SKILL_IP>");
                        stb.Append("<FreeText>" + i.DescricaoAuxiliar + "</FreeText>");
                        stb.Append("<ShipDate>" + i.DataEntrega.ToString("yyyyMMdd") + "</ShipDate>");

                        stb.Append("</row>");
                    }

                    stb.Append("</Document_Lines>");
                }
            }

            if (pedidoVendaDTO.DespesasAdicionais > 0)
            {
                stb.Append("<DocumentsAdditionalExpenses>");
                stb.Append("<row>");
                stb.Append("<ExpenseCode>1</ExpenseCode>");
                stb.Append("<LineTotal>" + pedidoVendaDTO.DespesasAdicionais.ToString("n6").Replace(".", "").Replace(",", ".") + "</LineTotal>");
                stb.Append("<DistributionMethod>aedm_RowTotal</DistributionMethod>");
                stb.Append("<TaxLiable>tYES</TaxLiable>");
                stb.Append("<TaxPercent>0.000000</TaxPercent>");
                stb.Append("<TaxSum>0.000000</TaxSum>");
                stb.Append("<DeductibleTaxSum>0.000000</DeductibleTaxSum>");
                stb.Append("<TaxType>aext_NormalTax</TaxType>");
                stb.Append("<EqualizationTaxPercent>0.000000</EqualizationTaxPercent>");
                stb.Append("<EqualizationTaxSum>0.000000</EqualizationTaxSum>");
                stb.Append("<BaseDocEntry>-1</BaseDocEntry>");
                stb.Append("<BaseDocType>-1</BaseDocType>");
                stb.Append("<LineNum>0</LineNum>");
                stb.Append("<LastPurchasePrice>tYES</LastPurchasePrice>");
                stb.Append("<Stock>tYES</Stock>");
                stb.Append("<WTLiable>tNO</WTLiable>");
                stb.Append("<LineGross>200.000000</LineGross>");
                stb.Append("</row>");
                stb.Append("</DocumentsAdditionalExpenses>");
            }

            if (!string.IsNullOrEmpty(pedidoVendaDTO.TransportadoraId) && pedidoVendaDTO.TransportadoraId != "-1" && pedidoVendaDTO.TransportadoraId != "0")
            {
                stb.Append("<TaxExtension>");
                stb.Append("<row>");
                stb.Append("<Carrier>" + pedidoVendaDTO.TransportadoraId + "</Carrier>");
                stb.Append("</row>");
                stb.Append("</TaxExtension>");
            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------

            stb.Append("</BO>");
            stb.Append("</BOM>");
          
            return stb.ToString();
        }
    }
}
