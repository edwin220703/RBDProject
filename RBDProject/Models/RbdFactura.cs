using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdFactura
{
    public long NumFac { get; set; }

    public int? CodNCf { get; set; }

    public int CodCli { get; set; }

    public int CodEm { get; set; }

    public int CodTipago { get; set; }

    public double TotalBalance { get; set; }

    public double TotalNeto { get; set; }

    public DateTime FechaReg { get; set; }

    public int CodEst { get; set; }

    public virtual RbdCliente CodCliNavigation { get; set; } = null!;

    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdComprobanteFiscal? CodNCfNavigation { get; set; }

    public virtual RbdTipoPago CodTipagoNavigation { get; set; } = null!;

    public virtual RbdCuentasPorCobrar? RbdCuentasPorCobrar { get; set; }

    public virtual ICollection<RbdDetalleFactura> RbdDetalleFacturas { get; set; } = new List<RbdDetalleFactura>();
}
