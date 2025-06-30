using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdCiudade
{
    public int IdCiudad { get; set; }

    [Required(ErrorMessage = "El nombre debe no debe estar vacio")]
    public string NomCiudad { get; set; } = null!;

    [Required(ErrorMessage = "El codigo postal no debe estar vacio")]
    public string? CodPostal { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
