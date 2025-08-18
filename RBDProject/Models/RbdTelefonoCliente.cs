using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTelefonoCliente
{
    public int CodCli { get; set; }

    [RegularExpression("^(\\+0?1\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$", ErrorMessage = "El telefono debe ser correcto. incluir guiones (-)")]
    public string TelCli { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdCliente CodCliNavigation { get; set; } = null!;
}
