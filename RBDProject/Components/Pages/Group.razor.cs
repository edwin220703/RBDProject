using ClosedXML.Excel;
using ConsoleApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Controllers;
using RBDProject.Models;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RBDProject.Components.Pages
{
    partial class Group
    {
        //LISTAS PRINCIPALES
        private List<RbdGrupo> grupos { get; set; } = new List<RbdGrupo>();
        IList<RbdGrupo> _selectedgrupo { get; set; } = new List<RbdGrupo>();
        private List<RbdEstado> estados { get; set; } = new List<RbdEstado>();

        //MODAL
        private RbdGrupo model { get; set; } = new RbdGrupo();
        private string utilitymodal { get; set; } = string.Empty;

        //URL PETICIONES
        private string httpServer = "Servidor";
        private string httpApi = "api/RBDGrupos";
        private string httpEstados = "api/RBDEstados";

        protected override async Task OnInitializedAsync()
        {
            await SendStatusModal();
            await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Grupo");
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdGrupo rbdGrupo, string e)
        {
            model = rbdGrupo;
            utilitymodal = e;
        }

        public async Task SendStatusModal()
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                //TRAYENDO Estados
                using (var content = await client.GetAsync(httpEstados))
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
            }
        }

        public async Task Get()
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                //TRAYENDO GRUPOS
                using (var content = await client.GetAsync(httpApi))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdGrupo>>(result);

                        if (result2 == null)
                            grupos = new List<RbdGrupo>();
                        else
                            grupos = result2;

                    }
                }

                StateHasChanged();
            }
        }


        public async Task Add(RbdGrupo grupo)
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(grupo));

                if (content.IsSuccessStatusCode)
                {
                    ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {grupo.NomGrup} correctamente");
                    await Get();
                }
            }
        }

        public async Task Update(RbdGrupo grupo)
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                var content = await client.PutAsJsonAsync(httpApi + $"/{grupo.CodGrup}", JsonSerializer.Serialize(grupo));

                if (content.IsSuccessStatusCode)
                {
                    ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {grupo.NomGrup} correctamente");
                    await Get();
                }
            }
        }

        public async Task Remove(RbdGrupo grupo)
        {
            using (HttpClient client = _http.CreateClient(httpServer))
            {
                var content = await client.DeleteAsync(httpApi + $"/{grupo.CodGrup}");

                if (content.IsSuccessStatusCode)
                {
                    ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {grupo.NomGrup} correctamente");
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
                        reporte.Grupos(grupos);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedgrupo.Count == 1)
                        {
                            reporte.Grupos(new List<RbdGrupo> { _selectedgrupo[0] });
                        }

                        if (_selectedgrupo.Count > 1)
                        {
                            reporte.Grupos(_selectedgrupo.ToList());
                        }
                    }
                    ; break;
            }
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Grupos");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("Descripcion"),
                new DataColumn("Fecha de Creacion"),
                new DataColumn("Estado")
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in grupos)
                        {
                            var estado = a.CodEstNavigation != null ? a.CodEstNavigation.NomEst : "";

                            dt.Rows.Add(a.CodGrup, a.NomGrup, a.DesGrup, a.FecCreacion, estado);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedgrupo)
                        {
                            var estado = a.CodEstNavigation != null ? a.CodEstNavigation.NomEst : "";
                            dt.Rows.Add(a.CodGrup, a.NomGrup, a.DesGrup, a.FecCreacion, estado);
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
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Grupos - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
