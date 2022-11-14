using SAPB1.DTO.Mensagens;
using System.Web.UI.WebControls;

namespace SAPB1.WebForms.Dagan.App_Code
{
    public static class Mensagens
    {
        public static void MostrarMensagem(ref Panel pnl, ref Label labelAviso, MensagemDTO mensagemDTO)
        {
            switch (mensagemDTO.Tipo)
            {
                case MensagemType.Erro:
                    pnl.CssClass = "alert alert-danger alert-dismissible";
                    break;

                case MensagemType.Aviso:
                    pnl.CssClass = "alert alert-info alert-dismissible";
                    break;

                case MensagemType.Confirmacao:
                    pnl.CssClass = "alert alert-success alert-dismissible";
                    break;
            }

            labelAviso.Text = mensagemDTO.Mensagem;
            pnl.Visible = true;
        }
    }
}