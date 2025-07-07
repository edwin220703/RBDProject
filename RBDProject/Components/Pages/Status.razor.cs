using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Controllers;
using RBDProject.Models;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Status
    {
        List<RbdEstado> estados { get; set; } = null;
        IList<RbdEstado> _selectedEstados { get; set; } = new List<RbdEstado>();

        //MODAL
        private RbdEstado model { get; set; } = new RbdEstado();
        private string utilitymodal { get; set; } = string.Empty;

        //Opcion buscar
        private string textobuscar { get; set; } = string.Empty;
        private int valor { get; set; } = 1;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
             Get();
        }

        public void SendTypeModal(RbdEstado rbdEstado, string e)
        {
            model = rbdEstado;
            utilitymodal = e;
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });


        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 == null)
                            estados = new List<RbdEstado>();
                        else
                            estados = result2;

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Add(RbdEstado estado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi,JsonSerializer.Serialize(estado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                    }
                }
            }
        }

        public async Task Update(RbdEstado estado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi+$"/{estado.CodEst}", JsonSerializer.Serialize(estado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Get();
                    }
                }
            }
        }

        public async Task Remove(RbdEstado estado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{estado.CodEst}"))
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
