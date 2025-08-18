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
    partial class Employee
    {
        List<RbdEmpleado> empleados { get; set; } = new List<RbdEmpleado>();
        IList<RbdEmpleado> _selectedEmpleados { get; set; } = new List<RbdEmpleado>();

        //MODAL
        private RbdEmpleado model { get; set; } = new RbdEmpleado();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDEmpleados";
        private string httpCiudad = "api/RBDCiudades";
        private string httpCalle = "api/RBDCalles";
        private string httpCargo = "api/RBDCargos";
        private string httpEstado = "api/RBDEstados";

        //ADICIONALES
        private List<RbdCiudade> _listCiudades { get; set; } = new List<RbdCiudade>();
        private List<RbdCalle> _listCalle { get; set; } = new List<RbdCalle>();
        private List<RbdCargo> _listCargo { get; set; } = new List<RbdCargo>();
        private List<RbdEstado> _listEstado { get; set; } = new List<RbdEstado>();

        protected override async Task OnInitializedAsync()
        {
            await GetbyOthers();
            await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Empleados");
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdEmpleado empleado, string e)
        {
            string? pre, result;

            if (empleado.IdEm == null)
            {
                pre = _confi.GetValue<string>("Configuracion:Codigo-Empleado");

                if (empleados.Count != 0)
                {
                    result = pre + (empleados.Max(e => e.CodEm) + 1);
                }
                else
                {
                    result = pre + "1";
                }

                empleado.IdEm = result;
            }

            model = empleado;
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

                        var result2 = JsonSerializer.Deserialize<List<RbdEmpleado>>(result);

                        if (result2 == null)
                            empleados = new List<RbdEmpleado>();
                        else
                            empleados = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetbyOthers()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.GetAsync(httpCiudad))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCiudade>>(result);

                        if (result2 != null)
                            _listCiudades = result2;
                    }
                }

                using (var content = await client.GetAsync(httpCalle))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCalle>>(result);

                        if (result2 != null)
                            _listCalle = result2;
                    }
                }

                using (var content = await client.GetAsync(httpCargo))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCargo>>(result);

                        if (result2 != null)
                            _listCargo = result2;
                    }
                }

                using (var content = await client.GetAsync(httpEstado))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdEstado>>(result);

                        if (result2 != null)
                            _listEstado = result2.Take(3).ToList();
                    }
                }
            }

            StateHasChanged();
        }

        public async Task Add(RbdEmpleado empleado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(empleado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {empleado.NomEm} correctamente");
                        
                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdEmpleado empleado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{empleado.CodEm}", JsonSerializer.Serialize(empleado)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {empleado.NomEm} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Remove(RbdEmpleado empleado)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{empleado.CodEm}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {empleado.NomEm} correctamente");
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
                        reporte.MultiEmpleados(empleados);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedEmpleados.Count == 1)
                        {
                            reporte.MultiEmpleados(new List<RbdEmpleado> { _selectedEmpleados[0] });
                        }

                        if (_selectedEmpleados.Count > 1)
                        {
                            reporte.MultiEmpleados(_selectedEmpleados.ToList());
                        }
                    }
                    ; break;
            }
        }

        public void ImprimirReporteIndividual(RbdEmpleado _empleado)
        {
            Reporte R = new Reporte();

            R.Empleados(_empleado);
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Articulos");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("DNI/Cedula"),
                new DataColumn("Genero"),

                new DataColumn("Provincia"),
                new DataColumn("Ciudad"),
                new DataColumn("Calle"),
                new DataColumn("Detalle"),

                new DataColumn("Usuario"),
                new DataColumn("Cargo"),
                new DataColumn("Sueldo"),
                new DataColumn("Estado"),
                new DataColumn("Fecha de Creacion"),
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in empleados)
                        {
                            var genero = a.CodGenNavigation != null ? a.CodGenNavigation.NomGen : "";
                            var provincia = a.IdProvinciaNavigation != null ? a.IdProvinciaNavigation.NomProvincia : "";
                            var Ciudad = a.IdCiudadNavigation != null ? a.IdCiudadNavigation.NomCiudad : "";
                            var Calle = a.IdCalleNavigation != null ? a.IdCalleNavigation.NomCalle : "";
                            var Cargo = a.CodCarNavigation != null ? a.CodCarNavigation.NomCar : "";


                            dt.Rows.Add(a.CodEm, a.IdEm, a.NomEm,a.DniEm,genero, provincia,
                                Ciudad,Calle,a.DetallDirec,a.NomUs,Cargo,a.Suedms,a.CodEstNavigation.NomEst,a.NumPer);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedEmpleados)
                        {
                            var genero = a.CodGenNavigation != null ? a.CodGenNavigation.NomGen : "";
                            var provincia = a.IdProvinciaNavigation != null ? a.IdProvinciaNavigation.NomProvincia : "";
                            var Ciudad = a.IdCiudadNavigation != null ? a.IdCiudadNavigation.NomCiudad : "";
                            var Calle = a.IdCalleNavigation != null ? a.IdCalleNavigation.NomCalle : "";
                            var Cargo = a.CodCarNavigation != null ? a.CodCarNavigation.NomCar : "";


                            dt.Rows.Add(a.CodEm, a.IdEm, a.NomEm, a.DniEm, genero, provincia,
                                Ciudad, Calle, a.DetallDirec, a.NomUs, Cargo, a.Suedms, a.CodEstNavigation.NomEst, a.NumPer);
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
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Empleados - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
