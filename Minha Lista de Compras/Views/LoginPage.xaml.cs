
using Minha_Lista_de_Compras.Models;
using Minha_Lista_de_Compras.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Minha_Lista_de_Compras.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        Label result = new Label();
        Entry Login = new Entry();
        Entry Password = new Entry();
        Conta Conta = new Conta();
        Cadastro cadastro = new Cadastro();

        public LoginPage()
        {
            InitializeComponent();
            Image image = new Image();
            image.Source = "drawable/bg.jpg";
            image.Aspect = Aspect.Fill;
            main.Children.Add(image);
            NavigationPage.SetHasNavigationBar(this, false);

            Grid grid = new Grid();
            grid.BackgroundColor = Color.FromRgba(200, 200, 200, 50);

            grid.Margin = new Thickness(40, 10, 40, 40);


            Image logo = new Image();
            logo.Source = "drawable/ForSale.png";
          
            logo.Aspect = Aspect.AspectFit;
            logo.Margin = new Thickness(90, 10, 90, 0);

            Label labelUsuario = new Label();
            labelUsuario.Text = "Usuario";
            labelUsuario.TextColor = Color.White;
            labelUsuario.Margin = new Thickness(10, 10, 10, 0);

            Label labelPassword = new Label();
            labelPassword.Text = "Senha";
            labelPassword.Margin = new Thickness(10, 10, 10, 0);



            Password.IsPassword = true;

            Password.Margin = new Thickness(10, 0, 10, 0);
            Password.TextColor = Color.White;
            labelPassword.TextColor = Color.White;
            Login.TextColor = Color.White;
            Login.Margin = new Thickness(10, 0);


            Button LoginButton = new Button();
            LoginButton.Text = "Entrar";
            LoginButton.Margin = new Thickness(40, 10);

            LoginButton.BackgroundColor = Color.FromHex("#F26671");
            LoginButton.Clicked += loginClick;
            LoginButton.CornerRadius = 20;



            Button CadastroButton = new Button();
            CadastroButton.Text = "Cadastrar";
            CadastroButton.Margin = new Thickness(40, 10);
            CadastroButton.BackgroundColor = Color.FromHex("#1261A6");
            CadastroButton.Clicked += CadastrarClick;
            CadastroButton.CornerRadius = 20;

            StackLayout loginForm = new StackLayout()
            {

                Children = {

                    labelUsuario,
                    Login,
                    labelPassword,
                    Password,
                    LoginButton,
                    CadastroButton,
                    result

                }


            };
            loginForm.Margin = new Thickness(5);

            Grid grid1 = new Grid();
            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            ColumnDefinition columnDefinition = new ColumnDefinition();
            ColumnDefinition columnDefinition2 = new ColumnDefinition();
            grid.Children.Add(
                loginForm
            );
            grid1.RowDefinitions.Add(row1);
            grid1.ColumnDefinitions.Add(columnDefinition);
            grid1.ColumnDefinitions.Add(columnDefinition2);

            Label dev = new Label();
            dev.Text = "Desenvolvido por ";
            dev.TextColor = Color.White;
            dev.FontSize = 16;
            dev.HorizontalTextAlignment = TextAlignment.End;
            dev.VerticalTextAlignment = TextAlignment.Center;
            Grid.SetColumn(dev, 0);

            Image image1 = new Image();

            image1.Source = "drawable/logo.png";
            image1.HorizontalOptions = LayoutOptions.Start;
            Grid.SetColumn(image1, 1);

            grid1.Children.Add(dev);
            grid1.Children.Add(image1);

            StackLayout pageF = new StackLayout()
            {

                Children = {

                    logo,
                    grid,
                    grid1

                }


            };

            main.Children.Add(pageF);

        }


        private void loginClick(object sender, EventArgs e)
        {
            Conta conta = new Conta();
            conta.Usuario = Login.Text;
            conta.Senha = Password.Text;
            //result.Text =

            login(conta);

        }

        private async void CadastrarClick(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(cadastro, true);
        }

        private async void login(Conta conta)
        {
            this.Conta = conta;

            if (await new ContaServices().login(Conta))
            {



                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await Navigation.PopAsync(true);
                    //await Navigation.PushAsync(new Home());

                });
            }

        }
    }
}