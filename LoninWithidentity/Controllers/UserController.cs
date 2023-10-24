using Data.Interface;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAllinterface<User> _IUser;

        public UserController(IAllinterface<User> iacc)
        {
            _IUser = iacc;
        }

        // GET: api/<AccountController>
        [HttpGet("get-all")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _IUser.GetAll();
        }



        // POST api/<AccountController>
        [HttpPost("post")]
        public async Task<IActionResult> Post(User user)
        {
            if (user != null)
            {
                await _IUser.Add(user);
                return Ok(user);
            }
            return BadRequest();
        }

        // PUT api/<AccountController>/5
        [HttpPut("put/{id}")]
        public async Task<IActionResult> Put(User user)
        {
            if (user != null)
            {
                await _IUser.Update(user);
                return Ok(user);
            }
            return BadRequest();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                await _IUser.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
