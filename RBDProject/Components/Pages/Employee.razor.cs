using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Controllers;
using RBDProject.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Employee
    {
        List<RbdEmpleado> empleados  { get; set; } = null;
        IList<RbdEmpleado> _selectedEmpleados { get; set; } = new List<RbdEmpleado>();
        //MODAL
        private RbdEmpleado model { get; set; } = new RbdEmpleado();
        private string utilitymodal { get; set; } = string.Empty;

        //Opcion buscar
        private string textobuscar { get; set; } = string.Empty;
        private int valor { get; set; } = 1;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDEmpleados";
        private string httpCiudad = "api/RBDCiudades";
        private string httpCalle = "api/RBDCalles";
        private string httpCargo = "api/RBDCargos";
        private string httpEstado = "api/RBDEstados";

        //ADICIONALES
        private List<RbdCiudade> _listCiudades { get; set; } = new List<RbdCiudade>();
        private List<RbdCalle> _listCalle { get; set; } = new List<RbdCalle>();
        private List<RbdCargo> _listCargo { get; set; } = new List<RbdCargo>();
        private List<RbdEstado> _listEstado { get; set; } = new List<RbdEstado>();

        protected override async Task OnInitializedAsync()
        {
            GetbyOthers();
            Get();
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });


        public void SendTypeModal(RbdEmpleado empleado, string e)
        {
            model = empleado;
            utilitymodal = e;
        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEmpleado>>(result);

                        if (result2 == null)
                            empleados = new List<RbdEmpleado>();
                        else
                            empleados = result2;

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task GetbyOthers()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpCiudad))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCiudade>>(result);

                        if (result2 != null)
                            _listCiudades = result2;
                    }
                }

                using (var content = await client.GetAsync(httpCalle))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCalle>>(result);

                        if (result2 != null)
                            _listCalle = result2;
                    }
                }

                using (var content = await client.GetAsync(httpCargo))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCargo>>(result);

                        if (result2 != null)
                            _listCargo = result2;
                    }
                }

                using (var content = await client.GetAsync(httpEstado))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 != null)
                            _listEstado = result2;
                    }
                }
            }

            StateHasChanged();
        }

        public async Task Add(RbdEmpleado empleado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi,JsonSerializer.Serialize(empleado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                    }
                }
            }
        }

        public async Task Update(RbdEmpleado empleado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi+$"/{empleado.CodEm}", JsonSerializer.Serialize(empleado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                    }
                }
            }
        }

        public async Task Remove(RbdEmpleado empleado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi+$"/{empleado.CodEm}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                    }
                }
            }
        }

        public async Task Search()
        {
           
            
        }
    }
}
