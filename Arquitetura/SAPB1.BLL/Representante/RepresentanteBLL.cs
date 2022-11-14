using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.Representante;
using SAPB1.DTO.Representante;
using SAPB1.IDAL.Representante;

namespace SAPB1.BLL.Representante
{
    public class RepresentanteBLL
    {
        public IList<RepresentanteDTO> Listar()
        {
            IRepresentante representanteDAL = RepresentanteFactory.RepresentanteDAL();

            return representanteDAL.Listar();
        }
    }
}
