/*
 * @author Victor Oliveira.
 */

using System.Collections.Generic;
using SAPB1.DTO.ParceiroNegocio;

namespace SAPB1.IDAL.ParceiroNegocio
{
    public interface IParceiroNegocio
    {
        IList<ParceiroNegocioDTO> Listar();
        ParceiroNegocioDTO Selecionar(string cardCode);

        IList<ParceiroNegocioDTO> Listar(ParceiroNegocioDTO parceiroNegocioDTO);

        int RetornarQtdParceiroNegocio(ParceiroNegocioDTO parceiroNegocioDTO);

        IList<ParceiroNegocioDTO> Buscar(ParceiroNegocioDTO parceiroNegocioDTO);

        ParceiroNegocioDTO RetornarParceiroNegocioPorCnpjESenha(string cpnj, string senha);

        ParceiroNegocioDTO RetornarParceiroNegocioPorCpfESenha(string cpf, string senha);
    }
}