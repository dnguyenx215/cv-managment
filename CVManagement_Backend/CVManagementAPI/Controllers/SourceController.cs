using CVManagementAPI.DTO;
using CVManagementAPI.Models;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{
    public class SourceController : BaseControllerGeneric<Source, SourceDTO>
    {
        /// <summary>
        /// Constructor Source 
        /// </summary>
        /// <param name="service"></param>
        public SourceController(ServiceFactory service) : base(service.SourceService)
        {
        }

        /// <summary>
        /// Search Source by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>ResponseFormat</returns>
        [HttpGet("SearchSource/{name}")]
        public async Task<ResponseFormat> SearchAsync(string name)
        {
            var result = await _service.SearchAsync(x => x.NameSource.Contains(name));

            return new ResponseFormat(HttpStatusCode.NoContent, "Search  Success!", result);
        }
    }
}