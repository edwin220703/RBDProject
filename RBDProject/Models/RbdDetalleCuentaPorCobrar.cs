using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdDetalleCuentaPorCobrar
{
    public long CodCcobro { get; set; }

    public DateTime FechaPago { get; set; }

    public double? Monto { get; set; }

    public int? CodTippago { get; set; }

    public int CodEm { get; set; }

    public virtual RbdCuentasPorCobrar CodCcobroNavigation { get; set; } = null!;

    public virtual RbdTipoPago? CodTippagoNavigation { get; set; }
}
