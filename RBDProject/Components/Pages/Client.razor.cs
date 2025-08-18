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
    partial class Client
    {
        List<RbdCliente> clientes { get; set; } = new List<RbdCliente>();
        IList<RbdCliente> _selectedCliente { get; set; } = new List<RbdCliente>();
        List<RbdCiudade> _listCiudades { get; set; } = new List<RbdCiudade>();
        List<RbdCalle> _listCalle { get; set; } = new List<RbdCalle>();
        List<RbdProvincium> _listProvincia { get; set; } = new List<RbdProvincium>();

        //MODAL
        private RbdCliente model { get; set; } = new RbdCliente();
        private string utilitymodal { get; set; } = string.Empty;

        //Opcion buscar
        private string textobuscar { get; set; } = string.Empty;
        private int valor { get; set; } = 1;

        //API
        private string httpServidor = "Servidor";
        private string httpApi = "api/RBDClientes";
        private string httpApiCiudades = "api/RBDCiudades";
        private string httpApiCalles = "api/RBDCalles";
        private string httpApiProvincia = "api/RBDProvince";

        protected override async Task OnInitializedAsync()
        {
            await GetOthers();
            await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Cliente");
        }


        //MENSAJE CUANDO PASAS EL MOUSE
        public void ShowTooltip(ElementReference elementReference, string text) => _tooltipService.Open(elementReference, text, new TooltipOptions() { Position = TooltipPosition.Top });


        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 4000 });
        }

        public void SendTypeModal(RbdCliente cliente, string e)
        {
            if (cliente.IdCli == null)
            {
                string? pre, result;

                pre = _confi.GetValue<string>("Configuracion:Codigo-Cliente");
                if (clientes.Count != 0)
                {
                    result = pre + (clientes.Max(c => c.CodCli) + 1).ToString();
                }
                else
                    result = pre + "1";

                cliente.IdCli = result;
            }

            model = cliente;
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

                        var result2 = JsonSerializer.Deserialize<List<RbdCliente>>(result);

                        if (result2 == null)
                            clientes = new List<RbdCliente>();
                        else
                            clientes = result2;

                    }
                }
                StateHasChanged();
            }
        }

        public async Task GetOthers()
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                //CIUDADES
                using (var content = await client.GetAsync(httpApiCiudades))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCiudade>>(result);

                        if (result2 == null)
                            _listCiudades = new List<RbdCiudade>();
                        else
                            _listCiudades = result2;
                    }
                }

                //CALLE
                using (var content = await client.GetAsync(httpApiCalles))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCalle>>(result);

                        if (result2 == null)
                            _listCalle = new List<RbdCalle>();
                        else
                            _listCalle = result2;

                    }
                }

                //PROVINCIA
                using (var content = await client.GetAsync(httpApiProvincia))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdProvincium>>(result);

                        if (result2 == null)
                            _listProvincia = new List<RbdProvincium>();
                        else
                        {
                            _listProvincia = result2;
                        }
                    }
                }
            }

            StateHasChanged();
        }

        public async Task Add(RbdCliente cliente)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(cliente)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {cliente.NomCli} correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"Error al añadir {cliente.NomCli} correctamente");
                    }
                }
            }
        }

        public async Task Update(RbdCliente cliente)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{cliente.CodCli}", JsonSerializer.Serialize(cliente)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {cliente.NomCli} correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"Error al actualizar {cliente.NomCli} correctamente");
                    }
                }
            }
        }

        public async Task Remove(RbdCliente cliente)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{cliente.CodCli}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminado", $"Se elimino {cliente.NomCli} correctamente");
                        await Get();
                    }
                    else
                    {
                        ShowNotification(NotificationSeverity.Error, "Error", $"Error al eliminar {cliente.NomCli} correctamente");
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
                        reporte.MultiClientes(clientes);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedCliente.Count == 1)
                        {
                            reporte.MultiClientes(new List<RbdCliente> { _selectedCliente[0] });
                        }

                        if (_selectedCliente.Count > 1)
                        {
                            reporte.MultiClientes(_selectedCliente.ToList());
                        }
                    }
                    ; break;
            }
        }

        public void ImprimirReporteIndividual(RbdCliente _cliente)
        {
            Reporte R = new Reporte();

            R.Clientes(_cliente);
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Clientes");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Codigo"),
                new DataColumn("Nombre"),
                new DataColumn("DNI"),
                new DataColumn("Correo"),
                new DataColumn("Genero"),

                new DataColumn("Provincia"),
                new DataColumn("Ciudad"),
                new DataColumn("Calle"),
                new DataColumn("Detalle"),

                new DataColumn("RNC"),
                new DataColumn("Fecha de Creacion"),
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in clientes)
                        {
                            var provincias = a.IdProvinciaNavigation != null ? a.IdProvinciaNavigation.NomProvincia : "";
                            var Ciudad = a.IdCiudadNavigation != null ? a.IdCiudadNavigation.NomCiudad : "";
                            var Calle = a.IdCalleNavigation != null ? a.IdCalleNavigation.NomCalle : "";
                            var genero = a.CodGenNavigation != null ? a.CodGenNavigation.NomGen : "";


                            dt.Rows.Add(a.CodCli, a.IdCli,a.NomCli,a.DniCli,a.CorrCli,genero,provincias,Ciudad,Calle,a.DetallDirec,
                                a.TipRnc,a.FecEnt);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedCliente)
                        {
                            var provincias = a.IdProvinciaNavigation != null ? a.IdProvinciaNavigation.NomProvincia : "";
                            var Ciudad = a.IdCiudadNavigation != null ? a.IdCiudadNavigation.NomCiudad : "";
                            var Calle = a.IdCalleNavigation != null ? a.IdCalleNavigation.NomCalle : "";
                            var genero = a.CodGenNavigation != null ? a.CodGenNavigation.NomGen : "";


                            dt.Rows.Add(a.CodCli, a.IdCli, a.NomCli, a.DniCli, a.CorrCli, genero, provincias, Ciudad, Calle, a.DetallDirec,
                                a.TipRnc, a.FecEnt);
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
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Clientes - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
