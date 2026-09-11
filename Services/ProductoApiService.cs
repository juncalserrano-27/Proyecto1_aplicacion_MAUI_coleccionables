using RepasoMAUI.Data.DTOs;
using RepasoMAUI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace RepasoMAUI.Services
{
    // Servicio que baja productos de una API por internet usando HttpClient
    public class ProductoApiService
    {
        private readonly HttpClient _http;

        public ProductoApiService()
        {
            _http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        // Regresa (la lista de productos, un mensaje de error). Si error es null, todo salió bien.
        public async Task<(List<Producto> productos, string error)> ObtenerProductosAsync(
            string url = "https://fakestoreapi.com/products")
        {
            try
            {
                var dtos = await _http.GetFromJsonAsync<List<ProductoApiDto>>(url);

                // Traducimos el DTO de la API a nuestro modelo Producto
                var productos = dtos?.Select(d => new Producto
                {
                    Id = d.Id.ToString(),
                    Modelo = d.Title,
                    Color = d.Category,
                    Precio = d.Price,
                    ImagenUrl = d.Image
                }).ToList() ?? new List<Producto>();

                return (productos, null);
            }
            catch (TaskCanceledException)
            {
                return (new List<Producto>(), "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.");
            }
            catch (HttpRequestException ex)
            {
                return (new List<Producto>(), $"No se pudo conectar al servidor ({ex.StatusCode}).");
            }
            catch (JsonException)
            {
                return (new List<Producto>(), "La respuesta del servidor no se pudo interpretar.");
            }
        }
    }
}
