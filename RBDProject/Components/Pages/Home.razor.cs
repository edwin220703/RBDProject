
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Home
    {
        private string _httpServidor = "Servidor";
        private string _httpFacturas = "api/RBDFacturas";
        private string _httpEmpleados = "api/RbdEmpleados";

        private List<RbdFactura> _listFacturas { get; set; } = new List<RbdFactura>();
        private List<RbdEmpleado> _listEmpleados { get; set; } = new List<RbdEmpleado>();

        protected override async Task OnInitializedAsync()
        {
            Get();
        }

        public async Task Get()
        {
            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpFacturas))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdFactura>>(result);

                        if (result2 != null)
                            _listFacturas = result2;
                    }
                }

                using (var content = await client.GetAsync(_httpEmpleados))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEmpleado>>(result);

                        if (result2 != null)
                            _listEmpleados = result2;
                    }
                }
                StateHasChanged();
            }
        }






    }
}
