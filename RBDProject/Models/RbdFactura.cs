using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBDProject.Models;

public partial class RbdFactura
{
    public long NumFac { get; set; }

    public int? CodNCf { get; set; }

    [Required(ErrorMessage = "El codigo de cliente no debe estar vacio")]
    public int CodCli { get; set; } = 1;

    [Required(ErrorMessage = "El codigo de empleado no debe estar vacio")]
    public int CodEm { get; set; }

    [Required(ErrorMessage = "El tipo de pago no debe estar vacio")]
    public int CodTipago { get; set; }

    [Required(ErrorMessage = "El balance no debe estar vacio")]
    public double TotalBalance { get; set; }

    [Required(ErrorMessage = "El balance neto no debe estar vacio")]
    public double TotalNeto { get; set; }

    public DateTime FechaReg { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El estado no debe estar vacio")]
    public int CodEst { get; set; }

    public virtual RbdCliente CodCliNavigation { get; set; } = null!;

    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdComprobanteFiscal? CodNCfNavigation { get; set; }

    public virtual RbdTipoPago CodTipagoNavigation { get; set; } = null!;

    public virtual RbdCuentasPorCobrar? RbdCuentasPorCobrar { get; set; }

    public virtual ICollection<RbdDetalleFactura> RbdDetalleFacturas { get; set; } = new List<RbdDetalleFactura>();
}
