using Expences.Aplication.Contracts;
using Expences.Aplication.Dto.Expences;
using Expences.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expences.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpencesController : ControllerBase
    {
        private readonly IExpencesService expencesService;
      

        public ExpencesController(IExpencesService expencesService)
        {
            this.expencesService = expencesService;

        }
      
        // GET: api/<ExpencesController>
        [HttpGet("GetAllExpences")]
        public IActionResult Get()
        {
            var result = expencesService.GetAll();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<ExpencesController>/5
        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            var result = expencesService.Get(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetLoginUserExpences")]
        public IActionResult GetByUsuario()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString(Session.SessionVariable.SessionCurrentUserId));
            var result = expencesService.GetByUserId(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetByCategoryId")]
        public IActionResult GetByCategory(int id)
        {
           var userId = Convert.ToInt32(HttpContext.Session.GetString(Session.SessionVariable.SessionCurrentUserId));
            var result = expencesService.FilterByCategory(userId, id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST api/<ExpencesController>
        [HttpPost("SaveExpence")]
        public IActionResult Post([FromBody] ExpencesSaveDto expencesSaveDto)
        {
            var result = expencesService.Save(expencesSaveDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<ExpencesController>/5
        [HttpPut("UpdateExpence")]
        public IActionResult Put([FromBody] ExpencesUpdateDto expencesUpdateDto)
        {
            var result = expencesService.Update(expencesUpdateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // DELETE api/<ExpencesController>/5
        [HttpDelete("DeleteExpence")]
        public IActionResult Delete(int id)
        {
            var result = expencesService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
