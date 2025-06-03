using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class RefreshToken
{
    public int IdRToken { get; set; }

    public string Token { get; set; } = null!;

    public int IdUsuario { get; set; }

    public DateTime Expira { get; set; }

    public bool Revocado { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
