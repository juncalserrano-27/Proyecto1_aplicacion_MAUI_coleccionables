using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ServicioOData.Data;
using ServicioOData.Models;

namespace ServicioOData.Controllers
{
    // ODataController: las rutas quedan como /odata/Productos y /odata/Productos(1)
    public class ProductosController : ODataController
    {
        private readonly TiendaContext _db;

        public ProductosController(TiendaContext db)
        {
            _db = db;
        }

        // GET /odata/Productos?$expand=Inventarios&$filter=contains(Modelo,'cur')
        // [EnableQuery] aplica $filter, $expand, $orderby, $select... sobre la consulta
        [EnableQuery]
        public IQueryable<Producto> Get()
            => _db.Productos;

        // GET /odata/Productos(1)?$expand=Inventarios
        [EnableQuery]
        public SingleResult<Producto> Get([FromRoute] int key)
            => SingleResult.Create(_db.Productos.Where(p => p.Id == key));

        // POST /odata/Productos  (alta desde el formulario de la app)
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Productos.Add(producto);
            await _db.SaveChangesAsync();
            return Created(producto);
        }
    }
}
