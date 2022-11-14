using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.IDAL.Funcionario.Vendedor;
using SAPB1.DALFactory.Funcionario.Vendedor;
using SAPB1.DTO.Funcionario.Vendedor;

namespace SAPB1.BLL.Funcionario.Vendedor
{
    public class VendedorBLL
    {
        public IList<VendedorDTO> Listar(VendedorDTO vendedorDTO)
        {
            IVendedor vendedorDAL = VendedorFactory.VendedorDAL();

            return vendedorDAL.Listar(vendedorDTO);
        }
    }
}
