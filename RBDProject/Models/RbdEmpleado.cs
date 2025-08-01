﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdEmpleado
{
    public int CodEm { get; set; }

    [Required(ErrorMessage = "El codigo es obligatorio")]
    public string IdEm { get; set; } = null!;

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string NomEm { get; set; } = null!;

    [Required(ErrorMessage = "El DNI/Cedula es obligatorio")]
    public string DniEm { get; set; } = null!;

    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string NomUs { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string NomClav { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
    public DateTime? NumPer { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El genero es obligatorio")]
    public int CodGen { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdCalle { get; set; }

    public string? DetallDirec { get; set; }

    [Required(ErrorMessage = "El cargo es obligatorio")]
    public int? CodCar { get; set; }

    [Required(ErrorMessage = "El sueldo es obligatorio")]
    public double Suedms { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    public int CodEst { get; set; }

    public int IdProvincia { get; set; }

    public virtual RbdCargo? CodCarNavigation { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdGenero CodGenNavigation { get; set; } = null!;

    public virtual RbdCalle? IdCalleNavigation { get; set; }

    public virtual RbdCiudade? IdCiudadNavigation { get; set; }

    public virtual RbdProvincium IdProvinciaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdFactura> RbdFacturas { get; set; } = new List<RbdFactura>();

    public virtual ICollection<RbdTelefonoEmpleado> RbdTelefonoEmpleados { get; set; } = new List<RbdTelefonoEmpleado>();
}
