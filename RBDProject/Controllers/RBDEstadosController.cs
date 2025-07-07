using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDEstadosController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDEstadosController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RBDEstadosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdEstados.ToListAsync();
                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RBDEstadosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdEstados.FindAsync(id);

                if(content == null)
                    return BadRequest();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST api/<RBDEstadosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdEstado>(value);

                if(content is null)
                    return BadRequest();

                _context.RbdEstados.Add(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<RBDEstadosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdEstados.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdEstado>(value);

                if(content == null || result is null)
                    return BadRequest();

                content.NomEst= result.NomEst;
                content.Descripcion = result.Descripcion;

                _context.RbdEstados.Entry(content).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<RBDEstadosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdEstados.FindAsync(id);

                if(content is null)
                    return BadRequest();

                _context.RbdEstados.Remove(content);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
