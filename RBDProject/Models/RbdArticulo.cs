using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RBDProject.Models;

public partial class RbdArticulo
{
    public int CodArt { get; set; }

    public string IdArt { get; set; } = null!;

    public string NomArt { get; set; } = null!;

    public string? DesArt { get; set; }

    public int ExistArt { get; set; }

    public DateTime FecArt { get; set; }

    public int CodGrup { get; set; }

    public int CodEst { get; set; }

    public virtual RbdGrupo CodGrupNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<RbdDetalleFactura> RbdDetalleFacturas { get; set; } = new List<RbdDetalleFactura>();

    [JsonIgnore]
    public virtual ICollection<RbdListaDePrecio> RbdListaDePrecios { get; set; } = new List<RbdListaDePrecio>();
}
