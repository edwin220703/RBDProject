using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasPorCobrarController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public CuentasPorCobrarController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<CuentasPorCobrarController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/<CuentasPorCobrarController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.FindAsync(id);

                if(content == null)
                    return NoContent();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CuentasPorCobrarController>/Factura/5
        [HttpGet("Factura/{id}")]
        public async Task<IActionResult> GetFacture(long id)
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.Where(x => x.NumFact == id).FirstOrDefaultAsync();

                if (content == null)
                    return NoContent();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<CuentasPorCobrarController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var result = JsonSerializer.Deserialize<RbdCuentasPorCobrar>(value);

                if(result == null)
                    return BadRequest();

                _context.RbdCuentasPorCobrars.Add(result);
                await _context.SaveChangesAsync();

                return StatusCode(201, JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<CuentasPorCobrarController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdCuentasPorCobrar>(value);

                if (value == null || content == null)
                    return BadRequest();

                _context.RbdCuentasPorCobrars.Update(content);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<CuentasPorCobrarController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.FindAsync(id);

                if (content == null) 
                    return BadRequest();

                _context.RbdCuentasPorCobrars.Remove(content);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        //DETALLE FACTURA
        //DETALLE FACTURA
        //DETALLE FACTURA
        //DETALLE FACTURA

        // GET api/<CuentasPorCobrarController>/Factura/5
        [HttpGet("DetalleCuentaPorCobrar/{id}")]
        public async Task<IActionResult> GetDetalleCuentaPorCobrar(long id)
        {
            try
            {
                var content = await _context.RbdDetalleCuentaPorCobrars.Where(x => x.CodCcobro == id).ToListAsync();

                if (content == null)
                    return NoContent();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<CuentasPorCobrarController>
        [HttpPost("DetalleCuentaPorCobrar")]
        public async Task<IActionResult> PostDetalleCuentaPorCobrar([FromBody] string value)
        {
            try
            {
                var result = JsonSerializer.Deserialize<RbdDetalleCuentaPorCobrar>(value);

                if (result == null)
                    return BadRequest();

                _context.RbdDetalleCuentaPorCobrars.Add(result);
                await _context.SaveChangesAsync();

                return StatusCode(201, JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<CuentasPorCobrarController>/5
        [HttpPut("DetalleCuentaPorCobrar/{id}")]
        public async Task<IActionResult> PutDetalleCuentaPorCobrar(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdCuentasPorCobrar>(value);

                if (value == null || content == null)
                    return BadRequest();

                _context.RbdCuentasPorCobrars.Update(content);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<CuentasPorCobrarController>/5
        [HttpDelete("DetalleCuentaPorCobrar/{id}")]
        public async Task<IActionResult> DeleteDetalleCuentaPorCobrar(int id)
        {
            try
            {
                var content = await _context.RbdCuentasPorCobrars.FindAsync(id);

                if (content == null)
                    return BadRequest();

                _context.RbdCuentasPorCobrars.Remove(content);
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
