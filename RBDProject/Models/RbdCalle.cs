using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdCalle
{
    public int IdCalle { get; set; }

    public string? NomCalle { get; set; }

    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
