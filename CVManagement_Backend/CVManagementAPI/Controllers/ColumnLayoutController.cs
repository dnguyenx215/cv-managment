using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Models;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{

    public class ColumnLayoutController : BaseControllerGeneric<ColumnLayout, ColumnLayoutDTO>
    {
        protected readonly IMapper _mapper;
        private readonly ColumnLayoutService _service;
        public ColumnLayoutController(ServiceFactory serviceFactory, IMapper mapper) : base(serviceFactory.ColumnLayoutService)
        {
            _service = serviceFactory.ColumnLayoutService;
            _mapper = mapper;
        }

        [HttpGet("GetAllPopulate")]
        public async Task<ActionResult<ResponseFormat>> GetAllPopulate()
        {
            var entities = await _service.ToListAsyncPopulate();

            //return Ok(JsonConvert.SerializeObject(dtos, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return new ResponseFormat(HttpStatusCode.OK, "Get List", entities);
        }
    }
}
