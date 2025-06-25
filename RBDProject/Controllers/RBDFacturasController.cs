using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDFacturasController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDFacturasController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDFacturasController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdFacturas.ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<RBDFacturasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdFacturas.FindAsync(id);

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

        // POST api/<RBDFacturasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdFactura>(value);

                if(content == null)
                    return BadRequest();

                _context.RbdFacturas.Add(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDFacturasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdFacturas.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdFactura>(value);

                if(content == null || result == null) 
                    return BadRequest();

                _context.RbdFacturas.Update(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDFacturasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdFacturas.FindAsync(id);

                if( content == null ) 
                    return BadRequest();

                _context.RbdFacturas.Remove(content);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return  StatusCode(500, ex.Message);
            }
        }


        //DETALLE FACTURA
        //DETALLE FACTURA
        //DETALLE FACTURA
        //DETALLE FACTURA

        // GET api/<RBDFacturasController>/DetalleFactura/5
        [HttpGet("DetalleFactura/{id}")]
        public async Task<IActionResult> GetDetalleFactura(int id)
        {
            try
            {
                var content = await _context.RbdDetalleFacturas.Where(x => x.NumFac == id).ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RBDFacturasController>/DetalleFactura
        [HttpPost("DetalleFactura")]
        public async Task<IActionResult> PostDetalleFactura([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdDetalleFactura>(value);

                if (content == null)
                    return BadRequest();

                _context.RbdDetalleFacturas.Add(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDFacturasController>/DetalleFactura/5
        [HttpPut("DetalleFactura/{id}")]
        public async Task<IActionResult> PutDetalleFactura(int id, [FromBody] string value)
        {
            try
            {
                var result = JsonSerializer.Deserialize<RbdDetalleFactura>(value);

                if (result == null)
                    return BadRequest();

                _context.RbdDetalleFacturas.Update(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDFacturasController>/DetalleFactura/5/5
        [HttpDelete("DetalleFactura/{id}/{art}")]
        public async Task<IActionResult> DeleteDetalleFactura(int id, int art)
        {
            try
            {
                var content = await _context.RbdDetalleFacturas.Where(x => x.NumFac == id && x.CodArt == art).FirstAsync();

                if (content == null)
                    return BadRequest();

                _context.RbdDetalleFacturas.Remove(content);
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
