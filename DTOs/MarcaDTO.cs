using System.Collections.Generic;

namespace MiEstiloAPI.DTOs
{
    public class MarcaDTO
    {
        public int IdMarca { get; set; }

        public string NombreMarca { get; set; } = null!;

        //public virtual ICollection<ProductoDTO> Productos { get; set; } = new List<ProductoDTO>();
    }
}
