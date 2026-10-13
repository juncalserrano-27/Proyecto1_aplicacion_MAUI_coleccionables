using Microsoft.AspNetCore.OData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ServicioOData.Data;
using ServicioOData.Models;

var builder = WebApplication.CreateBuilder(args);

// Base de datos SQLite en memoria: la conexión se deja abierta
// para que los datos vivan mientras el servicio esté corriendo
var conexion = new SqliteConnection("DataSource=:memory:");
conexion.Open();
builder.Services.AddDbContext<TiendaContext>(options => options.UseSqlite(conexion));

// Modelo EDM: qué entidades expone el servicio OData
var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<Producto>("Productos");
odataBuilder.EntitySet<Inventario>("Inventarios");

builder.Services.AddControllers().AddOData(options => options
    .Select().Filter().Expand().OrderBy().Count().SetMaxTop(100)
    .AddRouteComponents("odata", odataBuilder.GetEdmModel()));

var app = builder.Build();

// Crea las tablas y carga los datos iniciales (HasData)
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<TiendaContext>().Database.EnsureCreated();
}

app.MapControllers();
app.Run();
