using Firebase.Database;
using Firebase.Database.Query;
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
using TexasHoldemClient.pages;

namespace TexasHoldemClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager manager = UserManager.instance;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = manager;
            Navigate(new LoginPage());

            manager.PropertyChanged += Manager_PropertyChanged;
        }

        private void Manager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "User")
            {
                if(manager.User != null)
                    Navigate(new MasterPage());
                else
                    Navigate(new LoginPage());
            }
        }

        public void Navigate(object content)
        {
            mainFrame.Navigate(content);
        }
    }
}
