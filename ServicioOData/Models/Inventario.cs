namespace ServicioOData.Models
{
    // Entidad relacionada: cuántos pares hay de un producto en cada talla (lado "muchos")
    public class Inventario
    {
        public int Id { get; set; }

        public decimal Talla { get; set; }   // talla en cm (México), ej. 26.5

        public int Stock { get; set; }

        // Llave foránea + Navigation Property hacia el producto
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
    }
}
