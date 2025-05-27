using MiEstiloAPI.Models;

namespace MiEstiloAPI.DTOs
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public string? Descripcion { get; set; }

        //public virtual ICollection<ProductoDTO> Productos { get; set; } = new List<ProductoDTO>();
    }
}
