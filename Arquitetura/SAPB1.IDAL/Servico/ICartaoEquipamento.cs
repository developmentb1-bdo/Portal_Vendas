/*
 * @author Victor Oliveira.
 */

using SAPB1.DTO.Servico;
using System.Collections;
using System.Collections.Generic;

namespace SAPB1.IDAL.Servico
{
    /// <summary>
    /// Tabela do SAP Business One OINS.
    /// </summary>
    public interface ICartaoEquipamento
    {
        CartaoEquipamentoDTO Selecionar(int insID);

        IList<CartaoEquipamentoDTO> Listar();
    }
}