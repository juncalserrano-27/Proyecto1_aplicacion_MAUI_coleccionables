using Microsoft.Extensions.Logging;
using RepasoMAUI.Data;
using RepasoMAUI.Services;
using RepasoMAUI.ViewModels;
using RepasoMAUI.Views;

namespace RepasoMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Repositorio — Singleton: una sola instancia para toda la app
            builder.Services.AddSingleton<ProductoRepository>();

            // Lista
            builder.Services.AddTransient<ListaViewModel>();
            builder.Services.AddTransient<ListaPage>();

            // Detalle
            builder.Services.AddTransient<DetalleViewModel>();
            builder.Services.AddTransient<DetallePage>();

            // Favoritos
            builder.Services.AddTransient<FavoritosViewModel>();
            builder.Services.AddTransient<FavoritosPage>();

            // Formulario (Agregar Productos)
            builder.Services.AddTransient<FormularioViewModel>();
            builder.Services.AddTransient<FormularioPage>();

            // Catálogo Online (HttpClient / API)
            builder.Services.AddSingleton<ProductoApiService>();
            builder.Services.AddTransient<ApiViewModel>();
            builder.Services.AddTransient<ApiPage>();

            // ---- 2do parcial: Blazor Hybrid ----
            // Habilita el BlazorWebView (los componentes Razor corren dentro de la app nativa)
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddTransient<BlazorPage>();

            // Navegación de Razor -> página XAML nativa
            builder.Services.AddSingleton<INavegacionService, NavegacionService>();

            // Servicio OData (Northwind)
            builder.Services.AddSingleton<NorthwindService>();

            // Ciclos de vida de servicios (service lifetimes):
            //  - Singleton: una instancia para toda la app (ProductoRepository es compartido
            //    por XAML y Razor, por eso un favorito agregado en Blazor aparece en la página XAML).
            //  - Scoped: en Blazor Hybrid dura lo que vive el BlazorWebView.
            //  - Transient: una instancia nueva cada vez que se pide.

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
