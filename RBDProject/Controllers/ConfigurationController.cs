using Microsoft.AspNetCore.Mvc;
using RBDProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ConfigurationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: api/<ConfigurationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string? dato = configuration["Configuracion"];

            return Ok(dato);
        }

        // POST api/<ConfigurationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }
    }
}
