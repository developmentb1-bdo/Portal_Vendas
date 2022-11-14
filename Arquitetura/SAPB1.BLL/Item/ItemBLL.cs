using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Item;
using SAPB1.DTO.Item;
using SAPB1.DALFactory.Item;

namespace SAPB1.BLL.Item
{
    public class ItemBLL
    {
        public IList<ItemDTO> Listar(ItemDTO itemDTO)
        {
            IItem itemDAL = ItemFactory.ItemDAL();

            return itemDAL.Listar(itemDTO);
        }

        public IList<ItemDTO> BuscarItemPorId(ItemDTO itemDTO)
        {
            IItem itemDAL = ItemFactory.ItemDAL();

            return itemDAL.BuscarItemPorId(itemDTO);
        }

        public IList<ItemDTO> BuscarInfoItem(ItemDTO itemDTO)
        {
            IItem itemDAL = ItemFactory.ItemDAL();

            return itemDAL.BuscarInfoItem(itemDTO);
        }

        public IList<ItemDTO> BuscarInfoQtd(ItemDTO itemDTO)
        {
            IItem itemDAL = ItemFactory.ItemDAL();

            return itemDAL.BuscarInfoQtd(itemDTO);
        }

        public IList<ItemDTO> ListarPorCategoria(ItemDTO itemDTO, List<string> listCategorias)
        {
            IItem itemDAL = ItemFactory.ItemDAL();

            return itemDAL.ListarPorCategoria(itemDTO, listCategorias);

        }
    }
}
