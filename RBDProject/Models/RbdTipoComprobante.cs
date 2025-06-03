using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdTipoComprobante
{
    public int CodTipocom { get; set; }

    public string NomTipocom { get; set; } = null!;

    public string? DesTipocom { get; set; }

    public virtual ICollection<RbdComprobanteFiscal> RbdComprobanteFiscals { get; set; } = new List<RbdComprobanteFiscal>();
}
