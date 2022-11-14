using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.PedidoVenda;
using SAPB1.DTO.PedidoVenda;
using SAPB1.IDAL.PedidoVenda;

namespace SAPB1.BLL.PedidoVenda
{
    public class EnderecoBLL
    {
        public EnderecoDTO RetornarEndereco(PedidoVendaDTO pedidoVendaDTO)
        {
            IEndereco enderecoFactory = EnderecoFactory.EnderecoDAL();

            return enderecoFactory.RetonarEndereco(pedidoVendaDTO);
        }
    }
}
