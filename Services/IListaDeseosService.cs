using MiEstiloAPI.Models;

namespace MiEstiloAPI.Services
{
    public interface IListaDeseosService
    {
        public bool isWished(int? idUsuario, Producto producto);
    }
}
