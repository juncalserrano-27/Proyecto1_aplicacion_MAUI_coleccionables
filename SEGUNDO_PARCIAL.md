# Proyecto – Segundo Parcial

La app del primer parcial (catálogo de tenis) evolucionó: **ya no depende de una API externa**. Ahora tiene su **propio servicio OData en ASP.NET Core** y la app **MAUI Blazor Hybrid** lo consume.

```
RepasoMAUI.slnx
├── ServicioOData/      → API OData (ASP.NET Core + EF Core + SQLite en memoria)
└── RepasoMAUI.csproj   → App MAUI Blazor Hybrid (cliente)
```

## Cómo correrlo

1. **Primero el servicio:**
   ```bash
   cd ServicioOData
   dotnet run --launch-profile http
   ```
   Queda en `http://localhost:5080/odata/`. Para probarlo, abre en el navegador:
   - `http://localhost:5080/odata/Productos?$expand=Inventarios`
   - `http://localhost:5080/odata/Productos?$filter=contains(Modelo,'Cur')`
   - `http://localhost:5080/odata/$metadata`
2. **Después la app** (Windows o emulador de Android) desde Visual Studio.
   - El emulador de Android ve la computadora como `10.0.2.2`; eso ya está configurado en `MauiProgram.UrlServicioOData`.
   - En Visual Studio se pueden arrancar los dos al mismo tiempo: clic derecho en la solución → *Configurar proyectos de inicio* → *Varios proyectos de inicio*.

> La base de datos es SQLite **en memoria**: cada vez que reinicias el servicio, regresan los datos iniciales.

---

## Requisitos → dónde está cada uno

### 1. Servicio OData propio (ASP.NET Core)
| Pide | Dónde |
|---|---|
| Base de datos propia (SQLite en memoria) | `ServicioOData/Program.cs`: `SqliteConnection("DataSource=:memory:")` |
| 2 entidades con relación uno-a-muchos | `Models/Producto.cs` (1) → `Models/Inventario.cs` (muchos: talla y stock) |
| Navigation Property | `Producto.Inventarios` e `Inventario.Producto`, configuradas en `Data/TiendaContext.cs` |
| Endpoints OData con `$expand` y `$filter` | `Controllers/ProductosController.cs` con `[EnableQuery]`; en `Program.cs` se habilitan `.Select().Filter().Expand().OrderBy().Count()` |

### 2. Actualización del cliente MAUI
| Pide | Dónde |
|---|---|
| Servicio tipado con `AddHttpClient<T>` | `MauiProgram.cs` → `AddHttpClient<TiendaODataService>(...)`; el servicio recibe el `HttpClient` por constructor (`Services/TiendaODataService.cs`) |
| Ya no se usa la API pública | Se eliminaron `ProductoApiService` (fakestoreapi.com) y Northwind |
| DTOs | `Data/DTOs/ProductoDto.cs` (`ProductoDto`, `InventarioDto`, `NuevoProductoDto`) |
| Wrapper `ODataResponse<T>` | `Data/DTOs/ODataResponse.cs` (lee `value` y `@odata.count`) |
| 2+ propiedades calculadas que dependen de la entidad relacionada | `ProductoDto.StockTotal` (suma del stock de todas las tallas) y `ProductoDto.TallasDisponibles` (tallas con stock > 0). De ahí sale también `Estado` (Disponible / Pocas piezas / Agotado) |

### 3. Master-detail con Cascading Parameters
| Pide | Dónde |
|---|---|
| Pantalla principal con la lista | `Components/Pages/Catalogo.razor` (ruta `/`) |
| Al seleccionar, un panel muestra el detalle con los registros relacionados | `Components/Shared/PanelDetalle.razor`: stock total, tallas disponibles y tabla de inventario por talla |
| El seleccionado llega con `CascadingValue` y `[CascadingParameter(Name="...")]` | `Catalogo.razor`: `<CascadingValue Value="seleccionado" Name="ProductoSeleccionado">` → `PanelDetalle.razor`: `[CascadingParameter(Name = "ProductoSeleccionado")]` |

### 4. Navegación con rutas parametrizadas
| Pide | Dónde |
|---|---|
| `@page "/ruta/{Id:int}"` | `Components/Pages/Detalle.razor` → `@page "/producto/{Id:int}"` |
| Carga en `OnParametersSetAsync` (no en `OnInitializedAsync`) | `Detalle.razor`: se vuelve a ejecutar cada vez que cambia el `Id` |
| Navegación de ida y regreso | Ida: botón "Ver detalle completo" del panel (`NavigationManager.NavigateTo`). Regreso: botón "Regresar" del Footer Toolbar |

### 5. Formulario de alta + diseño Fiori
| Pide | Dónde |
|---|---|
| `EditForm`, `OnValidSubmit`, `DataAnnotationsValidator` | `Components/Pages/Agregar.razor` |
| 3+ validaciones con mensaje por campo | `[Required]` y `[StringLength]` en Modelo, `[Required]` y `[StringLength]` en Color, `[Range]` en Precio y `[Required]` en Imagen, cada uno con su `<ValidationMessage>` |
| Guarda en el servicio | `TiendaODataService.CrearProductoAsync` → `POST /odata/Productos` |
| Variables CSS de Fiori | `wwwroot/css/app.css` → `--sapBrandColor`, `--sapTextColor`, etc. |
| Shell Bar | `Components/Layout/MainLayout.razor` (título + Icon Tab Bar) |
| Chips de estado | Clase `.chip` (positivo / crítico / negativo) en la lista, el panel, el detalle y el catálogo XAML |
| Tipos de botón correctos | Emphasized (`.btn`) para la acción principal, Default (`.btn-sec`) para Cancelar/Regresar y Transparent (`.btn-transparente`) para "Ver favoritos" |

---

## Guion para el video (3–6 min)

1. **Antes (1 min):** corre la rama `main` (la app del 1er parcial) y muestra el catálogo que venía de fakestoreapi.com, sin validaciones y con el diseño básico.
2. **El servicio (1 min):** corre `ServicioOData`, abre en el navegador `Productos?$expand=Inventarios` y una URL con `$filter`. Explica las 2 entidades y la Navigation Property.
3. **Flujo completo (2 min):**
   - **Lista** con chips de estado.
   - **Master-detail:** toca un producto y explica que el panel lo recibe por `CascadingParameter`.
   - **"Ver detalle completo":** ruta `/producto/{Id:int}` cargada en `OnParametersSetAsync`.
   - **"Regresar".**
4. **Validación fallida (30 s):** en Agregar, dale Guardar con los campos vacíos o con 1 letra en Modelo, y muestra los mensajes por campo. Después llénalo bien, guárdalo y muestra que aparece en la lista.
5. **Cambios (1 min):** `AddHttpClient<T>`, `ODataResponse<T>`, las propiedades calculadas `StockTotal` y `TallasDisponibles`, y Fiori (variables CSS, Shell Bar, chips, botones).
