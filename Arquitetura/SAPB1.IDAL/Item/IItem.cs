using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Item;
using System.Data.SqlClient;

namespace SAPB1.IDAL.Item
{
    public interface IItem
    {
        IList<ItemDTO> Listar(ItemDTO itemDTO);

        IList<ItemDTO> BuscarItemPorId(ItemDTO itemDTO);

        IList<ItemDTO> BuscarInfoItem(ItemDTO itemDTO);
        IList<ItemDTO> BuscarInfoQtd(ItemDTO itemDTO);

        IList<ItemDTO> ListarPorCategoria(ItemDTO itemDTO, List<string> listCategorias);
    }
}
