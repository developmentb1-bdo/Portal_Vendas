using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPB1.DTO.Servico
{
    public class TransacaoDTO
    {
        /// <summary>
        /// TransID
        /// </summary>
        public int TransId { get; set; }

        /// <summary>
        /// DocNum
        /// </summary>
        public int DocNum { get; set; }

        /// <summary>
        /// DocLine
        /// </summary>
        public int DocLine { get; set; }

        /// <summary>
        /// DocDate
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// CardCode
        /// </summary>
        public string CardCode { get; set; }

        /// <summary>
        /// CardName
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// Direction
        /// </summary>
        public string Direction { get; set; }

    }
}
