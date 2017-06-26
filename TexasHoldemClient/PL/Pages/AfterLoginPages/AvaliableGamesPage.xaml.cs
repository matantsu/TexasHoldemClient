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

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{
    /// <summary>
    /// Interaction logic for AvaliableGamesPage.xaml
    /// </summary>
    public partial class AvaliableGamesPage : Page
    {
        IUserManager um = BL.UserManager;
        IGameManager gm = BL.GameManager;


        public AvaliableGamesPage()
        {
            InitializeComponent();
            TableOfGames.DataContext = gm;
            GamesDataGrid.Visibility = Visibility.Hidden;
            gm.PropertyChanged += GmChanedHandler;
        }

        private void GmChanedHandler(object sender, PropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.PropertyName == "Games")
                {
                    GamesDataGrid.Visibility = Visibility.Visible;
                    LoadingAnimation_Games.Visibility = Visibility.Hidden;
                }
            });
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
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

      
    }
}
