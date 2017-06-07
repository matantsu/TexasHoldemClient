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
            _mainFrame = mainFrame;
            InitializeComponent();

        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            await um.Login("barakmen@post.bgu.ac.il", "mz*+1TY7"/*Email_TextBox.Text , Password_TextBox.Password*/);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new RegisterPage(_mainFrame));
        }
    }
}
