using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Helpers
{
    public class Factura
    {
        [Inject]
        public IHttpClientFactory _http {  get; set; }

        private string _httpServidor = "Servidor";
        private string _httpCliente = "api/RBDClientes";
        private string _httpEmpleados = "api/Empleados";


        private RbdCliente _cliente {  get; set; } = new RbdCliente();

        private RbdEmpleado _empleado { get; set; } = new RbdEmpleado();


        //CLIENTE
        public string ID_CLI
        {
            get { return _cliente.IdCli; }

            set { GetByIdCli(value); }
        }

        public string NAME_CLI
        {
            get { return _cliente.NomCli; }

            set { GetByNamCli(value); }
        }

        //EMPLEADO
        public string ID_EM
        {
            get { return _empleado.IdEm; }

            set { GetByIdCli(value); }
        }

        public string NAME_EM
        {
            get { return _empleado.NomEm; }

            set { GetByNamCli(value); }
        }

        public int TIPO_DE_PAGO { get; set; }

        public int TIPO_NCF { get; set; }

        public double BALANCE_TOTAL { get; set; }

        public double BALANCE_NETO { get; set; }

        public DateTime FECHA_REGISTRO { get; } = DateTime.Now;


        //CLIENTE
        private async Task GetByIdCli(string value)
        {
            using(var client = _http.CreateClient(_httpServidor))
            {
                using(var content = await client.GetAsync(_httpCliente + $"/ID={value}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdCliente>(result);

                        if(result2 != null)
                        {
                            _cliente = result2;
                        }
                    }
                }
            }
        }

        private async Task GetByNamCli(string value)
        {
            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpCliente + $"/Name={value}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdCliente>(result);

                        if (result2 != null)
                        {
                            _cliente = result2;
                        }
                    }
                }
            }
        }


        //EMPLEADO
        private async Task GetByIdEm(string value)
        {
            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpCliente + $"/ID={value}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdCliente>(result);

                        if (result2 != null)
                        {
                            _cliente = result2;
                        }
                    }
                }
            }
        }

        private async Task GetByNameEm(string value)
        {
            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpCliente + $"/Name={value}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdCliente>(result);

                        if (result2 != null)
                        {
                            _cliente = result2;
                        }
                    }
                }
            }
        }

    }
}
