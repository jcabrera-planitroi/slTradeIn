using System.Net.Mail;

namespace slTradeIn.Help
{
    public class TradingMail
    {
        private SmtpClient smtp;

        public TradingMail()
        {
            /*
            smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.office365.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("jcaicedo@planitroi.com", "^B3(3C^s:8/fUth");
            */
        }

        public void Send(string to, string text, string subject)
        {/*
            MailMessage mail = new MailMessage("jcaicedo@planitroi.com", to);
            mail.Subject = subject;
            mail.Body = text;
            mail.IsBodyHtml = true;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            smtp.Send(mail);
            */
        }

        public void SendCode(string to, string code, string subject)
        {
            Send(to, $"<h4>Verification code</h4><h1>{code}</h1>", subject);
        }

        public void SendQuoteReview(string to, string content)
        {
            // Fill data in template
            Send(to, content, "Order Review");
        }
    }
}
