﻿using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Models;
using System.Text.Json;
using System.Threading.Tasks;

namespace RBDProject.Components.Pages
{
    partial class TypesOfReceipts
    {
        private string _httpServer = "Servidor";
        private string _httpApi = "api/RBDTipoComprobante";

        private IEnumerable<RbdTipoComprobante> _tipoComprobante { get; set; } = null;
        private IList<RbdTipoComprobante> _selectTipoComprobante { get; set; } = new List<RbdTipoComprobante>();

        //MODAL
        private RbdTipoComprobante model { get; set; } = new RbdTipoComprobante();
        private string utilitymodel { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Get();
            _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Tipos De Comprobantes");

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

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoComprobante>>(result);

                        if (result2 != null)
                        {
                            _tipoComprobante = result2;
                        }
                        else
                            _tipoComprobante = new List<RbdTipoComprobante>();
                    }

                }
                StateHasChanged();            }
        }

        public async Task Post(RbdTipoComprobante tc)
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.PostAsJsonAsync(_httpApi, JsonSerializer.Serialize(tc)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {tc.NomTipocom} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Put(RbdTipoComprobante tc)
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.PostAsJsonAsync(_httpApi + $"/{tc.CodTipocom}", JsonSerializer.Serialize(tc)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {tc.NomTipocom} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Delete(RbdTipoComprobante tc)
        {
            using (HttpClient client = _http.CreateClient(_httpServer))
            {
                using (var content = await client.DeleteAsync(_httpApi + $"/{tc.CodTipocom}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Elimincion", $"Se elimino {tc.NomTipocom} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task SendTypeModal(RbdTipoComprobante tc, string value)
        {
            if(tc == null)
                tc = new RbdTipoComprobante();

            utilitymodel = value;
            model = tc;
        }

        public async Task Search(int a, string b)
        {

        }

    }
}
