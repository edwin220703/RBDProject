using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdTipoComprobante
{
    public int CodTipocom { get; set; }

    [Required(ErrorMessage = "El nombre del comprobante no debe estar vacio")]
    public string NomTipocom { get; set; } = null!;

    [Required(ErrorMessage = "La descripcion del comprobante no debe estar vacio")]
    public string? DesTipocom { get; set; }

    [JsonIgnore]
    public virtual ICollection<RbdComprobanteFiscal> RbdComprobanteFiscals { get; set; } = new List<RbdComprobanteFiscal>();
}
