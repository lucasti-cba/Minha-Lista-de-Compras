using Minha_Lista_de_Compras.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Minha_Lista_de_Compras.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}