using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdComprobanteFiscal
{
    public int CodNcf { get; set; }

    public string SecCom { get; set; } = null!;

    public string? DesCom { get; set; }

    public double? ImpCom { get; set; }

    public DateTime DocFec { get; set; }

    public DateTime? FechaVec { get; set; }

    public int CodTipocom { get; set; }

    public virtual RbdTipoComprobante CodTipocomNavigation { get; set; } = null!;

    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();
}
