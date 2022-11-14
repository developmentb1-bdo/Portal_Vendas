/*
 * @author Victor Oliveira.
 */

using System.Collections.Generic;
using SAPB1.DTO.Administracao;

namespace SAPB1.IDAL.Administracao
{
    public interface IFilial
    {
        IList<FilialDTO> Listar();
    }
}