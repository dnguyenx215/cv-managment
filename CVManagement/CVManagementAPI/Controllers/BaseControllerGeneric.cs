using CVManagementAPI.Helpers.Page;
using CVManagementAPI.Services;
using global::CVManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace CVManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseControllerGeneric<TEntity, TDto>(IBaseService<TEntity, TDto> service)
        : ControllerBase
    {
        protected readonly IBaseService<TEntity, TDto> _service = service;


        [HttpGet]
        public async Task<ActionResult<ResponseFormat>> GetAll()
        {
            var entities = await _service.ToListAsync();
            return Ok(new ResponseFormat(HttpStatusCode.OK, "Get List", entities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseFormat>> FindAsync(int id)
        {
            var entityDto = await _service.FindAsync(id);

            return Ok(new ResponseFormat(HttpStatusCode.OK, "Find By Id", entityDto));
        }

        [HttpPost]
        public virtual async Task<ActionResult<ResponseFormat>> InsertAsync([FromForm] TDto dto)

        {
            var insertResult = await _service.InsertAsync(dto);
            if (insertResult < 0)
            {
                return BadRequest(new ResponseFormat(HttpStatusCode.BadRequest, "Insert Fail", null));
            }

            return Ok(new ResponseFormat(HttpStatusCode.OK, "Insert Success", insertResult));
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<ResponseFormat>> UpdateAsync(int id, TDto obj)
        {
            var result = await _service.UpdateAsync(id, obj);
            if (result < 0)
            {
                return BadRequest(new ResponseFormat(HttpStatusCode.BadRequest, "Update Fail!", null));
            }
            return Accepted(new ResponseFormat(HttpStatusCode.Accepted, "Update Success!", result));
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<ResponseFormat>> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result == -1)
            {
                return BadRequest(new ResponseFormat(HttpStatusCode.BadRequest, "Not found!", null));
            }
            else if (result == -2)
            {
                return BadRequest(new ResponseFormat(HttpStatusCode.BadRequest, "Unknown!", null));
            }
            else if (result == -3)
            {
                return BadRequest(new ResponseFormat(HttpStatusCode.BadRequest, "Not same Id!", null));
            }
            return Ok(new ResponseFormat(HttpStatusCode.NoContent, "Delete Success!", result));
        }

        /// <summary>
        /// Get all pagination CV
        /// </summary>
        /// <returns>ActionResult<ResponseFormat></returns>
        [HttpGet("GetAllPagination")]
        public async Task<ActionResult<ResponseFormat>> GetAllPagination([FromQuery] Pagination? pagination)
        {
            try
            {
                var result = await _service.GetAllPagination(pagination);
                return new ResponseFormat(HttpStatusCode.NoContent, "Get all pagination Success!", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
