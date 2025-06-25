using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDClientesController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDClientesController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDClientesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdClientes.ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/<RBDClientesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdClientes.FindAsync(id);

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

        // POST api/<RBDClientesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdCliente>(value);

                if(content == null) 
                    return BadRequest();

                _context.RbdClientes.Add(content);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDClientesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdClientes.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdCliente>(value);

                if (content == null || result == null)
                    return BadRequest();

                _context.RbdClientes.Update(content);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDClientesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdClientes.FindAsync(id);

                if(content == null) 
                    return BadRequest();

                _context.RbdClientes.Remove(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        //ZONA CELULAR
        //ZONA CELULAR
        //ZONA CELULAR
        // GET api/<RBDClientesController>/Telefono/5
        [HttpGet("Telefono/{id}")]
        public async Task<IActionResult> GetTelefono(int id)
        {
            try
            {
                var content = await _context.RbdTelefonoClientes.Where(x => x.CodCli == id).ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Telefono")]
        public async Task<IActionResult> PostTelefono([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdTelefonoCliente>(value);

                if (content == null)
                    return BadRequest();

                _context.RbdTelefonoClientes.Add(content);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDClientesController>/Telefono/5
        [HttpPut("Telefono/{id}")]
        public async Task<IActionResult> PutTelefono(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdTelefonoClientes.Where(x=>x.CodCli == id).FirstAsync();

                var result = JsonSerializer.Deserialize<RbdCliente>(value);

                if (content == null || result == null)
                    return BadRequest();

                _context.RbdTelefonoClientes.Update(content);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDClientesController>/Telefono/5
        [HttpDelete("/Telefono{id}")]
        public async Task<IActionResult> DeleteTelefono(int id, [FromBody]string value)
        {
            try
            {
                var content = await _context.RbdTelefonoClientes.Where(x=>x.CodCli==id).FirstAsync();

                var result = JsonSerializer.Deserialize<RbdTelefonoCliente>(value);

                if (content == null || result == null)
                    return BadRequest();

                _context.RbdTelefonoClientes.Remove(result);
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
