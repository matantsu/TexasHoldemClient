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
using TexasHoldemClient.BusinessLayer.Models;
using TexasHoldemClient.PL.Windows;

namespace TexasHoldemClient.PL.Pages
{
    /// <summary>
    /// Interaction logic for GameCenterPage.xaml
    /// </summary>
    public partial class GameCenterPage : Page
    {
        UserManager um = BL.UserManager;
        GameManager gm = BL.GameManager;

        public GameCenterPage()
        {
            InitializeComponent();
            DataContext = gm;
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            RegisterButton.IsEnabled = false;
            await um.Logout(null, null);
        }

        private void CreateNewGame_Click(object sender, RoutedEventArgs e)
        {
            new CreateNewGameWindow().Show();
        }

        private async void Join_Click(object sender, RoutedEventArgs e)
        {
            Game g = gm.Games.ElementAt(GamesDataGrid.SelectedIndex);
            await gm.Join(g);
        }

        private void returnToGame_Click(object sender, RoutedEventArgs e)
        {

            Game g = gm.ActiveGames.ElementAt(JoidGamesDataGrid.SelectedIndex);
            //new GameWindow(g.ID).Show();
        }
        
    }
}
