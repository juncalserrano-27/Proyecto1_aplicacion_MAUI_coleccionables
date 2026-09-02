using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class FormularioPage : ContentPage
    {
        public FormularioPage(FormularioViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
