using System.Net.Mail;

namespace RaterBee.Helpers
{
    public class MailHelper
    {
        public static string EmailFromArvixe(string sentToEmail, string subject, string body)
        {

            // Use credentials of the Mail account that you created with the steps above.
            const string FROM = "raterbeepromo@gmail.com";
            const string FROM_PWD = "glass7Beacon";
            const bool USE_HTML = true;

            // Get the mail server obtained in the steps described above.
            const string SMTP_SERVER = "smtp.gmail.com";
            try
            {
                MailMessage mailMsg = new MailMessage(FROM, sentToEmail);
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(FROM, FROM_PWD);

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                smtp.Send(mailMsg);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return "Success";
        }
    }

}
