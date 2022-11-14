/*
 * @author Victor Oliveira.
 */

using System;
using System.Web.UI.WebControls;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.DTO.CondicaoPagamento;
using SAPB1.BLL.CondicaoPagamento;
using SAPB1.DTO.FormasPagamento;
using SAPB1.BLL.FormasPagamento;
using SAPB1.DTO.TiposEnvio;
using SAPB1.BLL.TiposEnvio;
using SAPB1.DTO.Utilizacao;
using SAPB1.BLL.Utilizacao;
using SAPB1.DTO.Funcionario.Vendedor;
using SAPB1.BLL.Funcionario.Vendedor;
using SAPB1.DTO.Empresa.Filial;
using SAPB1.BLL.Empresa.Filial;
using SAPB1.BLL.Administracao.Configuracao;
using SAPB1.BLL.Estado;
using SAPB1.DTO.Estado;
using SAPB1.BLL.Municipio;
using SAPB1.DTO.Municipio;
using SAPB1.DTO.Empregado;
using SAPB1.DTO.Projeto;
using SAPB1.BLL.Empregado;
using SAPB1.BLL.Projeto;
using SAPB1.BLL.Utilizacao.Cfop;
using SAPB1.DTO.Utilizacao.Cfop;
using SAPB1.DTO.Item;
using SAPB1.BLL.Item;
using SAPB1.BLL.Representante;
using SAPB1.BLL.SetorIndustrial;
using SAPB1.BLL.Territorio;
using SAPB1.BLL.Estoque;
using System.Linq;

namespace SAPB1.WebForm.App_Code
{
    public static class Combo
    {
        public const string SelecioneOpcao = "<-- Selecione -->";
        public const string TodosOpcao = "<-- Todos -->";

        public static void Estoque(DropDownList dropDown, string valor)
        {
            try
            {
                EstoqueBLL estoqueBLL = new EstoqueBLL();
                var x = estoqueBLL.Listar(null).Select(y => new { y.Deposito.WhsCode, y.Deposito.WhsName }).ToList();

                dropDown.Items.Clear();
                dropDown.AppendDataBoundItems = true;
                dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
                dropDown.DataSource = x;
                dropDown.DataValueField = "WhsCode";
                dropDown.DataTextField = "WhsName";
                dropDown.DataBind();
                dropDown.SelectedValue = valor;
            }
            catch (Exception)
            {
                dropDown.SelectedValue = "0";
            }
        }

        public static void Filial(DropDownList dropDown, string valor)
        {
            try
            {
                FilialBLL filialBLL = new FilialBLL();

                dropDown.Items.Clear();
                dropDown.AppendDataBoundItems = true;
                dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
                dropDown.DataSource = filialBLL.Listar(new FilialDTO() { Disabled = "N" });
                dropDown.DataValueField = "BPLId";
                dropDown.DataTextField = "BPLName";
                dropDown.DataBind();
                dropDown.SelectedValue = valor;
            }
            catch (Exception)
            {
                dropDown.SelectedValue = "0";
            }
        }

        public static void Grupo(DropDownList dropDown, string valor, GroupType groupType)
        {
            try
            {
                GrupoBLL grupoBLL = new GrupoBLL();

                dropDown.Items.Clear();
                dropDown.AppendDataBoundItems = true;
                dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
                dropDown.DataSource = grupoBLL.Listar(groupType);
                dropDown.DataValueField = "GroupCode";
                dropDown.DataTextField = "GroupName";
                dropDown.DataBind();
                dropDown.SelectedValue = valor;
            }
            catch (Exception)
            {
                dropDown.SelectedValue = "0";
            }
        }

        public static void Moeda(DropDownList dropDown, string valor)
        {
            try
            {
                MoedaBLL moedaBLL = new MoedaBLL();

                dropDown.Items.Clear();
                dropDown.AppendDataBoundItems = true;
                dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
                dropDown.DataSource = moedaBLL.Listar();
                dropDown.DataValueField = "CurrCode";
                dropDown.DataTextField = "CurrName";
                dropDown.DataBind();
                dropDown.SelectedValue = valor;
            }
            catch (Exception)
            {
                dropDown.SelectedValue = "0";
            }
        }

