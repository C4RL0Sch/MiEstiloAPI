using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Kit
{
    public int IdKit { get; set; }

    public string NombreKit { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string? ImagenUrl { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<KitProducto> KitProductos { get; set; } = new List<KitProducto>();
}
