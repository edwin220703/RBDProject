using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDTipoComprobanteController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDTipoComprobanteController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDTipoComprobanteController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdTipoComprobantes.ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/<RBDTipoComprobanteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdTipoComprobantes.FindAsync(id);

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

        // POST api/<RBDTipoComprobanteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var resuelt = JsonSerializer.Deserialize<RbdTipoComprobante>(value);

                if(resuelt == null)
                    return BadRequest();

                _context.RbdTipoComprobantes.Add(resuelt);
                await _context.SaveChangesAsync();

                return Ok(resuelt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDTipoComprobanteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdTipoComprobantes.FindAsync(id);

                var resuelt = JsonSerializer.Deserialize<RbdTipoComprobante>(value);

                if(content == null || resuelt == null)
                    return BadRequest();

                content.NomTipocom = resuelt.NomTipocom;
                content.DesTipocom = resuelt.DesTipocom;

                _context.RbdTipoComprobantes.Entry(content).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(resuelt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDTipoComprobanteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdTipoComprobantes.FindAsync(id);

                if (content == null)
                    return BadRequest(id);

                _context.RbdTipoComprobantes.Remove(content);
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
