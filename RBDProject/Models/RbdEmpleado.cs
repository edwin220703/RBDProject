using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdEmpleado
{
    public int CodEm { get; set; }

    public string IdEm { get; set; } = null!;

    public string NomEm { get; set; } = null!;

    public string DniEm { get; set; } = null!;

    public string NomUs { get; set; } = null!;

    public string NomClav { get; set; } = null!;

    public DateTime? NumPer { get; set; }

    public int CodGen { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdCalle { get; set; }

    public string? DetallDirec { get; set; }

    public int? CodCar { get; set; }

    public double Suedms { get; set; }

    public int CodEst { get; set; }

    public virtual RbdCargo? CodCarNavigation { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdGenero CodGenNavigation { get; set; } = null!;

    public virtual RbdCalle? IdCalleNavigation { get; set; }

    public virtual RbdCiudade? IdCiudadNavigation { get; set; }

    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdTelefonoEmpleado> RbdTelefonoEmpleados { get; set; } = new List<RbdTelefonoEmpleado>();
}
