using Expences.Aplication.Contracts;
using Expences.Aplication.Dto.Category;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expences.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        // GET: api/<ExpencesController>
        [HttpGet("GetAllCategories")]
        public IActionResult Get()
        {
            var result = categoryService.GetAll();
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
            var result = categoryService.Get(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        // POST api/<ExpencesController>
        [HttpPost("SaveCategory")]
        public IActionResult Post([FromBody] CategorySaveDto categorySaveDto)
        {
            var result = categoryService.Save(categorySaveDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<ExpencesController>/5
        [HttpPut("UpdateCategory")]
        public IActionResult Put([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var result = categoryService.Update(categoryUpdateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // DELETE api/<ExpencesController>/5
        [HttpDelete("DeleteCategory")]
        public IActionResult Delete(int id)
        {
            var result = categoryService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
