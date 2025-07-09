using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdDetalleFactura
{
    public long NumFac { get; set; }

    [Required(ErrorMessage = "Es necesario el codigo del articulo")]
    public int CodArt { get; set; }

    [Required(ErrorMessage = "Es necesario la cantidad del articulo")]
    [Range(0, int.MaxValue)]
    public int? CantArt { get; set; }

    [Required(ErrorMessage = "Es necesario el precio del articulo")]
    [Range(0, double.MaxValue)]
    public double? Precio { get; set; }

    [Range(0, double.MaxValue)]
    public double? DescuentoArt { get; set; }

    public virtual RbdArticulo CodArtNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdFactura NumFacNavigation { get; set; } = null!;
}
