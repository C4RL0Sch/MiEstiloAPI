using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? FechaPedido { get; set; }

    public string? Estado { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<HistorialPago> HistorialPagos { get; set; } = new List<HistorialPago>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual Venta? Venta { get; set; }
}
