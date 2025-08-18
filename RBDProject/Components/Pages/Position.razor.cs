using ClosedXML.Excel;
using ConsoleApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Controllers;
using RBDProject.Models;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Position
    {
        List<RbdCargo> cargos { get; set; } = new List<RbdCargo>();
        IList<RbdCargo> _selectedCargos { get; set; } = new List<RbdCargo>();
        List<RbdEstado> _listEstados { get; set; } = new List<RbdEstado>();

        //MODAL
        private RbdCargo model { get; set; } = new RbdCargo();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDCargos";
        private string httpApiEstado = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
            await Get();
            await GetStatus();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Cargo");

        }

        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdCargo rbdCargo, string e)
        {
            model = rbdCargo;
            utilitymodal = e;
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

                        var result2 = JsonSerializer.Deserialize<List<RbdCargo>>(result);

                        if (result2 == null)
                            cargos = new List<RbdCargo>();
                        else
                            cargos = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetStatus()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpApiEstado))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 == null)
                            _listEstados = new List<RbdEstado>();
                        else
                            _listEstados = result2.Take(3).ToList();
                    }
                }
            }
        }

        public async Task Add(RbdCargo cargo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(cargo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadidco", $"Se agrego {cargo.NomCar} correctemente");
                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdCargo cargo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{cargo.CodCar}", JsonSerializer.Serialize(cargo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {cargo.NomCar} correctemente");
                        await Get();
                    }
                }
            }
        }

        public async Task Remove(RbdCargo cargo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{cargo.CodCar}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {cargo.NomCar} correctemente");
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
                        reporte.Cargo(cargos);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedCargos.Count == 1)
                        {
                            reporte.Cargo(new List<RbdCargo> { _selectedCargos[0] });
                        }

                        if (_selectedCargos.Count > 1)
                        {
                            reporte.Cargo(_selectedCargos.ToList());
                        }
                    }
                    ; break;
            }
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Cargo");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("Descripcion"),
                new DataColumn("Fecha de Creacion"),
                new DataColumn("Estado"),
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in cargos)
                        {
                            dt.Rows.Add(a.CodCar, a.NomCar,a.DesCar,a.FecCreacion,a.CodEstNavigation.NomEst);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedCargos)
                        {
                            dt.Rows.Add(a.CodCar, a.NomCar, a.DesCar, a.FecCreacion, a.CodEstNavigation.NomEst);
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
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Cargos - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}

