using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBDProject.Models;

public partial class RbdFactura
{
    public long NumFac { get; set; }

    public int? CodNCf { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio")]
    public int CodCli { get; set; }

    [Required(ErrorMessage = "El empleado es obligatorio")]
    public int CodEm { get; set; }

    [Required(ErrorMessage = "La forma de pago es obligatorio")]
    public int CodTipago { get; set; }

    [Required(ErrorMessage = "El balance es obligatorio")]
    public double TotalBalance { get; set; }

    [Required(ErrorMessage = "El balance neto es obligatorio")]
    public double TotalNeto { get; set; }

    [Required(ErrorMessage = "La fecha de registro es obligatoria")]
    public DateTime FechaReg { get; set; } = DateTime.Now;

    public int CodEst { get; set; }

    public string? Miscelaneo { get; set; }

    public virtual RbdCliente CodCliNavigation { get; set; } = null!;

    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdComprobanteFiscal? CodNCfNavigation { get; set; }

    public virtual RbdTipoPago CodTipagoNavigation { get; set; } = null!;

    public virtual RbdCuentasPorCobrar? RbdCuentasPorCobrar { get; set; }

    public virtual ICollection<RbdDetalleFactura> RbdDetalleFacturas { get; set; } = new List<RbdDetalleFactura>();
}
