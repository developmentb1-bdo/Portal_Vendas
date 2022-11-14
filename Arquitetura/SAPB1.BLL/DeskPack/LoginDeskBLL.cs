using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.BLL.DI;
using SAPB1.DALFactory.Usuario;
using SAPB1.IDAL.Usuario;

namespace SAPB1.BLL.DeskPack
{
    public class LoginDeskBLL
    {
        public string _erros = "";

        public string Erros
        {
            get { return this._erros; }
        }

        public int AutenticarUsuarioPeloSap(string usuario, string senha)
        {
            //string retorno = FuncoesSapDi.ConnectarDi();

            //int retornoAutenticacao = 0;

            //if (retorno == "0")
            //{
            //    var resultado = FuncoesSapDi._oCompany.AuthenticateUser(usuario, senha);

            //    if (resultado == SAPbobsCOM.AuthenticateUserResultsEnum.aturUsernamePasswordMatch)
            //    {
            //        IUsuario usuarioDAL = UsuarioFactory.UsuarioDAL();
            //        int codUsuario = usuarioDAL.RetornarCodigoUsuarioPorNomeUsuario(usuario);

            //        if(codUsuario > 0)
            //            retornoAutenticacao = codUsuario;
            //        else
            //        {
            //            retornoAutenticacao = 0;

            //            _erros = "Usuário não encontrado";
            //        }
            //    }
            //    else
            //    {
            //        retornoAutenticacao = 0;
            //        _erros = "Não foi possível realizar a autenticação";
            //    }

            //}
            //else
            //{
            //    _erros = retorno;
            //    retornoAutenticacao = 0;
            //}

            //FuncoesSapDi.DesConnDI();

            //return retornoAutenticacao;

            return 0;
        }
    }
}
