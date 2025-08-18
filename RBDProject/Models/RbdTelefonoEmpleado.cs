using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTelefonoEmpleado
{
    public int CodEm { get; set; }

    [RegularExpression("^(\\+0?1\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$", ErrorMessage = "El telefono debe ser correcto. incluir guiones (-)")]
    public string TelEm { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;
}
