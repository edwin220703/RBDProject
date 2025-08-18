using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdGrupo
{
    public int CodGrup { get; set; }

    [Required(ErrorMessage = "El codigo es obligatorio")]
    public string NomGrup { get; set; } = null!;

    public string? DesGrup { get; set; }

    [Required(ErrorMessage = "La fecha de registro es obligatorio")]
    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El estado del grupo es necesario")]
    public int? CodEst { get; set; }

    public virtual RbdEstado? CodEstNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdArticulo> RbdArticulos { get; set; } = new List<RbdArticulo>();
}
