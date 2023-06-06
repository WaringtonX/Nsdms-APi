using mersetaWebAPI.CustomModels.EmailModels;
using mersetaWebAPI.Exceptions;
using mersetaWebAPI.Interface;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net.Security;

namespace mersetaWebAPI.Implementation
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }
        public RemoteCertificateValidationCallback RemoteServerCertificateValidationCallback { get; private set; }

        private IConfiguration Configuration;

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger, IConfiguration configuration)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
            Configuration = configuration;
        }

        public void Send(EmailRequest request)
        {
            try
            {    
                //Read SMTP settings from AppSettings.json.
                string host = this.Configuration.GetValue<string>("MailSettings:SmtpHost");
                int port = this.Configuration.GetValue<int>("MailSettings:SmtpPort");
                string fromAddress = this.Configuration.GetValue<string>("MailSettings:EmailFrom");
                string userName = this.Configuration.GetValue<string>("MailSettings:SmtpUser");
                string password = this.Configuration.GetValue<string>("MailSettings:SmtpPass");
                string DisplayName = this.Configuration.GetValue<string>("MailSettings:DisplayName");
                string TargetName = this.Configuration.GetValue<string>("MailSettings:TargetName");
                var mailSettings = new MailSettings 
                {
                    EmailFrom = fromAddress,
                    SmtpPort=port,
                    SmtpHost = host,
                    SmtpUser = userName,
                    SmtpPass = password,
                    DisplayName= DisplayName,
                    TargetName= TargetName,
                    
                };
                // create message
                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress(request.To ?? mailSettings.EmailFrom));
                mail.From = new MailAddress(/*request.From ??*/ mailSettings.EmailFrom);
                mail.Subject = $"ES3 Notification - {request.Subject}";
                mail.IsBodyHtml = true;
                mail.Body = request.Body;

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
                SmtpClient smtp = new SmtpClient();
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mailSettings.SmtpUser, mailSettings.SmtpPass);
                client.Port = mailSettings.SmtpPort;//You can use Port 25 if 587 is blocked
                client.Host = mailSettings.SmtpHost;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.TargetName = mailSettings.TargetName;
                client.Send(mail);
                client.Dispose();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }


    }
}
