using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class ApiPage : ContentPage
    {
        public ApiPage(ApiViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
