using System.Text.Json.Serialization;

namespace RepasoMAUI.Data.DTOs
{
    // Wrapper genérico: toda colección OData llega como
    // { "@odata.context": "...", "@odata.count": 5, "value": [ ... ] }
    public class ODataResponse<T>
    {
        [JsonPropertyName("@odata.count")]
        public int? Count { get; set; }

        [JsonPropertyName("value")]
        public List<T> Value { get; set; } = new();
    }
}
