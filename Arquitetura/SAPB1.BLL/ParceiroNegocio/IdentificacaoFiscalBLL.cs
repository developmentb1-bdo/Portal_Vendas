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
    public class IdentificacaoFiscalBLL
    {
        public IList<IdentificacaoFiscalDTO> Listar(string cardCode)
        {
            IList<IdentificacaoFiscalDTO> listIdentificacaoDTO = new List<IdentificacaoFiscalDTO>();

            try
            {
                IIdentificacaoFiscal identificacaoFiscalDAL = ParceiroNegocioFactory.IdentificacaoFiscalDAL();
                listIdentificacaoDTO = identificacaoFiscalDAL.Listar(cardCode);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            return listIdentificacaoDTO;
        }
    }
}