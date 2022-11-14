using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Funcionario.Vendedor;

namespace SAPB1.IDAL.Funcionario.Vendedor
{
    public interface IVendedor
    {
        IList<VendedorDTO> Listar(VendedorDTO vendedorDTO);
    }
}
