using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;

namespace auth.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly MailJetSettings _settings;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _settings = _configuration.GetSection("MailJet")
                .Get<MailJetSettings>();
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(_settings.ApiKey, _settings.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            // Create email
            var emailBuilder = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_settings.SenderEmail, _settings.SenderName))
                .WithSubject(subject)
                .WithHtmlPart(htmlMessage)
                .WithTo(new SendContact(email))
                .Build();

            // Send email
            await client.SendTransactionalEmailAsync(emailBuilder);
        }
    }
}
