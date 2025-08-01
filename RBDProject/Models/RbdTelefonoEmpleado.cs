﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTelefonoEmpleado
{
    public int CodEm { get; set; }

    [Required(ErrorMessage = "El telefono es obligatorio")]
    public string TelEm { get; set; } = null!;

    [JsonIgnore]
    public virtual RbdEmpleado CodEmNavigation { get; set; } = null!;
}
