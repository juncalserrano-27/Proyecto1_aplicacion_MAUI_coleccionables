using Microsoft.Extensions.Logging;
using RepasoMAUI.Data;
using RepasoMAUI.Services;
using RepasoMAUI.ViewModels;
using RepasoMAUI.Views;

namespace RepasoMAUI
{
    public static class MauiProgram
    {
        // Dirección del servicio OData (ServicioOData corre en el puerto 5080).
        // El emulador de Android ve a la computadora como 10.0.2.2 en lugar de localhost.
        public static string UrlServicioOData =>
            DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5080/odata/"
                : "http://localhost:5080/odata/";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    // Fuente oficial de SAP Fiori ("72")
                    fonts.AddFont("72-Regular.ttf", "Fiori72");
                    fonts.AddFont("72-Bold.ttf", "Fiori72Bold");
                    // Íconos oficiales de SAP (SAP-icons, tema Horizon)
                    fonts.AddFont("SAP-icons.ttf", "SAPIcons");
                });

            // Quitamos el borde/subrayado nativo del Entry: el borde de Fiori
            // lo dibuja el Border con el estilo "FioriCampo"
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SinBordeNativo", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif WINDOWS
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#elif IOS || MACCATALYST
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });

            // ---- Servicio OData propio (proyecto ServicioOData) ----
            // Cliente tipado: AddHttpClient<T> crea y configura el HttpClient
            // y se lo pasa por constructor a TiendaODataService.
            builder.Services.AddHttpClient<TiendaODataService>(cliente =>
            {
                cliente.BaseAddress = new Uri(UrlServicioOData);
                cliente.Timeout = TimeSpan.FromSeconds(8);
            });

            // Favoritos (guardados en memoria) — Singleton: compartido por XAML y Razor
            builder.Services.AddSingleton<ProductoRepository>();
            builder.Services.AddTransient<FavoritosViewModel>();
            builder.Services.AddTransient<FavoritosPage>();

            // Catálogo nativo (XAML + MVVM) que también consume el servicio OData
            builder.Services.AddTransient<ApiViewModel>();
            builder.Services.AddTransient<ApiPage>();

            // ---- Blazor Hybrid ----
            // Habilita el BlazorWebView (los componentes Razor corren dentro de la app nativa)
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddTransient<BlazorPage>();

            // Navegación de Razor -> página XAML nativa
            builder.Services.AddSingleton<INavegacionService, NavegacionService>();

            // Ciclos de vida de servicios (service lifetimes):
            //  - Singleton: una instancia para toda la app (ProductoRepository es compartido
            //    por XAML y Razor, por eso un favorito agregado en Blazor aparece en la página XAML).
            //  - Scoped: en Blazor Hybrid dura lo que vive el BlazorWebView.
            //  - Transient: una instancia nueva cada vez que se pide (así registra AddHttpClient al servicio).

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
