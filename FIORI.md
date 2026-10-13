# SAP Fiori en RepasoMAUI

Aplicamos las **SAP Fiori Design Guidelines** (tema **Horizon**) a toda la app: a las pantallas nativas en **XAML** y a la parte **Blazor** (HTML/CSS). Las dos usan los mismos colores, la misma fuente y los mismos patrones, así que se ven como una sola app.

Referencia: https://experience.sap.com/fiori-design-web/

---

## 1. Qué usamos para lograrlo

| Herramienta | Parte de la app | Para qué |
|---|---|---|
| `Resources/Styles/Colors.xaml` | XAML | Los colores oficiales de Fiori Horizon (los *theming parameters* de SAP) |
| `Resources/Styles/Styles.xaml` | XAML | Estilos implícitos (se aplican solos) y estilos con `x:Key` (tarjeta, footer, botones, etc.) |
| `Resources/Fonts/72-*.ttf` | XAML | La fuente oficial de SAP, **"72"** |
| `wwwroot/css/app.css` | Blazor | Los mismos colores como variables CSS (`--sapBrandColor`, etc.) y los mismos patrones |
| `wwwroot/fonts/72-*.woff2` | Blazor | La fuente "72" cargada con `@font-face` |
| `EntryHandler.Mapper` (en `MauiProgram.cs`) | XAML | Quita el borde nativo del `Entry` para dibujar el borde de Fiori |

La fuente "72" sale del paquete oficial de SAP `@sap-theming/theming-base-content` (licencia Apache 2.0).

---

## 2. Fundamentos de Fiori que aplicamos

### Color
Usamos los valores oficiales del tema Horizon, con el nombre del parámetro de SAP en un comentario:

| Uso | Color | Parámetro SAP |
|---|---|---|
| Marca / botón principal | `#0070F2` | `sapBrandColor` |
| Fondo de la página | `#F5F6F7` | `sapBackgroundColor` |
| Barra superior (Shell) y tarjetas | `#FFFFFF` | `sapShellColor`, `sapTile_Background` |
| Texto principal | `#1D2D3E` | `sapTextColor` |
| Texto secundario / etiquetas | `#556B82` | `sapContent_LabelColor` |
| Separadores | `#E5E5E5` | `sapList_BorderColor` |
| Error | `#AA0808` sobre `#FFEAF4` | `sapNegativeColor`, `sapErrorBackground` |
| Éxito | `#256F3A` | `sapPositiveColor` |

Regla de Fiori: **los colores semánticos (rojo, verde) solo se usan para comunicar un estado**, no para decorar. Por eso el precio ya no es verde: ahora es texto normal en negritas, y el verde solo aparece al confirmar "Agregado a favoritos".

### Tipografía
- Fuente **"72"** en toda la app.
- Jerarquía: título de página → título de sección (16, negritas) → texto (14) → texto secundario (gris).

### Espaciado y forma
- Márgenes en múltiplos de 8 px (8, 12, 16).
- Esquinas redondeadas de Horizon: **12 px** en tarjetas, **8 px** en botones e imágenes y **4 px** en campos de texto.

### Tema
- Tema claro de Horizon (`UserAppTheme = AppTheme.Light` en `App.xaml.cs`).

---

## 3. Patrones (layout) de Fiori por pantalla

| Patrón Fiori | Dónde está | Cómo se ve |
|---|---|---|
| **Shell Bar** | Barra superior de toda la app | Blanca, con sombra y el título oscuro |
| **Icon Tab Bar** | Pestaña Blazor (`MainLayout.razor`) | Pestañas con subrayado azul en la activa |
| **List Report** | Catálogo (Blazor), Catálogo XAML, Favoritos | Título con contador "Productos (4)" + lista dentro de una tarjeta |
| **Standard List Item** | Cada producto de las listas | Imagen (avatar) + título + descripción gris + precio a la derecha + flecha `›` si abre un detalle |
| **Object Number** | Precios | Cifra en negritas + unidad en gris (`$2,499.00 MXN`) |
| **Object Page** | Detalle (Blazor) | Encabezado del objeto (imagen, nombre, precio) + sección "Información general" |
| **Footer Toolbar** | Detalle y Agregar (Blazor) | Barra inferior con las acciones a la derecha; la principal en azul |
| **Botones Emphasized / Default** | Todos | Una sola acción principal azul por pantalla; las demás en blanco con borde |
| **Formulario** | Agregar (Blazor) | Etiqueta arriba del campo (móvil), obligatorios con `*` rojo, campos con borde gris |
| **Message Strip** | Errores del formulario y del servicio OData | Recuadro rosa con borde rojo y el mensaje; en las APIs con botón "Reintentar" |
| **Object Status (chips)** | Lista, panel, detalle y Catálogo XAML | Disponible (verde), Pocas piezas (naranja), Agotado (rojo) |
| **Illustrated Message** | Favoritos vacío, búsqueda sin resultados | Ícono de línea + título + explicación corta |
| **Busy Indicator** | Catálogo (Blazor) y Catálogo XAML | Indicador mientras cargan los datos |

---

## 4. Archivos que cambiaron

**XAML (nativo)**
- `Resources/Styles/Colors.xaml` y `Styles.xaml`: paleta y estilos de Fiori.
- `Views/ApiPage.xaml` (Catálogo XAML) y `FavoritosPage.xaml`: List Report + Standard List Item + chips de estado.
- `MauiProgram.cs`: registro de la fuente 72 y el handler del `Entry`.
- `App.xaml.cs`: tema claro.
- `Platforms/Android/Resources/values/colors.xml` y `RepasoMAUI.csproj`: color azul de Fiori en Android, el ícono y el splash.

**Blazor (HTML/CSS)**
- `wwwroot/css/app.css`: tokens de Fiori, fuente 72 y todos los patrones.
- `Components/Layout/MainLayout.razor`: Icon Tab Bar.
- `Components/Shared/ProductoCard.razor`: Standard List Item.
- `Components/Pages/*.razor`: List Report, Object Page, formulario y Message Strip.
