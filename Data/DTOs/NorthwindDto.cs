using System.Text.Json.Serialization;

namespace RepasoMAUI.Data.DTOs
{
    // Un servicio OData responde { "@odata.context": "...", "value": [ ... ] }
    public class NorthwindRespuestaDto
    {
        [JsonPropertyName("value")]
        public List<NorthwindProductoDto> Value { get; set; } = new();
    }

    // Cada producto dentro de "value"
    public class NorthwindProductoDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }
}
