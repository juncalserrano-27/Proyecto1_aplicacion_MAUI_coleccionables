using RepasoMAUI.Models;
using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class ListaPage : ContentPage
    {
        private readonly ListaViewModel _viewModel;

        public ListaPage(ListaViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        // Cada vez que se muestra la lista, la recargamos (para ver altas y bajas)
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.CargarProductos();
        }

        // Cuando marcas/desmarcas un checkbox, seleccionamos ese producto
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox &&
                checkBox.BindingContext is Producto producto)
            {
                _viewModel.SeleccionarProductoCommand.Execute(producto);
            }
        }
    }
}
