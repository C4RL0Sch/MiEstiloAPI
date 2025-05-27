using System;
using System.Collections.Generic;

namespace MiEstiloAPI.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdMarca { get; set; }

    public string? ImagenUrl { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<KitProducto> KitProductos { get; set; } = new List<KitProducto>();

    public virtual ICollection<ListasDeseo> ListasDeseos { get; set; } = new List<ListasDeseo>();

    public virtual ICollection<ProductoCaracteristica> ProductoCaracteristicas { get; set; } = new List<ProductoCaracteristica>();
}
