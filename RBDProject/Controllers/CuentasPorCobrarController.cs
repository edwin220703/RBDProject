using DocumentFormat.OpenXml.Office2010.Excel;
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
                var content = await _context.RbdCuentasPorCobrars.Include(d=>d.RbdDetalleCuentaPorCobrars).
                    Include(f=>f.NumFactNavigation).ToListAsync();

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
                var content = await _context.RbdCuentasPorCobrars.
                    Include(d => d.RbdDetalleCuentaPorCobrars).
                    Include(f => f.NumFactNavigation).
                    FirstOrDefaultAsync(x => x.CodCcobro == id);

                if (content == null)
                    return BadRequest();

                return Ok(JsonSerializer.Serialize(content));
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
                var content = await _context.RbdCuentasPorCobrars.
                    Include(x=>x.NumFactNavigation).
                    Where(x => x.NumFact == id).
                    FirstOrDefaultAsync();

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

        // GET api/<CuentasPorCobrarController>/Factura/5
        [HttpGet("cliente/{id}")]
        public IActionResult GetCxCCliente(int id)
        {
            try
            {
                var content = from c in _context.RbdCuentasPorCobrars
                              join f in _context.RbdFacturas on c.NumFact equals f.NumFac
                              join cli in _context.RbdClientes on f.CodCli equals cli.CodCli
                              where cli.CodCli == id
                              select new RbdCuentasPorCobrar
                              {
                                  CodCcobro = c.CodCcobro,
                                  NumFact = f.NumFac,
                                  FechaEmi = c.FechaEmi,
                                  VencPago = c.VencPago,
                                  Balance = c.Balance,
                                  TotalFact = c.TotalFact,
                                  CodEm = c.CodEm,
                                  NumFactNavigation = f
                              };

                if (content == null)
                    return NoContent();

                var jsoncontent = JsonSerializer.Serialize(content);

                return Ok(jsoncontent);
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
        [HttpPost("DetalleCuentaPorCobrar/{id}")]
        public async Task<IActionResult> PostDetalleCuentaPorCobrar(long id,[FromBody] string value)
        {
            try
            {
                //FACTURA
                var fact = await _context.RbdCuentasPorCobrars.Where(X=>X.CodCcobro == id).FirstOrDefaultAsync();

                if (fact == null)
                    return BadRequest();

                //DETALLE
                var content = JsonSerializer.Deserialize<RbdDetalleCuentaPorCobrar>(value);

                if(content == null)
                    return BadRequest();

                _context.RbdDetalleCuentaPorCobrars.Add(content);
                await _context.SaveChangesAsync();

                return StatusCode(201, JsonSerializer.Serialize(content));
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
