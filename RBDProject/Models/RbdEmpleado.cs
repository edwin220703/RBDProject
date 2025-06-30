using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdEmpleado
{
    public int CodEm { get; set; }

    [Required(ErrorMessage = "El ID del empleado no debe estar vacio")]
    public string IdEm { get; set; } = null!;

    [Required(ErrorMessage = "El nombre del empleado no debe estar vacio")]
    public string NomEm { get; set; } = null!;

    [MinLength(11,ErrorMessage ="El DNI/Cedula debe contener minimo 11 numeros")]
    [MaxLength(11,ErrorMessage ="El DNI/Cedula debe contener maximo 11 numeros")]
    [Required(ErrorMessage ="El DNI/Cedula debe estar completo")]
    public string DniEm { get; set; } = null!;

    [Required(ErrorMessage = "El nombre de usuario no debe estar vacio")]
    public string NomUs { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña no debe estar vacio")]
    public string NomClav { get; set; } = null!;

    public DateTime? NumPer { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El genero no debe estar vacio")]
    public int CodGen { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdCalle { get; set; }

    public string? DetallDirec { get; set; }

    [Required(ErrorMessage = "El cargo no debe estar vacio")]
    public int? CodCar { get; set; }

    [Required(ErrorMessage = "El sueldo del empleado no debe estar vacio")]
    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$",ErrorMessage ="El sueldo debe contener un formato '00.00'")]
    public double Suedms { get; set; }

    [Required(ErrorMessage = "El estado de empleado no debe estar vacio")]
    public int CodEst { get; set; }

    public virtual RbdCargo? CodCarNavigation { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdGenero CodGenNavigation { get; set; } = null!;

    public virtual RbdCalle? IdCalleNavigation { get; set; }

    public virtual RbdCiudade? IdCiudadNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdTelefonoEmpleado> RbdTelefonoEmpleados { get; set; } = new List<RbdTelefonoEmpleado>();
}
