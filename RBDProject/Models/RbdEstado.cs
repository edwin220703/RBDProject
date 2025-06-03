using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdEstado
{
    public int CodEst { get; set; }

    public string? NomEst { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FecCreacion { get; set; }

    public virtual ICollection<RbdCargo> RbdCargos { get; set; } = new List<RbdCargo>();

    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();

    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdGrupo> RbdGrupos { get; set; } = new List<RbdGrupo>();

    public virtual ICollection<RbdListaDePrecio> RbdListaDePrecios { get; set; } = new List<RbdListaDePrecio>();
}
