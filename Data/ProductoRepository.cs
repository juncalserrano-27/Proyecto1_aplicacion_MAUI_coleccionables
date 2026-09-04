using RepasoMAUI.Models;

namespace RepasoMAUI.Data
{
    public class ProductoRepository
    {
        private readonly List<Producto> _productos =
    [
        new(){ Id = "1", Nombre = "Teclado Mecánico", Descripcion = "Teclado mecánico switches azules, retroiluminado.", Precio = 899.00m, ImagenUrl = "teclado.png" },
        new(){ Id = "2", Nombre = "Mouse Inalámbrico", Descripcion = "Mouse inalámbrico ergonómico, 2.4GHz.", Precio = 349.00m, ImagenUrl = "mouse.png" },
        new(){ Id = "3", Nombre = "Monitor 27\"", Descripcion = "Monitor 27 pulgadas, 144Hz, IPS.", Precio = 4599.00m, ImagenUrl = "monitor.png" },
        new(){ Id = "4", Nombre = "Audífonos USB", Descripcion = "Audífonos con micrófono, cancelación de ruido.", Precio = 599.00m, ImagenUrl = "audifonos.png" }
    ];

        public List<Producto> ObtenerTodos() => _productos;

        public Producto ObtenerPorId(string id) => _productos.FirstOrDefault(p => p.Id == id);

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
