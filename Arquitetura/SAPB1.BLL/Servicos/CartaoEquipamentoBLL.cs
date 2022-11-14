using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Servico;
using SAPB1.DALFactory.Servico;
using SAPB1.IDAL.Servico;

namespace SAPB1.BLL.Servicos
{
    public class CartaoEquipamentoBLL
    {
        private readonly ICartaoEquipamento _cartaoEquipamento;

        public CartaoEquipamentoBLL()
        {
            _cartaoEquipamento = ServicoFactory.CartaoEquipamentoDAL();
        }

        public CartaoEquipamentoDTO Selecionar(int insId)
        {
            return _cartaoEquipamento.Selecionar(insId);
        }

        public IList<CartaoEquipamentoDTO> Listar()
        {
            return _cartaoEquipamento.Listar();
        }
    }
}
