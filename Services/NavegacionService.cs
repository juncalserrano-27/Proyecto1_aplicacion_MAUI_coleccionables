namespace RepasoMAUI.Services
{
    // Permite que un componente Razor abra una página XAML nativa
    public interface INavegacionService
    {
        Task IrAAsync(string ruta);
    }

    public class NavegacionService : INavegacionService
    {
        // Shell.Current es el AppShell nativo; GoToAsync empuja la página XAML registrada.
        // Se manda al hilo principal porque Blazor puede llamar desde otro hilo.
        public Task IrAAsync(string ruta)
            => MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(ruta));
    }
}
