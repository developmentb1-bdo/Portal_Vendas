using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Deposito;
using SAPB1.DTO.Item;

namespace SAPB1.DTO.Estoque
{
    public class EstoqueDTO
    {
        /// <summary>
        /// Classe do Depósto
        /// </summary>
        public DepositoDTO Deposito { get; set; }

        /// <summary>
        /// Classe do Item
        /// </summary>
        public ItemDTO Item { get; set; }

        /// <summary>
        /// Em estoque
        /// </summary>
        public double OnHand { get; set; }

        /// <summary>
        /// Confirmado
        /// </summary>
        public double IsCommited { get; set; }

        /// <summary>
        /// Em Pedido
        /// </summary>
        public double OnOrder { get; set; }

        /// <summary>
        /// Disponível. Campo tem que ser calculado
        /// </summary>
        public double Disponivel { get; set; }

        public string BPLid { get; set; }
    }

    public class EstoqueConsulta
    {
        /// <summary>
        /// Classe do Depósto
        /// </summary>
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string WhsCode { get; set; }
        public int Comprimento { get; set; }
        public int TotalPecas { get; set; }
        public double EstoqueDisponivel { get; set; }
        public double EstoqueReservado { get; set; }
        public double PesoUnitario { get; set; }
        public string PrecoMinimo { get; set; }
        public string PrecoMaximo { get; set; }
        public string Lote { get; set; }
        public string GrupoItem { get; set; }
        public double EntradaPrevista { get; set; }


    }
}
