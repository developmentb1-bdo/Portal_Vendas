using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DALFactory.Funcionario;
using SAPB1.DTO.Funcionario;
using SAPB1.IDAL.Funcionario;

namespace SAPB1.BLL.Funcionario
{
    public class ColaboradorBLL
    {
        private readonly IColaborador _colaborador;

        public ColaboradorBLL()
        {
            _colaborador = ColaboradorFactory.ColaboradorDAL();
        }

        public ColaboradorDTO SelecionarColaboradorPorId(int empId)
        {
            return _colaborador.SelecionarColaboradorPorId(empId);
        }

        public ColaboradorDTO SelecionarColaboradorPorUsuarioESenha(string usuario, string senha)
        {
            return _colaborador.SelecionarColaboradorPorUsuarioESenha(usuario, senha);
        }
    }
}
