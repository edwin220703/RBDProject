using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdDetalleCuentaPorCobrar
{
    public long CodCcobro { get; set; }

    public DateTime FechaPago { get; set; }

    public double? Monto { get; set; }

    public int? CodTippago { get; set; }

    public int CodEm { get; set; }

    [JsonIgnore]
    public virtual RbdCuentasPorCobrar CodCcobroNavigation { get; set; } = null!;

    public virtual RbdTipoPago? CodTippagoNavigation { get; set; }
}
