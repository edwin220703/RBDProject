using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Globalization;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDArticulosController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDArticulosController(BdrbdContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdArticulos.ToListAsync();

                return Ok(JsonSerializer.Serialize(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        // GET api/<RBDArticulosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdArticulos.FindAsync(id);

                if(content == null)
                    return NoContent();

                return Ok(JsonSerializer.Serialize(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Cod={id}")]
        public async Task<IActionResult> GetByCod(string id)
        {
            try
            {
                var content = await _context.RbdArticulos.Where(x=>x.IdArt.Contains(id)).ToListAsync();

                if (content == null)
                    return NoContent();

                return Ok(JsonSerializer.Serialize(content.FirstOrDefault()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Name={name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var content = await _context.RbdArticulos.Where(x => x.NomArt.Contains(name)).ToListAsync();

                if (content == null)
                    return NoContent();

                return Ok(JsonSerializer.Serialize(content.FirstOrDefault()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RBDArticulosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdArticulo>(value);

                if( content == null )
                    return BadRequest();

                _context.RbdArticulos.Add(content);
                await _context.SaveChangesAsync();

                return StatusCode(201,content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDArticulosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdArticulos.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdArticulo>(value);

                if (result == null || content == null)
                    return BadRequest();

                _context.RbdArticulos.Update(result);
                await _context.SaveChangesAsync();

                return Ok(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDArticulosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdArticulos.FindAsync(id);

                if(content == null)
                    return BadRequest();

                _context.RbdArticulos.Remove(content);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        //LISTA DE PRECIOS
        //LISTA DE PRECIOS
        //LISTA DE PRECIOS
        //LISTA DE PRECIOS

        // GET api/<RBDArticulosController>/ListaPrecios/5
        [HttpGet("ListaPrecios/{id}")]
        public async Task<IActionResult> GetListaPrecios(int id)
        {
            try
            {
                var content = await _context.RbdListaDePrecios.Where(x=>x.CodArt==id).ToListAsync();

                if (content == null)
                    return NoContent();

                return Ok(JsonSerializer.Serialize(content));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<RBDArticulosController>/ListaPrecios
        [HttpPost("ListaPrecios")]
        public async Task<IActionResult> PostListaPrecios([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdListaDePrecio>(value);

                if (content == null)
                    return BadRequest();

                _context.RbdListaDePrecios.Add(content);
                await _context.SaveChangesAsync();

                return Ok(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RBDArticulosController>/ListaPrecios/5
        [HttpPut("ListaPrecios/{id}")]
        public async Task<IActionResult> PutListaPrecios(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdListaDePrecios.Where(x=>x.CodArt == id).FirstOrDefaultAsync();

                var result = JsonSerializer.Deserialize<RbdListaDePrecio>(value);

                if (result == null || content == null)
                    return BadRequest();

                _context.RbdListaDePrecios.Update(result);
                await _context.SaveChangesAsync();

                return Ok(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RBDArticulosController>/ListaPrecios/5
        [HttpDelete("ListaPrecios/{id}")]
        public async Task<IActionResult> DeleteListaPrecios(int id, [FromBody]string value)
        {
            try
            {
                var content = await _context.RbdListaDePrecios.Where(x => x.CodArt == id).FirstOrDefaultAsync();

                var result = JsonSerializer.Deserialize<RbdListaDePrecio>(value);

                if (content == null || result == null)
                    return BadRequest();

                _context.RbdListaDePrecios.Remove(result);
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
