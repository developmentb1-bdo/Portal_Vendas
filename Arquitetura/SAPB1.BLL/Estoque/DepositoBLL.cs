/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using SAPB1.DALFactory.Estoque;
using SAPB1.DTO.Deposito;
using SAPB1.IDAL.Estoque;

namespace SAPB1.BLL.Estoque
{
    public class DepositoBLL
    {
        public DepositoBLL() { }

        public IList<DepositoDTO> Listar()
        {
            try
            {
                IDeposito depositoDAL = EstoqueFactory.DepositoDAL();

                return depositoDAL.Listar();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}