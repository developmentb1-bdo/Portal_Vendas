/*
 * @author Victor Oliveira.
 */

using System;

namespace SAPB1.DTO.Servico
{
    /// <summary>
    /// Tabela do SAP Business One OINS.
    /// </summary>
    public class CartaoEquipamentoDTO
    {
        public CartaoEquipamentoDTO() { }

        public int insID { get; set; }
        public string customer { get; set; }
        public string custmrName { get; set; }
        public int contactCod { get; set; }
        public string directCsmr { get; set; }
        public string drctCsmNam { get; set; }
        public string manufSN { get; set; }
        public string internalSN { get; set; }
        public char warranty { get; set; }
        public DateTime wrrntyStrt { get; set; }
        public DateTime wrrntyEnd { get; set; }
        public short responsVal { get; set; }
        public char responsUnt { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public short itemGroup { get; set; }
        public DateTime manufDate { get; set; }
        public int delivery { get; set; }
        public int deliveryNo { get; set; }
        public int invoice { get; set; }
        public int invoiceNum { get; set; }
        public DateTime dlvryDate { get; set; }
        public string cntctPhone { get; set; }
        public string street { get; set; }
        public string block { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string instLction { get; set; }
        public int contract { get; set; }
        public DateTime cntrctStrt { get; set; }
        public DateTime cntrctEnd { get; set; }
        public string attachment { get; set; }
        public string objType { get; set; }
        public int logInstanc { get; set; }
        public short userSign { get; set; }
        public DateTime createDate { get; set; }
        public short userSign2 { get; set; }
        public DateTime updateDate { get; set; }
        public string Building { get; set; }
        public char status { get; set; }
        public int replcIns { get; set; }
        public int repByIns { get; set; }
        public int technician { get; set; }
        public int territory { get; set; }
        public int AtcEntry { get; set; }
        public char Transfered { get; set; }
        public string AddrType { get; set; }
        public short Instance { get; set; }
        public string StreetNo { get; set; }
    }
}