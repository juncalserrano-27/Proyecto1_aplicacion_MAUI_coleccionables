using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data.DTOs;
using RepasoMAUI.Services;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class ApiViewModel : ObservableObject
    {
        // Antes: ProductoApiService con new HttpClient() hacia fakestoreapi.com
        // Ahora: cliente tipado (AddHttpClient<T>) hacia nuestro servicio OData
        private readonly TiendaODataService _servicio;

        [ObservableProperty]
        private ObservableCollection<ProductoDto> productos = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public ApiViewModel(TiendaODataService servicio)
        {
            _servicio = servicio;
        }

        [RelayCommand]
        private async Task CargarProductos()
        {
            IsLoading = true;
            HasError = false;

            var (resultado, error) = await _servicio.ObtenerProductosAsync();

            if (error is not null)
            {
                HasError = true;
                ErrorMessage = error;
            }
            else
            {
                Productos = new ObservableCollection<ProductoDto>(resultado);
            }

            IsLoading = false;
        }
    }
}
