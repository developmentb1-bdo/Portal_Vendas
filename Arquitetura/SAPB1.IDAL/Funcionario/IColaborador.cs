using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Funcionario;

namespace SAPB1.IDAL.Funcionario
{
    public interface IColaborador
    {
        ColaboradorDTO SelecionarColaboradorPorId(int empId);

        ColaboradorDTO SelecionarColaboradorPorUsuarioESenha(string usuario, string senha);
    }
}
