using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using RepasoMAUI.Services;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class ApiViewModel : ObservableObject
    {
        private readonly ProductoApiService _api;

        [ObservableProperty]
        private ObservableCollection<Producto> productos = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string errorMessage;

        public ApiViewModel(ProductoApiService api)
        {
            _api = api;
            _ = CargarProductos();   // pedir los productos al arrancar la página
        }

        [RelayCommand]
        private async Task CargarProductos()
        {
            IsLoading = true;
            HasError = false;

            var (resultado, error) = await _api.ObtenerProductosAsync();

            if (error is not null)
            {
                HasError = true;
                ErrorMessage = error;
            }
            else
            {
                Productos = new ObservableCollection<Producto>(resultado);
            }

            IsLoading = false;
        }
    }
}
