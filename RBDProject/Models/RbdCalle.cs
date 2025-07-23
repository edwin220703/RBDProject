using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdCalle
{
    public int IdCalle { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string? NomCalle { get; set; }

    [Required(ErrorMessage = "La ciudad es obligatoria")]
    public int IdCiudad { get; set; }

    public virtual RbdCiudade IdCiudadNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
