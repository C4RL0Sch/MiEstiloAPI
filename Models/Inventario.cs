using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int? IdProducto { get; set; }

    public int Stock { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
