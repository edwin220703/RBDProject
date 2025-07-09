using Microsoft.AspNetCore.Components;
using Radzen;
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
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {articulo.NumFac} correcmtamente");
                        await Get();
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
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {articulo.NumFac} correctamente");
                        await Get();
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
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {articulo.NumFac} correctamente");
                        await Get();
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
