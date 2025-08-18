using Microsoft.AspNetCore.Components;
using RBDProject.Models;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace RBDProject.Components.Helpers
{
    public class DetalleFac
    {
        private int _cantidad { get; set; } = 1;
        private double _precio { get; set; }
        private double _descuentoUnit { get; set; } = 0;
        private double _sub_total { get; set; }

        public RbdArticulo articulo { get; set; } = new RbdArticulo();

        public string Id
        {
            get { return articulo.IdArt; }
        }

        public string Name
        {
            get { return articulo.NomArt; }
        }

        public int Cantidad
        {
            get { return _cantidad; }

            set { _cantidad = value; GetSubTotal(); }
        }

        public double Precio
        {
            get { return _precio; }

            set { _precio = value; GetSubTotal(); }
        }

        public double DescuentoUnit
        {
            get { return _descuentoUnit; }

            set { _descuentoUnit = value; GetSubTotal(); }
        }

        public double Sub_Total
        {
            get { return _sub_total; }
        }

        private void GetSubTotal() => _sub_total = _cantidad * (Precio - DescuentoUnit);
    }
}
