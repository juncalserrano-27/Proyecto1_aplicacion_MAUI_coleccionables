using RepasoMAUI.Data.DTOs;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RepasoMAUI.Services
{
    // Cliente tipado: el HttpClient lo crea y configura AddHttpClient<TiendaODataService>
    // en MauiProgram (ya no hacemos "new HttpClient()" aquí)
    public class TiendaODataService
    {
        private readonly HttpClient _http;

        public TiendaODataService(HttpClient http)
        {
            _http = http;
        }

        // GET Productos?$expand=Inventarios&$orderby=Modelo[&$filter=contains(...)]
        public async Task<(List<ProductoDto> productos, string? error)> ObtenerProductosAsync(string? busqueda = null)
        {
            var url = "Productos?$expand=Inventarios&$orderby=Modelo";

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                // $filter: el servidor regresa solo los que contienen el texto
                var texto = busqueda.Trim().ToLower().Replace("'", "''");
                url += "&$filter=" + Uri.EscapeDataString($"contains(tolower(Modelo),'{texto}')");
            }

            try
            {
                var respuesta = await _http.GetFromJsonAsync<ODataResponse<ProductoDto>>(url);
                return (respuesta?.Value ?? new List<ProductoDto>(), null);
            }
            catch (Exception ex)
            {
                return (new List<ProductoDto>(), Mensaje(ex));
            }
        }

        // GET Productos(5)?$expand=Inventarios
        public async Task<(ProductoDto? producto, string? error)> ObtenerProductoAsync(int id)
        {
            try
            {
                var producto = await _http.GetFromJsonAsync<ProductoDto>($"Productos({id})?$expand=Inventarios");
                return (producto, null);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return (null, "El producto no existe.");
            }
            catch (Exception ex)
            {
                return (null, Mensaje(ex));
            }
        }

        // POST Productos
        public async Task<string?> CrearProductoAsync(NuevoProductoDto nuevo)
        {
            try
            {
                var respuesta = await _http.PostAsJsonAsync("Productos", nuevo);
                return respuesta.IsSuccessStatusCode
                    ? null
                    : $"El servidor rechazó el producto ({(int)respuesta.StatusCode}).";
            }
            catch (Exception ex)
            {
                return Mensaje(ex);
            }
        }

        private static string Mensaje(Exception ex) => ex switch
        {
            TaskCanceledException => "La petición tardó demasiado. ¿Está corriendo el servicio OData?",
            HttpRequestException => "No se pudo conectar al servicio OData. ¿Está corriendo?",
            JsonException => "La respuesta del servidor no se pudo interpretar.",
            _ => "Ocurrió un error inesperado."
        };
    }
}
