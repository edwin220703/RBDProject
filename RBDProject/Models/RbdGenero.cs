using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdGenero
{
    public int CodGen { get; set; }

    public string NomGen { get; set; } = null!;

    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
