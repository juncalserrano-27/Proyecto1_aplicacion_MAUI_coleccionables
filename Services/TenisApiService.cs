using RepasoMAUI.Data.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace RepasoMAUI.Services
{
    // Servicio que busca tenistas en TheSportsDB usando HttpClient
    public class TenisApiService
    {
        private readonly HttpClient _http;

        public TenisApiService()
        {
            _http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        // Busca tenistas por nombre. Regresa (la lista, un mensaje de error).
        // Si error es null, todo salió bien.
        public async Task<(List<TenisApiDto> tenistas, string error)> BuscarTenistasAsync(
            string nombre = "Novak Djokovic")
        {
            try
            {
                // 3 = api key de prueba (gratis) de TheSportsDB
                var url = "https://www.thesportsdb.com/api/v1/json/3/searchplayers.php?p="
                          + Uri.EscapeDataString(nombre);

                var respuesta = await _http.GetFromJsonAsync<TenisRespuestaDto>(url);

                // La API regresa jugadores de TODOS los deportes; nos quedamos solo con tenis.
                // Si tu búsqueda sale vacía, quita el .Where(...) para ver todos los resultados.
                var tenistas = respuesta?.Player?
                    .Where(p => p.StrSport == "Tennis")
                    .ToList() ?? new List<TenisApiDto>();

                return (tenistas, null);
            }
            catch (TaskCanceledException)
            {
                return (new List<TenisApiDto>(), "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.");
            }
            catch (HttpRequestException ex)
            {
                return (new List<TenisApiDto>(), $"No se pudo conectar al servidor ({ex.StatusCode}).");
            }
            catch (JsonException)
            {
                return (new List<TenisApiDto>(), "La respuesta del servidor no se pudo interpretar.");
            }
        }
    }
}
