/*
 * @author Victor Oliveira.
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.WebForm.App_Code;
using SAPB1.DTO.Item;
using SAPB1.DTO.Servico;
using SAPB1.BLL.Servicos;
using SAPB1.DTO.Estado;
using SAPB1.DTO.Administracao.Configuracao;
using SAPB1.DTO.Municipio;
using SAPB1.DTO.Empregado;

namespace SAPB1.Web.Modulos.Servico
{
    public partial class CartaoEquipamentoCliente_Action : System.Web.UI.Page
    {
        private int _insId = 0;
        private readonly CartaoEquipamentoBLL _cartaoEquipamentoBLL;


        public CartaoEquipamentoCliente_Action()
        {
            _cartaoEquipamentoBLL = new CartaoEquipamentoBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = 0;

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    _insId = Convert.ToInt32(Request.QueryString["id"]);

                    CarregarDados();
                }
                else
                {

                }

                Combo.Status(cmbStatus, "0", Combo.SelecioneOpcao);
                Combo.ParceiroNegocio(ddlCodigoCliente, "0");
                Combo.ParceiroNegocio(ddlCodigoClienteVenda, "0");

                ItemDTO itemDTO = new ItemDTO();
                itemDTO.SellItem = "Y";
                Combo.Itens(ddlNumItem, "0", itemDTO);

                EmpregadoDTO empregadoDTO = new EmpregadoDTO();
                empregadoDTO.Active = "Y";
                empregadoDTO.Posicao = new PosicaoDTO();
                empregadoDTO.Posicao.PosId = 6;
                Combo.Empregado(ddlTecnico, "0", empregadoDTO);
                Combo.Territorio(ddlTerritorio, "0");
            }
        }

        private void CarregarDados()
        {
            CartaoEquipamentoDTO cartaoEquipamentoDTO = _cartaoEquipamentoBLL.Selecionar(_insId);
            
            txtNumSeriFabri.Text = cartaoEquipamentoDTO.manufSN;
            txtNumSerie.Text = cartaoEquipamentoDTO.internalSN;
            ddlNumItem.SelectedValue = cartaoEquipamentoDTO.itemCode;
            txtDescrItem.Text = cartaoEquipamentoDTO.itemName;

            ddlCodigoCliente.SelectedValue = cartaoEquipamentoDTO.customer;
            txtNomeCliente.Text = cartaoEquipamentoDTO.custmrName;
            ddlTecnico.SelectedValue = cartaoEquipamentoDTO.technician.ToString();
            ddlTerritorio.SelectedValue = cartaoEquipamentoDTO.territory.ToString();

            txtTipoLogradouro.Text = cartaoEquipamentoDTO.AddrType;
            txtRua.Text = cartaoEquipamentoDTO.street;
            txtNumeroRua.Text = cartaoEquipamentoDTO.StreetNo;
            txtComplemento.Text = cartaoEquipamentoDTO.Building;
            txtBairro.Text = cartaoEquipamentoDTO.block;
            txtCidade.Text = cartaoEquipamentoDTO.city;
            txtCep.Text = cartaoEquipamentoDTO.zip;

            EstadoDTO estadoDTO = new EstadoDTO();
            estadoDTO.Pais = new PaisDTO();
            estadoDTO.Pais.Name = cartaoEquipamentoDTO.country;
            Combo.Estado(ddlEstado, cartaoEquipamentoDTO.state, estadoDTO);

            MunicipioDTO municipioDTO = new MunicipioDTO();
            municipioDTO.Estado = new EstadoDTO();
            municipioDTO.Estado.Code = cartaoEquipamentoDTO.state;
            Combo.Municipio(ddlMunicipio, cartaoEquipamentoDTO.county, municipioDTO);

            Combo.Pais(ddlPais, cartaoEquipamentoDTO.country);

            txtLocal.Text = cartaoEquipamentoDTO.instLction;

            ddlCodigoClienteVenda.SelectedValue = cartaoEquipamentoDTO.directCsmr;
            txtNomeClienteVenda.Text = cartaoEquipamentoDTO.drctCsmNam;
        }
    }
}