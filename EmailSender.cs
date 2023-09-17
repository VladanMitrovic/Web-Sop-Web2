using System.Net.Mail;
using System.Net;

namespace web2projekat
{
    public class EmailSender
    {
        private const string SmtpHost = "smtp.gmail.com"; // e.g., "smtp.gmail.com"
        private const int SmtpPort = 587; // e.g., for Gmail SMTP
        private const string SmtpUsername = "webprojekat5@gmail.com";
        private const string SmtpPassword = "cjkqazchonlfzbuu";

        public void SendVerificationEmail(string recipientEmail, string verificationLink)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient(SmtpHost);
            smtpClient.Port = SmtpPort;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            mail.From = new MailAddress(SmtpUsername);
            mail.To.Add(recipientEmail);
            mail.Subject = "Verification Email";
            mail.Body = $"Please click the following link to verify your email: {verificationLink}";
            mail.IsBodyHtml = true;

            smtpClient.Credentials = new NetworkCredential(SmtpUsername, SmtpPassword);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Verification email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send verification email. Error: " + ex.Message);
            }
            finally
            {
                mail.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
