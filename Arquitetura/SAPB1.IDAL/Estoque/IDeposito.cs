/*
 * @author Victor Oliveira.
 */

using System.Collections.Generic;
using SAPB1.DTO.Deposito;

namespace SAPB1.IDAL.Estoque
{
    public interface IDeposito
    {
        IList<DepositoDTO> Listar();
    }
}