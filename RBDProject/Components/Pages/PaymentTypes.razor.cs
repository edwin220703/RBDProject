using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class PaymentTypes
    {
        private List<RbdTipoPago> _listPagos { get; set; } = null;
        private RbdTipoPago model { get; set; } = new RbdTipoPago();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDTipoPago";

        protected override async Task OnInitializedAsync()
        {
            Get();
        }

        public void SendTypeModal(RbdTipoPago tipopago, string e)
        {
            model = tipopago;
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

                    var result2 = JsonSerializer.Deserialize<List<RbdTipoPago>>(content);

                    if (result2 == null)
                        _listPagos = new List<RbdTipoPago>();
                    else
                        _listPagos = result2;

                    StateHasChanged();
                }
            }
        }

        public async Task Add(RbdTipoPago tipospagos)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(tipospagos));

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Update(RbdTipoPago tipospagos)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.PutAsJsonAsync(httpApi + $"/{tipospagos.CodTipago}", JsonSerializer.Serialize(tipospagos));

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Remove(RbdTipoPago tipospagos)
        {
            using (HttpClient client = _http.CreateClient("Servidor"))
            {
                var result = await client.DeleteAsync(httpApi + $"/{tipospagos.CodTipago}");

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }
    }
}
