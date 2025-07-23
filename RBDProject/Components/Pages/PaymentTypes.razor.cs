using Microsoft.AspNetCore.Components;
using Radzen;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class PaymentTypes
    {
        private List<RbdTipoPago> _listPagos { get; set; } = null;
        private IList<RbdTipoPago> _selectedPagos { get; set; } = new List<RbdTipoPago>();
        private RbdTipoPago model { get; set; } = new RbdTipoPago();
        private string utilitymodal { get; set; } = string.Empty;
        private List<RbdEstado> _listEstados { get; set; } = null;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDTipoPago";
        private string httpEstado = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
            GetByEstados();
            Get();
        }

        public void SendTypeModal(RbdTipoPago tp, string e)
        {
            if (tp != null)
            {
                model = tp;
                utilitymodal = e;
            }
        }

        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 2000 });
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

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoPago>>(content);

                        if (result2 == null)
                            _listPagos = new List<RbdTipoPago>();
                        else
                            _listPagos = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetByEstados()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpEstado))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 != null)
                            _listEstados = result2.Take(3).ToList();
                        else
                            _listEstados = new List<RbdEstado>();
                    }

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
                    ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {tipospagos.NomPago} correctamente");
                    await Get();
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
                    ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {tipospagos.NomPago} correctamente");
                    await Get();
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
                    ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {tipospagos.NomPago} correctamente");
                    await Get();
                }
            }
        }

    }
}
