using ClosedXML.Excel;
using ConsoleApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Models;
using System.Data;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace RBDProject.Components.Pages
{
    partial class Street
    {
        private List<RbdCalle> _listCalle { get; set; } = new List<RbdCalle>();
        private IList<RbdCalle> _selectedCalle { get; set; } = new List<RbdCalle>();
        private RbdCalle model { get; set; } = new RbdCalle();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDCalles";

        protected override async Task OnInitializedAsync()
        {
            await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Calle");
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

        public void ImprimirReporte(int value)
        {
            Reporte reporte = new Reporte();

            switch (value)
            {
                case 1:
                    {
                        reporte.Calle(_listCalle);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedCalle.Count == 1)
                        {
                            reporte.Calle(new List<RbdCalle> { _selectedCalle[0] });
                        }

                        if (_selectedCalle.Count > 1)
                        {
                            reporte.Calle(_selectedCalle.ToList());
                        }
                    }
                    ; break;
            }
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("TiposComprobantes");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("Ciudad"),
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in _listCalle)
                        {
                            dt.Rows.Add(a.IdCalle, a.NomCalle, a.IdCiudadNavigation.NomCiudad);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedCalle.ToList())
                        {
                            dt.Rows.Add(a.IdCalle, a.NomCalle, a.IdCiudadNavigation.NomCiudad);
                        }
                    }
                    ; break;
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                var mbyte = new byte[0];

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    mbyte = stream.ToArray();
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Calles - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
