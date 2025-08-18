using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Modals.CuentasXCobrar
{
    partial class Modal_Pagar
    {

        public RbdCliente _modeloCliente { get; set; } = new RbdCliente();

        public RbdTipoPago _modeloPago { get; set; } = new RbdTipoPago();

        public string _httpServidor = "Servidor";
        public string _httpClientes = "api/RBDClientes";
        public string _httpCxC = "api/CuentasPorCobrar/Cliente/";
        public string _httpCxCFact = "api/CuentasPorCobrar/Factura/";
        public string _httpPago = "api/RBDTipoPago";

        // public List<RbdCuentasPorCobrar> _modeloCXC { get; set; } = new List<RbdCuentasPorCobrar>();

        public List<RbdCliente> _listClientes { get; set; } = new List<RbdCliente>();

        public List<RbdCuentasPorCobrar> _listaCXC { get; set; } = new List<RbdCuentasPorCobrar>();

        public List<RbdTipoPago> _listPagos { get; set; } = new List<RbdTipoPago>();

        private int OpcionAbono { get; set; } = 1;

        private double Pendiente { get; set; } = 0;

        private double NoFactura { get; set; } = 0;

        private double PagoTotal { get; set; } = 0;

        private int CountFact { get; set; } = 0;

        private IList<RbdCuentasPorCobrar> _selectCXC { get; set; } = new List<RbdCuentasPorCobrar>();

        protected override async Task OnInitializedAsync()
        {
            await GetClientes();
            await GetByPago();
            var a = _jSRuntime.InvokeVoidAsync("CambiarTitle", "Cuentas Por Cobrar - Panel De Pago");
        }

        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 8000 });
        }

        public async Task ChangedClient(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    {
                        var id = _modeloCliente.IdCli != null ? _modeloCliente.IdCli : string.Empty;

                        if (id != string.Empty)
                        {
                            var m = _listClientes.Where(x => x.IdCli.Contains(id)).FirstOrDefault();

                            if (m != null)
                                _modeloCliente = m;
                        }
                    }
                    ; break;
                case 2:
                    {
                        var nombre = _modeloCliente.NomCli != null ? _modeloCliente.NomCli : string.Empty;

                        if (nombre != string.Empty)
                        {
                            var m = _listClientes.Where(x => x.NomCli.Contains(nombre)).FirstOrDefault();
                            if (m != null)
                                _modeloCliente = m;
                        }
                    }
                    ; break;
                case 3:
                    {
                        var dni = _modeloCliente.DniCli != null ? _modeloCliente.DniCli : string.Empty;

                        if (dni != string.Empty)
                        {
                            var m = _listClientes.Where(x => x.DniCli.Contains(dni)).FirstOrDefault();

                            if (m != null)
                                _modeloCliente = m;
                        }
                    }
                    ; break;
            }

            if (_modeloCliente.CodCli != 0)
            {
                await GetCXC(_modeloCliente.CodCli);
            }

            await GetClientes();
        }

        // LISTA DE CLIENTES
        public async Task GetClientes()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpClientes))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCliente>>(result);

                        if (result2 != null)
                        {
                            _listClientes = result2.Where(x => x.CodCli != 1).ToList();
                        }
                    }
                }
            }
        }

        public async Task GetCXC(int id)
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpCxC + $"{id}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdCuentasPorCobrar>>(result);

                        if (result2 is not null)
                        {
                            _listaCXC = result2;
                            CountFact = _listaCXC.Count();
                            var pendienteverificar = _listaCXC.Sum(x => x.Balance);
                            Pendiente = pendienteverificar != null ? (double)pendienteverificar : 0;
                            StateHasChanged();
                        }

                    }
                }
            }
        }

        public async Task GetByFactura(KeyboardEventArgs e)
        {
            await Task.Delay(100);

            if (e.Code != "Enter")
            {
                return;
            }

            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpCxCFact + NoFactura))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdCuentasPorCobrar>(result);

                        if (result2 != null)
                        {
                            _listaCXC = new List<RbdCuentasPorCobrar>() { result2 };
                            CountFact = _listaCXC.Count();
                            Pendiente = _listaCXC.Sum(x => x.Balance).Value;

                            _modeloCliente = _listClientes.Where(x => x.CodCli == result2.NumFactNavigation.CodCli).FirstOrDefault();

                            StateHasChanged();
                        }
                    }
                }
            }
        }

        public async Task GetByPago()
        {
            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpPago))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoPago>>(result);

                        if (result2 != null)
                        {
                            _listPagos = result2.Where(x => x.CodTipago != 2).ToList();
                        }
                    }
                }

            }
        }

        // //TERMINAR
        public async Task Crear()
        {
            if (!IsValid())
                return;

            double total = PagoTotal;

            if (OpcionAbono == 1 && _selectCXC.Count == 0)
            {
                foreach (var f in _listaCXC)
                {
                    total = (double)(f.Balance - total);

                    await SendDetalle(f.CodCcobro);

                    if (total >= 0)
                    {
                        return;
                    }
                    else
                    {
                        PagoTotal = total;
                    }
                }
            }
            else if (OpcionAbono == 1 && _selectCXC.Count > 1)
            {
                foreach (var f in _selectCXC)
                {
                    total = (double)(f.Balance - total);

                    await SendDetalle(f.CodCcobro);

                    if (total >= 0)
                    {
                        return;
                    }
                    else
                    {
                        PagoTotal = total;
                    }
                }
            }
            else
            {

                var f = _selectCXC.First();

                if (total > f.Balance)
                {
                    PagoTotal = (double)f.Balance;
                }

                await SendDetalle(f.CodCcobro);
            }
        }

        public async Task SendDetalle(long num)
        {
            RbdDetalleCuentaPorCobrar Dt = new RbdDetalleCuentaPorCobrar()
            {
                CodCcobro = num,
                FechaPago = DateTime.Now,
                Monto = PagoTotal,
                CodTippago = _modeloPago.CodTipago,
                CodEm = 0
            };

            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.PostAsJsonAsync($"api/CuentasPorCobrar/DetalleCuentaPorCobrar/{num}", JsonSerializer.Serialize(Dt)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Cuenta Saldad");
                    }
                }
            }
        }

        // TERMINAR
        public bool IsValid()
        {
            // VERIFICANDO EL PAGO
            if (_modeloPago == null || _modeloPago.CodTipago == 0)
            {
                ShowNotification(NotificationSeverity.Warning, "Pago no seleccionado", "Debe seleccionar un pago");
                return false;
            }

            // VERIFICANDO EL CLIENTE
            if (_modeloCliente.CodCli == 0)
            {
                ShowNotification(NotificationSeverity.Warning, "Cliente no seleccionado", "Debe seleccionar un cliente");
                return false;
            }

            // VERIFICANDO SI EL ABONO INDIVIDUAL ESTA SELECCIONADO
            if (OpcionAbono == 2 && _selectCXC.Count != 1)
            {
                ShowNotification(NotificationSeverity.Warning, "Error de seleccion",
                "Para procesar pagos individuales/especificos necesita seleccionar una cuenta por cobrar");

                return false;
            }

            // VERIFICANDO QUE SE ESTA PAGANDO UN MONTO
            if (PagoTotal <= 0)
            {
                ShowNotification(NotificationSeverity.Warning, "Error de cantidad",
                "Para procesar pagos necesita tener una cantidad mayor a '0'");

                return false;
            }

            return true;
        }
    }
}
