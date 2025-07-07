using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Models;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace RBDProject.Components.Pages
{
    partial class Street
    {
        private List<RbdCalle> _listCalle { get; set; } = null;
        private IList<RbdCalle> _selectedCalle { get; set; } = new List<RbdCalle>();
        private RbdCalle model { get; set; } = new RbdCalle();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDCalles";

        protected override async Task OnInitializedAsync()
        {
             Get();
        }

        public void SendTypeModal(RbdCalle rbdCalle, string e)
        {
            model = rbdCalle;
            utilitymodal = e;
        }

        public async Task Search(int id, string texto)
        {



        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });


        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.GetAsync(httpApi);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    var result2 = JsonSerializer.Deserialize<List<RbdCalle>>(content);

                    if (result2 == null)
                        _listCalle = new List<RbdCalle>();
                    else
                        _listCalle = result2;

                    StateHasChanged();
                }
            }
        }

        public async Task Add(RbdCalle calle)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(calle));

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Update(RbdCalle calle)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                var result = await client.PutAsJsonAsync(httpApi + $"/{calle.IdCalle}", JsonSerializer.Serialize(calle));

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }

        public async Task Remove(RbdCalle calle)
        {
            using (HttpClient client = _http.CreateClient("Servidor"))
            {
                var result = await client.DeleteAsync(httpApi + $"/{calle.IdCalle}");

                if (result.IsSuccessStatusCode)
                {
                    Get();
                }
            }
        }
    }
}
