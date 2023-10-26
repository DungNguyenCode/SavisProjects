using Data.Interface;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IAllinterface<Material> _IAll;

        public MaterialController(IAllinterface<Material> iacc)
        {
            _IAll = iacc;
        }

        // GET: api/<AccountController>
        [HttpGet("get-all")]
        public async Task<IEnumerable<Material>> GetAll()
        {
            return await _IAll.GetAll();
        }



        // POST api/<AccountController>
        [HttpPost("post")]
        public async Task<IActionResult> Post(Material item)
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
        public async Task<IActionResult> Put(Material item)
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
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var item = await _IAll.GetById(id);
                return Ok(item);
            }
            return NotFound();
        }
    }
}
