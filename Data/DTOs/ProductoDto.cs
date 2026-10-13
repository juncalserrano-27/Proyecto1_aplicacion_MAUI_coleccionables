namespace RepasoMAUI.Data.DTOs
{
    // Molde del JSON de /odata/Productos?$expand=Inventarios
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;

        // Entidad relacionada (llega gracias a $expand)
        public List<InventarioDto> Inventarios { get; set; } = new();

        // ---- Propiedades calculadas: dependen de la entidad relacionada ----

        // Total de pares sumando todas las tallas
        public int StockTotal => Inventarios.Sum(i => i.Stock);

        // Cuántas tallas tienen al menos un par
        public int TallasDisponibles => Inventarios.Count(i => i.Stock > 0);

        // Estado para el chip (Object Status de Fiori)
        public string Estado => StockTotal == 0 ? "Agotado"
                              : StockTotal < 5 ? "Pocas piezas"
                              : "Disponible";

        // Clase CSS del chip según el estado
        public string EstadoCss => StockTotal == 0 ? "negativo"
                                 : StockTotal < 5 ? "critico"
                                 : "positivo";
    }

    public class InventarioDto
    {
        public int Id { get; set; }
        public decimal Talla { get; set; }
        public int Stock { get; set; }
        public int ProductoId { get; set; }

        // Estado de cada talla para su chip
        public string Estado => Stock == 0 ? "Agotado" : Stock < 3 ? "Pocas piezas" : "Disponible";
        public string EstadoCss => Stock == 0 ? "negativo" : Stock < 3 ? "critico" : "positivo";
    }

    // Lo que mandamos en el POST (sin Id ni inventario)
    public class NuevoProductoDto
    {
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
    }
}
