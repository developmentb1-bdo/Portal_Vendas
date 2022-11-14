using SAPB1.BLL.ParceiroNegocio;
using SAPB1.DTO.Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAPB1.WebForms.Dagan.App_Code;

namespace SAPB1.WebForms.Dagan
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCpfCnpj.Attributes.Add("placeholder", "Digite o CPF ou CNPJ");
                txtSenha.Attributes.Add("placeholder", "Digite a senha");
            }
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

                var resultado = parceiroNegocioBLL.AutenticarClienteSistemaPortal(txtCpfCnpj.Text, txtSenha.Text);

                if (resultado.Contains("Erro"))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Tipo = MensagemType.Erro;
                    mensagemDTO.Mensagem = resultado;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref ds, mensagemDTO);
                }
                else
                {
                    Session["EmpId"] = resultado;

                    Response.Redirect("AreaCliente.aspx");
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Tipo = MensagemType.Erro;
                mensagemDTO.Mensagem = er.Message;

                Mensagens.MostrarMensagem(ref pnlAviso, ref ds, mensagemDTO);
            }
        }
    }
}