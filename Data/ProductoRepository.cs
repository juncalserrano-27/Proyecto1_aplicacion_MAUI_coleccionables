using RepasoMAUI.Models;

namespace RepasoMAUI.Data
{
    // Guarda los favoritos en memoria. El catálogo ya no vive aquí:
    // ahora viene del servicio OData (TiendaODataService).
    public class ProductoRepository
    {
        private readonly List<Producto> _favoritos = new();

        public List<Producto> ObtenerFavoritos() => _favoritos;

        public void AgregarFavorito(Producto producto)
        {
            if (!_favoritos.Any(p => p.Id == producto.Id))
            {
                _favoritos.Add(producto);
            }
        }

        public void EliminarFavoritos(List<Producto> productos)
        {
            foreach (var producto in productos)
            {
                var favorito = _favoritos.FirstOrDefault(p => p.Id == producto.Id);
                if (favorito != null)
                {
                    _favoritos.Remove(favorito);
                }
            }
        }
    }
}
