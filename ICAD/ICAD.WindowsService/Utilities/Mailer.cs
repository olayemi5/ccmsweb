using Fcmb.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.WindowsService.Utilities
{
    public class Mailer
    {
        public static void SendEmailReport(string emailTemplate, string subject, string messageBody)
        {
            //get email data
            var recipient = AppConfig.EmailTo;
            var sender = AppConfig.EmailFrom;
            var cc = AppConfig.EmailCc;
            var bcc = AppConfig.EmailBcc;
            //prepare the email
            var mailObject = EmailHandler.BuildMessage(emailTemplate, null, sender, subject, recipient, messageBody, cc, bcc);
            //send email
            EmailHandler.Send(mailObject, true);
        }
    }
}
