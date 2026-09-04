using RepasoMAUI.Models;

namespace RepasoMAUI.Data
{
    public class ProductoRepository
    {
        
        private readonly List<Producto> _productos = 
        [
            new(){ Id = "1", Modelo = "Sabrina 2", Color = "Blanco/Rosa", Precio = 2499.00m, ImagenUrl = "sabrina_dos_rosas.jpg" },
            new(){ Id = "2", Modelo = "LeBron 22", Color = "Negro/Rojo", Precio = 3799.00m, ImagenUrl = "lebron22.png" },
            new(){ Id = "3", Modelo = "Curry 12", Color = "Azul/Blanco", Precio = 3299.00m, ImagenUrl = "curry12.png" },
            new(){ Id = "4", Modelo = "AE 1", Color = "Negro/Blanco", Precio = 2999.00m, ImagenUrl = "ae1.png" }
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
