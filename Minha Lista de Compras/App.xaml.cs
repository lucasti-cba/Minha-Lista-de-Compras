
using Minha_Lista_de_Compras.Services;
using Minha_Lista_de_Compras.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Minha_Lista_de_Compras
{
    public partial class App : Application
    {

        public static string Token { get; set; }
        public static string Refresh { get; set; }

        public App()
        {
            InitializeComponent();
            
            
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            if (Token == null)
            {

                MainPage = new NavigationPage(new LoginPage());

            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
