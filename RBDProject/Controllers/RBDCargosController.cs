using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDCargosController : ControllerBase
    {
        private readonly BdrbdContext _context;
        
        public RBDCargosController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDCargosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdCargos.ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RBDCargosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdCargos.FindAsync(id);

                if (content == null)
                    return BadRequest(id);

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST api/<RBDCargosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                if (value == null)
                    return NoContent();

                var content = JsonSerializer.Deserialize<RbdCargo>(value);

                await _context.RbdCargos.AddAsync(content);
                await _context.SaveChangesAsync();

                return StatusCode(201, value);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<RBDCargosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdCargos.FindAsync(id);

                if (content == null || value == null)
                    return NoContent();

                var result = JsonSerializer.Deserialize<RbdCargo>(value);

                _context.RbdCargos.Update(content);
                await _context.SaveChangesAsync();

                return Ok(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<RBDCargosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdCargos.FindAsync(id);

                if(content is null)
                    return BadRequest();

                _context.RbdCargos.Remove(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
