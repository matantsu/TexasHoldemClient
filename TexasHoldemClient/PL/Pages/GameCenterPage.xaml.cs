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
        IUserManager um = BL.UserManager;
        IGameManager gm = BL.GameManager;

        public GameCenterPage()
        {

            InitializeComponent();
            MainBar.DataContext = um.CurrentUser;
            TableOfGames.DataContext = gm;
            TableOfJoinedGames.DataContext = gm;
            //JoidGamesDataGrid.DataContext = gm;

            gm.PropertyChanged += GmChanedHandler;
        }

        private void GmChanedHandler(object sender, PropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.PropertyName == "ActiveGames"){
                    LoadingAnimation_CurrentJoinedGame.Visibility = Visibility.Hidden;
                }
                if (e.PropertyName == "Games")
                {
                    LoadingAnimation_Games.Visibility = Visibility.Hidden;
                }
            });
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
            try
            {
                Game g = (Game)GamesDataGrid.SelectedItem;
                await gm.Join(g);
                new GameWindow(g.ID).Show();
            }catch(Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        }


        private void returnToGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Game g = (Game)JoidGamesDataGrid.SelectedItem;
                new GameWindow(g.ID).Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }
        private void SearchByPot_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void SearchByGamePreferences_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void SearchByPlayerName_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }


    }
}
