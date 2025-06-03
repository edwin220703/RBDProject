using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdCiudade
{
    public int IdCiudad { get; set; }

    public string NomCiudad { get; set; } = null!;

    public string? CodPostal { get; set; }

    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
