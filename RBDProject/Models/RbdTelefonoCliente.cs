using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdTelefonoCliente
{
    public int CodCli { get; set; }

    public string TelCli { get; set; } = null!;

    public virtual RbdCliente CodCliNavigation { get; set; } = null!;
}
