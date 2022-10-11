using Minha_Lista_de_Compras.ViewModels;
using Minha_Lista_de_Compras.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Minha_Lista_de_Compras
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
           
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
