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
using TexasHoldemClient.PL.UserControls;

namespace TexasHoldemClient.PL.Pages.AfterLoginPages
{
    public class booleaninverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }


    /// <summary>
    /// Interaction logic for GameRoomPage.xaml
    /// </summary>
    public partial class GameRoomPage : Page
    {
        public Bind<string> CurrentStatusMessage { get; } = new Bind<string>("You Are In Game");
        Game game = new Game();
        
        IGameManager gm = BL.GameManager;
        LinkedList<PlayerControl> playerControls = new LinkedList<PlayerControl>();
        LinkedList<Player> currentPlayers = new LinkedList<Player>();
        Me myPlayer = new Me();

        public GameRoomPage(int gameID)
        {
            this.game = gm.Listen(gameID);
            InitializeComponent();
            DataContext = game;
            chatControl.setGame(game);
            Button_StartRound.IsEnabled = !game.IsOnRound;
            UserActionsSpace.IsEnabled = true;


            ToolsBarSpace.DataContext = this;
            playerControls.AddFirst(Player0);
            playerControls.AddFirst(Player1);
            playerControls.AddFirst(Player2);
            playerControls.AddFirst(Player3);
            playerControls.AddFirst(Player4);
            
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
                // Console.WriteLine("" + e.PropertyName);
                if (e.PropertyName == "Chat")
                {
                    Console.Write("");

                }
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
                    setActions();

                    MyPlayer.DataContext = (Me)game.Players.First(x => x is Me);
                    //Player0.Player.ISCurrentPlayer
                    TwoPlayerCardsControl.Cards = IEnumerableToLinkedList(((Me)game.Players.First(x => x is Me)).Hand);
                }

                if (e.PropertyName == "OpenCards")
                {
                    OpenCardsControl.Cards = IEnumerableToLinkedList(game.OpenCards);
                }

                if (e.PropertyName == "CurrentPlayer")
                {
                    setActions();
                }
                
                if (e.PropertyName == "BigBlind")
                {
                    setBigBlind(game);
                }

                

            });

        }

        private void setSmallBlind(Game game)
        {
            string smallBlindPath = "/Resorces/smallblind400.png"; ;
            int i = 0;
            foreach (PlayerControl p in playerControls)
            {
                if (i == game.BigBlind)
                {
                    p.setPhotoPath(smallBlindPath);
                }
                i++;
            }
        }

        private void setBigBlind(Game game)
        {
            string bigBlindPath = "/Resorces/BigBlind.png";
            int i = 0;
            foreach (PlayerControl p in playerControls)
            {
                if (i == game.BigBlind)
                {
                    p.setPhotoPath(bigBlindPath);
                }
                i++;
            }
        }

        private void setActions()
        {
            if (game.CurrentPlayer == null) { return; }
            if (game.CurrentPlayer.UserID == ((Me)game.Players.First(x => x is Me)).UserID)
            {
                Button_Check.IsEnabled = false;
                Button_Fold.IsEnabled = false;
                Button_Raise.IsEnabled = false;
            }
            else
            {
                Button_Check.IsEnabled = true;
                Button_Fold.IsEnabled = true;
                Button_Raise.IsEnabled = true;
            }
        }


        private LinkedList<T> IEnumerableToLinkedList<T>(IEnumerable<T> lst)
        {
            LinkedList<T> newLst = new LinkedList<T>();

            foreach (T t in lst)
            {
                newLst.AddFirst(t);
            }

            return newLst;
        }


        private async void Check_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserActionsSpace.IsEnabled = false;
                CurrentStatusMessage.Data = "Starting Check...";
                await gm.Check(game);
                CurrentStatusMessage.Data = "Finish Check";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                CurrentStatusMessage.Data = "Error Check";
            }
            UserActionsSpace.IsEnabled = true;
        }

        private async void Fold_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserActionsSpace.IsEnabled = false;
                CurrentStatusMessage.Data = "Starting Fold...";
                await gm.Fold(game);
                CurrentStatusMessage.Data = "Finish Fold";    
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                CurrentStatusMessage.Data = "Error Fold";
            }
            UserActionsSpace.IsEnabled = true;
        }

        private async void Raise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserActionsSpace.IsEnabled = false;
                CurrentStatusMessage.Data = "Starting Raise...";
                await gm.Raise(game, Int32.Parse(BetTextBox.Text));
                CurrentStatusMessage.Data = "Finish Raise";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                CurrentStatusMessage.Data = "Error Raise";
            }
            UserActionsSpace.IsEnabled = true;
        }

        private async void StartRound_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserActionsSpace.IsEnabled = false;
                CurrentStatusMessage.Data = "Starting Round...";
                await gm.StartRound(game);
                CurrentStatusMessage.Data = "Finish Start Round";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                CurrentStatusMessage.Data = "Error Start Round";
            }
            UserActionsSpace.IsEnabled = true;
            Button_StartRound.IsEnabled = false;
        }


        public Game getGame()
        {
            return game;
        }
    }
}
