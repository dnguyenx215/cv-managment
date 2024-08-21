using CVManagementAPI.Models;
using Hangfire;
using System.Net;
using System.Net.Mail;

namespace CVManagementAPI.Helpers
{
    public class SendMail
    {
        public static bool Send(MailContent content)
        {
            //var smtpClient = new SmtpClient("smtp.ethereal.email")
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("deas13579@gmail.com", "Xyz12345!"),
                //Credentials = new NetworkCredential("lyric55@ethereal.email", "gZqJRrnSDqAHHXaUH7"),
                EnableSsl = true
            };
            var fileHtml = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\EmailTemplates\EmailTemplate.html");
            var emailTemplate = System.IO.File.ReadAllText(fileHtml);
            string mail = emailTemplate.Replace(@"{body_email}", content.Content);
            var mailMessage = new MailMessage
            {
                From = new MailAddress("deas13579@gmail.com"),
                Subject = content.Subject,
                Body = mail,
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(content.MailTo));


            if (content.EmailsCC != null && content.EmailsCC.Any())
            {
                foreach (string email in content.EmailsCC)
                {
                    mailMessage.CC.Add(new MailAddress(email));
                }
            }

            if (content.EmailsBCC != null && content.EmailsBCC.Any())
            {
                foreach (string email in content.EmailsBCC)
                {
                    mailMessage.Bcc.Add(new MailAddress(email));
                }
            }

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static void DeleteExistingJobs(int cvId)
        {
            //RecurringJob.RemoveFromSchedule();

            var monitor = JobStorage.Current.GetMonitoringApi();

            var jobsProcessing = monitor.ProcessingJobs(0, int.MaxValue)
                .Where(x => x.Value.Job.Method.Name == "Send");
            foreach (var j in jobsProcessing)
            {
                var t = (MailContent)j.Value.Job.Args[0];
                if (t.IdCvNeedToSendMail == cvId)
                {
                    BackgroundJob.Delete(j.Key);
                }
            }
        }
    }
}
