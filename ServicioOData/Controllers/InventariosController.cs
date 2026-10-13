using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ServicioOData.Data;
using ServicioOData.Models;

namespace ServicioOData.Controllers
{
    public class InventariosController : ODataController
    {
        private readonly TiendaContext _db;

        public InventariosController(TiendaContext db)
        {
            _db = db;
        }

        // GET /odata/Inventarios?$filter=Stock eq 0&$expand=Producto
        [EnableQuery]
        public IQueryable<Inventario> Get()
            => _db.Inventarios;
    }
}
