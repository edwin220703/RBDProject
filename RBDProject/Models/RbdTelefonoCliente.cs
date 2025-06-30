using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTelefonoCliente
{
    public int CodCli { get; set; }

    [RegularExpression("\"/\\(?([0-9]{3})\\)?([ .-]?)([0-9]{3})\\2([0-9]{4})/\"", ErrorMessage = "Coloca correctamente el numero telefonico")]
    public string TelCli { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdCliente CodCliNavigation { get; set; } = null!;
}
