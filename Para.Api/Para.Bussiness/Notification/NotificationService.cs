using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Para.Bussiness.Notification;

public class NotificationService : INotificationService
{

    private readonly IConfiguration _configuration;
    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string subject, string email, string content)
    {

        try
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = Convert.ToInt32(smtpSettings["Port"]); // Şifrelenmemiş Port : 587 , TLS,SSL Portu : 465
            smtpClient.Host = smtpSettings["Host"]; // Kullandığınız her bir mail sunucusunun farklı hostları olabilir.
            smtpClient.EnableSsl = true; // SSL’in açılımı Secure Socket Layer’dır. Türkçe anlamıysa Güvenli Giriş Katmanı’dır.
            smtpClient.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]); // Gönderen Kişinin Mail Adresi ve Şifresi 
            mailMessage.From = new MailAddress(smtpSettings["Username"]); // Gönderen Kişinin Mail Adresi
            mailMessage.To.Add(email); // Alıcının Mail Adresi
            mailMessage.Subject = subject; // Mail'inizin Konusu
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = "<b>Test Mail</b><br>using <b>HTML</b>." + content;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true; // Mail mesajı(içeriği)
            smtpClient.Send(mailMessage); // SMTP İsteği
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }
    }
}