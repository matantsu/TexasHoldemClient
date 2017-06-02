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
using TexasHoldemClient.bl;
using TexasHoldemClient.Models;

namespace TexasHoldemClient
{
    /// <summary>
    /// Interaction logic for GameCenter.xaml
    /// </summary>
    public partial class GameCenter : Window
    {
        public NavigationManager navi { get; set; } = NavigationManager.instance;
        public UserManager manager { get; set; } = UserManager.instance;
        public GameCenterManager gcm { get; set; } = GameCenterManager.instance;

        public GameCenter()
        {
            DataContext = this;
            InitializeComponent();
            navi.PropertyChanged += PageChange_PropertyChanged;
        }


        private void PageChange_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Page")
            {

            }
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(ActiveGamesDataGrid.SelectedIndex + "");
        }

        private void Spectate_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show(ActiveGamesDataGrid.SelectedIndex + "");
        }

        private void CreateNewGame_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGame cng = new CreateNewGame();
            cng.Show();
        }
    }
}
