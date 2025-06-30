using Microsoft.AspNetCore.Components.Web;
using RBDProject.Models;
using System.Text.Json;

namespace RBDProject.Components.Pages
{
    partial class CreatedBill
    {
        private string _httpServidor = "Servidor";
        private string _httpFactura = "api/RBDFactura";
        private string _httpDetalleFactura = "api/RBDFactura/DetalleFactura";
        private string _httpPago = "api/RBDTipoPago";
        private string _httpArticulo = "api/RBDArticulos";

        //MODELOS DE FACTURA Y DETALLE
        private RbdFactura _factura { get; set; } = new RbdFactura();
        private List<RbdDetalleFactura> _detalleFactura { get; set; } = new List<RbdDetalleFactura>();

        //TIPO DE PAGO
        private List<RbdTipoPago> _tipoDePago { get; set; } = null;

        //TIPO  DE COMPROBANTE
        private List<RbdTipoComprobante> _tipoComprobante { get; set; } = null;

        //BUSCAR PRODUCTO
        private string CodArt { get; set; }
        private string NomArt { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _factura.CodCliNavigation = new RbdCliente();
            _factura.CodEmNavigation = new RbdEmpleado();
            _factura.FechaReg = DateTime.Now;

            await GetTipoPagos();
            StateHasChanged();
        }


        public async Task GetTipoPagos()
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using(var content = await client.GetAsync(_httpPago))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<List<RbdTipoPago>>(result);

                        if(result2 != null)
                            _tipoDePago = result2;
                    }
                }
            }
        }

        public async Task VerifiqueProductById(RbdDetalleFactura r)
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync($"api/RBDArticulos/Cod={r.CodArtNavigation.IdArt}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdArticulo>(result);

                        if(result2 != null)
                        {
                            r.CodArtNavigation.NomArt = result2.NomArt;
                            r.CodArt = result2.CodArt;
                            StateHasChanged();
                        }

                    }
                }
            }
        }

        public async Task VerifiqueProductByName(RbdDetalleFactura r)
        {
            using (HttpClient client = _http.CreateClient(_httpServidor))
            {
                using (var content = await client.GetAsync($"api/RBDArticulos/Name={r.CodArtNavigation.NomArt}"))
                {
                    if (content.IsSuccessStatusCode)
                    {
                        var result = await content.Content.ReadAsStringAsync();

                        var result2 = JsonSerializer.Deserialize<RbdArticulo>(result);

                        if (result2 != null)
                        {
                            r.CodArtNavigation.IdArt = result2.IdArt;
                            r.CodArt = result2.CodArt;
                            r.CodArtNavigation.NomArt = result2.NomArt;
                            StateHasChanged();
                        }

                    }
                }
            }
        }

        public async Task Enter(KeyboardEventArgs e, RbdDetalleFactura r, string focus)
        {
            if(e.Key == "Enter")
            {
                switch (focus)
                {
                    case "id": await VerifiqueProductById(r);break;
                    case "name":await VerifiqueProductByName(r);break;
                }
                
            }
        }

        public async Task AddProduct()
        {
            _detalleFactura.Add(new RbdDetalleFactura()
            {
                CodArtNavigation = new RbdArticulo(),
                NumFacNavigation = new RbdFactura()
            });

            StateHasChanged();
        }
    }
}
