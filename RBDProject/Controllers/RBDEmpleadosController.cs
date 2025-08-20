using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RBDEmpleadosController : ControllerBase
    {
        private readonly BdrbdContext _context;

        public RBDEmpleadosController(BdrbdContext context)
        {
            _context = context;
        }

        // GET: api/<RbdEmpleadosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var content = await _context.RbdEmpleados.Include(G => G.CodGenNavigation).
                    Include(ci => ci.IdProvinciaNavigation).
                    Include(ci => ci.IdCiudadNavigation).
                    Include(ca => ca.IdCalleNavigation).
                    Include(c => c.CodCarNavigation).
                    Include(e => e.CodEstNavigation).
                    Include(t => t.RbdTelefonoEmpleados).ToListAsync();

                var result = JsonSerializer.Serialize(content);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<RbdEmpleadosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var content = await _context.RbdEmpleados.Include(G => G.CodGenNavigation).
                                Include(ci => ci.IdProvinciaNavigation).
                                Include(ci => ci.IdCiudadNavigation).
                                Include(ca => ca.IdCalleNavigation).
                                Include(c => c.CodCarNavigation).
                                Include(e => e.CodEstNavigation).
                                Include(t => t.RbdTelefonoEmpleados).
                                Where(x=>x.CodEm == id).FirstOrDefaultAsync();

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


        // GET api/<RbdEmpleadosController>/5
        [HttpGet("{name}/{password}")]
        public async Task<IActionResult> Get(string name, string password)
        {
            try
            {
                var content = await _context.RbdEmpleados.
                                Include(c => c.CodCarNavigation).
                                Where(x => x.NomUs == name && x.NomClav == password).FirstOrDefaultAsync();

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

        // GET api/<RbdEmpleadosController>/5
        [HttpGet("Name={name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var content = await _context.RbdEmpleados.
                                Include(c => c.CodCarNavigation).
                                Where(x => x.NomUs == name).FirstOrDefaultAsync();

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

        // POST api/<RbdEmpleadosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            try
            {
                var content = JsonSerializer.Deserialize<RbdEmpleado>(value);

                if (content == null)
                    return BadRequest();

                _context.RbdEmpleados.Add(content);
                await _context.SaveChangesAsync();

                var id = _context.RbdEmpleados.Max(x => x.CodEm);

                //AÑADIR TELEFONO
                if (content.RbdTelefonoEmpleados.Count != 0)
                {
                    RbdTelefonoEmpleado tel = new RbdTelefonoEmpleado();
                    tel.CodEm = id;
                    tel.TelEm = content.RbdTelefonoEmpleados.First().TelEm;

                    _context.RbdTelefonoEmpleados.Add(tel);
                    await _context.SaveChangesAsync();
                }

                return StatusCode(201, JsonSerializer.Serialize(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RbdEmpleadosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdEmpleados.FindAsync(id);

                var result = JsonSerializer.Deserialize<RbdEmpleado>(value);

                if (content == null || result == null)
                    return BadRequest();

                content.IdEm = result.IdEm;
                content.NomEm = result.NomEm;
                content.DniEm = result.DniEm;
                content.NomUs = result.NomUs;
                content.NomClav = result.NomClav;
                content.CodGen = result.CodGen;
                content.IdCiudad = result.IdCiudad;
                content.IdCalle = result.IdCalle;
                content.DetallDirec = result.DetallDirec;
                content.CodCar = result.CodCar;
                content.Suedms = result.Suedms;
                content.CodEst = result.CodEst;

                _context.RbdEmpleados.Entry(content).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RbdEmpleadosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var content = await _context.RbdEmpleados.FindAsync(id);

                if (content == null)
                    return BadRequest();

                _context.RbdEmpleados.Remove(content);
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
        // GET api/<RbdEmpleadosController>/Telefono/5
        [HttpGet("Telefono/{id}")]
        public async Task<IActionResult> GetTelefono(int id)
        {
            try
            {
                var content = await _context.RbdTelefonoEmpleados.Where(x => x.CodEm == id).ToListAsync();

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
                var content = JsonSerializer.Deserialize<RbdTelefonoEmpleado>(value);

                if (content == null)
                    return BadRequest();

                _context.RbdTelefonoEmpleados.Add(content);
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<RbdEmpleadosController>/Telefono/5
        [HttpPut("Telefono/{id}")]
        public async Task<IActionResult> PutTelefono(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdTelefonoEmpleados.Where(x => x.CodEm == id).FirstAsync();

                var result = JsonSerializer.Deserialize<RbdEmpleado>(value);

                if (content == null || result == null)
                    return BadRequest();

                _context.RbdTelefonoEmpleados.Update(content);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<RbdEmpleadosController>/Telefono/5
        [HttpDelete("/Telefono{id}")]
        public async Task<IActionResult> DeleteTelefono(int id, [FromBody] string value)
        {
            try
            {
                var content = await _context.RbdTelefonoEmpleados.Where(x => x.CodEm == id).FirstAsync();

                var result = JsonSerializer.Deserialize<RbdTelefonoEmpleado>(value);

                if (content == null || result == null)
                    return BadRequest();

                _context.RbdTelefonoEmpleados.Remove(result);
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
