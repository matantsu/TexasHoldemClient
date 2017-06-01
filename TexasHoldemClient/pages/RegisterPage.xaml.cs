using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TexasHoldemClient.bl;
using TexasHoldemClient.Exceptions;

namespace TexasHoldemClient.pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        UserManager manager = UserManager.instance;
        NavigationManager navi = NavigationManager.instance;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            navi.Page = new LoginPage();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            textBoxUsername.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            errorEmailMessage.Text = isEmailOK(textBoxEmail.Text) ? "" : "Invalid Email.";
            errorPasswordMessage.Text = isPasswordOK(passwordBox1.Password, passwordBoxConfirm.Password) ? "" : "Invalid Password.";
            errorUsernameMessage.Text = isUsernameOK(textBoxUsername.Text) ? "" : "Invalid Username.";

            if (isEmailOK(textBoxEmail.Text) && isPasswordOK(passwordBox1.Password, passwordBoxConfirm.Password) && isUsernameOK(textBoxUsername.Text))
            { 
                Task<bool> task = manager.Register(textBoxUsername.Text, textBoxEmail.Text, passwordBox1.Password);

                // If it failed.
                task.ContinueWith(t =>
                {
                    Exception ex = t.Exception;
                    while (ex is AggregateException && ex.InnerException != null)
                        ex = ex.InnerException;
                    if (ex is Exception)
                    {
                        MessageBox.Show(ex.Message);
                    }
                },
                    TaskContinuationOptions.OnlyOnFaulted);

                // If it succeeded.
                task.ContinueWith(t =>
                {                   
                    MessageBox.Show("Registeration succses!");
                    Application.Current.Dispatcher.Invoke((Action)delegate {
                        navi.Page = new LoginPage();
                    });
                    
                },
                    TaskContinuationOptions.OnlyOnRanToCompletion);
            }
         
        }

        private bool isPasswordOK(string password, string passwordAgain)
        {
            return !(password.Length == 0 || passwordAgain.Length == 0 || password != passwordAgain);
        }

        private bool isEmailOK(string email)
        {
            return Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        }
         
        private bool isUsernameOK(string username)
        {
            return username.Length > 0;
        }
    }
}
