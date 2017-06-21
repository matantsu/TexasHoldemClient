using NewDesignClient.Binding;
using NewDesignClient.Helpers;
using NewDesignClient.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewDesignClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private Frame _mainFrame;

        public Bind<SolidColorBrush> TextBox_Email_Forground { get; } = new Bind<SolidColorBrush>(Brushes.Gray);
        public Bind<SolidColorBrush> TextBox_Password_Forground { get; } = new Bind<SolidColorBrush>(Brushes.Gray);


        public Login(Frame mainFrame)
        {
            InitializeComponent();
            DataContext = this;
            _mainFrame = mainFrame;
            ProgressBar_LoginPressed.Visibility = Visibility.Hidden;
            LoginForm.IsEnabled = true;

        }

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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            bool isResterDataOk = true;
            isResterDataOk &= checkEmail(TextBox_Email.Text);
            
            if (isResterDataOk)
            {
                LoginForm.IsEnabled = false;
                ProgressBar_LoginPressed.Visibility = Visibility.Visible;
            }


            
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {
           _mainFrame.Navigate(new Register(_mainFrame));
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

        
    }

    
}
