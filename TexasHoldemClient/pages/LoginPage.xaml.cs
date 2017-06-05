using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using TexasHoldemClient.Models;
using TexasHoldemClient.pages;

namespace TexasHoldemClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        UserManager manager = UserManager.instance;
        NavigationManager navi = NavigationManager.instance;

        public LoginPage()
        {
            InitializeComponent();
        }
        
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            navi.Page = new RegisterPage();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            Task task = BusinessLayer.BL.UserManager.Login("test1","123456");
            //Task<User> task = manager.Login(textBoxUsername.Text, passwordBox1.Password);

            // If it failed.
            task.ContinueWith(t => 
                {
                    

                    Exception ex = t.Exception;
                    while (ex is AggregateException && ex.InnerException != null)
                        ex = ex.InnerException;
                    if (ex is LoginException)
                    {

                        this.Dispatcher.Invoke(() =>
                        {
                            errorToLogIn.Text = "Cannot Log In :\\";
                        });
                        
                    }
                },
                TaskContinuationOptions.OnlyOnFaulted);

            // If it succeeded.
            task.ContinueWith(t =>
                {
                    MessageBox.Show("Login Succses");
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        GameCenter gc = new GameCenter();
                        gc.Show();

                        Application.Current.Windows.OfType<Window>().ElementAt(0).Close();
                    });

                },
                TaskContinuationOptions.OnlyOnRanToCompletion);

        }
    }
}
