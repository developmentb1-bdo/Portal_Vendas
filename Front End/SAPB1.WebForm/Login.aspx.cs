using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.BLL.Funcionario;
using SAPB1.DTO.Funcionario;
using SAPB1.DTO.Mensagens;
using SAPB1.WebForm.App_Code;

namespace SAPB1.WebForm
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlAviso.Visible = false;
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            ColaboradorBLL colaboradorBLL = new ColaboradorBLL();
            ColaboradorDTO colaborador = colaboradorBLL.SelecionarColaboradorPorUsuarioESenha(txtEmail.Text, txtSenha.Text);


            if (colaborador.EmpId == 0)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Tipo = MensagemType.Erro;
                mensagemDTO.Mensagem ="Usuário ou senha inválidos";

                Mensagens.MostrarMensagem(ref pnlAviso, ref ds, mensagemDTO);
            }
            else
            {
                Session.Add("EmpId", colaborador.EmpId);

                if (!string.IsNullOrEmpty(colaborador.U_AcessoPortal))
                    Session.Add("U_AcessoPortal", colaborador.U_AcessoPortal);

                Response.Redirect("DashBoard.aspx");
            }
        }
    }
}