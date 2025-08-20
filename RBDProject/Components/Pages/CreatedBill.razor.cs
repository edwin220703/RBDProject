using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Radzen;
using RBDProject.Components.Helpers;
using RBDProject.Models;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.Tasks;

namespace RBDProject.Components.Pages
{
    partial class CreatedBill
    {
        //API PRINCIPAL
        private string _httpServidor = "Servidor";
        private string _httpFactura = "api/RBDFacturas";
        private string _httpDetalleFactura = "api/RBDFacturas/DetalleFactura";
        private string _httpCuentasPorCobrar = "api/CuentasPorCobrar";

        //API NECESARIA
        private string _httpArticulo = "api/RBDArticulos";
        private string _httpClientes = "api/RBDClientes";
        private string _httpPago = "api/RBDTipoPago";
        private string _httpComprobante = "api/RBDTipoComprobante";
        private string _httpSecComprobante = "api/RBDComprobanteFiscales";

        //SECCION DE LISTAS
        private List<RbdCliente> _listCliente { get; set; } = new List<RbdCliente>();
        private List<RbdArticulo> _listArticulo { get; set; } = new List<RbdArticulo>();

        private List<RbdTipoPago> _tipoDePago { get; set; } = new List<RbdTipoPago>();
        private List<RbdTipoComprobante> _tipoComprobante { get; set; } = new List<RbdTipoComprobante>();

        private List<DetalleFac> _listDetalleFac { get; set; } = new List<DetalleFac>();

        //SECCION DE MODELOS
        private RbdFactura _modeloFactura { get; set; } = new RbdFactura();
        private DetalleFac _modeloDetalle { get; set; } = new DetalleFac();
        private RbdArticulo _modeloArticulo { get; set; } = new RbdArticulo();
        private RbdCliente _modeloCliente { get; set; } = new RbdCliente();
        private RbdTipoPago _modeloPago { get; set; } = new RbdTipoPago();
        private RbdComprobanteFiscal _modeloSecComprobante { get; set; } = new RbdComprobanteFiscal();
        private RbdTipoComprobante _modeloComprobante { get; set; } = new RbdTipoComprobante();
        private RbdEmpleado _modeloEmpleado { get; set; } = new RbdEmpleado();

        //DATOS GENERALES
        private double Descuento { get; set; } = 0;
        private double DescuentoPorciento { get; set; } = 0;

        private double Impuesto { get; set; } = 0;
        private double ImpuestoPorciento { get; set; } = 0;

        private double Sub_Total { get; set; } = 0;
        private double Total { get; set; } = 0;

        [Parameter]
        public long NumFac { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            await GetArticle();
            await GetClients();
            await GetComprobante();
            await GetPagos();

            if (NumFac != 0)
            {
                await GetEditFactura();
            }
            else
            {
                await ConfiguracionInicial();
            }

            StateHasChanged();
        }

        public async Task ConfiguracionInicial()
        {
            //CLIENTE GENERICO
            var clienteGenerico = _listCliente.Where(x => x.NomCli.Contains("Cliente Generico")).FirstOrDefault();

            if (clienteGenerico != null)
                _modeloCliente = clienteGenerico;

            //TIPO DE PAGO CONTADO
            var tip = _tipoDePago.Where(x => x.NomPago.Contains("Contado")).FirstOrDefault();

            if (tip != null)
                _modeloPago.CodTipago = tip.CodTipago;


            //COMPROBANTE CONSUMIDOR FINAL
            var com = _tipoComprobante.Where(x => x.NomTipocom.Contains("Consumo Final")).FirstOrDefault();

            if (com != null)
            {
                _modeloComprobante.CodTipocom = com.CodTipocom;
                 await GetSecComprobante(com.CodTipocom);
            }

            //DESCUENTO EN PORCIENTO
            DescuentoPorciento = 0;
            
            //MISCELANEOS
            _modeloFactura.Miscelaneo = string.Empty;

            //DETALLE
            LimpiarFormularioProducto();

            //LIMPIANDO LISTA DEL DETALLE
            _listDetalleFac.Clear();

            //REALIZANDO CALCULOS DESPUES DE TODO LIMPIO
            Contabilidad();

            StateHasChanged();
        }

