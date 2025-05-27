using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Caracteristica
{
    public int IdCaracteristica { get; set; }

    public string NombreCaracteristica { get; set; } = null!;

    public virtual ICollection<ProductoCaracteristica> ProductoCaracteristicas { get; set; } = new List<ProductoCaracteristica>();
}
