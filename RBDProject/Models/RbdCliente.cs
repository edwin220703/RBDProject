using System;
using System.Collections.Generic;

namespace RBDProject.Models;

public partial class RbdCliente
{
    public int CodCli { get; set; }

    public string IdCli { get; set; } = null!;

    public string NomCli { get; set; } = null!;

    public string? DniCli { get; set; }

    public string? CorrCli { get; set; }

    public int? CodGen { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdCalle { get; set; }

    public string? DetallDirec { get; set; }

    public double? TipRnc { get; set; }

    public DateTime? FecEnt { get; set; }

    public virtual RbdGenero? CodGenNavigation { get; set; }

    public virtual RbdCalle? IdCalleNavigation { get; set; }

    public virtual RbdCiudade? IdCiudadNavigation { get; set; }

    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdTelefonoCliente> RbdTelefonoClientes { get; set; } = new List<RbdTelefonoCliente>();
}