        public void VolveraFactura()
        {
            _navigation.NavigateTo("/Facturas");
        }

        //NOTIFICACIONES
        public void ShowNotification(NotificationSeverity modelo, string titulo, string detalle)
        {
            _notificationService.Notify(new NotificationMessage { Severity = modelo, Summary = titulo, Detail = detalle, Duration = 8000 });
        }

        public async Task GetArticle()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpArticulo))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdArticulo>>(result);

                        //CONDICION DEBE TENER AL MENOS UN PRECIO, Activo y con stock
                        if (result2 != null)
                            _listArticulo = result2.Where(x => x.CodEst == 1 && x.RbdListaDePrecios.Count > 0 && x.ExistArt > 0).ToList();
                    }
                }
            }
        }

        public async Task GetClients()
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
                            _listCliente = result2;
                    }
                }
            }
        }

        public async Task GetComprobante()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpComprobante))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoComprobante>>(result);

                        if (result2 != null)
                            _tipoComprobante = result2;
                    }
                }
            }
        }

        public async Task GetSecComprobante(int TipoComprobante)
        {
            await Task.Delay(50);

            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync($"{_httpSecComprobante}/tipo={TipoComprobante}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdComprobanteFiscal>(result);

                        if (result2 != null)
                        {
                            _modeloSecComprobante = result2;

                            if (_modeloSecComprobante.ImpCom != null)
                            {
                                ImpuestoPorciento = (double)_modeloSecComprobante.ImpCom;

                            }
                        }
                    }
                    else
                    {
                        _modeloSecComprobante = new RbdComprobanteFiscal();
                        ImpuestoPorciento = 0;
                    }
                }

                Contabilidad();
            }
        }

        public async Task GetPagos()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpPago))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoPago>>(result);

                        if (result2 != null)
                            _tipoDePago = result2.Where(x => x.CodEst == 1).ToList();
                    }
                }
            }
        }

        //EDITAR LA FACTURA
        public async Task GetEditFactura()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpFactura + $"/{NumFac}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdFactura>(result);

                        if (result2 != null)
                        {
                            _modeloFactura = result2;
                            _modeloCliente = _modeloFactura.CodCliNavigation;

                            //AÑADIENDO COMPROBANTE FISCAL
                            if (_modeloFactura.CodNCfNavigation != null)
                            {
                                _modeloSecComprobante = _modeloFactura.CodNCfNavigation;
                                _modeloComprobante.CodTipocom = _modeloSecComprobante.CodTipocom;
                                ImpuestoPorciento = (double)_modeloFactura.CodNCfNavigation.ImpCom;
                            }

                            //Añadiendo el tipo de pago
                            _modeloPago = _modeloFactura.CodTipagoNavigation;

                            //AÑADIENDO ARTICULOS
                            foreach (var d in _modeloFactura.RbdDetalleFacturas)
                            {
                                _listDetalleFac.Add(new DetalleFac() { articulo = d.CodArtNavigation, Cantidad = (int)d.CantArt, Precio = (double)d.Precio, DescuentoUnit = (double)d.DescuentoArt });
                            }

                            Contabilidad();
                        }

                    }
                }
            }
        }


        private async Task GetEmpleado(string Name)
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync($"api/RBDEmpleados/Name={Name}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdEmpleado>(result);

                        if (result2 != null)
                            _modeloEmpleado = result2;
                    }
                }
            }
        }

        //BUSQUEDA CLIENTES
        private async Task VerifiqueClientes(int search)
        {
            try
            {
                await GetClients();

                var id = _modeloCliente.IdCli;
                var Name = _modeloCliente.NomCli;

                switch (search)
                {
                    case 1:
                        {
                            if (id == null || id == string.Empty)
                            {
                                _modeloCliente = new RbdCliente();
                                return;
                            }

                            var a = _listCliente.Where(x => x.IdCli.Contains(id)).FirstOrDefault();

                            if (a == null)
                            {
                                _modeloCliente = new RbdCliente();
                                _modeloCliente.IdCli = id;
                            }
                            else
                            {
                                _modeloCliente = a;
                            }
                        }
                        ; break;

                    case 2:
                        {
                            if (Name == null || Name == string.Empty)
                            {
                                _modeloCliente = new RbdCliente();
                                return;
                            }

                            var a = _listCliente.Where(x => x.NomCli == Name).FirstOrDefault();

                            if (a == null)
                            {
                                _modeloCliente = new RbdCliente();
                                _modeloCliente.NomCli = Name;
                            }
                            else
                            {
                                _modeloCliente = a;
                            }
                        }
                        ; break;
                }
            }
            catch (Exception ex)
            {
                _modeloArticulo = new RbdArticulo();
                Console.WriteLine(ex.Message);
            }
        }

        //BUSQUEDA Articulos
        private async Task VerifiqueProduct(int search)
        {
            try
            {
                var id = _modeloArticulo.IdArt;
                var Name = _modeloArticulo.NomArt;

                switch (search)
                {
                    case 1:
                        {
                            if (id == null || id == string.Empty)
                            {
                                _modeloArticulo = new RbdArticulo();
                                return;
                            }

                            var a = _listArticulo.Where(x => x.IdArt.Contains(id)).FirstOrDefault();

                            if (a == null)
                            {
                                _modeloArticulo = new RbdArticulo();
                                _modeloArticulo.IdArt = id;
                            }
                            else
                            {
                                _modeloArticulo = a;

                                if (a.RbdListaDePrecios.Count > 0)
                                    _modeloDetalle.Precio = a.RbdListaDePrecios.First().Precio;
                            }
                        }
                        ; break;
                    case 2:
                        {
                            if (Name == null || Name == string.Empty)
                            {
                                _modeloArticulo = new RbdArticulo();
                                return;
                            }

                            var a = _listArticulo.Where(x => x.NomArt.Contains(Name)).FirstOrDefault();

                            if (a == null)
                            {
                                _modeloArticulo = new RbdArticulo();
                                _modeloArticulo.NomArt = Name;
                            }
                            else
                            {
                                _modeloArticulo = a;
                                if (a.RbdListaDePrecios.Count > 0)
                                    _modeloDetalle.Precio = a.RbdListaDePrecios.First().Precio;
                            }
                        }
                        ; break;

                }

                await GetArticle();
            }
            catch (Exception ex)
            {
                _modeloArticulo = new RbdArticulo();
                Console.WriteLine(ex.Message);
            }
        }

        //PROCESO PARA CRUD PARA EL PRODUCTO
        public void AddProduct()
        {
            //Validando Producto
            if (_modeloArticulo.IdArt == null && _modeloArticulo.NomArt == null)
            {
                ShowNotification(NotificationSeverity.Error, "Articulo No Existente", "No existen articulos que introducir");
                return;
            }

            //SI LA CANTIDAD ES 0
            if (_modeloDetalle.Cantidad == 0)
            {
                ShowNotification(NotificationSeverity.Error, "Error De Cantidad", "Debe agregar una cantidad mayor a 0. Pero, menor o igual a las existencias");
                _modeloDetalle.Cantidad = 1;
            }

            //SI EL PRECIO NO COINCIDE CON NINGUNO
            if (!_modeloArticulo.RbdListaDePrecios.Where(x => x.Precio == _modeloDetalle.Precio).Any())
            {
                ShowNotification(NotificationSeverity.Error, "Precio no introducido", "El precio introducido no concuerda con ninguno relacionado al articulo");
                return;
            }

            //SI NO HA SELECCIONADO PRECIO
            if (_modeloDetalle.Precio == 0)
            {
                ShowNotification(NotificationSeverity.Error, "Precio no seleccionado", "seleccione un precio por favor");
                return;
            }

            if (_listDetalleFac.Where(x => x.Id == _modeloArticulo.IdArt && x.Precio != _modeloDetalle.Precio).Any())
            {
                ShowNotification(NotificationSeverity.Error, "Error de precio", "El mismo producto no se puede vender a dos precios distintos");
                return;
            }

            //SI SE DESEA AGREGAR MAS CANTIDAD DEL MISMO PRODUCTO
            if (_listDetalleFac.Where(x => x.Id == _modeloArticulo.IdArt && x.Precio == _modeloDetalle.Precio).Any())
            {
                ShowNotification(NotificationSeverity.Warning, "Modificacion", "Los articulos ya se agregaron al detalle. se agrego la cantidad");

                //NOTA:SI EL PRODUCTO INTRODUCIDO SE AGREGA SE NUEVO SOLO SE CAMBIARA LA CANTIDAD
                var co = _listDetalleFac.Where(x => x.Id == _modeloArticulo.IdArt && x.Precio == _modeloDetalle.Precio).First();
                co.Cantidad += _modeloDetalle.Cantidad;

                return;
            }

            //AÑADIENDO PRODUCTO
            _listDetalleFac.Add(new DetalleFac() { articulo = _modeloArticulo, Cantidad = _modeloDetalle.Cantidad, DescuentoUnit = _modeloDetalle.DescuentoUnit, Precio = _modeloDetalle.Precio });

            Contabilidad();

            LimpiarFormularioProducto();
        }

        //HACE LOS CALCULOS DEL PROGRAMA
        public void Contabilidad()
        {
            try
            {
                Sub_Total = _listDetalleFac.Sum(x => x.Sub_Total);

                Descuento = Sub_Total * (DescuentoPorciento / 100);

                var _total = Sub_Total - Descuento;

                Impuesto = _total * (ImpuestoPorciento / 100);

                Total = _total + Impuesto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteProduct(DetalleFac art)
        {
            _listDetalleFac.Remove(art);

            Contabilidad();
        }

        public async Task EditProduct(DetalleFac art)
        {
            _modeloArticulo.IdArt = art.Id;
            await VerifiqueProduct(1);

            _modeloDetalle.Cantidad = art.Cantidad;
            _modeloDetalle.Precio = art.Precio;
            _modeloDetalle.DescuentoUnit = art.DescuentoUnit;

            DeleteProduct(art);
        }

        //LIMPIAR FORM ARTCULO
        public void LimpiarFormularioProducto()
        {
            _modeloArticulo = new RbdArticulo();
            _modeloDetalle = new DetalleFac();
        }

        //AGREGAR FACTURA
        public async Task CREAR()
        {
            if (!IsValidandoFactura())
                return;

            //CREANDO COMPROBANTE
            if (_modeloSecComprobante.SecCom != null && _modeloComprobante.CodTipocom != 0)
            {
                await CrearComprobante();
            }

            //Creando factura
            long id = await CrearFactura();

            //Creando detalle factura
            if (id != 0)
                await CrearDetalleFactura(id);

            //GENERANDO LA FACTURA
            if (id != 0)
            {
                RbdFactura F = new RbdFactura();

                //OBTENIENDO LA FACTURA DEL SERVIDOR
                using (HttpClient client = _http.CreateClient(_httpServidor))
                {
                    //TRAER EL DETALLE
                    using (var content = await client.GetAsync("api/RBDFacturas/" + id))
                    {
                        if (content.IsSuccessStatusCode)
                        {
                            var result = await content.Content.ReadAsStringAsync();

                            var result2 = JsonSerializer.Deserialize<RbdFactura>(result);

                            if (result2 != null)
                            {
                                F = result2;

                                //CREADO CUENTAS POR COBRAR
                                //2.CREDITO 3.CHEQUE
                                if (F.CodTipago == 2 || F.CodTipago == 3)
                                {
                                    await CrearCuentasPorCobrar(F);
                                }
                            }
                        }
                    }
                }

                //LLAMANDO LA FACTURA
                FacturaPDF pdf = new FacturaPDF("000000", F, _listDetalleFac);
                pdf.GenerarFactura();

                await ConfiguracionInicial();
            }
        }

        public bool IsValidandoFactura()
        {
            //Cliente
            //-VERIFICANDO SI EL CODIGO DEL CLIENTE ESTA NULO
            if (_modeloCliente == null || _modeloCliente.CodCli == 0 && _modeloCliente.NomCli == string.Empty)
            {
                ShowNotification(NotificationSeverity.Warning, "Error De Cliente", "Debe seleccionar un cliente");
                return false;
            }

            //CLIENTES
            if(_modeloCliente.CodCli == 1 && _modeloCliente.NomCli == "Cliente Generico" && _modeloPago.CodTipago == 2)
            {
                ShowNotification(NotificationSeverity.Warning, "Error de credito", "Selecciones un cliente. Nota: El cliente generico no puede ser hacer notas a credito");
                return false;
            }

            //PAGO
            if (_modeloPago.CodTipago == 0)
            {
                ShowNotification(NotificationSeverity.Warning, "Error De Pago", "debe seleccionar un pago");
                return false;
            }

            //COMPROBANTE
            //if (_modeloComprobante.CodTipocom == 0 || _modeloComprobante.NomTipocom == string.Empty)
            //{
            //    _modeloComprobante = null;
            //}

            if (_listDetalleFac.Count == 0)
            {
                ShowNotification(NotificationSeverity.Warning, "Sin productos", "Añada un producto a la lista");
                return false;
            }

            return true;
        }

        //MODULO DE CREACION DE FACTURA
        public async Task<long> CrearFactura()
        {
            try
            {
                RbdFactura factura = new RbdFactura()
                {
                    CodCli = _modeloCliente.CodCli,
                    CodEm = _modeloEmpleado.CodEm == 0 ? 0 : _modeloEmpleado.CodEm,
                    CodTipago = _modeloPago.CodTipago,
                    TotalBalance = Sub_Total,
                    TotalNeto = Total,
                    FechaReg = DateTime.Now,
                    CodEst = 4, /*Codigo de emision*/
                    Miscelaneo = _modeloFactura.Miscelaneo,
                };

                if (_modeloSecComprobante.SecCom != null || _modeloComprobante.CodTipocom != 0)
                {
                    factura.CodNCf = _modeloSecComprobante.CodNcf;
                }
                else
                {
                    factura.CodNCf = null;
                }

                using (HttpClient client = _http.CreateClient(_httpServidor))
                {
                    using (var content = await client.PostAsJsonAsync(_httpFactura, JsonSerializer.Serialize(factura)))
                    {
                        if (content.IsSuccessStatusCode)
                        {
                            var result = await content.Content.ReadAsStringAsync();

                            var id = JsonSerializer.Deserialize<long>(result);

                            if (id != 0)
                            {
                                Console.WriteLine($"Factura creada {id}");
                                return id;
                            }
                        }

                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        //CREANDO DETALLE FACTURA
        public async Task CrearDetalleFactura(long id)
        {
            try
            {
                foreach (var d in _listDetalleFac)
                {
                    RbdDetalleFactura detalle = new RbdDetalleFactura()
                    {
                        NumFac = id,
                        CodArt = d.articulo.CodArt,
                        CantArt = d.Cantidad,
                        Precio = d.Precio,
                        DescuentoArt = d.DescuentoUnit
                    };

                    using (HttpClient client = _http.CreateClient(_httpServidor))
                    {
                        using (var content = await client.PostAsJsonAsync(_httpDetalleFactura, JsonSerializer.Serialize(detalle)))
                        {
                            if (content.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Producto insertado");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task ActualizarFactura()
        {
            try
            {
                Contabilidad();

                if (!IsValidandoFactura())
                    return;

                //ENVIANDO LA FACTURA
                using (HttpClient client = _http.CreateClient(_httpServidor))
                {
                    _modeloFactura.TotalBalance = Sub_Total;
                    _modeloFactura.TotalNeto = Total;

                    _modeloFactura.CodCliNavigation = null;
                    _modeloFactura.CodTipagoNavigation = null;
                    _modeloFactura.CodEstNavigation = null;
                    _modeloFactura.CodEmNavigation = null;
                    _modeloFactura.RbdDetalleFacturas = null;

                    using (var content = await client.PutAsJsonAsync(_httpFactura + $"/{NumFac}", JsonSerializer.Serialize(_modeloFactura)))
                    {
                        if (content.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Factura Actualizada");
                        }
                    }
                }

                //BORRANDO LOS ARTICULOS VIEJOS
                using (HttpClient client = _http.CreateClient(_httpServidor))
                {
                    //BORRANDO LOS ARTICULOS YA REGISTRADOS DE LA FACTURA
                    using (var content = await client.DeleteAsync(_httpDetalleFactura + $"/{NumFac}"))
                    {
                        if (content.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Productos ELIMINADOS");
                        }
                    }
                }

                //ENVIANDO EL DETALLE NUEVO
                foreach (var d in _listDetalleFac)
                {
                    RbdDetalleFactura detalle = new RbdDetalleFactura()
                    {
                        NumFac = _modeloFactura.NumFac,
                        CodArt = d.articulo.CodArt,
                        CantArt = d.Cantidad,
                        Precio = d.Precio,
                        DescuentoArt = d.DescuentoUnit
                    };

                    using (HttpClient client = _http.CreateClient(_httpServidor))
                    {
                        //AÑADIENDO LA NUEVA LISTA
                        using (var content = await client.PostAsJsonAsync(_httpDetalleFactura, JsonSerializer.Serialize(detalle)))
                        {
                            if (content.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Producto Actualizado");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //MODULO Comprobante fiscal
        public async Task CrearComprobante()
        {
            try
            {
                RbdComprobanteFiscal comprobante = new RbdComprobanteFiscal()
                {
                    SecCom = _modeloSecComprobante.SecCom,
                    DesCom = _modeloSecComprobante.DesCom,
                    ImpCom = _modeloSecComprobante.ImpCom,
                    DocFec = _modeloSecComprobante.DocFec,
                    FechaVec = _modeloSecComprobante.FechaVec,
                    CodTipocom = _modeloSecComprobante.CodTipocom
                };

                using (HttpClient client = _http.CreateClient(_httpServidor))
                {
                    using (var content = await client.PostAsJsonAsync(_httpSecComprobante, JsonSerializer.Serialize(comprobante)))
                    {
                        if (content.IsSuccessStatusCode)
                        {
                            var result = await content.Content.ReadAsStringAsync();

                            var id = JsonSerializer.Deserialize<int>(result);

                            if (id != 0)
                            {
                                _modeloSecComprobante.CodNcf = id;
                            }

                            Console.WriteLine("Comprobante insertado");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CrearCuentasPorCobrar(RbdFactura factura)
        {
            RbdCuentasPorCobrar cxc = new RbdCuentasPorCobrar();

            cxc.NumFact = factura.NumFac;
            cxc.FechaEmi = DateTime.Now;
            cxc.VencPago = DateTime.Now.AddDays(30);
            cxc.Balance = factura.TotalNeto;
            cxc.TotalFact = factura.TotalNeto;
            cxc.CodEm = 0;

            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.PostAsJsonAsync(_httpCuentasPorCobrar, JsonSerializer.Serialize(cxc)))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Cuenta por cobrar registrada");
                    }
                }
            }
        }

    }
}
