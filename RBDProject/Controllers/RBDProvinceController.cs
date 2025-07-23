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
    public class RBDProvinceController : ControllerBase
    {
        private BdrbdContext _context;

        public RBDProvinceController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDProvinceController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _context.RbdProvincia.ToListAsync();

                return Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<RBDProvinceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _context.RbdProvincia.FindAsync(id);

                if (result == null)
                    return NotFound();

                return Ok(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RBDProvinceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var result = JsonSerializer.Deserialize<RbdProvincium>(value);

                if (result == null)
                    return BadRequest();

                _context.RbdProvincia.Add(result);
                await _context.SaveChangesAsync();

                var id = _context.RbdProvincia.Max(x => x.IdProvincia);

                return StatusCode(201, id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDProvinceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var result = JsonSerializer.Deserialize<RbdProvincium>(value);

                var content = await _context.RbdProvincia.FindAsync(id);

                if (result == null || content == null)
                    return BadRequest(content);

                if (result.IdProvincia != content.IdProvincia)
                    return BadRequest();

                content.NomProvincia = result.NomProvincia;

                _context.RbdProvincia.Entry(content).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(JsonSerializer.Serialize(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDProvinceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _context.RbdProvincia.FindAsync(id);

                if (result == null)
                    return BadRequest();

                _context.RbdProvincia.Remove(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
