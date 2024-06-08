using CVManagementAPI.DTO;
using CVManagementAPI.Models;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{
    public class PositionController : BaseControllerGeneric<Position, PositionDTO>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public PositionController(ServiceFactory service) : base(service.PositionService)
        {
        }

        /// <summary>
        /// Search Postition by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>ResponseFormat</returns>
        [HttpGet("SearchPosition/{name}")]
        public async Task<ResponseFormat> SearchPosition(string name)
        {
            var result = await _service.SearchAsync(x => x.PositionName.Contains(name));

            return new ResponseFormat(HttpStatusCode.NoContent, "Search  Success!", result);
        }

    }
}