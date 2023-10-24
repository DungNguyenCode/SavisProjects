using Data.Interface;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IAllinterface<Brand> _IAll;

        public BrandController(IAllinterface<Brand> iacc)
        {
            _IAll = iacc;
        }

        // GET: api/<AccountController>
        [HttpGet("get-all")]
        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _IAll.GetAll();
        }



        // POST api/<AccountController>
        [HttpPost("post")]
        public async Task<IActionResult> Post(Brand item)
        {
            if (item != null)
            {
                await _IAll.Add(item);
                return Ok(item);
            }
            return BadRequest();
        }

        // PUT api/<AccountController>/5
        [HttpPut("put/{id}")]
        public async Task<IActionResult> Put(Brand item)
        {
            if (item != null)
            {
                await _IAll.Update(item);
                return Ok(item);
            }
            return BadRequest();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                await _IAll.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
