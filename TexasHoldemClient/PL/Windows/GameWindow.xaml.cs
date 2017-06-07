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
using System.Windows.Shapes;
using TexasHoldemClient.BusinessLayer;
using TexasHoldemClient.BusinessLayer.Models;
using TexasHoldemClient.PL.UserControls;

namespace TexasHoldemClient.PL.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game game = new Game();
        IGameManager gm = BL.GameManager;
        LinkedList<PlayerControl> playerControls = new LinkedList<PlayerControl>();
        LinkedList<Player> currentPlayers = new LinkedList<Player>();

        public GameWindow(int gameID)
        {
            this.game = gm.Listen(gameID);
            InitializeComponent();
            DataContext = game;


            playerControls.AddFirst(Player0);
            playerControls.AddFirst(Player1);
            playerControls.AddFirst(Player2);
            playerControls.AddFirst(Player3);
            playerControls.AddFirst(Player4);
            playerControls.AddFirst(Player5);

            foreach (PlayerControl pc in playerControls)
            {
                pc.Visibility = Visibility.Hidden;
            }

            game.PropertyChanged += GameChangeHandler;
        }

        private void GameChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                

                if (e.PropertyName == "Players")
                {

                    for (int i = 0; i < playerControls.Count; i++)
                    {
                        if (i < game.Players.Where(x => !(x is Me)).Count())
                        {
                            playerControls.ElementAt(i).Player = game.Players.Where(x => !(x is Me)).ElementAt(i);
                            playerControls.ElementAt(i).Visibility = Visibility.Visible;
                        }
                        else
                        {
                            playerControls.ElementAt(i).Visibility = Visibility.Collapsed;
                        }
                    }

                    MyPlayer.DataContext = (Me)game.Players.First(x => x is Me);
                }

            });
            
        }

        private async void Check_Click(object sender, RoutedEventArgs e)
        {
            await gm.Check(game);
        }

        private async void Fold_Click(object sender, RoutedEventArgs e)
        {
           await gm.Fold(game);
        }

        private async void Raise_Click(object sender, RoutedEventArgs e)
        {
           await gm.Raise(game, Int32.Parse(BetTextBox.Text));
        }
    }
}
