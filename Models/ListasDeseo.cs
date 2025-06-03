using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class ListasDeseo
{
    public int IdListaDeseo { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? FechaAgregado { get; set; }

    public bool? Activo { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
