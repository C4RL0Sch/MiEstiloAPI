using MiEstiloAPI.Models;

namespace MiEstiloAPI.Services
{
    public class ListaDeseosService : IListaDeseosService
    {
        public bool isWished(int? idUsuario, Producto producto)
        {
            if (idUsuario == null)
                return false;

            var wish = producto.ListasDeseos.FirstOrDefault(d => d.IdProducto == producto.IdProducto && d.IdUsuario == Convert.ToInt32(idUsuario));
            if (wish == null)
                return false;

            return true;
        }
    }
}
