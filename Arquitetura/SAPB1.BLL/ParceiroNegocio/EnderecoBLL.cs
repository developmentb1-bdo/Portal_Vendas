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
    public class EnderecoBLL
    {
        public EnderecoBLL() { }

        public IList<EnderecoDTO> Listar(string cardCode)
        {
            IList<EnderecoDTO> listEnderecoDTO = new List<EnderecoDTO>();

            try
            {
                IEndereco enderecoDAL = ParceiroNegocioFactory.EnderecoDAL();
                listEnderecoDTO = enderecoDAL.Listar(cardCode);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            return listEnderecoDTO;
        }
    }
}