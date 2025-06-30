using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdCargo
{ 
    public int CodCar { get; set; }

    [Required(ErrorMessage = "El nombre del cargo es obligatorio")]
    public string NomCar { get; set; } = null!;

    public string? DesCar { get; set; }

    public DateTime? FecCreacion { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El estado es obligatorio")]
    public int CodEst { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdEmpleado> RbdEmpleados { get; set; } = new List<RbdEmpleado>();
}
