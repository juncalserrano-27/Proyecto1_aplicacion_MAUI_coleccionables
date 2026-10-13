using RepasoMAUI.Data.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace RepasoMAUI.Services
{
    // Servicio que consume un servicio OData (el mismo tipo de servicio que expone SAP)
    public class NorthwindService
    {
        private readonly HttpClient _http;

        public NorthwindService()
        {
            _http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        public async Task<(List<NorthwindProductoDto> productos, string? error)> ObtenerProductosAsync()
        {
            // Opciones de consulta OData (van en la URL):
            //  $select  -> solo estos campos
            //  $filter  -> precio mayor (gt) a 20
            //  $orderby -> del más caro al más barato
            //  $top     -> solo 10 registros
            var url = "https://services.odata.org/V4/Northwind/Northwind.svc/Products"
                    + "?$select=ProductID,ProductName,UnitPrice,UnitsInStock"
                    + "&$filter=UnitPrice gt 20"
                    + "&$orderby=UnitPrice desc"
                    + "&$top=10";

            try
            {
                var respuesta = await _http.GetFromJsonAsync<NorthwindRespuestaDto>(url);
                return (respuesta?.Value ?? new List<NorthwindProductoDto>(), null);
            }
            catch (TaskCanceledException)
            {
                return (new List<NorthwindProductoDto>(), "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.");
            }
            catch (HttpRequestException ex)
            {
                return (new List<NorthwindProductoDto>(), $"No se pudo conectar al servidor ({ex.StatusCode}).");
            }
            catch (JsonException)
            {
                return (new List<NorthwindProductoDto>(), "La respuesta del servidor no se pudo interpretar.");
            }
        }
    }
}
