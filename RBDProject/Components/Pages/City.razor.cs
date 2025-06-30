using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class City
    {
        private List<RbdCiudade> _lisciudades { get; set; } = null;
        private IList<RbdCiudade> _selectedCiudades { get; set; } = new List<RbdCiudade>();
        private RbdCiudade model { get; set; } = new RbdCiudade();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RbdCiudades";

        protected override async Task OnInitializedAsync()
        {
             Get();
        }

        public void SendTypeModal(RbdCiudade cliente, string e)
        {
            model = cliente;
            utilitymodal = e;
        }

        public async Task Search(int id, string texto)
        {



        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.GetAsync(httpApi);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var result2 = JsonSerializer.Deserialize<List<RbdCiudade>>(content);

                    if (result2 == null)
                        _lisciudades = new List<RbdCiudade>();
                    else
                        _lisciudades = result2;

                    StateHasChanged();
                }
            }
        }

        public async Task Add(RbdCiudade ciudade)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(ciudade));

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Update(RbdCiudade ciudade)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.PutAsJsonAsync(httpApi + $"/{ciudade.IdCiudad}", JsonSerializer.Serialize(ciudade));

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Remove(RbdCiudade ciudade)
        {
            using (HttpClient client = _http.CreateClient("Servidor"))
            {
                var result = await client.DeleteAsync(httpApi + $"/{ciudade.IdCiudad}");

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }
    }
}
