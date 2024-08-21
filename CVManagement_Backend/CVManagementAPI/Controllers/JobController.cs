using CVManagementAPI.Helpers;
using CVManagementAPI.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace CVManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        /// <summary>
        /// Fire-and-forget jobs are executed only once and almost immediately after creation.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateBackgroundJob")]
        public ActionResult CreateBackgroudJob()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Background Job Triggered"));
            return Ok();
        }

        /// <summary>
        /// Delayed jobs are executed only once too, but not immediately, after a certain time interval.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateScheduledJob")]
        public ActionResult CreateScheduledJob()
        {
            SendMail.DeleteExistingJobs(1);
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(500);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);

            BackgroundJob.Schedule(() => SendMail.Send(new MailContent
            {
                IdCvNeedToSendMail = 1,
                MailTo = "lehuyhaianh0808@gmail.com",
                Subject = "test",
                Content = "test"
            }), dateTimeOffset);
            return Ok();
        }

        /// <summary>
        /// Continuations are executed when its parent job has been finished.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            SendMail.DeleteExistingJobs(1);
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(2);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);
            var jobId = BackgroundJob.Schedule(() => SendMail.Send(new MailContent
            {
                IdCvNeedToSendMail = 1,
                MailTo = "lehuyhaianh0808@gmail.com",
                Subject = "test",
                Content = "test"
            }), dateTimeOffset);

            var job2Id = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("successfully send mail"));
            return Ok();
        }

        /// <summary>
        /// Recurring jobs fire many times on the specified CRON schedule.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateRecurringJob")]
        public ActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate("RecurringJob1", () => Console.WriteLine("Recurring Job Triggered"), "* * * * *");
            return Ok();
        }
    }
}

