using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdTipoPago
{
    public int CodTipago { get; set; }

    public string NomPago { get; set; } = null!;

    public DateTime? FecCreacion { get; set; }

    public int? CodEst { get; set; }

    public virtual ICollection<RbdDetalleCuentaPorCobrar> RbdDetalleCuentaPorCobrars { get; set; } = new List<RbdDetalleCuentaPorCobrar>();

    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();
}
