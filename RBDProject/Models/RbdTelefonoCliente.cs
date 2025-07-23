using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTelefonoCliente
{
    public int CodCli { get; set; }

    [Required(ErrorMessage = "El telefono es obligatorio")]
    public string TelCli { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdCliente CodCliNavigation { get; set; } = null!;
}
