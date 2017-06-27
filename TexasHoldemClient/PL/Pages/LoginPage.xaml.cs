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
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.Helpers;

namespace TexasHoldemClient.PL.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        IUserManager um = BL.UserManager;
        private Frame _mainFrame;

        public LoginPage(Frame mainFrame)
        {
            InitializeComponent();
            DataContext = this;
            ProgressBar_LoginPressed.Visibility = Visibility.Hidden;
            LoginForm.IsEnabled = true;
            _mainFrame = mainFrame;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {

            bool isResterDataOk = true;
            isResterDataOk &= checkEmail(TextBox_Email.Text);

            if (true/*isResterDataOk*/)
            {
                try
                {
                    LoginForm.IsEnabled = false;
                    ProgressBar_LoginPressed.Visibility = Visibility.Visible;
                    //await um.Login(TextBox_Email.Text, PasswordBox_Password.Password);
                    await um.Login("x@gmail.com", "zaq1xsw2");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    LoginForm.IsEnabled = true;
                    ProgressBar_LoginPressed.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new RegisterPage(_mainFrame));
        }


        private bool checkEmail(string text)
        {
            if (!Validators.isEmailValid(text))
            {
                TextBlock_EmailError.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                TextBlock_EmailError.Visibility = Visibility.Collapsed;
                return true;
            }
        }


        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmail(((TextBox)sender).Text);
        }

        private void TextBox_Email_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBlock_Email.Foreground = Brushes.Black;
        }
        private void TextBox_Email_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBlock_Email.Foreground = Brushes.Blue;
        }

        private void PasswordBox_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBlock_Password.Foreground = Brushes.Black;
        }

        private void PasswordBox_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBlock_Password.Foreground = Brushes.Blue;
        }
    }
}
