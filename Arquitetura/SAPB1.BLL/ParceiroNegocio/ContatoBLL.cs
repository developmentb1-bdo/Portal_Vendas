/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using SAPB1.DALFactory.ParceiroNegocio;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.BLL.ParceiroNegocio
{
    public class ContatoBLL
    {
        public ContatoBLL() { }

        public IList<ContatoDTO> Listar(string cardCode)
        {
            IList<ContatoDTO> listContatoDTO = new List<ContatoDTO>();

            try
            {
                IContato contatoDAL = ParceiroNegocioFactory.ContatoDAL();
                listContatoDTO = contatoDAL.Listar(cardCode);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            return listContatoDTO;
        }
    }
}