using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class ApiPage : ContentPage
    {
        private readonly ApiViewModel _viewModel;

        public ApiPage(ApiViewModel vm)
        {
            InitializeComponent();
            BindingContext = _viewModel = vm;
        }

        // Cada vez que se muestra, recarga del servicio (para ver los productos nuevos)
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.CargarProductosCommand.Execute(null);
        }
    }
}
