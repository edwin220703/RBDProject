using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdEstado
{
    public int CodEst { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string? NomEst { get; set; }

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "La fecha es obligatorio")]
    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    [JsonIgnore]
    public virtual ICollection<RbdArticulo> RbdArticulos { get; set; } = new List<RbdArticulo>();

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

    [JsonIgnore]
    public virtual ICollection<RbdTipoPago> RbdTipoPagos { get; set; } = new List<RbdTipoPago>();
}
