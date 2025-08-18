using ClosedXML.Excel;
using ConsoleApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Models;
using System.Data;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Province
    {
        List<RbdProvincium> _listProvincias { get; set; } = new List<RbdProvincium>();
        IList<RbdProvincium> _selectedProvincias { get; set; } = new List<RbdProvincium>();

        //MODAL
        private RbdProvincium model { get; set; } = new RbdProvincium();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDProvince";

        protected override async Task OnInitializedAsync()
        {
            await Get();
            var a =_jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Provincia");
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
            if (_listProvincias.Where(p => p.NomProvincia.Contains(province.NomProvincia)).Count() >= 1)
            {
                bool continous = await _jSRuntime.InvokeAsync<bool>("AlertaInfo", _listProvincias.Where(p => p.NomProvincia.Contains(province.NomProvincia)).Count(), "provincias");

                if (!continous)
                {
                    return;
                }
            }

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

        public void ImprimirReporte(int value)
        {
            Reporte reporte = new Reporte();

            switch (value)
            {
                case 1:
                    {
                        reporte.Provincias(_listProvincias);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedProvincias.Count == 1)
                        {
                            reporte.Provincias(new List<RbdProvincium> { _selectedProvincias[0] });
                        }

                        if (_selectedProvincias.Count > 1)
                        {
                            reporte.Provincias(_selectedProvincias.ToList());
                        }
                    }
                    ; break;
            }
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Articulos");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("Descripcion"),
                new DataColumn("Fecha de Creacion"),
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in _listProvincias)
                        {
                            dt.Rows.Add(a.IdProvincia, a.NomProvincia);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedProvincias)
                        {
                            dt.Rows.Add(a.IdProvincia, a.NomProvincia);
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
                    //(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Nombre");
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Provincias - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
