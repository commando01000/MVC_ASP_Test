using System.Net.Mail;

namespace Company.Service.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email model)
        {
            // send email
            var client = new SmtpClient(host: "smtp.gmail.com", port: 587);
            //client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("jfijcc124@gmail.com", "aainxxblnxedgxqy");
            client.EnableSsl = true;
            client.Send("jfijcc124@gmail.com", model.To, model.Subject, model.Body);
        }
    }
}
