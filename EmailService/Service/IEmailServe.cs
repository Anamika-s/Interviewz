using System.Net.Mail;
using System.Net;

namespace EmailService.Service
{
    public interface IEmailServe
    {
        public Task<bool> SendEmail(string candidateEamil, string recruiterEmail);
    }

    public class EmailServe : IEmailServe
    {
        public async Task<bool> SendEmail(string candidateEamil, string recruiterEmail)
        {
            var companyEmail = "interq96@gmail.com";
            var password = "thxggdgrbsbcahoj";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(companyEmail, password)
            };
            try
            {
                //await client.SendMailAsync(new MailMessage(from: companyEmail, to: candidateEmail, subject: "Interview", body: "You have an incoming interview"));
                await client.SendMailAsync(new MailMessage(from: companyEmail, to: recruiterEmail, subject: "Interview", body: "You have an incoming interview"));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
