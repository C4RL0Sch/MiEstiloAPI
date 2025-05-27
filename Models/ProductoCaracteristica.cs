using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class ProductoCaracteristica
{
    public int IdProducto { get; set; }

    public int IdCaracteristica { get; set; }

    public string Valor { get; set; } = null!;

    public virtual Caracteristica IdCaracteristicaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
