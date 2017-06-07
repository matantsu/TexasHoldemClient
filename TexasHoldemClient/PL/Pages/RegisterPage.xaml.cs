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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        IUserManager um = BL.UserManager;

        private Frame _mainFrame;

        public RegisterPage(Frame mainFrame)
        {
            _mainFrame = mainFrame;
            InitializeComponent();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            Button_Submit.IsEnabled = false;
            LoadingAnimation_SubmitLoading.Visibility = Visibility.Visible;
            await um.Register(Email_TextBox.Text, Username_TextBox.Text, Password_TextBox.Password);
            LoadingAnimation_SubmitLoading.Visibility = Visibility.Hidden;
            _mainFrame.GoBack();
        }
    }
}
