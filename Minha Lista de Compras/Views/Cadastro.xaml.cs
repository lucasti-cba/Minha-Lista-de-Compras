using Minha_Lista_de_Compras.Services;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Minha_Lista_de_Compras.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        Entry cpf_input = new Entry();
        Entry Password = new Entry();
        Entry Password2 = new Entry();
        Entry Email = new Entry();
        Label msg_error = new Label();
        Button CadastroButton = new Button();
        public Cadastro()
        {
            InitializeComponent();


            cpf_input.TextChanged += Cpf_input_TextChanged;
            cpf_input.Margin = new Thickness(10, 0);
            Grid grid = new Grid();
            grid.BackgroundColor = Color.White;
            grid.Margin = new Thickness(40);

            Label cpf = new Label();
            cpf.Text = "CPF";
            cpf.Margin = new Thickness(10, 0);




            Label labelPassword = new Label();
            labelPassword.Text = "Senha";
            labelPassword.Margin = new Thickness(10, 0);


            Label labelPassword2 = new Label();
            labelPassword2.Text = "Repita a Senha";
            labelPassword2.Margin = new Thickness(10, 0);


            Label labelEmail = new Label();
            labelEmail.Text = "Email";
            labelEmail.Margin = new Thickness(10, 0);


            Password.IsPassword = true;
            Password.BackgroundColor = Color.White;
            Password.Margin = new Thickness(10, 0);

            Password2.IsPassword = true;
            Password2.BackgroundColor = Color.White;
            Password2.Margin = new Thickness(10, 0);

            Email.BackgroundColor = Color.White;
            Email.Margin = new Thickness(10, 0);


            CadastroButton.IsEnabled = false;
            CadastroButton.Text = "Cadastrar";
            CadastroButton.Margin = new Thickness(10);
            CadastroButton.BackgroundColor = Color.FromHex("#1261A6");
            CadastroButton.Clicked += CadastrarSubimit;

            StackLayout loginForm = new StackLayout()
            {

                Children = {

                    cpf,
                    cpf_input,
                    labelEmail,
                    Email,
                    labelPassword,
                    Password,
                    labelPassword2,
                    Password2,

                    CadastroButton,
                   msg_error

                }


            };
            loginForm.Margin = new Thickness(5);


            grid.Children.Add(
                loginForm
            );

            main.Children.Add(grid);
        }

        private async void Cpf_input_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cpf_input.Text.Length >= 11)
                try
                {
                    var person = await "https://barber-auth.herokuapp.com/api/validate_cpf/"
                       .PostJsonAsync(new
                       {
                           CPF = cpf_input.Text,

                       })
                        .ReceiveString();

                    CadastroButton.IsEnabled = true;
                    CadastroButton.BackgroundColor = Color.FromHex("#1261A6");
                    CadastroButton.Clicked += CadastrarSubimit;
                    cpf_input.BackgroundColor = Color.Green;
                }
                catch
                {

                    cpf_input.BackgroundColor = Color.Red;
                }

        }

        private async void CadastrarSubimit(object sender, EventArgs e)
        {

            try
            {
               var cadastroReques =  await "https://barber-auth.herokuapp.com/api/register/"
                   .PostJsonAsync(new
                   {
                       username = cpf_input.Text,
                       password = Password.Text,
                       password2 = Password2.Text,
                       email = Email.Text,
                       first_name = "",
                       last_name = ""
                   })
                   ;

                if (cadastroReques.StatusCode == 200 || cadastroReques.StatusCode == 201)
                    CadastroButton.BackgroundColor = Color.Green;
                else if (cadastroReques.StatusCode == 400 || cadastroReques.StatusCode == 401) {

                    CadastroButton.BackgroundColor = Color.Yellow;

                    msg_error.Text = "Confira todos os campos.";
                }
                    
                else {

                    msg_error.Text = await cadastroReques.GetStringAsync();

                }


            }
            catch(Exception ed)
            {
                
                //msg_error.Text = ed.Message;
                //CadastroButton.BackgroundColor = Color.Red;
            }


        }
    }
}