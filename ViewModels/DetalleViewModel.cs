using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        public DetalleViewModel(ProductoRepository repo)
        {
            _repo = repo;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
        }

        [RelayCommand]
        private void AgregarFavorito()
        {
            if (Producto == null)
                return;

            _repo.AgregarFavorito(Producto);
        }

        [RelayCommand]
        static async Task IrAFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }

    }
}
