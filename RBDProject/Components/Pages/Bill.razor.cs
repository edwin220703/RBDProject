using ClosedXML.Excel;
using ConsoleApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Components.Helpers;
using RBDProject.Models;
using System.Data;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class Bill
    {
        private List<RbdFactura> _listFacturas { get; set; } = new List<RbdFactura>();
        private IList<RbdFactura> _selectedFactura { get; set; } = new List<RbdFactura>();
        private RbdFactura model { get; set; } = new RbdFactura();
        private string utilitymodal { get; set; } = string.Empty;

        //API
        private readonly string httpServidor = "Servidor";
        private readonly string httpApi = "api/RBDFacturas";

        protected override async Task OnInitializedAsync()
        {
            await Get();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Panel Factura");
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

                        var result2 = JsonSerializer.Deserialize<List<RbdFactura>>(result);

                        if (result2 == null)
                            _listFacturas = new List<RbdFactura>();
                        else
                            _listFacturas = result2.Where(x=>x.CodEst == 4).ToList();

                        StateHasChanged();
                    }
                }
            }
        }

        public async Task Add(RbdFactura articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(httpApi, JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Añadido", $"Se añadio {articulo.NumFac} correcmtamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Update(RbdFactura articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.PutAsJsonAsync(httpApi + $"/{articulo.NumFac}", JsonSerializer.Serialize(articulo)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Actualizacion", $"Se actualizo {articulo.NumFac} correctamente");
                        await Get();
                    }
                }
            }
        }

        public async Task Remove(RbdFactura articulo)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                using (var content = await client.DeleteAsync(httpApi + $"/{articulo.NumFac}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        ShowNotification(NotificationSeverity.Success, "Eliminacion", $"Se elimino {articulo.NumFac} correctamente");
                        await Get();
                    }
                }
            }
        }

        public void SendTypeModal(RbdFactura rbdFactura, string e)
        {
            model = rbdFactura;
            utilitymodal = e;
        }

        public void CrearFactura()
        {
            _navigation.NavigateTo("CrearFactura");
        }

        public async Task ImprimirFactura(RbdFactura f)
        {
            using (HttpClient client = _http.CreateClient(httpServidor))
            {
                List<RbdDetalleFactura> detalle = new List<RbdDetalleFactura>();

                //TRAER EL DETALLE
                using (var content = await client.GetAsync("api/RBDFacturas/DetalleFactura/" + f.NumFac))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdDetalleFactura>>(result);

                        if (result2 != null)
                            detalle = result2;

                        StateHasChanged();
                    }
                }

                List<DetalleFac> detalle2 = new List<DetalleFac>();

                foreach (var a in detalle)
                {
                    detalle2.Add(new DetalleFac() { articulo = a.CodArtNavigation, Cantidad = (int)a.CantArt, Precio = (double)a.Precio, DescuentoUnit = (double)a.DescuentoArt });
                }


                FacturaPDF pdf = new FacturaPDF("00000", f, detalle2);
                pdf.GenerarFactura();
            }
        }

        public void ImprimirReporte(int value)
        {
            Reporte reporte = new Reporte();

            switch (value)
            {
                case 1:
                    {
                        reporte.Facturas(_listFacturas);
                    }
                    ; break;
                case 2:
                    {
                        if (_selectedFactura.Count == 1)
                        {
                            reporte.Facturas(new List<RbdFactura> { _selectedFactura[0] });
                        }

                        if (_selectedFactura.Count > 1)
                        {
                            reporte.Facturas(_selectedFactura.ToList());
                        }
                    }
                    ; break;
            }
        }

        public async Task ExportarDocumento(int value, int tipo)
        {
            DataTable dt = new DataTable("Factura");

            //GENERANDO COLUMNAS
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Numero"),
                new DataColumn("NCF"),
                new DataColumn("Cliente"),
                new DataColumn("Empleado"),
                new DataColumn("Pago"),
                new DataColumn("Balance Total"),
                new DataColumn("Total Neto"),
                new DataColumn("Estado"),
                new DataColumn("Miscelaneos"),
                new DataColumn("Fecha de registro"),
            });

            switch (value)
            {
                case 1:
                    {
                        foreach (var a in _listFacturas)
                        {

                            var cliente = a.CodCliNavigation != null ? a.CodCliNavigation.NomCli : "";
                            var Empleado = a.CodEmNavigation != null ? a.CodEmNavigation.NomEm : "";
                            var estado = a.CodEstNavigation != null ? a.CodEstNavigation.NomEst : "";
                            var sec = a.CodNCfNavigation != null ? a.CodNCfNavigation.SecCom : "";
                            var pago = a.CodTipagoNavigation != null ? a.CodTipagoNavigation.NomPago : "";

                            dt.Rows.Add(a.NumFac, sec, cliente, Empleado, pago, a.TotalBalance, a.TotalNeto, estado, a.Miscelaneo, a.FechaReg);
                        }
                    }
                    ; break;
                case 2:
                    {
                        foreach (var a in _selectedFactura)
                        {

                            var cliente = a.CodCliNavigation != null ? a.CodCliNavigation.NomCli : "";
                            var Empleado = a.CodEmNavigation != null ? a.CodEmNavigation.NomEm : "";
                            var estado = a.CodEstNavigation != null ? a.CodEstNavigation.NomEst : "";
                            var sec = a.CodNCfNavigation != null ? a.CodNCfNavigation.SecCom : "";
                            var pago = a.CodTipagoNavigation != null ? a.CodTipagoNavigation.NomPago : "";

                            dt.Rows.Add(a.NumFac, sec, cliente, Empleado, pago, a.TotalBalance, a.TotalNeto, estado, a.Miscelaneo, a.FechaReg);
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
                    await _jSRuntime.InvokeAsync<object>("BlazorFile", $"Reporte De Factura - {DateTime.Now} - RBD SOFTWARE.xlsx", Convert.ToBase64String(stream.ToArray()));
                }

            }
        }
    }
}
