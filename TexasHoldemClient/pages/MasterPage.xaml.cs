using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TexasHoldemClient.pages
{
    /// <summary>
    /// Interaction logic for MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        UserManager manager = UserManager.instance;

        public MasterPage()
        {
            InitializeComponent();
            this.DataContext = manager;
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            manager.Logout();
        }
    }
}
