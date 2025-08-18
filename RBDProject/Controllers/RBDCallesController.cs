using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDCallesController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDCallesController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RbdCallesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.RbdCalles.Include(ciu=>ciu.IdCiudadNavigation).ToListAsync();

            return Ok(JsonSerializer.Serialize(result));
        }

        // GET api/<RbdCallesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.RbdCalles.FirstOrDefaultAsync(x => x.IdCalle == id);

            if (result is not null)
            {
                return Ok(JsonSerializer.Serialize(result));
            }

            return NotFound();
        }

        // POST api/<RbdCallesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdCalle>(value);

                if (content == null)
                    return NoContent();

                await _context.RbdCalles.AddAsync(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RbdCallesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdCalles.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdCalle>(value);

                if (content is null || result is null)
                {
                    return BadRequest();
                }

                content.NomCalle = result.NomCalle;

                _context.RbdCalles.Entry(content).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RbdCallesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _context.RbdCalles.FindAsync(id);

                if (result == null)
                    return BadRequest();

                _context.RbdCalles.Remove(result);
                await _context.SaveChangesAsync();

                return Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }
    }
}
