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

namespace TexasHoldemClient
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        UserManager manager = UserManager.instance;

        public LoginPage()
        {
            InitializeComponent();
            DataContext = manager;
        }
        
        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            manager.Register();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            manager.Login("matan");
        }
    }
}
