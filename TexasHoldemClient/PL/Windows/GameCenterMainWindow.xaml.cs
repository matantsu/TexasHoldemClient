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
using System.Windows.Shapes;
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.PL.Pages;

namespace TexasHoldemClient.PL.Windows
{
    /// <summary>
    /// Interaction logic for GameCenterMainWindow.xaml
    /// </summary>
    public partial class GameCenterMainWindow : Window
    {
        IUserManager um = BL.UserManager;

        public GameCenterMainWindow()
        {
            InitializeComponent();

            _mainFrame.Navigate(new LoginPage());

            um.PropertyChanged += OnUserChange_PropertyChanged;
        }

        private void OnUserChange_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "CurrentUser")
            {
                if (um.CurrentUser != null)
                {
                    _mainFrame.Navigate(new GameCenterPage());
                }
                else
                {
                    _mainFrame.Navigate(new LoginPage());
                }

            }
            

            

        }
    }
}
