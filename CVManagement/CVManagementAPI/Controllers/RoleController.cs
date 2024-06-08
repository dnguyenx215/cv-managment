using CVManagementAPI.DTO;
using CVManagementAPI.Helpers;
using CVManagementAPI.Models;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{
    public class RoleController : BaseControllerGeneric<Role, RoleDTO>
    {
        public RoleController(ServiceFactory service) : base(service.RoleService)
        {

        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<ResponseFormat>> SearchByName(string name)
        {
            var entities = await _service.SearchAsync(x => x.RoleName == name);
            return Ok(new ResponseFormat(HttpStatusCode.OK, "Get List", entities));
        }
    }
}
