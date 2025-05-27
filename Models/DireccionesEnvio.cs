using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class DireccionesEnvio
{
    public int IdDireccion { get; set; }

    public int? IdUsuario { get; set; }

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
