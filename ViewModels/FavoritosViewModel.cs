using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel: ObservableObject
    {
        private readonly ProductoRepository _repo;

        [ObservableProperty]
        private ObservableCollection<Producto> favoritos;

        private List<Producto> seleccionados = new();

        [ObservableProperty]
        private bool modoEliminar;

        public FavoritosViewModel(ProductoRepository repo)
        {
            _repo = repo;

            Favoritos = new ObservableCollection<Producto>();
        }

        public void CargarFavoritos()
        {
            Favoritos.Clear();

            foreach (var producto in _repo.ObtenerFavoritos())
            {
                Favoritos.Add(producto);
            }

            seleccionados.Clear();
            ModoEliminar = false;
        }

        [RelayCommand]
        private void ActivarEliminar()
        {
            if (ModoEliminar)
            {
                EliminarSeleccionados();
            }
            else
            {
                ModoEliminar = true;
            }
        }

        [RelayCommand]
        private void SeleccionarProducto(Producto producto)
        {
            if (producto == null)
                return;

            if (seleccionados.Any(p => p.Id == producto.Id))
            {
                seleccionados.RemoveAll(p => p.Id == producto.Id);
            }
            else
            {
                seleccionados.Add(producto);
            }
        }

        private void EliminarSeleccionados()
        {
            if (seleccionados.Count == 0)
            {
                ModoEliminar = false;
                return;
            }

            _repo.EliminarFavoritos(seleccionados);

            foreach (var producto in seleccionados.ToList())
            {
                Favoritos.Remove(producto);
            }

            seleccionados.Clear();

            ModoEliminar = false;
        }


    }
}
