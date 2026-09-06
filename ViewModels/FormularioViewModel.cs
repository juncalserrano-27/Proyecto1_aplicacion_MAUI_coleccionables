using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;

namespace RepasoMAUI.ViewModels
{
    public partial class FormularioViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;

        // Campos del formulario (se conectan con los Entry de la pantalla)
        [ObservableProperty]
        private string modelo;

        [ObservableProperty]
        private string color;

        [ObservableProperty]
        private string precio;   // lo manejamos como texto y lo convertimos al guardar

        [ObservableProperty]
        private string imagenUrl;

        // Mensaje para el usuario (solo si falta un dato)
        [ObservableProperty]
        private string mensaje;

        public FormularioViewModel(ProductoRepository repo)
        {
            _repo = repo;
        }

        [RelayCommand]
        private async Task Guardar()
        {
            // Validación mínima: que no falten los datos importantes
            if (string.IsNullOrWhiteSpace(Modelo) || string.IsNullOrWhiteSpace(Color))
            {
                Mensaje = "⚠️ Escribe al menos el modelo y el color.";
                return;
            }

            // Convertir el precio de texto a número
            decimal.TryParse(Precio, out decimal precioNumero);

            // Crear el producto nuevo
            var nuevo = new Producto
            {
                Modelo = Modelo,
                Color = Color,
                Precio = precioNumero,
                ImagenUrl = ImagenUrl
            };

            // Guardarlo en el repositorio
            _repo.Agregar(nuevo);

            // Limpiar el formulario
            Modelo = string.Empty;
            Color = string.Empty;
            Precio = string.Empty;
            ImagenUrl = string.Empty;
            Mensaje = string.Empty;

            // Regresar a la lista (ahí aparecerá el nuevo producto)
            await Shell.Current.GoToAsync("..");
        }
    }
}
