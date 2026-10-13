using System.ComponentModel.DataAnnotations;

namespace ServicioOData.Models
{
    // Entidad principal: un modelo de tenis (lado "uno" de la relación)
    public class Producto
    {
        public int Id { get; set; }

        [Required, StringLength(40, MinimumLength = 2)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;

        [Range(1, 100000)]
        public decimal Precio { get; set; }

        public string ImagenUrl { get; set; } = string.Empty;

        // Navigation Property: un producto tiene muchos registros de inventario.
        // Se trae con $expand=Inventarios
        public List<Inventario> Inventarios { get; set; } = new();
    }
}
