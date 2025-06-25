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

        //MODAL
        private RbdEmpleado model { get; set; } = new RbdEmpleado();
        private string utilitymodal { get; set; } = string.Empty;

        //Opcion buscar
        private string textobuscar { get; set; } = string.Empty;
        private int valor { get; set; } = 1;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDEmpleados";

        protected override async Task OnInitializedAsync()
        {
            Get();
        }

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
