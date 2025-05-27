using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class MetodosPago
{
    public int IdMetodoPago { get; set; }

    public string Metodo { get; set; } = null!;

    public virtual ICollection<HistorialPago> HistorialPagos { get; set; } = new List<HistorialPago>();
}