        public static void CondicaoPagamento(DropDownList dropDown, CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            CondicaoPagamentoBLL condicaoPagamentoBLL = new CondicaoPagamentoBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = condicaoPagamentoBLL.Listar(condicaoPagamentoDTO);
            dropDown.DataValueField = "GroupNum";
            dropDown.DataTextField = "PymntGroup";
            dropDown.DataBind();

            if (condicaoPagamentoDTO.GroupNum != 0)
            {
                dropDown.SelectedValue = condicaoPagamentoDTO.GroupNum.ToString();
            }
        }

        public static void FormaPagamento(DropDownList dropDown, FormaPagamentoDTO formaPagamentoDTO)
        {
            FormaPagamentoBLL formaPagamentoBLL = new FormaPagamentoBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = formaPagamentoBLL.Listar(formaPagamentoDTO);
            dropDown.DataValueField = "PayMethCod";
            dropDown.DataTextField = "Descript";
            dropDown.DataBind();

            if(!string.IsNullOrEmpty(formaPagamentoDTO.PayMethCod))
            {
                dropDown.SelectedValue = formaPagamentoDTO.PayMethCod;
            }
        }

        public static void TiposEnvio(DropDownList dropDown, TipoEnvioDTO tipoEnvioDTO)
        {
            TipoEnvioBLL tipoEnvioBLL = new TipoEnvioBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "-1"));
            dropDown.DataSource = tipoEnvioBLL.Listar(tipoEnvioDTO);
            dropDown.DataValueField = "TrnspCode";
            dropDown.DataTextField = "TrnspName";
            dropDown.DataBind();

