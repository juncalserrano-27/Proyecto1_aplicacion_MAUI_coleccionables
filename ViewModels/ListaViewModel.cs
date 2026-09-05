using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;

        [ObservableProperty]
        private ObservableCollection<Producto> productos;

        // Productos que el usuario seleccionó para eliminar
        private List<Producto> seleccionados = new();

        // Indica si estamos en modo eliminar (muestra los checkboxes)
        [ObservableProperty]
        private bool modoEliminar;

        public ListaViewModel(ProductoRepository repo)
        {
            _repo = repo;
            Productos = new ObservableCollection<Producto>();
        }

        // Vuelve a llenar la lista desde el repositorio (para ver altas y bajas)
        public void CargarProductos()
        {
            Productos.Clear();

            foreach (var producto in _repo.ObtenerTodos())
            {
                Productos.Add(producto);
            }

            seleccionados.Clear();
            ModoEliminar = false;
        }

        [RelayCommand]
        static async Task VerDetalle(Producto producto)
        {
            if (producto is null) return;

            await Shell.Current.GoToAsync($"/{nameof(DetallePage)}?id={producto.Id}");
        }

        [RelayCommand]
        static async Task IrAFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }

        [RelayCommand]
        static async Task IrAFormulario()
        {
            await Shell.Current.GoToAsync(nameof(FormularioPage));
        }

        // Activa el modo eliminar; si ya está activo, borra los seleccionados
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

        // Selecciona o deselecciona un producto
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

        // Elimina del catálogo todos los productos seleccionados
        private void EliminarSeleccionados()
        {
            if (seleccionados.Count == 0)
            {
                ModoEliminar = false;
                return;
            }

            _repo.EliminarProductos(seleccionados);

            foreach (var producto in seleccionados.ToList())
            {
                Productos.Remove(producto);
            }

            seleccionados.Clear();
            ModoEliminar = false;
        }
    }
}
