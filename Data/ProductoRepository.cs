using RepasoMAUI.Models;

namespace RepasoMAUI.Data
{
    public class ProductoRepository
    {
        private readonly List<Producto> _productos =
        [
            new(){ Id = "1", Modelo = "Sabrina 2", Color = "Rosa",   Precio = 2499.00m, ImagenUrl = "sabrina_dos_rosas.jpg" },
            new(){ Id = "2", Modelo = "LeBron 22", Color = "Blanco", Precio = 3799.00m, ImagenUrl = "lebron_22_blancos.jpg" },
            new(){ Id = "3", Modelo = "Curry 12",  Color = "Negro",  Precio = 3299.00m, ImagenUrl = "curry12_negros.jpg" },
            new(){ Id = "4", Modelo = "AE 1",      Color = "Blanco", Precio = 2999.00m, ImagenUrl = "ae_1_blancos.jpg" }
        ];

        public List<Producto> ObtenerTodos() => _productos;

        public Producto ObtenerPorId(string id) => _productos.FirstOrDefault(p => p.Id == id);

        // Agrega un producto nuevo y le asigna el siguiente Id automáticamente
        public void Agregar(Producto producto)
        {
            int siguienteId = _productos.Count == 0
                ? 1
                : _productos.Max(p => int.Parse(p.Id)) + 1;

            producto.Id = siguienteId.ToString();
            _productos.Add(producto);
        }

        // Elimina varios productos del catálogo (mismo patrón que EliminarFavoritos)
        public void EliminarProductos(List<Producto> productos)
        {
            foreach (var producto in productos)
            {
                var existente = _productos.FirstOrDefault(p => p.Id == producto.Id);

                if (existente != null)
                {
                    _productos.Remove(existente);
                }
            }
        }

        // ---- Favoritos (de Juncal) ----
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
