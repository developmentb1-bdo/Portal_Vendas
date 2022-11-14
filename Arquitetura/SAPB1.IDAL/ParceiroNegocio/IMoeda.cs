/*
 * @author Victor Oliveira.
 */

using System.Collections.Generic;
using SAPB1.DTO.ParceiroNegocio;

namespace SAPB1.IDAL.ParceiroNegocio
{
    /// <summary>
    /// Tabela OCRN SAP B1.
    /// </summary>
    public interface IMoeda
    {
        IList<MoedaDTO> Listar();
    }
}