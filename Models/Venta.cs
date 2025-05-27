using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int? IdPedido { get; set; }

    public DateTime? FechaVenta { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
