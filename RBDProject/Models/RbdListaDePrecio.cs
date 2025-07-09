using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdListaDePrecio
{
    public int CodArt { get; set; }

    [Required(ErrorMessage = "El precio es necesario")]
    [Range(0, int.MaxValue,ErrorMessage ="El precio no debe ser negativo")]
    public int Precio { get; set; }

    public DateTime? FecCreacion { get; set; }

    public int? CodEst { get; set; }

    [JsonIgnore]
    public virtual RbdArticulo CodArtNavigation { get; set; } = null!;

    public virtual RbdEstado? CodEstNavigation { get; set; }
}
