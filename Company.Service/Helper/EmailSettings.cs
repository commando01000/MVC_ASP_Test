using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
   public static class EmailSettings
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
