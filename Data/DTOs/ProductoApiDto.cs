namespace RepasoMAUI.Data.DTOs
{
    // Molde que coincide con el JSON que devuelve fakestoreapi.com/products
    public class ProductoApiDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
