using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdArticulo
{
    public int CodArt { get; set; }

    [Required(ErrorMessage = "Es necesario el ID del producto")]
    public string IdArt { get; set; } = null!;

    [Required(ErrorMessage = "Es necesario el Nombre del Producto")]
    public string NomArt { get; set; } = null!;

    public string? DesArt { get; set; }

    [Required(ErrorMessage = "Es necesario las existencias del producto")]
    [Range(0, int.MaxValue,ErrorMessage ="El numero minimo de existencias es 0")]
    public int ExistArt { get; set; }

    public DateTime FecArt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Es necesario el grupo a pertenecer los articulos")]
    public int CodGrup { get; set; }

    [Required(ErrorMessage = "el estado del producto es necesario")]
    public int CodEst { get; set; }

    public virtual RbdEstado CodEstNavigation { get; set; } = null!;

    public virtual RbdGrupo CodGrupNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdDetalleFactura> RbdDetalleFacturas { get; set; } = new List<RbdDetalleFactura>();

    public virtual ICollection<RbdListaDePrecio> RbdListaDePrecios { get; set; } = new List<RbdListaDePrecio>();
}
