using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdListaDePrecio
{
    public int CodArt { get; set; }

    public int Precio { get; set; }

    public DateTime? FecCreacion { get; set; }

    public int? CodEst { get; set; }

    [JsonIgnore]
    public virtual RbdArticulo CodArtNavigation { get; set; } = null!;

    public virtual RbdEstado? CodEstNavigation { get; set; }
}
