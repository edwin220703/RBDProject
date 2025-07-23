using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdDetalleFactura
{
    public long NumFac { get; set; }

    [Required(ErrorMessage = "El articulo es obligatorio")]
    public int CodArt { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatorio")]
    public int? CantArt { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio minimo es 0")]
    public double? Precio { get; set; }

    public double? DescuentoArt { get; set; }

    public virtual RbdArticulo CodArtNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdFactura NumFacNavigation { get; set; } = null!;
}
