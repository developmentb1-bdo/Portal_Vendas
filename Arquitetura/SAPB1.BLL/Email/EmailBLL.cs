using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPB1.DTO.Email;
using System.Net.Mail;

namespace SAPB1.BLL.Email
{
    public class EmailBLL
    {
        public string EnviarEmail(EmailDTO emailDTO)
        {
            try
            {
                var email = new MailMessage();

                if (emailDTO.Destinatario != null)
                {
                    if (emailDTO.Destinatario.Count > 0)
                    {
                        foreach (string to in emailDTO.Destinatario)
                            email.To.Add(to);
                    }
                }

                if (emailDTO.Copia != null)
                {
                    if (emailDTO.Copia.Count > 0)
                    {
                        foreach (string cc in emailDTO.Copia)
                            email.CC.Add(cc);
                    }
                }

                //Assunto
                email.Subject = emailDTO.Titulo;

                email.IsBodyHtml = emailDTO.IsHtml;

                //Corpo do e-mail   
                email.Body = emailDTO.Mensagem;

                email.From = new MailAddress("naoresponda@redefotonmotors.com.br", "Aviso de UP");

                //SMTP
                var smtpClient = new SmtpClient(emailDTO.Smtp, emailDTO.Porta);
                smtpClient.EnableSsl = emailDTO.IsSsl;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailDTO.Usuario, emailDTO.Senha);

                smtpClient.Send(email);

                return "";
            }
            catch (Exception er)
            {
                return "Erro ao enviar o e-mail: " + er.Message;
            }
        }
    }
}
