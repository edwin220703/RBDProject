using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTelefonoEmpleado
{
    public int CodEm { get; set; }

    [RegularExpression("\"/\\(?([0-9]{3})\\)?([ .-]?)([0-9]{3})\\2([0-9]{4})/\"",ErrorMessage ="Coloca correctamente el numero telefonico")]
    public string TelEm { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;
}
