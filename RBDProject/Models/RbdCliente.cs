using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdCliente
{
    public int CodCli { get; set; }

    [Required(ErrorMessage = "El ID del cliente no  debe estar vacio")]
    public string IdCli { get; set; } = null!;

    [Required(ErrorMessage = "El Nombre del cliente no  debe estar vacio")]
    public string NomCli { get; set; } = null!;

    [MinLength(13, ErrorMessage = "El DNI/Cedula debe contener minimo 11 numeros")]
    [MaxLength(11, ErrorMessage = "El DNI/Cedula debe contener maximo 11 numeros")]
    [Required(ErrorMessage = "El DNI/Cedula debe estar completo. Coloque solo numeros")]
    public string? DniCli { get; set; } 

    [EmailAddress(ErrorMessage = "Coloque un correo valido")]
    public string? CorrCli { get; set; }

    public int? CodGen { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdCalle { get; set; }

    public string? DetallDirec { get; set; }

    public double? TipRnc { get; set; }

    public DateTime? FecEnt { get; set; } = DateTime.Now;

    public virtual RbdGenero? CodGenNavigation { get; set; }

    public virtual RbdCalle? IdCalleNavigation { get; set; }

    public virtual RbdCiudade? IdCiudadNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdTelefonoCliente> RbdTelefonoClientes { get; set; } = new List<RbdTelefonoCliente>();
}
