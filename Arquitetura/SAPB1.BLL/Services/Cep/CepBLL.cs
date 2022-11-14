using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.ParceiroNegocio;
using System.Data;
using SAPB1.BLL.Municipio;
using SAPB1.DTO.Municipio;

namespace SAPB1.BLL.Services.Cep
{
    public class CepBLL
    {
        public EnderecoDTO RetornarDadosEnderecoPorCep(string cep)
        {
            EnderecoDTO enderecoDTO = new EnderecoDTO();

            string resultado = "";

            if (!string.IsNullOrEmpty(cep))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + cep.Replace("-", "").Trim() + "&formato=xml");

                    if (dataSet != null)
                    {
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            resultado = dataSet.Tables[0].Rows[0]["resultado"].ToString();

                            switch (resultado)
                            {
                                case "1":
                                    enderecoDTO.Address = dataSet.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim() + " " + dataSet.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                                    enderecoDTO.County = dataSet.Tables[0].Rows[0]["bairro"].ToString().Trim();
                                    enderecoDTO.State = dataSet.Tables[0].Rows[0]["uf"].ToString().Trim();
                                    enderecoDTO.City = dataSet.Tables[0].Rows[0]["cidade"].ToString().Trim();
                                    enderecoDTO.AddrType = dataSet.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();

                                    MunicipioBLL municipioBLL = new MunicipioBLL();
                                    IList<MunicipioDTO> municipio = new List<MunicipioDTO>();
                                    municipio = municipioBLL.RetornarCodigoMunicipioPorNome(enderecoDTO.City);

                                    if(municipio.Count > 0)
                                    {
                                        enderecoDTO.CardCode = municipio[0].AbsId.ToString();
                                    }
                                    
                                    break;
                                default:
                                    return null;
                            }
                        }
                    }
                }
                catch (Exception erro)
                {
                    throw new Exception("Erro ao pesquisar o CEP!\n " + erro.Message);
                }
            }

            return enderecoDTO;
        }
    }
}
