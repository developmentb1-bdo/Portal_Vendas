/*
 * @author Victor Oliveira.
 */

using System.Collections.Generic;
using SAPB1.DTO.ParceiroNegocio;

namespace SAPB1.IDAL.ParceiroNegocio
{
    public interface IContato
    {
        IList<ContatoDTO> Listar(string cardCode);
    }
}