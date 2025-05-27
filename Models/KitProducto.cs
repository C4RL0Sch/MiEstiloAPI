using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class KitProducto
{
    public int IdKit { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Kit IdKitNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
