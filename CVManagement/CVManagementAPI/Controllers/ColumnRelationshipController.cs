using AutoMapper;
using CVManagementAPI.DTO;
using CVManagementAPI.Models;
using CVManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CVManagementAPI.Controllers
{
    public class ColumnRelationshipController : BaseControllerGeneric<ColumnRelationship, ColumnRelationshipDTO>
    {

        protected readonly IMapper _mapper;
        private readonly ColumnRelationService _service;
        public ColumnRelationshipController(ServiceFactory serviceFactory, IMapper mapper) : base(serviceFactory.ColumnRelationService)
        {
            _mapper = mapper;
            _service = serviceFactory.ColumnRelationService;
        }

        [HttpGet("GetAllPopulate")]
        public async Task<ActionResult<ResponseFormat>> GetAllPopulate()
        {
            var dtos = await _service.ToListAsyncPopulate();

            //return Ok(JsonConvert.SerializeObject(dtos, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
            return new ResponseFormat(HttpStatusCode.OK, "Get List", dtos);
        }
    }
}
