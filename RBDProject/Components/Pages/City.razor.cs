using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class City
    {
        private List<RbdCiudade> _lisciudades { get; set; } = new List<RbdCiudade>();
        private List<RbdProvincium> _listprovincia { get; set; } = new List<RbdProvincium>();
        private IList<RbdCiudade> _selectedCiudades { get; set; } = new List<RbdCiudade>();

        private RbdCiudade model { get; set; } = new RbdCiudade();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RbdCiudades";
        private string httpProvincia = "api/RbdProvince";

        protected override async Task OnInitializedAsync()
        {
            GetOthers();
            Get();
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdCiudade cliente, string e)
        {
            model = cliente;
            utilitymodal = e;
        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var result = await client.GetAsync(httpApi))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCiudade>>(content);

                        if (result2 == null)
                            _lisciudades = new List<RbdCiudade>();
                        else
                            _lisciudades = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetOthers()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var result = await client.GetAsync(httpProvincia))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdProvincium>>(content);

                        if (result2 == null)
                            _listprovincia = new List<RbdProvincium>();
                        else
                            _listprovincia = result2;

                    }
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
                    ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {ciudade.NomCiudad} correctamente");
                    await Get();
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
                    ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {ciudade.NomCiudad} correctamente");
                    await Get();
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
                    ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {ciudade.NomCiudad} correctamente");
                    await Get();
                }
            }
        }
    }
}
