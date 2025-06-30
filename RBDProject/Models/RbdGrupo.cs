using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdGrupo
{
    public int CodGrup { get; set; }

    [Required(ErrorMessage = "El nombre de grupo no debe estar vacio")]
    public string NomGrup { get; set; } = null!;

    public string? DesGrup { get; set; }

    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El estado de grupo no debe estar vacio")]
    public int? CodEst { get; set; }

    public virtual RbdEstado? CodEstNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdArticulo> RbdArticulos { get; set; } = new List<RbdArticulo>();
}
