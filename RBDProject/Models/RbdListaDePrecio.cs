using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdListaDePrecio
{
    public int CodArt { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0, int.MaxValue,ErrorMessage ="El valor minimo del precio debe ser 0")]
    public double Precio { get; set; }

    [Required(ErrorMessage = "La fecha de cracion es obligatorio")]
    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    public int? CodEst { get; set; }

    [JsonIgnore]
    public virtual RbdArticulo CodArtNavigation { get; set; } = null!;

    public virtual RbdEstado? CodEstNavigation { get; set; }
}
