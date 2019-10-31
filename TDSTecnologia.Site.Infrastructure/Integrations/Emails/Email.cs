﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TDSTecnologia.Site.Infrastructure.Integrations.Emails
{
    public class Email : IEmail
    {
        private ConfiguracoesEmail _configuracoesEmail;

        public Email(IOptions<ConfiguracoesEmail> configuracoesEmail)
        {
            _configuracoesEmail = configuracoesEmail.Value;
        }

        public async Task EnviarEmail(string email, string assunto, string mensagem)
        {
            var destinatario = String.IsNullOrEmpty(email) ? _configuracoesEmail.Email : email;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_configuracoesEmail.Email, "TDS Tecnologia")
            };

            mailMessage.To.Add(new MailAddress(destinatario));
            mailMessage.Subject = assunto;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            using (SmtpClient smtpClient = new SmtpClient(_configuracoesEmail.Endereco, _configuracoesEmail.Porta))
            {
                smtpClient.Credentials = new NetworkCredential(_configuracoesEmail.Email, _configuracoesEmail.Senha);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        Task IEmail.EnviarEmail(string email, string assunto, string mensagem)
        {
            throw new NotImplementedException();
        }
    }
}
