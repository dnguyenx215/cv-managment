using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Models;
using CVManagementAPI.Request;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(ServiceFactory serviceFactory)
        {
            _service = serviceFactory.AccountService;
        }

        // POST: Account/register
        [HttpPost("register")]
        public async Task<ActionResult<ResponseFormat>> Register(RegisterModel account)
        {
            var result = await _service.Register(account);

            if (result.Succeeded)
            {
                return Ok("Account created and role assigned successfully!");
            }

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseFormat>> Login([FromForm] string email, [FromForm] string password)
        {
            var result = await _service.Login(email, password);

            if (result == "")
            {
                return new ResponseFormat(HttpStatusCode.NotFound, "Failed", null);
            }

            return Ok(new ResponseFormat(HttpStatusCode.OK, "Login Success", new { accessToken = result }));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _service.Logout();
            return Ok(result);
        }


        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            var result = await _service.ChangePassword(accountId, oldPassword, newPassword);
            if (!result)
            {
                return BadRequest();
            }

            return Ok("Password Change Successful!");
        }

        // GET: api/Account
        [HttpGet("All")]
        public ActionResult<ResponseFormat> GetAccount()
        {
            var users = _service.GetAllAccount();
            return new ResponseFormat(HttpStatusCode.OK, "List", users);
        }

        [HttpGet("Pagination")]
        public ActionResult<ResponseFormat> GetAllAccountWithPagination([FromQuery] Pagination? pagination)
        {
            var users = _service.GetAllPagination(pagination);
            return new ResponseFormat(HttpStatusCode.OK, "List", users);
        }


        [HttpGet("searchByName/{name}")]
        public ActionResult<ResponseFormat> GetAccountsByName(string name)
        {
            var users = _service.GetAccountsByName(name);
            return new ResponseFormat(HttpStatusCode.OK, "List", users);
        }


        // GET: api/Account/email

        [HttpGet("{email}")]
        public async Task<ActionResult<ResponseFormat>> GetAccountByEmail(string email)
        {
            var users = await _service.GetAccountByEmail(email);
            return new ResponseFormat(HttpStatusCode.OK, "Find Account By Email!", users);
        }


        [HttpGet("findbyid/{id}")]
        public async Task<ActionResult<ResponseFormat>> GetAccountById(string id)
        {
            var user = await _service.GetAccountById(id);
            return new ResponseFormat(HttpStatusCode.OK, "Find Account By Id!", user);
        }


        [HttpGet("getId/{email}")]
        public async Task<ActionResult<ResponseFormat>> GetAccountId(string email)
        {
            var userId = await _service.GetAccountId(email);

            return new ResponseFormat(HttpStatusCode.OK, "Get User Id!", userId);
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseFormat>> PutAccount(string id, EditAccountRequest account)
        {
            var result = await _service.UpdateAccount(id, account);
            if (!result.Succeeded)
            {
                return new ResponseFormat(HttpStatusCode.NotFound, "Failed!", NotFound());
            }

            return new ResponseFormat(HttpStatusCode.NoContent, "Update Success!", result);
        }


        [HttpPut("editPassword/{id}")]
        public async Task<ActionResult<ResponseFormat>> PutPassWord(string id, string password)
        {
            var result = await _service.editPassWord(id, password);
            if (!result.Succeeded)
            {
                return new ResponseFormat(HttpStatusCode.NotFound, "Failed!", NotFound());
            }

            return new ResponseFormat(HttpStatusCode.NoContent, "Update Success!", result);
        }


        // DELETE: api/Account/5

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseFormat>> DeleteAccount(string id)
        {
            var result = await _service.DeleteAccount(id);
            if (!result.Succeeded)
            {
                return new ResponseFormat(HttpStatusCode.NoContent, "Delete Failed!", 0);
            }

            return new ResponseFormat(HttpStatusCode.NoContent, "Delete Success!", 1);
        }
    }
}