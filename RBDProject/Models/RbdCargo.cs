using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdCargo
{
    public int CodCar { get; set; }

    public string NomCar { get; set; } = null!;

    public string? DesCar { get; set; }

    public DateTime? FecCreacion { get; set; }

    public int CodEst { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
