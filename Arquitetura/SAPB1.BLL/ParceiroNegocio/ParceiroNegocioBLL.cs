/*
 * @author Victor Oliveira.
 */

using System;
using System.Collections.Generic;
using System.Text;
using SAPB1.BLL.SAP.Web.Services.WsIntegra;
using SAPB1.DALFactory.ParceiroNegocio;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.IDAL.ParceiroNegocio;
using System.Xml;

namespace SAPB1.BLL.ParceiroNegocio
{
    public class ParceiroNegocioBLL
    {
        public ParceiroNegocioBLL() { }

        public string ErrorMessege { get; private set; }


        public bool Inserir(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            try
            {
                Message messege = new Message();
                WsIntegraSoapClient wsIntegra = new WsIntegraSoapClient();

                string a = ConvertToXml(parceiroNegocioDTO, false);
                messege = wsIntegra.AddBusinessPartner("1", ConvertToXml(parceiroNegocioDTO, false));

                // Solução provisória, para inserir registros com referência em outras tabelas.
                //if (!string.IsNullOrEmpty(messege.Result))
                //{
                //parceiroNegocioDTO.CardCode = messege.Result;
                //messege = new Message();
                //messege = wsIntegra.AddBusinessPartner("1", ConvertToXml(parceiroNegocioDTO, true));
                //}

                ErrorMessege = ((messege.Error != null) ? messege.Error.ErrMsg : "");

                if (!string.IsNullOrEmpty(messege.Result))
                    return true;
                else
                    return false;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public bool Editar(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            try
            {
                Message messege = new Message();
                WsIntegraSoapClient wsIntegra = new WsIntegraSoapClient();
                messege = wsIntegra.GetBusinessPartnerByKey("1", parceiroNegocioDTO.CardCode);

                //messege = wsIntegra.AddBusinessPartner("1", ConvertToXml(parceiroNegocioDTO, true));

                ErrorMessege = ((messege.Error != null) ? messege.Error.ErrMsg : "");

                if (!string.IsNullOrEmpty(messege.Result))
                {
                    string xmlAtualizado = AtualizarNosPn(parceiroNegocioDTO, messege.Result);

                    messege = new Message();
                    messege = wsIntegra.AddBusinessPartner("1", xmlAtualizado);

                    return true;
                }
                else
                    return false;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public IList<ParceiroNegocioDTO> Listar()
        {
            IList<ParceiroNegocioDTO> listParceiroNegocioDTO = new List<ParceiroNegocioDTO>();

            try
            {
                IParceiroNegocio parceiroNegocioDAL = ParceiroNegocioFactory.ParceiroNegocioDAL();
                listParceiroNegocioDTO = parceiroNegocioDAL.Listar();
                return listParceiroNegocioDTO;
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        public IList<ParceiroNegocioDTO> Listar(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            IParceiroNegocio parceiroNegocioDAL = ParceiroNegocioFactory.ParceiroNegocioDAL();

            return parceiroNegocioDAL.Listar(parceiroNegocioDTO);
        }

        public ParceiroNegocioDTO Selecionar(string cardCode)
        {
            ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();

            try
            {
                IParceiroNegocio parceiroNegocioDAL = ParceiroNegocioFactory.ParceiroNegocioDAL();
                parceiroNegocioDTO = parceiroNegocioDAL.Selecionar(cardCode);

                EnderecoBLL enderecoBLL = new EnderecoBLL();
                parceiroNegocioDTO.ListEndereco = enderecoBLL.Listar(parceiroNegocioDTO.CardCode);

                ContatoBLL contatoBLL = new ContatoBLL();
                parceiroNegocioDTO.ListContato = contatoBLL.Listar(parceiroNegocioDTO.CardCode);

                IdentificacaoFiscalBLL identificacaoFiscalBLL = new IdentificacaoFiscalBLL();
                parceiroNegocioDTO.ListIdentificacaoFiscal = identificacaoFiscalBLL.Listar(parceiroNegocioDTO.CardCode);
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            return parceiroNegocioDTO;
        }

        private string ReturnCardType(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            if (parceiroNegocioDTO.CardType.ToString() != "cLid")
            {
                return parceiroNegocioDTO.SlpCode.ToString();
            }
            else
            {
                return "-1";
            }
        }

        private string ConvertToXml(ParceiroNegocioDTO parceiroNegocioDTO, bool update)
        {
            StringBuilder xml = new StringBuilder();

            try
            {
                //Inicio da tag BOM
                xml.Append("<BOM>");

                //Inicio da tag BO
                xml.Append("<BO>");
                xml.Append("<AdmInfo>");
                xml.Append("<Object>2</Object>");
                xml.Append("<Version>2</Version>");
                xml.Append("</AdmInfo>");

                if (parceiroNegocioDTO.CardType != "cLid")
                {
                    xml.Append("<BusinessPartners>");
                    xml.Append("<row>");
                    xml.Append("<CardName>" + parceiroNegocioDTO.CardName + "</CardName>");
                    xml.Append("<CardType>" + parceiroNegocioDTO.CardType + "</CardType>");
                    xml.Append("<GroupCode>" + parceiroNegocioDTO.GroupCode + "</GroupCode>");
                    xml.Append("<Series>" + parceiroNegocioDTO.Series.ToString() + "</Series>");
                    xml.Append("<Phone1>" + parceiroNegocioDTO.Phone1 + "</Phone1>");
                    xml.Append("<Phone2>" + parceiroNegocioDTO.Phone2 + "</Phone2>");

                    if (parceiroNegocioDTO.CardType != "cSupplier")
                        xml.Append("<SalesPersonCode>" + parceiroNegocioDTO.SlpCode.ToString() + "</SalesPersonCode>");
                    else
                        xml.Append("<SalesPersonCode>6</SalesPersonCode>");

                    xml.Append("<Fax>" + parceiroNegocioDTO.Fax + "</Fax>");
                    xml.Append("<PayTermsGrpCode>" + parceiroNegocioDTO.GroupNum.ToString() + "</PayTermsGrpCode>");
                    xml.Append("<CreditLimit>" + parceiroNegocioDTO.CreditLine.ToString("n6") + "</CreditLimit>");
                    xml.Append("<FreeText>" + parceiroNegocioDTO.Free_Text + "</FreeText>");
                    xml.Append("<Currency>" + parceiroNegocioDTO.Currency + "</Currency>");
                    xml.Append("<Cellular>" + parceiroNegocioDTO.Cellular + "</Cellular>");
                    xml.Append("<Country>" + parceiroNegocioDTO.Country + "</Country>");
                    xml.Append("<EmailAddress>" + parceiroNegocioDTO.E_Mail + "</EmailAddress>");
                    xml.Append("<PeymentMethodCode />");
                    xml.Append("<Valid>tYES</Valid>");
                    xml.Append("<Frozen>tNO</Frozen>");
                    xml.Append("<Website>" + parceiroNegocioDTO.IntrntSite + "</Website>");
                    xml.Append("</row>");
                    xml.Append("</BusinessPartners>");

                    xml.Append("<BPAddresses>");

                    for (int i = 0; i < parceiroNegocioDTO.ListEndereco.Count; i++)
                    {
                        xml.Append("<row>");
                        xml.Append("<AddressName>" + parceiroNegocioDTO.ListEndereco[i].Address + "</AddressName>");
                        xml.Append("<Street>" + parceiroNegocioDTO.ListEndereco[i].Street + "</Street>");
                        xml.Append("<Block>" + parceiroNegocioDTO.ListEndereco[i].Block + "</Block>");

                        if (string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].ZipCode))
                            xml.Append("<ZipCode>00000000</ZipCode>");
                        else
                            xml.Append("<ZipCode>" + parceiroNegocioDTO.ListEndereco[i].ZipCode + "</ZipCode>");

                        xml.Append("<City>" + parceiroNegocioDTO.ListEndereco[i].City + "</City>");
                        xml.Append("<County>" + parceiroNegocioDTO.ListEndereco[i].County + "</County>");
                        xml.Append("<Country>" + parceiroNegocioDTO.ListEndereco[i].Country + "</Country>");
                        xml.Append("<State>" + parceiroNegocioDTO.ListEndereco[i].State + "</State>");
                        xml.Append("<BuildingFloorRoom>" + parceiroNegocioDTO.ListEndereco[i].Building + "</BuildingFloorRoom>");

                        if (parceiroNegocioDTO.ListEndereco[i].AdresType == 'B')
                            xml.Append("<AddressType>bo_BillTo</AddressType>");
                        else
                            xml.Append("<AddressType>bo_ShipTo</AddressType>");

                        xml.Append("<TypeOfAddress>" + parceiroNegocioDTO.ListEndereco[i].AddrType + "</TypeOfAddress>");
                        xml.Append("<StreetNo>" + parceiroNegocioDTO.ListEndereco[i].StreetNo + "</StreetNo>");
                        xml.Append("<RowNum>" + i.ToString() + "</RowNum>");

                        xml.Append("</row>");
                    }

                    xml.Append("</BPAddresses>");

                    if (parceiroNegocioDTO.ListContato.Count > 0)
                    {
                        xml.Append("<ContactEmployees>");
                        xml.Append("<row>");
                        xml.Append("<Name>" + parceiroNegocioDTO.ListContato[0].Name + "</Name>");
                        xml.Append("<Phone1>" + parceiroNegocioDTO.ListContato[0].Tel1 + "</Phone1>");
                        xml.Append("<E_Mail>" + parceiroNegocioDTO.ListContato[0].E_MailL + "</E_Mail>");
                        xml.Append("<Remarks1>" + parceiroNegocioDTO.ListContato[0].Notes1 + "</Remarks1>");
                        xml.Append("<Active>tYES</Active>");
                        xml.Append("</row>");
                        xml.Append("</ContactEmployees>");
                    }

                    xml.Append("<BPFiscalTaxID>");
                    xml.Append("<row>");
                    xml.Append("<Address/>");
                    xml.Append("<TaxId0>" + parceiroNegocioDTO.U_CNPJ + "</TaxId0>");
                    xml.Append("</row>");
                    xml.Append("<row>");
                    xml.Append("<Address>" + parceiroNegocioDTO.ListEndereco[0].Address + "</Address>");
                    xml.Append("<AddrType>bo_BillTo</AddrType>");
                    xml.Append("</row>");
                    xml.Append("<row>");
                    xml.Append("<Address>" + parceiroNegocioDTO.ListEndereco[1].Address + "</Address>");
                    xml.Append("<TaxId0>" + parceiroNegocioDTO.U_CNPJ + "</TaxId0>");
                    xml.Append("<AddrType>bo_ShipTo</AddrType>");
                    xml.Append("</row>");
                    xml.Append("</BPFiscalTaxID>");

                    xml.Append("<BPBankAccounts>");
                    xml.Append("<row>");
                    xml.Append("<LogInstance>0</LogInstance>");
                    xml.Append("<State/>");
                    xml.Append("<Country>BR</Country>");
                    xml.Append("<BankCode>237</BankCode>");
                    xml.Append("<AccountNo>436</AccountNo>");
                    xml.Append("</row>");
                    xml.Append("</BPBankAccounts>");
                }
                else
                {
                    xml.Append("<BusinessPartners>");
                    xml.Append("<row>");
                    xml.Append("<CardName>" + parceiroNegocioDTO.CardName + "</CardName>");
                    xml.Append("<CardType>" + parceiroNegocioDTO.CardType + "</CardType>");
                    xml.Append($@"<Series>{parceiroNegocioDTO.Series}</Series>");
                    xml.Append($@"<SalesPersonCode>{ReturnCardType(parceiroNegocioDTO)}</SalesPersonCode>");
                    xml.Append("<Valid>tYES</Valid>");
                    xml.Append("</row>");
                    xml.Append("</BusinessPartners>");

                    if (parceiroNegocioDTO.ListContato.Count > 0)
                    {
                        xml.Append("<ContactEmployees>");
                        xml.Append("<row>");
                        xml.Append("<Name>" + parceiroNegocioDTO.ListContato[0].Name + "</Name>");
                        xml.Append("<Phone1>" + parceiroNegocioDTO.ListContato[0].Tel1 + "</Phone1>");
                        xml.Append("<E_Mail>" + parceiroNegocioDTO.ListContato[0].E_MailL + "</E_Mail>");
                        xml.Append("<Remarks1>" + parceiroNegocioDTO.ListContato[0].Notes1 + "</Remarks1>");
                        xml.Append("<Active>tYES</Active>");
                        xml.Append("</row>");
                        xml.Append("</ContactEmployees>");
                    }

                    if (parceiroNegocioDTO.ListEndereco.Count > 0)
                    {
                        xml.Append("<BPAddresses>");

                        for (int i = 0; i < parceiroNegocioDTO.ListEndereco.Count; i++)
                        {
                            xml.Append("<row>");
                            xml.Append("<AddressName>" + parceiroNegocioDTO.ListEndereco[i].Address + "</AddressName>");
                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].Street))
                                xml.Append("<Street>" + parceiroNegocioDTO.ListEndereco[i].Street + "</Street>");

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].Block))
                                xml.Append("<Block>" + parceiroNegocioDTO.ListEndereco[i].Block + "</Block>");

                            if (string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].ZipCode))
                                xml.Append("<ZipCode>00000000</ZipCode>");
                            else
                                xml.Append("<ZipCode>" + parceiroNegocioDTO.ListEndereco[i].ZipCode + "</ZipCode>");

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].City))
                                xml.Append("<City>" + parceiroNegocioDTO.ListEndereco[i].City + "</City>");
                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].County))
                                xml.Append("<County>" + parceiroNegocioDTO.ListEndereco[i].County + "</County>");
                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].Country))
                                xml.Append("<Country>" + parceiroNegocioDTO.ListEndereco[i].Country + "</Country>");

                            xml.Append("<State>" + parceiroNegocioDTO.ListEndereco[i].State + "</State>");

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].Building))
                                xml.Append("<BuildingFloorRoom>" + parceiroNegocioDTO.ListEndereco[i].Building + "</BuildingFloorRoom>");

                            if (parceiroNegocioDTO.ListEndereco[i].AdresType == 'B')
                                xml.Append("<AddressType>bo_BillTo</AddressType>");
                            else
                                xml.Append("<AddressType>bo_ShipTo</AddressType>");

                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].AddrType))
                                xml.Append("<TypeOfAddress>" + parceiroNegocioDTO.ListEndereco[i].AddrType + "</TypeOfAddress>");
                            if (!string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[i].StreetNo))
                                xml.Append("<StreetNo>" + parceiroNegocioDTO.ListEndereco[i].StreetNo + "</StreetNo>");
                            xml.Append("<RowNum>" + i.ToString() + "</RowNum>");

                            xml.Append("</row>");
                        }

                        xml.Append("</BPAddresses>");
                    }

                    //xml.Append("<BPFiscalTaxID>");
                    //xml.Append("<row>");
                    //xml.Append("<Address/>");
                    //xml.Append("<TaxId0>" + parceiroNegocioDTO.U_CNPJ + "</TaxId0>");
                    //xml.Append("</row>");
                    //xml.Append("</BPFiscalTaxID>");
                }

                xml.Append("</BO>");
                xml.Append("</BOM>");

                return xml.ToString();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }

        private string AtualizarNosPn(ParceiroNegocioDTO parceiroNegocioDTO, string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            XmlNode nosPnRow = xmlDocument.SelectSingleNode("/BOM/BO/BusinessPartners/row");
            XmlNodeList listNosPn = nosPnRow.ChildNodes;

            foreach (XmlNode noPn in listNosPn)
            {
                if (noPn.NodeType == XmlNodeType.Element)
                {
                    switch (noPn.Name)
                    {
                        case "CardName":
                            noPn.InnerText = parceiroNegocioDTO.CardName;
                            continue;
                        case "GroupCode":
                            noPn.InnerText = parceiroNegocioDTO.GroupCode.ToString();
                            continue;
                        case "Phone1":
                            noPn.InnerText = parceiroNegocioDTO.Phone1;
                            continue;
                        case "Phone2":
                            noPn.InnerText = parceiroNegocioDTO.Phone2;
                            continue;
                        case "Fax":
                            noPn.InnerText = parceiroNegocioDTO.Fax;
                            continue;
                        case "CreditLimit":
                            noPn.InnerText = parceiroNegocioDTO.CreditLine.ToString();
                            continue;
                        case "EmailAddress":
                            noPn.InnerText = parceiroNegocioDTO.E_Mail;
                            continue;
                    }
                }

                XmlNodeList listNoEnderecos = xmlDocument.SelectNodes("/BOM/BO/BPAddresses/row");
                int contadorEndereco = 0;

                foreach (XmlNode noRowEndereco in listNoEnderecos)
                {
                    if (noRowEndereco.NodeType == XmlNodeType.Element)
                    {
                        if (noRowEndereco.Name.Equals("row"))
                        {
                            XmlNodeList listNoEnderecoRow = noRowEndereco.ChildNodes;

                            foreach (XmlNode noEnd in listNoEnderecoRow)
                            {
                                if (noEnd.NodeType == XmlNodeType.Element)
                                {
                                    switch (noEnd.Name)
                                    {
                                        case "AddressName":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].Address;
                                            continue;
                                        case "Street":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].Street;
                                            continue;
                                        case "ZipCode":
                                            if (string.IsNullOrEmpty(parceiroNegocioDTO.ListEndereco[contadorEndereco].ZipCode))
                                                noEnd.InnerText = "00000000";
                                            else
                                                noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].ZipCode;
                                            continue;
                                        case "City":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].City;
                                            continue;
                                        case "County":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].County;
                                            continue;
                                        case "Country":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].Country;
                                            continue;
                                        case "State":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].State;
                                            continue;
                                        case "BuildingFloorRoom":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].Building;
                                            continue;
                                        case "TypeOfAddress":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].AddrType;
                                            continue;
                                        case "StreetNo":
                                            noEnd.InnerText = parceiroNegocioDTO.ListEndereco[contadorEndereco].StreetNo;
                                            continue;
                                    }
                                }
                            }

                            contadorEndereco += 1;
                        }
                    }
                }

                XmlNodeList listNodeContatos = xmlDocument.SelectNodes("/BOM/BO/ContactEmployees/row");

                int contadorContato = 0;

                foreach (XmlNode noContatoRow in listNodeContatos)
                {
                    if (noContatoRow.NodeType == XmlNodeType.Element)
                    {
                        if (noContatoRow.Name.Equals("row"))
                        {
                            XmlNodeList listNoContatoRow = noContatoRow.ChildNodes;

                            foreach (XmlNode noCont in listNoContatoRow)
                            {
                                if (noCont.NodeType == XmlNodeType.Element)
                                {
                                    switch (noCont.Name)
                                    {
                                        case "Name":
                                            noCont.InnerText = parceiroNegocioDTO.ListContato[contadorContato].Name;
                                            continue;
                                        case "Phone1":
                                            noCont.InnerText = parceiroNegocioDTO.ListContato[contadorContato].Tel1;
                                            continue;
                                        case "E_Mail":
                                            noCont.InnerText = parceiroNegocioDTO.ListContato[contadorContato].E_MailL;
                                            continue;
                                    }
                                }
                            }

                            if (parceiroNegocioDTO.ListContato.Count < listNoContatoRow.Count)
                                break;
                            else
                                contadorContato += 1;
                        }
                    }
                }
            }

            return xmlDocument.InnerXml;
        }

        public int RetornarQtdParceiroNegocio(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            IParceiroNegocio parceiroNegocioDAL = ParceiroNegocioFactory.ParceiroNegocioDAL();

            return parceiroNegocioDAL.RetornarQtdParceiroNegocio(parceiroNegocioDTO);
        }

        public IList<ParceiroNegocioDTO> Buscar(ParceiroNegocioDTO parceiroNegocioDTO)
        {
            IParceiroNegocio parceiroNegocioDAL = ParceiroNegocioFactory.ParceiroNegocioDAL();

            return parceiroNegocioDAL.Buscar(parceiroNegocioDTO);
        }

        public string AutenticarClienteSistemaPortal(string usuario, string senha)
        {
            string retorno = "";

            if (string.IsNullOrEmpty(usuario))
                retorno += "<li>Erro: Usuário está em branco.";

            if (string.IsNullOrEmpty(senha))
                retorno += "<li>Erro: Senha está em branco.";

            if (retorno.Equals(""))
            {
                IParceiroNegocio parceiroNegocioDAL = ParceiroNegocioFactory.ParceiroNegocioDAL();

                string valor = usuario.Replace(".", "").Replace("-", "").Replace("/", "");

                var resultado = new ParceiroNegocioDTO();

                if (valor.Length == 14)
                    resultado = parceiroNegocioDAL.RetornarParceiroNegocioPorCnpjESenha(usuario, senha);
                else if (valor.Length == 11)
                    resultado = parceiroNegocioDAL.RetornarParceiroNegocioPorCpfESenha(usuario, senha);
                else
                    retorno += "<li>Erro: CPF ou CNPJ inválido.";

                if (retorno.Equals(""))
                {
                    if (string.IsNullOrEmpty(resultado.CardCode))
                        retorno += "Login inválido";
                    else
                        retorno = resultado.CardCode;
                }
            }

            return retorno;
        }
    }
}