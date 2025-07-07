using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdDetalleFactura
{
    public long NumFac { get; set; }

    public int CodArt { get; set; }

    public int? CantArt { get; set; }

    public double? Precio { get; set; }

    public double? DescuentoArt { get; set; }

    public virtual RbdArticulo CodArtNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdFactura NumFacNavigation { get; set; } = null!;
}
