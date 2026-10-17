# Presentación: SAP Fiori en la app (para exentar el 3er parcial)

**Duración:** unos 5 minutos.
**Qué tener abierto:** la app (acceso directo "Tienda de Tenis") y Visual Studio con este proyecto.

**De dónde salió cada cosa:**
- **Las reglas** (colores, tipografía, layouts, componentes): la página oficial **SAP Fiori Design Guidelines** → https://experience.sap.com/fiori-design-web/
- **Los recursos oficiales** (valores de color del tema Horizon, fuente "72" e íconos SAP-icons): el paquete de SAP **`@sap-theming/theming-base-content`**, que es el que usa SAP para sus propias apps.
- **Los componentes** (Shell Bar, chips, Object Page, etc.): los implementamos nosotros con **CSS en Blazor** y **estilos en XAML**, siguiendo esas guías. No usamos la librería UI5 Web Components.

---

## 1. Qué es Fiori y por qué (30 s)
**Enseñas:** la app en la pestaña **Tienda**.

> "Fiori es el sistema de diseño de SAP: define cómo se ven y cómo se acomodan todas sus apps, para que en una empresa todas se usen igual. Lo aplicamos a toda nuestra app, tanto a las pantallas en Blazor como a las nativas en XAML."

## 2. Estilo: colores (1 min)
**Enseñas:** `Resources/Styles/Colors.xaml` y luego `wwwroot/css/app.css` (las líneas `--sap...` de arriba).

> "Los colores no los inventamos: son los valores oficiales del tema Horizon de SAP. Los saqué del archivo de variables de SAP y les dejé su nombre oficial, por ejemplo `sapBrandColor` o `sapTextColor`.
> En XAML están como recursos de color y en Blazor como variables CSS, con los mismos valores, para que las dos partes se vean como una sola app.
> Los colores de estado, verde, naranja y rojo, solo se usan para comunicar algo, como lo pide Fiori."

## 3. Estilo: tipografía e íconos (45 s)
**Enseñas:** la carpeta `Resources/Fonts` (`72-Regular.ttf`, `72-Bold.ttf`, `SAP-icons.ttf`) y `Components/Shared/Icono.razor`.

> "La letra es **72**, la tipografía oficial de SAP. Y los íconos son los **SAP-icons** oficiales: el carrito, la lista, la lupa, el corazón y los de estado.
> En Blazor hice un componente `Icono` donde escribes el nombre del ícono de SAP, por ejemplo `search`, y lo dibuja con la fuente oficial."

**En la app:** señala los íconos de las pestañas, la lupa y los chips.

## 4. Layout: los patrones de pantalla (1 min 30 s)
**Enseñas:** la app, pantalla por pantalla.

| Lo que enseñas | Patrón de Fiori | Qué dices |
|---|---|---|
| La barra de arriba con "Tienda de Tenis" y las pestañas Catálogo / Agregar | **Shell Bar** + **Icon Tab Bar** | "Arriba va la Shell Bar con el título, y las pestañas con el subrayado azul en la activa." |
| La lista con "Productos (4)" dentro de una tarjeta | **List Report** | "Las listas llevan un título con el contador y van dentro de una tarjeta blanca." |
| Cada renglón: imagen, nombre, color, precio y flechita | **Standard List Item** + **Object Number** | "Cada renglón sigue el formato estándar: imagen, título, descripción en gris, y el precio en negritas con la unidad en gris." |
| Clic en un tenis → el panel con la tabla de tallas | **Master-detail** + **Responsive Table** | "Al seleccionar se abre el detalle con una tabla." |
| "Ver detalle completo" | **Object Page** | "El detalle es un Object Page: encabezado con el producto y secciones abajo." |
| Los botones de abajo | **Footer Toolbar** | "Las acciones van abajo a la derecha." |

**Código que puedes enseñar:** `Components/Layout/MainLayout.razor` (la Shell Bar) y `Components/Pages/Detalle.razor` (el Object Page con su footer).

## 5. Componentes (1 min)
**Enseñas:** la app.

- **Chips de estado** (Disponible / Pocas piezas / Agotado) → "Es el **Object Status** de Fiori: color e ícono según el estado."
- **Botones** en el detalle → "Fiori tiene tipos de botón: **Emphasized**, el azul, para la acción principal, que solo es una por pantalla; **Default**, blanco con borde, como Regresar; y **Transparent**, como Ver favoritos."
- **Agregar**, dale Guardar con los campos vacíos → "El formulario sigue Fiori: etiqueta arriba del campo, asterisco en los obligatorios y el error debajo de cada campo."
- **Favoritos** vacío → "Cuando no hay datos se usa un **Illustrated Message**: un ícono, un título y qué hacer."

**Código que puedes enseñar:** en `wwwroot/css/app.css`, busca `.chip` y `.btn`.

## 6. Cierre (15 s)
> "En resumen: seguimos las SAP Fiori Design Guidelines, usamos los colores, la fuente y los íconos oficiales de SAP, y armamos con CSS y XAML los patrones de Fiori en toda la app."
