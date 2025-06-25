using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdEstado
{
    public int CodEst { get; set; }

    public string? NomEst { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FecCreacion { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdCargo> RbdCargos { get; set; } = new List<RbdCargo>();

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    [JsonIgnore]
    public virtual ICollection<RbdGrupo> RbdGrupos { get; set; } = new List<RbdGrupo>();

    [JsonIgnore]
    public virtual ICollection<RbdListaDePrecio> RbdListaDePrecios { get; set; } = new List<RbdListaDePrecio>();
}
