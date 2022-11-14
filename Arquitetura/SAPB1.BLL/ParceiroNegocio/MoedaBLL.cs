using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.ParceiroNegocio;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;

namespace SAPB1.BLL.ParceiroNegocio
{
    public class MoedaBLL
    {
        public IList<MoedaDTO> Listar()
        {
            IMoeda moedaDAL = ParceiroNegocioFactory.MoedaDAL();

            return moedaDAL.Listar();
        }
    }
}
