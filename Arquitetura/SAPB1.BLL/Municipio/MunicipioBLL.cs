using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Municipio;
using SAPB1.DTO.Municipio;
using SAPB1.DALFactory.Municipio;

namespace SAPB1.BLL.Municipio
{
    public class MunicipioBLL
    {
        public IList<MunicipioDTO> Listar(MunicipioDTO municipioDTO)
        {
            IMunicipio municipioDAL = MunicipioFactory.MunicipioDAL();

            return municipioDAL.Listar(municipioDTO);
        }

        public IList<MunicipioDTO> RetornarCodigoMunicipioPorNome(string nome)
        {
            IMunicipio municipioDAL = MunicipioFactory.MunicipioDAL();

            return municipioDAL.RetornarCodigoMunicipioPorNome(nome);
        }
    }
}
