using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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

        //API NECESARIA
        private string _httpArticulo = "api/RBDArticulos";
        private string _httpClientes = "api/RBDClientes";
        private string _httpPago = "api/RBDTipoPago";
        private string _httpComprobante = "api/RBDTipoComprobante";

        //SECCION DE LISTAS
        private List<RbdCliente> _listCliente { get; set; } = new List<RbdCliente>();
        private List<RbdArticulo> _listArticulo { get; set; } = new List<RbdArticulo>();

        private List<RbdTipoPago> _tipoDePago { get; set; } = new List<RbdTipoPago>();
        private List<RbdTipoComprobante> _tipoComprobante { get; set; } = new List<RbdTipoComprobante>();

        private List<DetalleFac> _listDetalleFac { get; set; } = new List<DetalleFac>();

        //SECCION DE MODELOS
        private DetalleFac _modeloDetalle { get; set; } = new DetalleFac();

        private RbdArticulo _modeloArticulo { get; set; } = new RbdArticulo();
        private RbdCliente _modeloCliente { get; set; } = new RbdCliente();
        private RbdTipoPago _modeloPago { get; set; } = new RbdTipoPago();
        private RbdTipoComprobante _modeloComprobante { get; set; } = new RbdTipoComprobante();


        //SECCION DE BUSQUEDAS PRODUCTO
        private string IDCLI
        {
            get { return _modeloCliente.IdCli; }

            set { VerifiqueClientes(value, 1); }
        }

        private string NAMECLI
        {
            get { return _modeloArticulo.NomArt; }

            set { VerifiqueClientes(value, 2); }
        }

        //SECCION DE BUSQUEDA PRODUCTO
        private string IDPRO
        {
            get { return _modeloArticulo.IdArt; }

            set { VerifiqueProduct(value, 1); }
        }

        private string NAMEPRO
        {
            get { return _modeloArticulo.NomArt; }

            set { VerifiqueProduct(value, 2); }
        }

        private double Descuento { get; set; } = 0;
        private double DescuentoPorciento { get; set; } = 0;

        private double ImpuestoPorciento { get; set; } = 0;
        private double Impuesto { get; set; } = 0;

        private double Sub_Total { get; set; } = 0;
        private double Total { get; set; } = 0;

        [Parameter]
        public long NumFac { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            await GetArticleAndClients();
            await GetByOther();

            if (NumFac != 0)
            {
                await GetEditFactura();
            }

            StateHasChanged();
        }

        public async Task GetArticleAndClients()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpArticulo))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdArticulo>>(result);

                        if (result2 != null)
                            _listArticulo = result2.Where(x => x.CodEst == 1).ToList();
                    }
                }

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

            StateHasChanged();
        }

        public async Task GetByOther()
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
            StateHasChanged();
        }

        public async Task GetEditFactura()
        {
            using (var client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync(_httpFactura + $"/{NumFac}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdFactura>(result);

                        if (result2 != null)
                        {
                            await VerifiqueClientes(result2.CodCliNavigation.IdCli, 1);
                            NAMECLI = _modeloCliente.NomCli;

                            _modeloPago = result2.CodTipagoNavigation;
                            _modeloArticulo.FecArt = result2.FechaReg;

                            if (result2.CodNCfNavigation != null)
                                _modeloComprobante.CodTipocom = result2.CodNCfNavigation.CodTipocom;
                        }
                    }
                }

                using (var content = await client.GetAsync(_httpDetalleFactura + $"/{NumFac}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var resultDetalle = await content.Content.ReadAsStringAsync();

                        var resultDetalle2 = JsonSerializer.Deserialize<List<RbdDetalleFactura>>(resultDetalle);

                        if (resultDetalle2 != null)
                            foreach (var d in resultDetalle2)
                            {
                                _listDetalleFac.Add(new DetalleFac()
                                {
                                    articulo = d.CodArtNavigation,
                                    Cantidad = d.CantArt.GetValueOrDefault(),
                                    Precio = Convert.ToInt16(d.Precio.GetValueOrDefault()),
                                    DescuentoUnit = d.DescuentoArt.GetValueOrDefault()
                                });
                            }
                    }
                }

                StateHasChanged();
            }
        }

        //BUSQUEDA CLIENTES
        private async Task VerifiqueClientes(string arti, int search)
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                switch (search)
                {
                    case 1:
                        {

                            using (var content = await client.GetAsync(_httpClientes + $"/Id={arti}"))
                            {
                                if (content.IsSuccessStatusCode)
                                {
                                    var result = await content.Content.ReadAsStringAsync();

                                    var result2 = JsonSerializer.Deserialize<RbdCliente>(result);

                                    if (result2 != null)
                                    {
                                        _modeloCliente = result2;
                                    }

                                }
                            }
                        }
                        ; break;

                    case 2:
                        {
                            using (var content = await client.GetAsync(_httpClientes + $"/Name={arti}"))
                            {
                                if (content.IsSuccessStatusCode)
                                {
                                    var result = await content.Content.ReadAsStringAsync();

                                    var result2 = JsonSerializer.Deserialize<RbdCliente>(result);

                                    if (result2 != null)
                                    {
                                        _modeloCliente = result2;
                                    }

                                }
                            }
                        }
                        ; break;
                }
            }
            StateHasChanged();
        }

        //BUSQUEDA Articulos
        private async Task VerifiqueProduct(string arti, int Search)
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                switch (Search)
                {
                    case 1:
                        {
                            using (var content = await client.GetAsync(_httpArticulo + $"/Name={arti}"))
                            {
                                if (content.IsSuccessStatusCode)
                                {
                                    var result = await content.Content.ReadAsStringAsync();

                                    var result2 = JsonSerializer.Deserialize<RbdArticulo>(result);

                                    if (result2 != null)
                                    {
                                        _modeloArticulo = result2;
                                        _modeloDetalle.articulo = result2;
                                    }

                                }
                            }
                        }
                        ; break;
                    case 2:
                        {
                            using (var content = await client.GetAsync(_httpArticulo + $"/Name={arti}"))
                            {
                                if (content.IsSuccessStatusCode)
                                {
                                    var result = await content.Content.ReadAsStringAsync();

                                    var result2 = JsonSerializer.Deserialize<RbdArticulo>(result);

                                    if (result2 != null)
                                    {
                                        _modeloArticulo = result2;
                                        _modeloDetalle.articulo = result2;
                                    }

                                }
                            }
                        }
                        ; break;
                }
            }
            StateHasChanged();
        }

        //PROCESO PARA CRUD PARA EL PRODUCTO
        public void AddProduct()
        {
            _listDetalleFac.Add(new DetalleFac()
            {
                articulo = _modeloDetalle.articulo,
                Cantidad = _modeloDetalle.Cantidad,
                Precio = _modeloDetalle.Precio,
                DescuentoUnit = _modeloDetalle.DescuentoUnit,
            });


            Contabilidad();

            StateHasChanged();
        }

        public void Contabilidad()
        {
            Sub_Total = _listDetalleFac.Sum(x => x.Sub_Total);


            if (Impuesto != 0)
            {
                var verdaderoImpuesto = ImpuestoPorciento / 100;
                Impuesto = Sub_Total * verdaderoImpuesto;
            }

            if (DescuentoPorciento != 0)
            {
                var verdaderoPorciento = DescuentoPorciento / 100;
                Descuento = Sub_Total * verdaderoPorciento;
            }

            Total = (Sub_Total + Impuesto) - Descuento;
        }

        public void DeleteProduct(DetalleFac art)
        {
            Contabilidad();

            _listDetalleFac.Remove(art);
        }

        public void EditProduct(DetalleFac art)
        {


        }

        public void VolveraFactura()
        {
            _navigation.NavigateTo("/Facturas");
        }

        //AGREGAR FACTURA
        //AGREGAR FACTURA
        //AGREGAR FACTURA
        //AGREGAR FACTURA
        //AGREGAR FACTURA
        public async Task CREAR()
        {
            try
            {
                Contabilidad();

                //CREANDO LA FACTURA BASE
                var codCli = _modeloCliente.IdCli;
                var codEm = 1;
                var codTipago = _modeloPago.CodTipago;
                var FacDate = DateTime.Now;
                var totalBalance = Total;
                var totalNeto = Sub_Total;
                int codEst;

                if (_modeloPago.CodTipago == 2)
                {
                    //PENDIENTE
                    codEst = 6;
                }
                else
                {
                    //EMITIDA
                    codEst = 4;
                }

                using (HttpClient client = _http.CreateClient(_httpServidor))
                {
                    using (var content = await client.PostAsJsonAsync(_httpFactura, JsonSerializer.Serialize(new RbdFactura()
                    {
                        CodCli = _modeloCliente.CodCli,
                        CodEm = 1,
                        CodTipago = _modeloPago.CodTipago,
                        TotalBalance = Total,
                        TotalNeto = Sub_Total,
                        CodEst = codEst,
                    })))
                    {
                        if (content.IsSuccessStatusCode)
                        {
                            var id = await content.Content.ReadAsStringAsync();

                            var id2 = JsonSerializer.Deserialize<int>(id);

                            foreach (var d in _listDetalleFac)
                            {
                                using (var contentDetalle = await client.PostAsJsonAsync(_httpDetalleFactura, JsonSerializer.Serialize(new RbdDetalleFactura()
                                {
                                    NumFac = id2,
                                    CodArt = d.articulo.CodArt,
                                    CantArt = d.Cantidad,
                                    Precio = Convert.ToDouble(d.Precio),
                                    DescuentoArt = d.DescuentoUnit
                                })))
                                {
                                    if (contentDetalle.IsSuccessStatusCode)
                                    {

                                    }
                                }
                            }

                            _notificationService.Notify(NotificationSeverity.Success, "FACTURA AGREGADA", "SE AGREGO LA FACTURA", 10000);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }








        }


    }
}