            if(tipoEnvioDTO.TrnspCode !=0)
            {
                dropDown.SelectedValue = tipoEnvioDTO.TrnspCode.ToString();
            }
        }

        public static void Utilizacao (DropDownList dropDown, UtilizacaoDTO utilizacaoDTO)
        {
            UtilizacaoBLL utilizacaoBLL = new UtilizacaoBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = utilizacaoBLL.Listar(utilizacaoDTO);
            dropDown.DataValueField = "ID";
            dropDown.DataTextField = "Usage";
            dropDown.DataBind();

            if(utilizacaoDTO.ID !=0)
            {
                dropDown.SelectedValue = utilizacaoDTO.ID.ToString();
            }
        }

        public static void Vendedor(DropDownList dropDown, string valor, VendedorDTO vendedorDTO)
        {
            VendedorBLL vendedorBLL = new VendedorBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = vendedorBLL.Listar(vendedorDTO);
            dropDown.DataValueField = "SlpCode";
            dropDown.DataTextField = "SlpName";
            dropDown.DataBind();

            if (!string.IsNullOrEmpty(valor))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Filial(DropDownList dropDown, string valor, FilialDTO filialDTO)
        {
            FilialBLL filialBLL = new FilialBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = filialBLL.Listar(filialDTO);
            dropDown.DataValueField = "BPLId";
            dropDown.DataTextField = "BPLName";
            dropDown.DataBind();

            if(!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Idioma(DropDownList dropDown, string valor)
        {
            IdiomaBLL idiomaBLL = new IdiomaBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = idiomaBLL.Listar();
            dropDown.DataValueField = "Code";
            dropDown.DataTextField = "Name";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Estado(DropDownList dropDown, string valor, EstadoDTO estadoDTO)
        {
            EstadoBLL estadoBLL = new EstadoBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = estadoBLL.Listar(estadoDTO);
            dropDown.DataValueField = "Code";
            dropDown.DataTextField = "Name";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Municipio(DropDownList dropDown, string valor, MunicipioDTO municipioDTO)
        {
            MunicipioBLL municipioBLL = new MunicipioBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = municipioBLL.Listar(municipioDTO);
            dropDown.DataValueField = "AbsId";
            dropDown.DataTextField = "Name";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void ParceiroNegocio(DropDownList dropDown, string valor)
        {
            ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = parceiroNegocioBLL.Listar();
            dropDown.DataValueField = "CardCode";
            dropDown.DataTextField = "CardName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void ParceiroNegocio(DropDownList dropDown, string valor, ParceiroNegocioDTO parceiroNegocioDTO)
        {
            ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = parceiroNegocioBLL.Listar(parceiroNegocioDTO);
            dropDown.DataValueField = "CardCode";
            dropDown.DataTextField = "CardName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Projeto(DropDownList dropDown, string valor, ProjetoDTO projetoDTO)
        {
            ProjetoBLL projetoBLL = new ProjetoBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = projetoBLL.Listar(projetoDTO);
            dropDown.DataValueField = "PrjCode";
            dropDown.DataTextField = "PrjName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Empregado(DropDownList dropDown, string valor, EmpregadoDTO empregadoDTO)
        {
            EmpregadoBLL empregadoBLL = new EmpregadoBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = empregadoBLL.Listar(empregadoDTO);
            dropDown.DataValueField = "EmpID";
            dropDown.DataTextField = "LastName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Cfop(DropDownList dropDown, string valor, CfopDTO cfopDTO)
        {
            CfopBLL cfopBLL = new CfopBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.DataSource = cfopBLL.Listar(cfopDTO);
            dropDown.DataValueField = "Code";
            dropDown.DataTextField = "Descrip";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Itens(DropDownList dropDown, string valor, ItemDTO itemDTO)
        {
            ItemBLL itemBLL = new ItemBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.DataSource = itemBLL.Listar(itemDTO);
            dropDown.DataValueField = "ItemCode";
            dropDown.DataTextField = "ItemName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Pais(DropDownList dropDown, string valor)
        {
            PaisBLL paisBLL = new PaisBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.DataSource = paisBLL.Listar();
            dropDown.DataValueField = "CntCodNum";
            dropDown.DataTextField = "Name";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = "1058";
            }
        }

        public static void Representante(DropDownList dropDown, string valor)
        {
            RepresentanteBLL representanteBLL = new RepresentanteBLL();

            dropDown.Items.Clear();
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.AppendDataBoundItems = true;
            dropDown.DataSource = representanteBLL.Listar();
            dropDown.DataValueField = "AgentCode";
            dropDown.DataTextField = "AgentName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void SetorIndustrial(DropDownList dropDown, string valor)
        {
            SetorIndustrialBLL setorIndustrialBLL = new SetorIndustrialBLL();

            dropDown.Items.Clear();
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.AppendDataBoundItems = true;
            dropDown.DataSource = setorIndustrialBLL.Listar();
            dropDown.DataValueField = "IndCode";
            dropDown.DataTextField = "IndName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Status(DropDownList dropDown, string valor, string opcao)
        {
            try
            {
                dropDown.Items.Clear();
                dropDown.AppendDataBoundItems = true;
                dropDown.Items.Add(new ListItem(opcao, "0"));
                dropDown.Items.Add(new ListItem("Ativo", "1"));
                dropDown.Items.Add(new ListItem("Devolvido", "2"));
                dropDown.Items.Add(new ListItem("Encerrado", "3"));
                dropDown.Items.Add(new ListItem("Emprestado", "4"));
                dropDown.Items.Add(new ListItem("No laboratório de reparo", "5"));
                dropDown.DataBind();
                dropDown.SelectedValue = ((valor.Equals("0")) ? "1" : "0");
            }
            catch (Exception)
            {
                dropDown.SelectedValue = "0";
            }
        }

        public static void Territorio(DropDownList dropDown, string valor)
        {
            TerritorioBLL territorioBLL = new TerritorioBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.DataSource = territorioBLL.Listar();
            dropDown.DataValueField = "TerritryId";
            dropDown.DataTextField = "Descript";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }

        public static void Transportadora(DropDownList dropDown, string valor, ParceiroNegocioDTO parceiroNegocioDTO)
        {
            ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

            dropDown.Items.Clear();
            dropDown.AppendDataBoundItems = true;
            dropDown.Items.Add(new ListItem("<-- Selecione -->", "0"));
            dropDown.DataSource = parceiroNegocioBLL.Buscar(parceiroNegocioDTO);
            dropDown.DataValueField = "CardCode";
            dropDown.DataTextField = "CardName";
            dropDown.DataBind();

            if (!valor.Equals("0"))
            {
                dropDown.SelectedValue = valor;
            }
        }
    }
}