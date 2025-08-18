using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdCliente
{
    public int CodCli { get; set; }

    [Required(ErrorMessage = "El codigo es obligatorio")]
    public string IdCli { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NomCli { get; set; } = null!;

    [Required(ErrorMessage = "El DNI/Cedula es obligatorio")]
    [RegularExpression("^[0-9]+",ErrorMessage ="Solo se aceptan numeros")]
    public string? DniCli { get; set; }

    [EmailAddress(ErrorMessage = "El correo es invalido")]
    public string? CorrCli { get; set; }

    public int? CodGen { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdCalle { get; set; }

    public string? DetallDirec { get; set; }

    public double? TipRnc { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public DateTime? FecEnt { get; set; } = DateTime.Now;

    public int? IdProvincia { get; set; }

    public virtual RbdGenero? CodGenNavigation { get; set; }

    public virtual RbdCalle? IdCalleNavigation { get; set; }

    public virtual RbdCiudade? IdCiudadNavigation { get; set; }

    public virtual RbdProvincium? IdProvinciaNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdTelefonoCliente> RbdTelefonoClientes { get; set; } = new List<RbdTelefonoCliente>();
}
