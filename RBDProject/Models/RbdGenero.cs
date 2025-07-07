using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdGenero
{
    public int CodGen { get; set; }

    public string NomGen { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
