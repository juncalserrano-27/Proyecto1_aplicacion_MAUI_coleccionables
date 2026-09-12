using System.Text.Json.Serialization;

namespace RepasoMAUI.Data.DTOs
{
    // Molde que coincide con el JSON de TheSportsDB (searchplayers.php).
    // La API responde { "player": [ ... ] }, por eso usamos un envoltorio.
    public class TenisRespuestaDto
    {
        [JsonPropertyName("player")]
        public List<TenisApiDto> Player { get; set; } = new();
    }

    // Cada tenista dentro de la lista "player".
    public class TenisApiDto
    {
        [JsonPropertyName("idPlayer")]
        public string IdPlayer { get; set; } = string.Empty;

        [JsonPropertyName("strPlayer")]
        public string StrPlayer { get; set; } = string.Empty;

        [JsonPropertyName("strSport")]
        public string StrSport { get; set; } = string.Empty;

        [JsonPropertyName("strNationality")]
        public string StrNationality { get; set; } = string.Empty;

        [JsonPropertyName("strThumb")]
        public string StrThumb { get; set; } = string.Empty;
    }
}
