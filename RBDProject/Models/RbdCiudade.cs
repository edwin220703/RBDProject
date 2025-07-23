using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdCiudade
{
    public int IdCiudad { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NomCiudad { get; set; } = null!;

    public string? CodPostal { get; set; }

    [Required(ErrorMessage = "La provincia es obligatorio")]
    public int IdProvincia { get; set; }

    public virtual RbdProvincium IdProvinciaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdCalle> RbdCalles { get; set; } = new List<RbdCalle>();

    [JsonIgnore]
    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
