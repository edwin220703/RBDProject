using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDTipoPagoController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDTipoPagoController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDTipoPagoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdTipoPagos.ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/<RBDTipoPagoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdTipoPagos.FindAsync(id);

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

        // POST api/<RBDTipoPagoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                if (value is null)
                    return BadRequest();

                var content = JsonSerializer.Deserialize<RbdTipoPago>(value);

                if(content == null)
                    return BadRequest();

                _context.RbdTipoPagos.Add(content);
                await _context.SaveChangesAsync();

                return StatusCode(201, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDTipoPagoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdTipoPagos.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdTipoPago>(value);

                if (content is null || result is null) 
                    return BadRequest();


                _context.RbdTipoPagos.Update(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDTipoPagoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdTipoPagos.FindAsync(id);

                if(content is null)
                    return BadRequest();

                _context.RbdTipoPagos.Remove(content);
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
