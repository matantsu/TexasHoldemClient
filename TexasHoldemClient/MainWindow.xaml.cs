
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
        NavigationManager navi = NavigationManager.instance;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = manager;

            manager.PropertyChanged += Manager_PropertyChanged;
            navi.PropertyChanged += PageChange_PropertyChanged;

            navi.Page = new LoginPage();
        }

        private void Manager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "User")
            {
                if(manager.User != null)
                    navi.Page = new RegisterPage();
                else
                   navi.Page = new LoginPage();
            }
        }


        private void PageChange_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Page")
            {
                mainFrame.Navigate(navi.Page);
            }
        }

        

    }
}
