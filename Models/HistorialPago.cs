using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class HistorialPago
{
    public int IdPago { get; set; }

    public int? IdPedido { get; set; }

    public int? IdMetodoPago { get; set; }

    public decimal Monto { get; set; }

    public string? ComprobanteUrl { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual MetodosPago? IdMetodoPagoNavigation { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
