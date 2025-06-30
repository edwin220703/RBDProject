using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class TaxReceipt
    {
        private string _httpServer = "Servidor";
        private string _httpApi = "api/RBDComprobanteFiscales";

        private IEnumerable<RbdComprobanteFiscal> _listComprobante { get; set; } = null;
        private IList<RbdComprobanteFiscal> _selectComprobante { get; set; } = new List<RbdComprobanteFiscal>();
        private List<RbdTipoComprobante> _listTiposComprobantes { get; set; } = new List<RbdTipoComprobante>();

        //MODAL
        private RbdComprobanteFiscal model { get; set; } = new RbdComprobanteFiscal();
        private string utilitymodel { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Get();

            //_selectComprobante.Add(_listComprobante.First());
            StateHasChanged();
        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.GetAsync(_httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdComprobanteFiscal>>(result);

                        if (result2 != null)
                            _listComprobante = result2;
                        else
                            _listComprobante = new List<RbdComprobanteFiscal>();
                    }

                }
            }
        }

        public async Task GetTypeReceipts()
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.GetAsync("api/RBDTipoComprobante"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoComprobante>>(result);

                        if (result2 != null)
                            _listTiposComprobantes = result2;
                        else
                            _listTiposComprobantes = new List<RbdTipoComprobante>();
                    }

                }
            }
        }

        public async Task Post(RbdComprobanteFiscal tc)
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.PostAsJsonAsync(_httpApi, JsonSerializer.Serialize(tc)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        await Get();
                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Put(RbdComprobanteFiscal tc)
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.PostAsJsonAsync(_httpApi + $"/{tc.CodTipocom}", JsonSerializer.Serialize(tc)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        await Get();
                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Delete(RbdComprobanteFiscal tc)
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.DeleteAsync(_httpApi + $"/{tc.CodTipocom}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        await Get();
                        StateHasChanged();
                    }
                }
            }
        }

        public async Task SendTypeModal(RbdComprobanteFiscal tc, string value)
        {
            if (tc == null)
                tc = new RbdComprobanteFiscal();

            await GetTypeReceipts();
            utilitymodel = value;
            model = tc;
        }

        public async Task Search(int a, string b)
        {

        }

    }
}
