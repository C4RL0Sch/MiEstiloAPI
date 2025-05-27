using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MiEstiloAPI.Models;

public partial class MiEstiloContext : DbContext
{
    public MiEstiloContext()
    {
    }

    public MiEstiloContext(DbContextOptions<MiEstiloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caracteristica> Caracteristicas { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<DireccionesEnvio> DireccionesEnvios { get; set; }

    public virtual DbSet<HistorialPago> HistorialPagos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Kit> Kits { get; set; }

    public virtual DbSet<KitProducto> KitProductos { get; set; }

    public virtual DbSet<ListasDeseo> ListasDeseos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoCaracteristica> ProductoCaracteristicas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CHARLY\\SQLEXPRESS;Database=MiEstilo;Trusted_Connection=True;TrustServerCertificate=True;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caracteristica>(entity =>
        {
            entity.HasKey(e => e.IdCaracteristica).HasName("PK__caracter__26122F359DB12E6F");

            entity.ToTable("caracteristicas");

            entity.HasIndex(e => e.NombreCaracteristica, "UQ__caracter__70121E91A3CAEE26").IsUnique();

            entity.Property(e => e.IdCaracteristica).HasColumnName("id_caracteristica");
            entity.Property(e => e.NombreCaracteristica)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_caracteristica");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__carritos__83A2AD9C728CB0ED");

            entity.ToTable("carritos");

            entity.Property(e => e.IdCarrito).HasColumnName("id_carrito");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaAgregado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_agregado");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__carritos__id_pro__6EF57B66");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__carritos__id_usu__6E01572D");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__categori__CD54BC5A7F8D61D5");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.NombreCategoria, "UQ__categori__4EBF625959230A99").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__detalle___4F1332DEAE656649");

            entity.ToTable("detalle_pedido");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__detalle_p__id_pe__5EBF139D");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__detalle_p__id_pr__5FB337D6");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__detalle___5B265D47A471319A");

            entity.ToTable("detalle_ventas");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalle_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([cantidad]*[precio_unitario])", true)
                .HasColumnType("decimal(21, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__detalle_v__id_pr__7C4F7684");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__detalle_v__id_ve__7B5B524B");
        });

        modelBuilder.Entity<DireccionesEnvio>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__direccio__25C35D07FFA04C0F");

            entity.ToTable("direcciones_envios");

            entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DireccionesEnvios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__direccion__id_us__6A30C649");
        });

        modelBuilder.Entity<HistorialPago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__historia__0941B074E9E713AD");

            entity.ToTable("historial_pagos");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.ComprobanteUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comprobante_url");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.HistorialPagos)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK__historial__id_me__6754599E");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.HistorialPagos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__historial__id_pe__66603565");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__inventar__013AEB515F54B224");

            entity.ToTable("inventario");

            entity.Property(e => e.IdInventario).HasColumnName("id_inventario");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__inventari__id_pr__48CFD27E");
        });

        modelBuilder.Entity<Kit>(entity =>
        {
            entity.HasKey(e => e.IdKit).HasName("PK__kits__D49600A7959C2AAB");

            entity.ToTable("kits");

            entity.Property(e => e.IdKit).HasColumnName("id_kit");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen_url");
            entity.Property(e => e.NombreKit)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_kit");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<KitProducto>(entity =>
        {
            entity.HasKey(e => new { e.IdKit, e.IdProducto }).HasName("PK__kit_prod__1B6541672113561C");

            entity.ToTable("kit_productos");

            entity.Property(e => e.IdKit).HasColumnName("id_kit");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");

            entity.HasOne(d => d.IdKitNavigation).WithMany(p => p.KitProductos)
                .HasForeignKey(d => d.IdKit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__kit_produ__id_ki__4E88ABD4");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.KitProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__kit_produ__id_pr__4F7CD00D");
        });

        modelBuilder.Entity<ListasDeseo>(entity =>
        {
            entity.HasKey(e => e.IdListaDeseo).HasName("PK__listas_d__2D1F5839D1E389A5");

            entity.ToTable("listas_deseos");

            entity.Property(e => e.IdListaDeseo).HasColumnName("id_lista_deseo");
            entity.Property(e => e.FechaAgregado)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_agregado");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ListasDeseos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__listas_de__id_pr__73BA3083");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ListasDeseos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__listas_de__id_us__72C60C4A");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__marcas__7E43E99E039BB029");

            entity.ToTable("marcas");

            entity.HasIndex(e => e.NombreMarca, "UQ__marcas__6059F572109BDAAE").IsUnique();

            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_marca");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__metodos___85BE0EBC37612021");

            entity.ToTable("metodos_pago");

            entity.HasIndex(e => e.Metodo, "UQ__metodos___A66E5D4AF1E8B7B6").IsUnique();

            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.Metodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metodo");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__pedidos__6FF01489B7FCAC14");

            entity.ToTable("pedidos");

            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pendiente")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_pedido");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__pedidos__id_usua__5BE2A6F2");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__producto__FF341C0DF77B8985");

            entity.ToTable("productos");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen_url");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__productos__id_ca__440B1D61");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__productos__id_ma__44FF419A");
        });

        modelBuilder.Entity<ProductoCaracteristica>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdCaracteristica }).HasName("PK__producto__BD553EFE9D6B5BEE");

            entity.ToTable("producto_caracteristicas");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdCaracteristica).HasColumnName("id_caracteristica");
            entity.Property(e => e.Valor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("valor");

            entity.HasOne(d => d.IdCaracteristicaNavigation).WithMany(p => p.ProductoCaracteristicas)
                .HasForeignKey(d => d.IdCaracteristica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__producto___id_ca__5629CD9C");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoCaracteristicas)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__producto___id_pr__5535A963");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__4E3E04AD38FAD195");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E616405BBE0C4").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("cliente")
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__ventas__459533BFD13D6AEE");

            entity.ToTable("ventas");

            entity.HasIndex(e => e.IdPedido, "UQ__ventas__6FF01488C8AE4FDD").IsUnique();

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdPedidoNavigation).WithOne(p => p.Venta)
                .HasForeignKey<Venta>(d => d.IdPedido)
                .HasConstraintName("FK__ventas__id_pedid__787EE5A0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
