namespace MiEstiloAPI.DTOs
{
    public class ProductoNuevoDTO
    {
        public string NombreProducto { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdMarca { get; set; }
        public string? ImagenUrl { get; set; }

        // Lista de IDs de características relacionadas
        public List<int>? CaracteristicasIds { get; set; }
    }
}
