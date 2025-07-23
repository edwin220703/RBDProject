using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTipoPago
{
    public int CodTipago { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NomPago { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de creacion es obligatorio")]
    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El estado es obligatorio")]
    public int? CodEst { get; set; }

    public virtual RbdEstado? CodEstNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdDetalleCuentaPorCobrar> RbdDetalleCuentaPorCobrars { get; set; } = new List<RbdDetalleCuentaPorCobrar>();

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();
}
