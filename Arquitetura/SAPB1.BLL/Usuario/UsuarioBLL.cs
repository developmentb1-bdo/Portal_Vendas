using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Usuario;
using SAPB1.DALFactory.Usuario;
using SAPB1.IDAL.Usuario;

namespace SAPB1.BLL.Usuario
{
    public class UsuarioBLL
    {
        private readonly IUsuario _usuario;

        public UsuarioBLL()
        {
            _usuario = UsuarioFactory.UsuarioDAL();
        }

        public string RetornarCodigoVideoYoutubeDoUsuarioPortal(string usuario)
        {
            return _usuario.RetornarCodigoVideoYoutubeDoUsuarioPortal(usuario);
        }
    }
}
