﻿using Data.Interface;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VorcherController : ControllerBase
    {
        private readonly IAllinterface<Vorcher> _IAll;

        public VorcherController(IAllinterface<Vorcher> iacc)
        {
            _IAll = iacc;
        }

        // GET: api/<AccountController>
        [HttpGet("get-all")]
        public async Task<IEnumerable<Vorcher>> GetAll()
        {
            return await _IAll.GetAll();
        }



        // POST api/<AccountController>
        [HttpPost("post")]
        public async Task<IActionResult> Post(Vorcher item)
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
        public async Task<IActionResult> Put(Vorcher item)
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
