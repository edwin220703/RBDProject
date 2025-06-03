using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdCuentasPorCobrar
{
    public long CodCcobro { get; set; }

    public long? NumFact { get; set; }

    public DateTime? FechaEmi { get; set; }

    public DateTime? VencPago { get; set; }

    public double? Balance { get; set; }

    public double? TotalFact { get; set; }

    public int CodEm { get; set; }

    public virtual RbdFactura? NumFactNavigation { get; set; }

    public virtual ICollection<RbdDetalleCuentaPorCobrar> RbdDetalleCuentaPorCobrars { get; set; } = new List<RbdDetalleCuentaPorCobrar>();
}
