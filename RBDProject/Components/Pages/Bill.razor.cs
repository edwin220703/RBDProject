using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Bill
    {
        private List<RbdFactura> _listFacturas { get; set; } = null;
        private IList<RbdFactura> _selectedFactura { get; set; } = new List<RbdFactura>();
        private RbdFactura model { get; set; } = new RbdFactura();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private readonly string httpServidor = "Servidor";
        private readonly string httpApi = "api/RBDFacturas";

        protected override async Task OnInitializedAsync()
        {
            Get();
        }

        public async Task Search(int id, string value)
        {

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

                        var result2 = JsonSerializer.Deserialize<List<RbdFactura>>(result);

                        if (result2 == null)
                            _listFacturas = new List<RbdFactura>();
                        else
                            _listFacturas = result2;

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Add(RbdFactura articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Update(RbdFactura articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{articulo.NumFac}", JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Remove(RbdFactura articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{articulo.NumFac}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                        StateHasChanged();
                    }
                }
            }
        }

        public void SendTypeModal(RbdFactura rbdFactura, string e)
        {
            model = rbdFactura;
            utilitymodal = e;
        }

        public async Task CrearFactura()
        {
            _navigation.NavigateTo("CrearFactura");
        }
    }
}
