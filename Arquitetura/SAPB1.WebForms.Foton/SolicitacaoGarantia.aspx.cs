using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Servicos;
using SAPB1.DTO.Servico;

namespace SAPB1.WebForms.Foton
{
    public partial class SolicitacaoGarantia : System.Web.UI.Page
    {
        private readonly ChamadoServicoBLL _chamadoBLL;

        public SolicitacaoGarantia()
        {
            _chamadoBLL = new ChamadoServicoBLL();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                hfIdConcessionario.Value = Session["CardCode"].ToString();

                gridCartaoEquipamento.DataSource = _chamadoBLL.ListarChamadoPorCustomer(hfIdConcessionario.Value);
                gridCartaoEquipamento.DataBind();
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            Response.Redirect("SolicitacaoGarantia_Action.aspx");
        }

        protected void btnCarregarTudo_Click(object sender, EventArgs e)
        {
            gridCartaoEquipamento.PageIndex = 0;
            gridCartaoEquipamento.DataSource = _chamadoBLL.ListarChamadoPorCustomer(hfIdConcessionario.Value);
            gridCartaoEquipamento.DataBind();
        }

        protected void gridCartaoEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCartaoEquipamento.PageIndex = e.NewPageIndex;
            gridCartaoEquipamento.DataSource = _chamadoBLL.ListarChamadoPorCustomer(hfIdConcessionario.Value);
            gridCartaoEquipamento.DataBind();
        }

        protected void lkbDetalhesChamadoGrid_Click(object sender, EventArgs e)
        {
            LinkButton lkbGrid = (LinkButton)sender;

            Response.Redirect("SolicitacaoGarantia_Action.aspx?id=" + lkbGrid.CommandArgument);
        }

        protected string RetornarDataFechamento(string valor)
        {
            DateTime data = DateTime.MinValue;

            if (DateTime.TryParse(valor, out data))
            {
                if (data == DateTime.MinValue)
                    return "";
                else
                    return data.ToString("dd/MM/yyyy");
            }
            else
                return "";
        }

        protected string RetornarStatus(string valor)
        {
            switch(valor)
            {
                case "P":
                    return "PENDENTE";
                case "A":
                    return "APROVADO";
                case "R":
                    return "RECUSADO";
                default:
                    return "PENDENTE";
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if(txtNumeroSg.Text.Equals("") && txtDataInicial.Text.Equals("") && txtDataFinal.Text.Equals(""))
            {
                return;
            }
            else
            {
                ChamadoServicoDTO chamadoDTO = new ChamadoServicoDTO();
                chamadoDTO.callID = (txtNumeroSg.Text.Equals("") ? 0 : Convert.ToInt32(txtNumeroSg.Text));

                if(!txtDataInicial.Text.Equals("") && !txtDataFinal.Text.Equals(""))
                {
                    chamadoDTO.createDate = Convert.ToDateTime(txtDataInicial.Text);
                    chamadoDTO.closeDate = Convert.ToDateTime(txtDataFinal.Text);
                }

                ChamadoServicoBLL chamadoBLL = new ChamadoServicoBLL();
                gridCartaoEquipamento.DataSource = chamadoBLL.BuscarChamadoPorCustomer(hfIdConcessionario.Value, chamadoDTO);
                gridCartaoEquipamento.DataBind();
            }
        }
    }
}