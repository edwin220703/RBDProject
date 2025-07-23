using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Province
    {
        List<RbdProvincium> _listProvincias { get; set; } = null;
        IList<RbdProvincium> _selectedProvincias { get; set; } = new List<RbdProvincium>();

        //MODAL
        private RbdProvincium model { get; set; } = new RbdProvincium();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDProvince";

        protected override async Task OnInitializedAsync()
        {
            Get();
        }

        public void SendTypeModal(RbdProvincium rbdProvince, string e)
        {
            model = rbdProvince;
            utilitymodal = e;
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
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

                        var result2 = JsonSerializer.Deserialize<List<RbdProvincium>>(result);

                        if (result2 == null)
                            _listProvincias = new List<RbdProvincium>();
                        else
                            _listProvincias = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task Add(RbdProvincium province)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(province)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {province.NomProvincia} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdProvincium provincium)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{provincium.IdProvincia}", JsonSerializer.Serialize(provincium)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {provincium.NomProvincia} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Remove(RbdProvincium provincia)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{provincia.IdProvincia}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Elminacnion", $"Se elimino {provincia.NomProvincia} correctamente");
                        await Get();
                    }
                }
            }
        }
    }
}
