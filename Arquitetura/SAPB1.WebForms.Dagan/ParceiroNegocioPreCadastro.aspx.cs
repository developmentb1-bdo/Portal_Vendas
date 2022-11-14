using SAPB1.BLL.Funcionario;
using SAPB1.BLL.ParceiroNegocio;
using SAPB1.DTO.Funcionario;
using SAPB1.DTO.Mensagens;
using SAPB1.DTO.ParceiroNegocio;
using SAPB1.WebForms.Dagan.App_Code;
using System;
using System.Collections.Generic;

namespace SAPB1.WebForms.Dagan
{
    public partial class ParceiroNegocioPreCadastro : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ColaboradorBLL colaboradorBLL = new ColaboradorBLL();
                ColaboradorDTO colaborador = colaboradorBLL.SelecionarColaboradorPorId(Convert.ToInt32(Session["EmpId"]));

                if (colaborador.EmpId == 0)
                    return;

                hfVendedor.Value = colaborador.SalesPrson.ToString();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string erros = "";

                if (txtRazaoSocial.Text.Trim().Equals(""))
                    erros += "<li>Digite a Razão Social";

                if (txtCpfCnpj.Text.Trim().Equals(""))
                    erros += "<li>Digite o CNPJ/CPF.";

                if (txtIdContato.Text.Trim().Equals(""))
                    erros += "<li>Digite o Nome do contato.";

                if (txtEmailContato.Text.Trim().Equals(""))
                    erros += "<li>Digite o e-mail.";


                if (txtDddContato.Text.Trim().Equals(""))
                    erros += "<li>Digite o DDD.";

                if (txtTelefoneContato.Text.Trim().Equals(""))
                    erros += "<li>Digite  telefone do contato.";

                if (txtObservacaoContato.Text.Trim().Equals(""))
                    erros += "<li>A observação do contato.";

                if (!erros.Equals(""))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Tipo = MensagemType.Erro;
                    mensagemDTO.Mensagem = erros;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                    return;
                }


                ParceiroNegocioDTO parceiroNegocioDTO = new ParceiroNegocioDTO();
                parceiroNegocioDTO.CardName = txtRazaoSocial.Text.Replace("@", "").Replace("&", "").Replace("%", "");
                parceiroNegocioDTO.CardType = "cLid";
                parceiroNegocioDTO.U_CNPJ = ((!string.IsNullOrEmpty(txtCpfCnpj.Text)) ? txtCpfCnpj.Text.Replace("@", "").Replace("&", "").Replace("%", "") : "tNO");

                if (hfVendedor.Value.Equals(""))
                    parceiroNegocioDTO.SlpCode = -1;
                else
                    parceiroNegocioDTO.SlpCode = Convert.ToInt32(hfVendedor.Value);

                ContatoDTO contatoDTO = new ContatoDTO();
                contatoDTO.Name = ((!string.IsNullOrEmpty(txtIdContato.Text)) ? txtIdContato.Text.Replace("@", "").Replace("&", "").Replace("%", "") : "tNO");
                contatoDTO.E_MailL = ((!string.IsNullOrEmpty(txtEmailContato.Text)) ? txtEmailContato.Text : "tNO");
                contatoDTO.Tel1 = ((!string.IsNullOrEmpty(txtDddContato.Text) && !string.IsNullOrEmpty(txtTelefoneContato.Text)) ? txtDddContato.Text + txtTelefoneContato.Text : "tNO");
                contatoDTO.Notes1 = ((!string.IsNullOrEmpty(txtObservacaoContato.Text)) ? txtObservacaoContato.Text.Replace("@", "").Replace("&", "").Replace("%", "") : "tNO");

                parceiroNegocioDTO.ListContato = new List<ContatoDTO>();
                parceiroNegocioDTO.ListContato.Add(contatoDTO);


                IdentificacaoFiscalDTO identificacaoDTO = new IdentificacaoFiscalDTO();
                identificacaoDTO.TaxId1 = ((!string.IsNullOrEmpty(txtIe.Text)) ? txtIe.Text.Replace("@", "").Replace("&", "").Replace("%", "") : "tNO");

                parceiroNegocioDTO.ListIdentificacaoFiscal = new List<IdentificacaoFiscalDTO>();
                parceiroNegocioDTO.ListIdentificacaoFiscal.Add(identificacaoDTO);

                ParceiroNegocioBLL parceiroNegocioBLL = new ParceiroNegocioBLL();

                if (parceiroNegocioBLL.Inserir(parceiroNegocioDTO))
                {
                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Tipo = MensagemType.Confirmacao;
                    mensagemDTO.Mensagem = "Lead cadatrado com sucesso";

                    txtRazaoSocial.Text = "";
                    txtCpfCnpj.Text = "";
                    txtEmailContato.Text = "";
                    txtDddContato.Text = "";
                    txtEmailContato.Text = "";
                    txtIe.Text = "";
                    txtObservacaoContato.Text = "";
                    txtTelefoneContato.Text = "";
                }
                else
                {
                    lblAvisos.Text = "Não foi possível inserir o lead.";

                    if (!string.IsNullOrEmpty(parceiroNegocioBLL.ErrorMessege))
                        lblAvisos.Text += " Mensagem do serviço: " + parceiroNegocioBLL.ErrorMessege;

                    MensagemDTO mensagemDTO = new MensagemDTO();
                    mensagemDTO.Tipo = MensagemType.Erro;
                    mensagemDTO.Mensagem = lblAvisos.Text;

                    Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
                }
            }
            catch (Exception er)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Tipo = MensagemType.Erro;
                mensagemDTO.Mensagem = "Erro interno: " + er.Message;

                Mensagens.MostrarMensagem(ref pnlAviso, ref lblAvisos, mensagemDTO);
            }
        }

    }
}