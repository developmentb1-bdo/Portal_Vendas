using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Municipio;

namespace SAPB1.IDAL.Municipio
{
    public interface IMunicipio
    {
        IList<MunicipioDTO> Listar(MunicipioDTO municipioDTO);

        IList<MunicipioDTO> RetornarCodigoMunicipioPorNome(string nome);
    }
}
