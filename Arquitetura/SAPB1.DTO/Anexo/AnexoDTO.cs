using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Anexo
{
    [Serializable]
    public class AnexoDTO
    {
        public string AbsEntry { get; set; }

        public string Line { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime Date { get; set; }

        public string Extensao { get; set; }

        public string Caminho { get; set; }
    }
}
