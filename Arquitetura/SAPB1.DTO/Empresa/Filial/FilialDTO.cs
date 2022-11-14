using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Empresa.Filial
{
    public class FilialDTO
    {
        /// <summary>
        /// Id da filial
        /// </summary>
        public int BPLId { get; set; }

        /// <summary>
        /// Razão Social da empresa
        /// </summary>
        public string BPLName { get; set; }

        /// <summary>
        /// Nome fantasia/Nome Reduzido
        /// </summary>
        public string BPLFrName { get; set; }

        /// <summary>
        /// CNPJ
        /// </summary>
        public string TaxIdNum { get; set; }

        /// <summary>
        /// CNPJ 2
        /// </summary>
        public string TaxIdNum2 { get; set; }

        /// <summary>
        /// CNPJ 3
        /// </summary>
        public string TaxIdNum3 { get; set; }

        /// <summary>
        /// Fiial principal Y - Sim N-Não
        /// </summary>
        public string MainBPL { get; set; }

        /// <summary>
        /// Filial desabilitada Y-Sim N-Não
        /// </summary>
        public string Disabled { get; set; }

        public string U_Matriz { get; set; }

        public string U_MatrizGrupo { get; set; }
    }
}
