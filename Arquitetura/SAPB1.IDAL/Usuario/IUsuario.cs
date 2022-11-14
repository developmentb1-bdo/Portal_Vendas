using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Usuario;

namespace SAPB1.IDAL.Usuario
{
    public interface IUsuario
    {
        int RetornarCodigoUsuarioPorNomeUsuario(string usuario);

        string RetornarCodigoVideoYoutubeDoUsuarioPortal(string usuario);
    }
}
