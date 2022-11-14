using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Servicos;
using SAPB1.DTO.Servico;

namespace SAPB1.WebForm
{
    public partial class CartaoEquipamentoCliente : System.Web.UI.Page
    {
        private readonly CartaoEquipamentoBLL _cartaoEquipamentoBLL;

        public CartaoEquipamentoCliente()
        {
            _cartaoEquipamentoBLL = new CartaoEquipamentoBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                gridCartaoEquipamento.DataSource = _cartaoEquipamentoBLL.Listar();
                gridCartaoEquipamento.DataBind();
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {

        }

        protected void btnCarregarTudo_Click(object sender, EventArgs e)
        {
            gridCartaoEquipamento.PageIndex = 0;
            gridCartaoEquipamento.DataSource = _cartaoEquipamentoBLL.Listar();
            gridCartaoEquipamento.DataBind();
        }

        protected void gridCartaoEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCartaoEquipamento.PageIndex = e.NewPageIndex;
            gridCartaoEquipamento.DataSource = _cartaoEquipamentoBLL.Listar();
            gridCartaoEquipamento.DataBind();
        }
    }
}