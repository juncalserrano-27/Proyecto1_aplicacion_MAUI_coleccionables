using Microsoft.Extensions.DependencyInjection;

namespace RepasoMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Usamos el tema claro de Fiori (Horizon) en toda la app
            UserAppTheme = AppTheme.Light;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}