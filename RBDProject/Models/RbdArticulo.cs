using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdArticulo
{
    public int CodArt { get; set; }

    [Required(ErrorMessage = "El codigo es obligatorio")]
    public string IdArt { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NomArt { get; set; } = null!;

    public string? DesArt { get; set; }

    [Required(ErrorMessage = "Las existencias son obligatorio")]
    [Range(0, int.MaxValue,ErrorMessage ="El valor minimo tiene que ser [0]")]
    public int ExistArt { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria")]
    public DateTime FecArt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El grupo es obligatorio")]
    public int CodGrup { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    public int CodEst { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdGrupo CodGrupNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdDetalleFactura> RbdDetalleFacturas { get; set; } = new List<RbdDetalleFactura>();

    public virtual ICollection<RbdListaDePrecio> RbdListaDePrecios { get; set; } = new List<RbdListaDePrecio>();
}
