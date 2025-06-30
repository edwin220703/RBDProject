using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDComprobanteFiscalesController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDComprobanteFiscalesController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDComprobanteFiscalesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdComprobanteFiscals.Include(t=>t.CodTipocomNavigation).ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/<RBDComprobanteFiscalesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdComprobanteFiscals.FindAsync(id);

                if (content == null)
                    return BadRequest();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RBDComprobanteFiscalesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var result = JsonSerializer.Deserialize<RbdComprobanteFiscal>(value);

                if(result == null)
                    return BadRequest();

                _context.RbdComprobanteFiscals.Add(result);
                await _context.SaveChangesAsync();

                return StatusCode(201,result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDComprobanteFiscalesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdComprobanteFiscals.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdComprobanteFiscal>(value);

                if (content == null || result == null) 
                    return BadRequest();

                _context.RbdComprobanteFiscals.Update(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDComprobanteFiscalesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdComprobanteFiscals.FindAsync(id);

                if(content == null)
                    return BadRequest();

                _context.RbdComprobanteFiscals.Remove(content);
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
