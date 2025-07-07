using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RBDProject.Models;

public partial class RbdComprobanteFiscal
{
    public int CodNcf { get; set; }

    [RegularExpression("^[ABEP]{1}[0-9]{2}[0-9]{8}$", ErrorMessage ="Coloque un NCF correctamente")]
    public string SecCom { get; set; } = null!;

    public string? DesCom { get; set; }

    public double? ImpCom { get; set; }

    public DateTime DocFec { get; set; } = DateTime.Now;

    public DateTime? FechaVec { get; set; }

    [Required(ErrorMessage = "El tipo de comprobante no debe estar vacio")]
    public int CodTipocom { get; set; }

    public virtual RbdTipoComprobante CodTipocomNavigation { get; set; } = null!;

    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();
}
