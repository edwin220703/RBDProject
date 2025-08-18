using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdDetalleCuentaPorCobrar
{
    public long CodCcobro { get; set; }

    [Required(ErrorMessage = "La fecha de pago es necesaria")]
    public DateTime FechaPago { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "La fecha de pago es necesaria")]
    public double? Monto { get; set; }

    [Required(ErrorMessage = "La fecha de pago es necesaria")]
    public int? CodTippago { get; set; }

    [Required(ErrorMessage = "La fecha de pago es necesaria")]
    public int CodEm { get; set; }

    [JsonIgnore]
    public virtual RbdCuentasPorCobrar CodCcobroNavigation { get; set; } = null!;

    public virtual RbdTipoPago? CodTippagoNavigation { get; set; }
}
