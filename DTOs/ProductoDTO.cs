namespace MiEstiloAPI.DTOs
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int? IdCategoria { get; set; }

        public int? IdMarca { get; set; }

        public string? ImagenUrl { get; set; }

        public bool? Deseado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public CategoriaDTO? Categoria { get; set; }

        public MarcaDTO? Marca { get; set; }

    }
}
