using RBDProject.Components.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTipoPago
{
    public int CodTipago { get; set; }

    [Required(ErrorMessage ="El nombre no debe estar vacio")]
    public string NomPago { get; set; } = null!;

    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    [Required(ErrorMessage ="El Estado no debe estar vacio")]
    public int? CodEst { get; set; }

    public virtual RbdEstado? CodEstNavigation { get; set; }

    public virtual ICollection<RbdDetalleCuentaPorCobrar> RbdDetalleCuentaPorCobrars { get; set; } = new List<RbdDetalleCuentaPorCobrar>();

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();
}
