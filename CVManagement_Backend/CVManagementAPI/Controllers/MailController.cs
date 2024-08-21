using CVManagementAPI.Models;
using CVManagementAPI.Services;
// requires installation of https://www.nuget.org/packages/DnsClient
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;

        public MailController(ServiceFactory serviceFactory)
        {
            _mailService = serviceFactory.MailService;
        }

        [HttpGet("GetAllMails")]
        public async Task<ActionResult<ResponseFormat>> GetAllPopulate()
        {
            var entities = _mailService.GetAllEmails(System.Configuration.ConfigurationManager.AppSettings["HostAddress"]);

            //return Ok(JsonConvert.SerializeObject(dtos, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return new ResponseFormat(HttpStatusCode.OK, "Get List", entities);
        }
    }
}
