using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
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
            Get();
            GetTypeReceipts();
            _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Comprobantes");
            StateHasChanged();
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
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.GetAsync(_httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdComprobanteFiscal>>(result);

                        if (result2 != null)
                        {
                            _listComprobante = result2;
                        }
                        else
                            _listComprobante = new List<RbdComprobanteFiscal>();
                    }

                }

                StateHasChanged();
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
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se agrego {tc.SecCom} correctamente");
                        await Get();
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
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {tc.SecCom} correctamente");
                        await Get();
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
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {tc.SecCom} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task SendTypeModal(RbdComprobanteFiscal tc, string value)
        {
            utilitymodel = value;
            model = tc;
        }
    }
}
