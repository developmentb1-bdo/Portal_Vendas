using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Empresa;

namespace SAPB1.DTO.Usuario
{
    /// <summary>
    /// Domínio do Usuário
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>
        /// Id do Usuario
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Senha do Usuário
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Super Usuário Y-Sim N-Não
        /// </summary>
        public string SuperUser { get; set; }

        /// <summary>
        /// Usuário
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string UseName { get; set; }

        public EmpresaDTO Empresa { get; set; }
    }
}
