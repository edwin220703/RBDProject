using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdProvincium
{
    public int IdProvincia { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NomProvincia { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdCiudade> RbdCiudades { get; set; } = new List<RbdCiudade>();

    [JsonIgnore]
    public virtual ICollection<RbdCliente> RbdClientes { get; set; } = new List<RbdCliente>();

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
