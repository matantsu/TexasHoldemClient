using NewDesignClient.Binding;
using NewDesignClient.Helpers;
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

namespace NewDesignClient.Pages
{
   


    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private Frame _mainFrame;

        public Bind<SolidColorBrush> TextBox_Email_Forground { get; } = new Bind<SolidColorBrush>(Brushes.Gray);
        public Bind<SolidColorBrush> TextBox_Password_Forground { get; } = new Bind<SolidColorBrush>(Brushes.Gray);
        public Bind<SolidColorBrush> TextBox_ConfrimPassword_Forground { get; } = new Bind<SolidColorBrush>(Brushes.Gray);
        public Bind<SolidColorBrush> TextBox_Username_Forground { get; } = new Bind<SolidColorBrush>(Brushes.Gray);

      

        public Register(Frame mainFrame)
        {
            _mainFrame = mainFrame;

            InitializeComponent();
            DataContext = this;


            ProgressBar_RegisterPressed.Visibility = Visibility.Hidden;
            RegisterForm.IsEnabled = true;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool isResterDataOk = true;
            isResterDataOk &= checkUsername(TextBox_Username.Text);
            isResterDataOk &= checkEmail(TextBox_Email.Text);
            isResterDataOk &= checkPasswordEquals(PasswordBox_Password.Password, PasswordBox_ConfrimPassword.Password);

            if (isResterDataOk)
            {
                RegisterForm.IsEnabled = false;
                ProgressBar_RegisterPressed.Visibility = Visibility.Visible;
            }

        }

      
        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.GoBack();
        }


        #region text boxs get and loss focus
        private void TextBox_Email_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Email_Forground.Data = Brushes.Gray;
        }
        private void TextBox_Email_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Email_Forground.Data = Brushes.Blue;
        }

        private void PasswordBox_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Password_Forground.Data = Brushes.Gray;
        }
        private void PasswordBox_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Password_Forground.Data = Brushes.Blue;
        }

        private void TextBox_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Username_Forground.Data = Brushes.Blue;
        }

        private void TextBox_Username_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Username_Forground.Data = Brushes.Gray;
        }

        private void PasswordBox_ConfrimPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_ConfrimPassword_Forground.Data = Brushes.Gray;
        }

        private void PasswordBox_ConfrimPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_ConfrimPassword_Forground.Data = Brushes.Blue;
        }

        #endregion

        #region textbox data validation and errors
        private bool checkPasswordEquals(string password1, string password2)
        {
            if (password1 != password2 || password1 == null || password1 == "")
            {
                TextBlock_PasswordEqualError.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                TextBlock_PasswordEqualError.Visibility = Visibility.Collapsed;
                return true;
            }
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

        private bool checkUsername(string text)
        {
            if (!Validators.isUsaernameValid(text))
            {
                TextBlock_UsernameError.Visibility = Visibility.Visible;
                return false;   
            }
            else
            {
                TextBlock_UsernameError.Visibility = Visibility.Collapsed;
                return true;
            }
        }

        private void TextBox_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkUsername(TextBox_Username.Text);
        }

        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkEmail(TextBox_Email.Text);
        }

        private void PasswordBox_ConfrimPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            checkPasswordEquals(PasswordBox_Password.Password, PasswordBox_ConfrimPassword.Password);
        }

        #endregion

    }
}
