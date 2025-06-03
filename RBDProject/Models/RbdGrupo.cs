using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdGrupo
{
    public int CodGrup { get; set; }

    public string NomGrup { get; set; } = null!;

    public string? DesGrup { get; set; }

    public DateTime? FecCreacion { get; set; }

    public int? CodEst { get; set; }

    public virtual RbdEstado? CodEstNavigation { get; set; }

    public virtual ICollection<RbdArticulo> RbdArticulos { get; set; } = new List<RbdArticulo>();
}
