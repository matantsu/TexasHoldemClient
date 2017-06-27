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
using TexasHoldemClient.PL.Helpers;
using TexasHoldemClient.PL.Windows;

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{

    /// <summary>
    /// Interaction logic for ActiveGamesPage.xaml
    /// </summary>
    public partial class ActiveGamesPage : Page
    {
        IUserManager um = BL.UserManager;
        IGameManager gm = BL.GameManager;
        public CurrentRooms CurrentRoomsPage { get; set; } = new CurrentRooms();

        public ActiveGamesPage()
        {
            InitializeComponent();
            TableOfJoinedGames.DataContext = gm;
            ShowGamesFrame.Navigate(CurrentRoomsPage);
            JoidGamesDataGrid.DataContext = this;
            JoidGamesDataGrid.Visibility = Visibility.Hidden;
            gm.PropertyChanged += GmChanedHandler;
        }

        LinkedList<Game> JoinedGames = new LinkedList<Game>();
        public Bind<IEnumerable<Game>> ActiveGames { get; } = new Bind<IEnumerable<Game>>();


        private void GmChanedHandler(object sender, PropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (e.PropertyName == "ActiveGames")
                {
                    ActiveGames.Data = gm.ActiveGames.Where((g) => { return !JoinedGames.Contains(g); });
                    LoadingAnimation_Games.Visibility = Visibility.Hidden;
                    JoidGamesDataGrid.Visibility = Visibility.Visible;

                }
            });
        }

       
        private void returnToGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((Button)sender).IsEnabled = false;
                Game g = (Game)JoidGamesDataGrid.SelectedItem;
                MessageBox.Show("You are added to GameID: " + g.ID);

                CurrentRoomsPage.addGame(g);
                //new GameWindow(g.ID).Show();

                JoinedGames.AddFirst(g);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                ((Button)sender).IsEnabled = true;
            }

        }


    }
}
