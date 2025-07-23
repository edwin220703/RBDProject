using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
            _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Calle");
        }

        public void SendTypeModal(RbdCalle rbdCalle, string e)
        {
            model = rbdCalle;
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
                    ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {calle.NomCalle} correctamente");
                    await Get();
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
                    ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {calle.NomCalle}");
                    await Get();
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
                    ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {calle.NomCalle}");
                    await Get();
                }
            }
        }
    }
}
