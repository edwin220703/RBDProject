using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDGruposController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDGruposController(BdrbdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdGrupos.Include(x => x.CodEstNavigation).ToListAsync();

                return Ok(JsonSerializer.Serialize(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<RBDGruposController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdGrupos.FindAsync(id);

                if (content is null)
                    return NoContent();

                return Ok(JsonSerializer.Serialize(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RBDGruposController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {

                var content = JsonSerializer.Deserialize<RbdGrupo>(value);

                if (content == null)
                    return BadRequest();

                _context.RbdGrupos.Add(content);
                await _context.SaveChangesAsync();

                return StatusCode(201, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<RBDGruposController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] string value)
        {
            try
            {
                var content = await _context.RbdGrupos.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdGrupo>(value);

                if (content is null || result is null)
                    return BadRequest(id);

                _context.RbdGrupos.Update(result);
                await _context.SaveChangesAsync();

                return Ok(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDGruposController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdGrupos.FindAsync(id);

                if (content is null)
                    return BadRequest(id);

                _context.RbdGrupos.Remove(content);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
