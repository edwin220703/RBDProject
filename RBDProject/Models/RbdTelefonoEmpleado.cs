using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdTelefonoEmpleado
{
    public int CodEm { get; set; }

    public string TelEm { get; set; } = null!;

    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;
}
