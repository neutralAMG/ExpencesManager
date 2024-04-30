using Expences.Aplication.Contracts;
using Expences.Aplication.Dto.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Expences.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        // GET: UsersController
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        // GET: api/<UsersController>
        [HttpGet("GetAllUsers")]
        public IActionResult Get()
        {
            var reult = usersService.GetAll();
            if (!reult.IsSuccess)
            {
                return BadRequest(reult);
            }
            return Ok(reult);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public IActionResult Get(int id)
        {
            var result = usersService.Get(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("LogIn")]
        public IActionResult Get(string UserName, string pass)
        {
            
            var result = usersService.LogIn(UserName, pass);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(Session.SessionVariable.SessionCurrentUserId))){
                HttpContext.Session.SetString(Session.SessionVariable.SessionCurrentUserName, result.Data.Name);
                HttpContext.Session.SetString(Session.SessionVariable.SessionCurrentUserLimite, Convert.ToString(result.Data.LimiteGasto));
                HttpContext.Session.SetString(Session.SessionVariable.SessionCurrentUserUserName, result.Data.UserName);
                HttpContext.Session.SetString(Session.SessionVariable.SessionCurrentUserId, Convert.ToString(result.Data.Id));
            }
            return Ok(result);
        }

        // POST api/<UsersController>
        [HttpPost("Register")]
        public IActionResult Post([FromBody] UsersSaveDto usersSaveDto)
        {
            var result = usersService.Save(usersSaveDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<UsersController>/5
        [HttpPut("UpdateUser")]
        public IActionResult Put([FromBody] UsersUpdateDto usersUpdateDto)
        {
            var result = usersService.Update(usersUpdateDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateUserCredentials")]
        public IActionResult Put([FromBody] UsersUpdateCredentialDto user)
        {
            var result = usersService.UpdateCredentials(user);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("DeleteUser")]
        public IActionResult Delete(int id)
        {
            var result = usersService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
