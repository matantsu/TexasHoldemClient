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

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        IUserManager um = BL.UserManager;

        public EditUserPage()
        {
            
            InitializeComponent();
            UpdateForm.DataContext = um.CurrentUser;

        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {

            bool isResterDataOk = true;
            isResterDataOk &= checkEmail(TextBox_Email.Text);
            isResterDataOk &= PasswordBox_Password.Password != null; 
            if (isResterDataOk)
            {
                try
                {
                    UpdateForm.IsEnabled = false;
                    ProgressBar_UpdatePressed.Visibility = Visibility.Visible;                    
                    await um.ChangeEmail(TextBox_Email.Text);
                    await um.ChangePassword(PasswordBox_Password.Password);
                    MessageBox.Show("Update Succsess!");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    UpdateForm.IsEnabled = true;
                    ProgressBar_UpdatePressed.Visibility = Visibility.Hidden;
                }
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
