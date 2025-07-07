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
    public class RBDCiudadesController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDCiudadesController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDCiudadesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var content = await _context.RbdCiudades.ToListAsync();

            if (content == null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(content));
        }

        // GET api/<RBDCiudadesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var content = await _context.RbdCiudades.FindAsync(id);

            if (content == null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(content));
        }

        // POST api/<RBDCiudadesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            if (value == null)
                return BadRequest();

            try
            {
                var content = JsonSerializer.Deserialize<RbdCiudade>(value);

                if (content != null)
                {
                    await _context.RbdCiudades.AddAsync(content);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, JsonSerializer.Serialize(content));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return NotFound();
        }

        // PUT api/<RBDCiudadesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdCiudades.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdCiudade>(value);

                if (content == null || result == null)
                    return NotFound();

                content.NomCiudad = result.NomCiudad;
                content.CodPostal = result.CodPostal;

                _context.RbdCiudades.Entry(content).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(value);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return NotFound();
        }

        // DELETE api/<RBDCiudadesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdCiudades.FindAsync(id);

                if (content is null)
                {
                    return NotFound();
                }

                _context.RbdCiudades.Remove(content);
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
