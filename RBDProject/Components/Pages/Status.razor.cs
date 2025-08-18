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
    partial class Status
    {
        List<RbdEstado> estados { get; set; } = new List<RbdEstado>();
        IList<RbdEstado> _selectedEstados { get; set; } = new List<RbdEstado>();

        //MODAL
        private RbdEstado model { get; set; } = new RbdEstado();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
             await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Estados");
        }

        public void SendTypeModal(RbdEstado rbdEstado, string e)
        {
            model = rbdEstado;
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

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 == null)
                            estados = new List<RbdEstado>();
                        else
                            estados = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task Add(RbdEstado estado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(estado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {estado.NomEst} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdEstado estado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{estado.CodEst}", JsonSerializer.Serialize(estado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {estado.NomEst} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Remove(RbdEstado estado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{estado.CodEst}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Elminacnion", $"Se elimino {estado.NomEst} correctamente");
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
                        reporte.Estados(estados);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedEstados.Count == 1)
                        {
                            reporte.Estados(new List<RbdEstado> { _selectedEstados[0] });
                        }

                        if (_selectedEstados.Count > 1)
                        {
                            reporte.Estados(_selectedEstados.ToList());
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
                        foreach (var a in estados)
                        {
                            dt.Rows.Add(a.CodEst, a.NomEst, a.Descripcion, a.FecCreacion);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedEstados)
                        {
                            dt.Rows.Add(a.CodEst, a.NomEst, a.Descripcion, a.FecCreacion);
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
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Estados - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
