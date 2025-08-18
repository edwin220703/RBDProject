using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly BdrbdContext _context;
        private Info _information;

        public InformationController(BdrbdContext context)
        {
            this._context = context;
            _information = new Info();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var countArt = await _context.RbdArticulos.CountAsync();

                var countCxc = await _context.RbdCuentasPorCobrars.CountAsync();

                var countFact = await _context.RbdFacturas.CountAsync();

                var countCli = await _context.RbdClientes.CountAsync();

                //VENTAS DEL DIA
                var a = _context.RbdFacturas.GroupBy(x => x.FechaReg.Date).Select(f => new WeekSell
                {
                    Day = f.Key.ToString(),
                    Cant = f.Sum(x => x.TotalNeto)
                }).ToArray();

                //VENTAS MENSUALESK
                var m = _context.RbdFacturas.GroupBy(x => x.FechaReg.Month).Select(f => new MonthShell
                {
                    MONTH = f.Key.ToString(),
                    Cant = f.Sum(x=>x.TotalNeto)
                }).ToArray();

                _information.CountArt = countArt;
                _information.CountCxc = countCxc;
                _information.CountFact = countFact;
                _information.CountCli = countCli;
                _information.ShellWeek = a;
                _information.MonthSell = m;

                var result = JsonSerializer.Serialize(_information);

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
