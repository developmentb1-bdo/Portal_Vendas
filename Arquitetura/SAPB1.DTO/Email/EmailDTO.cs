using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Email
{
    public class EmailDTO
    {
        public string Remetente { get; set; }

        public List<string> Destinatario { get; set; }

        public List<string> Copia { get; set; }

        public int Porta { get; set; }

        public string Smtp { get; set; }

        public bool IsHtml { get; set; }

        public string Mensagem { get; set; }

        public string Titulo { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public bool IsSsl { get; set; }
    }
}
