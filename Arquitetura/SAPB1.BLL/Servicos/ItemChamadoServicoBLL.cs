using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.DALFactory.Servico;
using SAPB1.IDAL.Servico;

namespace SAPB1.BLL.Servicos
{
    public class ItemChamadoServicoBLL
    {
        private readonly IItemChamadoServico _itemChamado;

        public ItemChamadoServicoBLL()
        {
            _itemChamado = ItensChamadoFactory.ItensChamadoServicoDAL();
        }

        public IList<ItemChamadoServicoDTO> ListarPorIdChamado(int callId)
        {
            IList<ItemChamadoServicoDTO> listItens = _itemChamado.ListarPorIdChamado(callId);

            if(listItens.Count > 0)
            {
                foreach(ItemChamadoServicoDTO item in listItens)
                {
                    if (item.U_Price > 0 && item.U_Quantity > 0)
                        item.Total = item.U_Quantity * item.U_Price;
                }
            }

            return listItens;
        }
    }
}
