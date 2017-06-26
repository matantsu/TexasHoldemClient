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
using TexasHoldemClient.PL.Helpers;
using TexasHoldemClient.PL.UserControls;

namespace TexasHoldemClient.PL.Windows
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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public Bind<string> CurrentStatusMessage { get; } = new Bind<string>("You Are In Game");
        Game game = new Game();
        IGameManager gm = BL.GameManager;
        LinkedList<PlayerControl> playerControls = new LinkedList<PlayerControl>();
        LinkedList<Player> currentPlayers = new LinkedList<Player>();

        public GameWindow(int gameID)
        {
            this.game = gm.Listen(gameID);
            InitializeComponent();
            DataContext = game;

            Button_StartRound.IsEnabled = !game.IsOnRound;
            UserActionsSpace.IsEnabled = true;


            ToolsBarSpace.DataContext = this;
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
               // Console.WriteLine("" + e.PropertyName);

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

                    #region add cards manualy
                    /*LinkedList<Card> Cardslst = new LinkedList<Card>();
                    Card c0 = new Card(CardType.Club, CardRank.Ace);
                    Card c1 = new Card(CardType.Spade, CardRank.Ace);
                    Cardslst.AddFirst(c0);
                    Cardslst.AddFirst(c1);
                    ((Me)game.Players.First(x => x is Me)).Hand = Cardslst;*/
                    #endregion
                    
                    TwoPlayerCardsControl.Cards = IEnumerableToLinkedList(((Me)game.Players.First(x => x is Me)).Hand);
                }

                if(e.PropertyName == "OpenCards")
                {
                    OpenCardsControl.Cards = IEnumerableToLinkedList(game.OpenCards);
                }

                if (e.PropertyName == "CurrentPlayer")
                {
                    setActions();
                }

            });
            
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
            UserActionsSpace.IsEnabled = false;
            CurrentStatusMessage.Data = "Starting Check...";
            await gm.Check(game);
            CurrentStatusMessage.Data = "Finish Check";
            UserActionsSpace.IsEnabled = true;
        }

        private async void Fold_Click(object sender, RoutedEventArgs e)
        {
            UserActionsSpace.IsEnabled = false;
            CurrentStatusMessage.Data = "Starting Fold...";
            await gm.Fold(game);
            CurrentStatusMessage.Data = "Finish Fold";
            UserActionsSpace.IsEnabled = true;
        }

        private async void Raise_Click(object sender, RoutedEventArgs e)
        {
            UserActionsSpace.IsEnabled = false;
            CurrentStatusMessage.Data = "Starting Raise...";
            await gm.Raise(game, Int32.Parse(BetTextBox.Text));
            CurrentStatusMessage.Data = "Finish Raise";
            UserActionsSpace.IsEnabled = true;
            
        }

        private async void StartRound_Click(object sender, RoutedEventArgs e)
        {
            UserActionsSpace.IsEnabled = false;
            CurrentStatusMessage.Data = "Starting Round...";
            await gm.StartRound(game);
            CurrentStatusMessage.Data = "Finish Start Round";
            UserActionsSpace.IsEnabled = true;
            Button_StartRound.IsEnabled = false;
        }


    }
}
