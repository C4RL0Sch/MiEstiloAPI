using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string NombreMarca { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
